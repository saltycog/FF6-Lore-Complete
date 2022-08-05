namespace FF6Hack
{
	using System;
	using System.Globalization;


	public partial class Dialogue
	{
		public struct FormatTag
		{
			public readonly int Index;
			public readonly string Tag;


			public FormatTag(int index, string tag)
			{
				this.Index = index;
				this.Tag = tag.ToUpper();
			}


			public byte[] ToBytes()
			{
				switch (this.Tag[0])
				{
					case 'A':
						return GetActorByte();

					case 'D':
						return GetDelayBytes();

					case 'E':
						// End of page tag.
						if (this.Tag.Equals("EOP"))
							return new byte[] { 0x13 };
						// Event tag.
						return new byte[] { 0x11, GetNumericParameter() };

					case 'O':
						return GetOpBytes();

					case 'C':
						return new byte[] { 0x15 };

					case 'N':
						return new byte[] { 0x19 };

					case 'I':
						return new byte[] { 0x1A };

					case 'S':
						return new byte[] { 0x1B };

					case 'B':
						return GetKanjiCharBytes();

					default:
						throw new InvalidOperationException(
							$"Malformed tag at index {this.Index}: {this.Tag}");
				}
			}


			private byte[] GetKanjiCharBytes()
			{
				switch (this.Tag.Substring(0, 3))
				{
					case "BK1$":
						return new byte[] { 0x1C, GetHexParameter(5) };
					case "BK2$":
						return new byte[] { 0x1D, GetHexParameter(5) };
					case "BK3$":
						return new byte[] { 0x1E, GetHexParameter(5) };
					case "BK4$":
						return new byte[] { 0x1F, GetHexParameter(5) };
					default:
						throw new InvalidOperationException(
							$"Malformed tag at index {this.Index}: {this.Tag}");
				}
			}


			private byte[] GetOpBytes()
			{
				switch (this.Tag)
				{
					case "OP$12":
						return new byte[] { 0x12 };
					case "OP$17":
						return new byte[] { 0x17 };
					case "OP$18":
						return new byte[] { 0x18 };
					default:
						throw new InvalidOperationException(
							$"Malformed tag at index {this.Index}: {this.Tag}");
				}
			}


			private byte GetNumericParameter()
			{
				if (this.Tag[1] == '$')
				{
					return GetHexParameter();
				}

				else if (char.IsDigit(this.Tag[1]))
				{
					return GetDecimalParameter();
				}

				throw new InvalidOperationException(
					$"Malformed tag at index {this.Index}: {this.Tag}");
			}


			private byte[] GetDelayBytes()
			{
				// Simple 1 second delay.
				if (this.Tag.Length == 1)
					return new byte[] { 0x10 };

				// Delay = 1/4 sec * parameter.
				return new byte[] { 0x16, GetNumericParameter() };
			}


			private byte GetDecimalParameter(int substringIndex = 1)
			{
				int decimalValue = Int16.Parse(this.Tag.Substring(substringIndex));

				return (byte)decimalValue;
			}


			private byte GetHexParameter(int substringIndex = 2)
			{
				byte hexValue = byte.Parse(
					this.Tag.Substring(substringIndex),
					NumberStyles.HexNumber);

				return hexValue;
			}


			private byte[] GetActorByte()
			{
				byte actorByte = 0x02;
				byte actorNumber = GetDecimalParameter();
				actorByte += actorNumber;

				return new[] { actorByte };
			}
		}
	}
}