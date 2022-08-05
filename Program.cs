namespace FF6Hack
{
	using System;
	using System.IO;
	using FF6Hack.Properties;


	public static class Program
	{
		public static void Main(string[] args)
		{
			// Load original ROM into memory.
			byte[] patchedRom = new byte[Resources.ff6.Length];
			Resources.ff6.CopyTo(patchedRom, 0);
			Console.WriteLine($"Loaded ROM: {patchedRom.Length} bytes.\n");
			
			// Queue up edits/hacks.
			StaminaDefenseHack.ApplyToRom(patchedRom);
			TieredEspers.ApplyToRom(patchedRom);
			SpeedEvadeHack.ApplyToRom(patchedRom);
			//BooksHack.ApplyToRom(patchedRom);

			//TestHere(patchedRom);

			// Apply edits to ROM and output to new file.
			Console.WriteLine("Writing finished ROM...");
			File.WriteAllBytes("patchedRom.smc", patchedRom);
			Console.WriteLine("Done.");
			Console.ReadLine();
		}


		private static void TestHere(byte[] patchedRom)
		{
			//Console.WriteLine(Dialogue.GetDialogue(patchedRom, 5).Offset.ToString("X"));
			var dialogues = Dialogue.GetAll(patchedRom);
			
			for (int i = 0; i < dialogues.Count; i++)
			{
				var dialogue = dialogues[i];
				//if (dialogue.Index == 2)
				//	dialogue.FormattedText = "HERE BE TEST!!! <D24><OP$12>";

				Dialogue.SetText(dialogue, patchedRom);
			}

			Console.WriteLine(Dialogue.GetDialogue(patchedRom, 1).FormattedText + "\n");
			Console.WriteLine(Dialogue.GetDialogue(patchedRom, 2).FormattedText + "\n");
			Console.WriteLine(Dialogue.GetDialogue(patchedRom, 3).FormattedText + "\n");

			/*
			int testIndex = 7;
			var test = Dialogue.GetDialogue(patchedRom, testIndex);
			Console.WriteLine($"{test.Offset:X8}:\n{test.FormattedText}");
			Console.WriteLine();

			//var convertedText = Dialogue.CompressFormattedText("<A1>: I'm with the Returners. Name's <A1>.<EOP><A6>: Returners!!! I used to be General <A6>_Now I'm just a common traitor_");
			//Dialogue.SetText(patchedRom, testIndex, convertedText);
			//test = Dialogue.GetDialogue(patchedRom, testIndex);
			//Console.WriteLine(test);
			Console.WriteLine();
			*/
		}
	}
}