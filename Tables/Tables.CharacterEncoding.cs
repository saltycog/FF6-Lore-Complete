namespace FF6Hack
{
	using System.Collections.Generic;


	public partial class Tables
    {
		public static readonly Dictionary<byte, string> characterTable = 
			new Dictionary<byte, string>()
				{
					{ 0x00, "<EOC>"},		// End of dialogue
					{ 0x01, "\r\n" },		// New line
					{ 0x02, "<A0>"},		// Terra
					{ 0x03, "<A1>" },		// Locke
					{ 0x04, "<A2>" },		// Cyan
					{ 0x05, "<A3>" },		// Shadow
					{ 0x06, "<A4>" },		// Edgar
					{ 0x07, "<A5>" },		// Sabin
					{ 0x08, "<A6>" },		// Celes
					{ 0x09, "<A7>" },		// Strago
					{ 0x0A, "<A8>" },		// Relm
					{ 0x0B, "<A9>" },		// Setzer
					{ 0x0C, "<A10>" },		// Mog
					{ 0x0D, "<A11>" },		// Gau
					{ 0x0E, "<A12>" },		// Gogo
					{ 0x0F, "<A13>" },		// Umaro
					{ 0x10, "<D>" },		// Delay of 1 second
					{ 0x11, "<E$" },		// An event in hex
					{ 0x12, "<OP$12>" },	// Opcode specifying some events
					{ 0x13, "<EOP>" },		// End of dialogue page
					{ 0x14, "<S$" },		// Number of repeated spaces in hex
					{ 0x15, "<C>" },		// A choice
					{ 0x16, "<D$" },		// Delay in hex, 1/4 sec increments
					{ 0x17, "<OP$17>" },	// Unknown opcode
					{ 0x18, "<OP$18>" },	// Unknown opcode
					{ 0x19, "<N>" },		// Supplied number from calling code to display
					{ 0x1A, "<I>" },		// Supplied item from calling code to display
					{ 0x1B, "<S>" },		// Supplied spell from calling code to display
					{ 0x1C, "<BK1$" },		// Kanji character in bank (FF6J only)
					{ 0x1D, "<BK2$" },		// Kanji character in bank (FF6J only)
					{ 0x1E, "<BK3$" },		// Kanji character in bank (FF6J only)
					{ 0x1F, "<BK4$" },		// Kanji character in bank (FF6J only)
					{ 0x20, "A" },
					{ 0x21, "B" },
					{ 0x22, "C" },
					{ 0x23, "D" },
					{ 0x24, "E" },
					{ 0x25, "F" },
					{ 0x26, "G" },
					{ 0x27, "H" },
					{ 0x28, "I" },
					{ 0x29, "J" },
					{ 0x2A, "K" },
					{ 0x2B, "L" },
					{ 0x2C, "M" },
					{ 0x2D, "N" },
					{ 0x2E, "O" },
					{ 0x2F, "P" },
					{ 0x30, "Q" },
					{ 0x31, "R" },
					{ 0x32, "S" },
					{ 0x33, "T" },
					{ 0x34, "U" },
					{ 0x35, "V" },
					{ 0x36, "W" },
					{ 0x37, "X" },
					{ 0x38, "Y" },
					{ 0x39, "Z" },
					{ 0x3A, "a" },
					{ 0x3B, "b" },
					{ 0x3C, "c" },
					{ 0x3D, "d" },
					{ 0x3E, "e" },
					{ 0x3F, "f" },
					{ 0x40, "g" },
					{ 0x41, "h" },
					{ 0x42, "i" },
					{ 0x43, "j" },
					{ 0x44, "k" },
					{ 0x45, "l" },
					{ 0x46, "m" },
					{ 0x47, "n" },
					{ 0x48, "o" },
					{ 0x49, "p" },
					{ 0x4A, "q" },
					{ 0x4B, "r" },
					{ 0x4C, "s" },
					{ 0x4D, "t" },
					{ 0x4E, "u" },
					{ 0x4F, "v" },
					{ 0x50, "w" },
					{ 0x51, "x" },
					{ 0x52, "y" },
					{ 0x53, "z" },
					{ 0x54, "0" },
					{ 0x55, "1" },
					{ 0x56, "2" },
					{ 0x57, "3" },
					{ 0x58, "4" },
					{ 0x59, "5" },
					{ 0x5A, "6" },
					{ 0x5B, "7" },
					{ 0x5C, "8" },
					{ 0x5D, "9" },
					{ 0x5E, "!" },
					{ 0x5F, "?" },
					{ 0x60, "/" },
					{ 0x61, ":" },
					{ 0x62, "]" },
					{ 0x63, "'" },
					{ 0x64, "-" },
					{ 0x65, "." },
					{ 0x66, "," },
					{ 0x67, "_" },			// ... (Ellipsis)
					{ 0x68, ";" },
					{ 0x69, "#" },
					{ 0x6A, "+" },
					{ 0x6B, "(" },
					{ 0x6C, ")" },
					{ 0x6D, "%" },
					{ 0x6E, "~" },
					{ 0x6F, "*" },
					{ 0x70, "@" },
					{ 0x71, "&" },			// Music note symbol
					{ 0x72, "=" },
					{ 0x73, "[" },			// Beginning/reversed quotes
					{ 0x74, "<$74>" },		// Box symbol
					{ 0x75, "<$75>" },		// Another box symbol
					{ 0x76, "$" },			// Holy/Pearl symbol
					{ 0x77, "\\" },			// Death/X symbol
					{ 0x78, "^" },			// Thunder symbol
					{ 0x79, "±" },			// Wind symbol
					{ 0x7A, "£" },			// Earth symbol
					{ 0x7B, "{" },			// Ice symbol
					{ 0x7C, "}" },			// Fire symbol
					{ 0x7D, "`" },			// Water symbol
					{ 0x7E, "|" },			// Poison symbol
					{ 0x7F, " " },			// Space
					{ 0x80, "e " },			// From here, used in compression
					{ 0x81, " t" },
					{ 0x82, ": " },
					{ 0x83, "th" },
					{ 0x84, "t " },
					{ 0x85, "he" },
					{ 0x86, "s " },
					{ 0x87, "er" },
					{ 0x88, " a" },
					{ 0x89, "re" },
					{ 0x8A, "in" },
					{ 0x8B, "ou" },
					{ 0x8C, "d " },
					{ 0x8D, " w" },
					{ 0x8E, " s" },
					{ 0x8F, "an" },
					{ 0x90, "o " },
					{ 0x91, " h" },
					{ 0x92, " o" },
					{ 0x93, "r " },
					{ 0x94, "n " },
					{ 0x95, "at" },
					{ 0x96, "to" },
					{ 0x97, " i" },
					{ 0x98, ", " },
					{ 0x99, "ve" },
					{ 0x9A, "ng" },
					{ 0x9B, "ha" },
					{ 0x9C, " m" },
					{ 0x9D, "Th" },
					{ 0x9E, "st" },
					{ 0x9F, "on" },
					{ 0xA0, "yo" },
					{ 0xA1, " b" },
					{ 0xA2, "me" },
					{ 0xA3, "y " },
					{ 0xA4, "en" },
					{ 0xA5, "it" },
					{ 0xA6, "ar" },
					{ 0xA7, "ll" },
					{ 0xA8, "ea" },
					{ 0xA9, "I " },
					{ 0xAA, "ed" },
					{ 0xAB, " f" },
					{ 0xAC, " y" },
					{ 0xAD, "hi" },
					{ 0xAE, "is" },
					{ 0xAF, "es" },
					{ 0xB0, "or" },
					{ 0xB1, "l " },
					{ 0xB2, " c" },
					{ 0xB3, "ne" },
					{ 0xB4, "'s" },
					{ 0xB5, "nd" },
					{ 0xB6, "le" },
					{ 0xB7, "se" },
					{ 0xB8, " I" },
					{ 0xB9, "a " },
					{ 0xBA, "te" },
					{ 0xBB, " l" },
					{ 0xBC, "pe" },
					{ 0xBD, "as" },
					{ 0xBE, "ur" },
					{ 0xBF, "u " },
					{ 0xC0, "al" },
					{ 0xC1, " p" },
					{ 0xC2, "g " },
					{ 0xC3, "om" },
					{ 0xC4, " d" },
					{ 0xC5, "f " },
					{ 0xC6, " g" },
					{ 0xC7, "ow" },
					{ 0xC8, "rs" },
					{ 0xC9, "be" },
					{ 0xCA, "ro" },
					{ 0xCB, "us" },
					{ 0xCC, "ri" },
					{ 0xCD, "wa" },
					{ 0xCE, "we" },
					{ 0xCF, "Wh" },
					{ 0xD0, "et" },
					{ 0xD1, " r" },
					{ 0xD2, "nt" },
					{ 0xD3, "m " },
					{ 0xD4, "ma" },
					{ 0xD5, "I'" },
					{ 0xD6, "li" },
					{ 0xD7, "ho" },
					{ 0xD8, "of" },
					{ 0xD9, "Yo" },
					{ 0xDA, "h " },
					{ 0xDB, " n" },
					{ 0xDC, "ee" },
					{ 0xDD, "de" },
					{ 0xDE, "so" },
					{ 0xDF, "gh" },
					{ 0xE0, "ca" },
					{ 0xE1, "ra" },
					{ 0xE2, "n'" },
					{ 0xE3, "ta" },
					{ 0xE4, "ut" },
					{ 0xE5, "el" },
					{ 0xE6, "! " },
					{ 0xE7, "fo" },
					{ 0xE8, "ti" },
					{ 0xE9, "We" },
					{ 0xEA, "lo" },
					{ 0xEB, "e!" },
					{ 0xEC, "ld" },
					{ 0xED, "no" },
					{ 0xEE, "ac" },
					{ 0xEF, "ce" },
					{ 0xF0, "k " },
					{ 0xF1, " u" },
					{ 0xF2, "oo" },
					{ 0xF3, "ke" },
					{ 0xF4, "ay" },
					{ 0xF5, "w " },
					{ 0xF6, "!!" },
					{ 0xF7, "ag" },
					{ 0xF8, "il" },
					{ 0xF9, "ly" },
					{ 0xFA, "co" },
					{ 0xFB, ". " },
					{ 0xFC, "ch" },
					{ 0xFD, "go" },
					{ 0xFE, "ge" },
					{ 0xFF, "e_" }
				};

		/*
		public static readonly string[] CharacterEncoding =
			new string[]
				{
					"", "\r\n", "<A0>", "<A1>", "<A2>", "<A3>", "<A4>", "<A5>",
					"<A06>", "<A07>", "<A08>", "<A09>", "<A10>", "<A11>", "<A12>", "<A13>",
					"<D>", "<E$", "<OP$12>", "<EOP>", "<S$", "<C>", "<D$", "<OP$17>",
					"<OP$18>", "<N>", "<I>", "<S>", "", "", "", "",
					"A", "B", "C", "D", "E", "F", "G", "H",
					"I", "J", "K", "L", "M", "N", "O", "P",
					"Q", "R", "S", "T", "U", "V", "W", "X",
					"Y", "Z", "a", "b", "c", "d", "e", "f",
					"g", "h", "i", "j", "k", "l", "m", "n",
					"o", "p", "q", "r", "s", "t", "u", "v",
					"w", "x", "y", "z", "0", "1", "2", "3",
					"4", "5", "6", "7", "8", "9", "!", "?",
					"", ":", "\"", "'", "-", ".", ",", "_",
					";", "#", "+", "(", ")", "%", "~", "",
					"@", "<note>", "=", "\"", "74", "75", "<pearl>", "<death>",
					"<lit>", "<wind>", "<earth>", "<ice>", "<fire>", "<water>", "<poison>", " ",
					"e ", " t", ": ", "th", "t ", "he", "s ", "er",
					" a", "re", "in", "ou", "d ", " w", " s", "an",
					"o ", " h", " o", "r ", "n ", "at", "to", " i",
					", ", "ve", "ng", "ha", " m", "Th", "st", "on",
					"yo", " b", "me", "y ", "en", "it", "ar", "ll",
					"ea", "I ", "ed", " f", " y", "hi", "is", "es",
					"or", "l ", " c", "ne", "'s", "nd", "le", "se",
					" I", "a ", "te", " l", "pe", "as", "ur", "u ",
					"al", " p", "g ", "om", " d", "f ", " g", "ow",
					"rs", "be", "ro", "us", "ri", "wa", "we", "Wh",
					"et", " r", "nt", "m ", "ma", "I'", "li", "ho",
					"of", "Yo", "h ", " n", "ee", "de", "so", "gh",
					"ca", "ra", "n'", "ta", "ut", "el", "! ", "fo",
					"ti", "We", "lo", "e!", "ld", "no", "ac", "ce",
					"k ", " u", "oo", "ke", "ay", "w ", "!!", "ag",
					"il", "ly", "co", ". ", "ch", "go", "ge", "e..."
				};
				*/
		
	}
}
