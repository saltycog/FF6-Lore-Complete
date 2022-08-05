namespace FF6Hack
{
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using SnesEditing;


	public partial class Dialogue
	{
		#region Properties
		public string FormattedText { get; set; }
		public int Index { get; set; }
		public uint Offset { get; set; }
		#endregion


		public static byte[] CompressFormattedText(string str)
		{
			if (string.IsNullOrEmpty(str))
				return new byte[] { };

			int len = str.Length;
			byte[] pText = new byte[len + 1024];
			Dictionary<int, FormatTag> formatTags = GetTagDictionary(str);

			string temp = "";
			bool bMalformed = false;

			int index = 0;
			for (int i = 0; i < len; i++)
			{
				if (str[i] >= 'A'
					&& str[i] <= 'Z')
					pText[index] = (byte)(str[i] - 0x21);
				else if (str[i] >= 'a'
						&& str[i] <= 'z')
					pText[index] = (byte)(str[i] - 0x27);
				else if (str[i] >= '0'
						&& str[i] <= '9')
					pText[index] = (byte)(str[i] + 0x24);

				else if (str[i] == '!')
					pText[index] = 0x5E;
				else if (str[i] == '?')
					pText[index] = 0x5F;
				else if (str[i] == '/')
					pText[index] = 0x60;
				else if (str[i] == ':')
					pText[index] = 0x61;
				else if (str[i] == '$')
					pText[index] = 0x62; // $ is the right quote in FF3.FNT
				else if (str[i] == '\'')
					pText[index] = 0x63;
				else if (str[i] == '-')
					pText[index] = 0x64;
				else if (str[i] == '.')
					pText[index] = 0x65;
				else if (str[i] == ',')
					pText[index] = 0x66;
				else if (str[i] == '_')
					pText[index] = 0x67; // _ is the ellipsis
				else if (str[i] == ';')
					pText[index] = 0x68;
				else if (str[i] == '#')
					pText[index] = 0x69;
				else if (str[i] == '+')
					pText[index] = 0x6A;
				else if (str[i] == '(')
					pText[index] = 0x6B;
				else if (str[i] == ')')
					pText[index] = 0x6C;
				else if (str[i] == '%')
					pText[index] = 0x6D;
				else if (str[i] == '~')
					pText[index] = 0x6E;
				else if (str[i] == '*')
					pText[index] = 0x6F;
				else if (str[i] == '@')
					pText[index] = 0x70;
				else if (str[i] == '&')
					pText[index] = 0x71; // & is the musical note
				else if (str[i] == '=')
					pText[index] = 0x72;
				else if (str[i] == '"')
					pText[index] = 0x73;
				else if (str[i] == ' ')
					pText[index] = 0x7F;

				else if (str[i] == '\r'
						&& str[i + 1] == '\n')
				{
					pText[index] = 0x01;
					i++;
				}

				// Format tag handling
				else if (str[i] == '<')
				{
					if (formatTags.ContainsKey(i))
					{
						FormatTag tag = formatTags[i];
						byte[] tagBytes = tag.ToBytes();
						foreach (byte tagByte in tagBytes)
						{
							pText[index] = tagByte;
							index++;
						}

						i += (tag.Tag.Length + 1);
						continue;
					}

					else
					{
						bMalformed = true;
						break;
					}
				}

				else
				{
					index++;
					break;
				}

				index++;
			}

			// Something wasn't formatted right.
			if (bMalformed)
				throw new InvalidOperationException($"Unexpected token: {str[index + 1]}");

			// Finished processing, cap with 0x00, then compress.
			pText[index] = 0x00;
			byte[] compressed = Compress(index + 1, pText);

			return compressed;
		}


		/// <summary>
		/// Retrieve a dialogue at given index in a ROM.
		/// </summary>
		/// <param name="rom">ROM bytes to retrieve from.</param>
		/// <param name="index">Index of the dialogue, starting at 0.</param>
		/// <returns></returns>
		public static Dialogue GetDialogue(byte[] rom, int index)
		{
			Dialogue dialogue = new Dialogue();
			dialogue.Index = index;

			// Actual dialogue indexes in hex start at 1.
			index++;

			// Get bank where dialogue will be written.
			// Dialogues with indexes over 1680 go in a different bank.
			int startingIndexInBank0E = HexPatcher.GetShort(rom, 0x0CE600);
			int dataBank;
			if (index < startingIndexInBank0E)
				dataBank = 0x0D0000;
			else
				dataBank = 0x0E0000;

			// Offset of pointer that describes where dialogue data is located.
			int dataPointerOffset = index * 2 + 0x0CE602;

			// Value of pointer (not including bank).
			int dataPointerValue = HexPatcher.GetShort(rom, dataPointerOffset);

			// Full bank address where dialogue is written.
			int dataOffset = dataPointerValue + dataBank;

			dialogue.Offset = (uint)dataOffset;

			string formattedText = "";
			bool endOfDialogue = false;
			while (rom[dataOffset] != 0)
			{
				switch (rom[dataOffset])
				{
					case 0x00:
						endOfDialogue = true;
						break;

					case 0x11:
					case 0x14:
					case 0x16:
					case 0x1C:
					case 0x1D:
					case 0x1E:
					case 0x1F:
						formattedText += ProcessTag(rom, dataOffset, out dataOffset);
						break;

					default:
						formattedText += Tables.characterTable[rom[dataOffset]];
						dataOffset++;
						break;
				}
			}

			dialogue.FormattedText = formattedText;
			return dialogue;
		}


		public static IList<Dialogue> LoadScript(string dialogueFile)
		{
			Regex captionLine = new Regex(@"\[caption #\d+\]" + "\r\n");
			string[] blocks = captionLine.Split(dialogueFile);

			List<Dialogue> dialogueList = new List<Dialogue>();
			for (int i = 0; i < blocks.Length; i++)
			{
				// Skip header.
				if (i == 0)
					continue;

				// Remove blank line between entries.
				blocks[i] = blocks[i].Substring(0, blocks[i].LastIndexOf("\r\n"));

				Dialogue dialogue = new Dialogue
										{
											FormattedText = blocks[i],
											Index = i - 1
										};

				dialogueList.Add(dialogue);
			}

			return dialogueList;
		}


		/// <summary>
		/// Write a Dialogue to ROM byte array.
		/// </summary>
		/// <param name="dialogue">Dialogue object to be written.</param>
		/// <param name="rom">ROM to write dialogue to.</param>
		public static void SetText(Dialogue dialogue, byte[] rom)
		{
			byte[] compressedText = dialogue.GetCompressed();
			if (compressedText.Length == 0)
				Console.WriteLine($"{dialogue.Index} was empty.");

			SetText(
				compressedText,
				dialogue.Index,
				rom);
		}


		/// <summary>
		/// Write dialogue bytes to ROM byte array at given dialogue index.
		/// Note: Dialogue should be prepared using GetCompressed(), first.
		/// </summary>
		/// <param name="convertedText">Converted text bytes ready for output.</param>
		/// <param name="index">Dialogue index where text should be written.</param>
		/// <param name="rom">ROM to write dialogue to.</param>
		public static void SetText(byte[] convertedText, int index, byte[] rom)
		{
			index++;

			// Get bank where dialogue will be written.
			// Dialogues with indexes over 1680 go in a different bank.
			int startingIndexInBank0E = HexPatcher.GetShort(rom, 0x0CE600);
			int dataBank;
			if (index < startingIndexInBank0E)
				dataBank = 0x0D0000;
			else
				dataBank = 0x0E0000;

			// Offset of pointer that describes where dialogue data is located.
			int dataPointerOffset = index * 2 + 0x0CE602;

			// Value of pointer (not including bank).
			int dataPointerValue = HexPatcher.GetShort(rom, dataPointerOffset);

			// Full bank address where dialogue is written.
			int dataOffset = dataPointerValue + dataBank;

			// Write dialog data.
			for (int i = 0; i < convertedText.Length; i++)
			{
				rom[dataOffset + i] = convertedText[i];
			}

			// The next dialogue's data pointer needs to be updated,
			// to account for written dialogue's length.
			ushort nextPointerValue = (ushort)(dataPointerValue + convertedText.Length);
			HexPatcher.SetShort(rom, nextPointerValue, GetDialogueBankOffset(index, rom));
		}


		/// <summary>
		/// Get dialogue text bytes, converted and compressed, ready for writing to ROM.
		/// </summary>
		/// <returns>Converted and compressed dialogue bytes.</returns>
		public byte[] GetCompressed()
		{
			return CompressFormattedText(this.FormattedText);
		}


		public void Write(byte[] rom)
		{
			SetText(this, rom);
		}


		#region Helper Methods
		/// <summary>
		/// Compresses output text bytes using DTE tables.
		/// </summary>
		/// <param name="textLength">Length of encoded text.</param>
		/// <param name="convertedBytes"></param>
		/// <returns>DTE compressed bytes.</returns>
		private static byte[] Compress(int textLength, byte[] convertedBytes)
		{
			byte[] dteEncodedBytes = new byte[textLength];

			int numberOfEncodedBytes = 0;
			for (int i = 0; i < textLength; i++)
			{
				bool foundByteMatch = false;

				if (i < textLength - 1)
				{
					byte b1 = convertedBytes[i];
					byte b2 = convertedBytes[i + 1];

					for (byte j = 0; j < 128; j++)
					{
						if (b1 == Tables.DteCompression[j * 2]
							&& b2 == Tables.DteCompression[j * 2 + 1])
						{
							dteEncodedBytes[numberOfEncodedBytes] = (byte)(0x80 + j);
							foundByteMatch = true;
							numberOfEncodedBytes++;
							i++;
							break;
						}
					}

					if (foundByteMatch == false)
					{
						dteEncodedBytes[numberOfEncodedBytes] = b1;
						numberOfEncodedBytes++;
					}
				}
				else
				{
					dteEncodedBytes[numberOfEncodedBytes] = convertedBytes[i];
					numberOfEncodedBytes++;
				}
			}

			int compressedLength = numberOfEncodedBytes;
			byte[] pCompressed = new byte[compressedLength]; // new byte[textLength]
			Buffer.BlockCopy(dteEncodedBytes, 0, pCompressed, 0, compressedLength);
			return pCompressed;
		}


		private static int GetDialogueBankOffset(int dialogueIndex, byte[] rom)
		{
			dialogueIndex++;

			// Get bank where dialogue will be written.
			// Dialogues with indexes over 1680 go in a different bank.
			int startingIndexInBank0E = HexPatcher.GetShort(rom, 0x0CE600);
			int dataBank;
			if (dialogueIndex < startingIndexInBank0E)
				dataBank = 0x0D0000;
			else
				dataBank = 0x0E0000;

			// Offset of pointer that describes where dialogue data is located.
			int dataPointerOffset = dialogueIndex * 2 + 0x0CE602;

			// Value of pointer (not including bank).
			int dataPointerValue = HexPatcher.GetShort(rom, dataPointerOffset);

			// Full bank address where dialogue is written.
			int dataOffset = dataPointerValue + dataBank;

			return dataOffset;
		}


		private static Dictionary<int, FormatTag> GetTagDictionary(string formattedText)
		{
			Dictionary<int, FormatTag> tagDictionary = new Dictionary<int, FormatTag>();
			IList<FormatTag> tags = GetTags(formattedText);
			foreach (FormatTag tag in tags)
			{
				tagDictionary.Add(tag.Index, tag);
			}

			return tagDictionary;
		}


		private static IList<FormatTag> GetTags(string formattedText)
		{
			List<FormatTag> formatTags = new List<FormatTag>();
			for (int i = 0; i < formattedText.Length; i++)
			{
				if (formattedText[i] == '<')
				{
					int index = i;
					string tag = "";
					i++;
					while (formattedText[i] != '>')
					{
						tag += formattedText[i];
						i++;
					}

					formatTags.Add(new FormatTag(index, tag));
				}
			}

			return formatTags.ToArray();
		}


		public static IList<Dialogue> GetAll(byte[] rom)
		{

			List<Dialogue> dialogues = new List<Dialogue>();
			int currentDialogueBankOffset = -1;
			int nextDialogueBankOffset = 0;
			int dialogueIndex = 0;
			while (nextDialogueBankOffset != currentDialogueBankOffset)
			{
				currentDialogueBankOffset = GetDialogueBankOffset(dialogueIndex, rom);
				dialogues.Add(Dialogue.GetDialogue(rom, dialogueIndex));
				
				dialogueIndex++;
				nextDialogueBankOffset = GetDialogueBankOffset(dialogueIndex, rom);
			}

			return dialogues;
		}


		/// <summary>
		/// Process remaining bytes of a tag from ROM at given offset.
		/// Outputs the new offset after the relevant tag bytes.
		/// </summary>
		/// <param name="rom">ROM to get tag from.</param>
		/// <param name="offset">Offset tag bytes are located at.</param>
		/// <param name="newOffset">Offset after the relevant tag bytes.</param>
		/// <returns>The rest of the tag.</returns>
		private static string ProcessTag(byte[] rom, int offset, out int newOffset)
		{
			newOffset = offset;
			string tag = Tables.characterTable[rom[offset]];
			newOffset++; // Type of tag will have already been read.
			tag += rom[newOffset].ToString("X2");
			newOffset++;
			tag += ">";

			return tag;
		}
		#endregion
	}


	public static class DialogueExtensions
	{
		/// <summary>
		/// Write a dialogue list to ROM byte array.
		/// </summary>
		/// <param name="dialogues">Dialogue list to be written.</param>
		/// <param name="romBytes">ROM to write dialogues to.</param>
		public static void Write(this IList<Dialogue> dialogues, byte[] romBytes)
		{
			foreach (Dialogue dialogue in dialogues)
			{
				Dialogue.SetText(dialogue, romBytes);
			}
		}
	}
}