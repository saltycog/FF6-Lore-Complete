namespace FF6Hack
{
	using System;
	using System.Collections.Generic;


	public static class BooksHack
	{
		public static MovedBytesReport BlockOf89Bytes;
		public static MovedBytesReport BlockOf129Bytes;

		public static void ApplyToRom(byte[] patchedRom)
		{
			EventPatcher eventPatcher = new EventPatcher(logToConsole: false);
			BooksHack.CreateFreeSpace(eventPatcher, patchedRom);
			BooksHack.AddReadableBooks(eventPatcher);
			eventPatcher.Apply(patchedRom);
		}


		public static void AddReadableBooks(EventPatcher eventPatcher)
		{
			Console.WriteLine("Adding books to ROM...");
			List<Book> books = PrepareBooks();

			ReferenceDoc booksDoc = new ReferenceDoc(
				"books.txt",
				"Edit Dialogue in FF3usME, Events in Zone Doctor");
			foreach (Book book in books)
			{
				eventPatcher.CreateReadableBookEvent(book);
				string eventAddress = $"{book.EventAddress:X4}".Replace("CC", "2");
				booksDoc.AddText(
					$"\n{book.Title}:\n" +
					$"Event #{eventAddress}, Dialogue #{book.DialogueIndex}\n");      
			}

			booksDoc.WriteFile();

			Console.WriteLine($"Added {books.Count} books to ROM. See books.txt for reference.\n");
		}


		public static void CreateFreeSpace(EventPatcher eventPatcher, byte[] patchedRom)
		{
			Console.WriteLine("Creating free space for books...");

			// Frees 94 bytes.
			BlockOf89Bytes = eventPatcher.MoveEvent(0xCC99F1, 0x2E4520, 94, patchedRom);
			Console.WriteLine(CreateBookReport(BlockOf89Bytes));

			BlockOf129Bytes = eventPatcher.MoveEvent(0xCC9A4F, 0x2E457E, 134, patchedRom);
			Console.WriteLine(CreateBookReport(BlockOf129Bytes));
		}


		#region Helper Methods
		/// <summary>
		/// Make a readable book event.
		/// Place event below book.
		/// FF3usME: Add 1 to index, then convert to hex to get dialogue address.
		/// Bytes: 12.
		/// </summary>
		/// <param name="dialogue">Hex address of dialogue.</param>
		private static EventPatcher CreateReadableBookEvent(
			this EventPatcher eventPatcher,
			Book book)
		{
			eventPatcher.ChangeOffset(book.EventAddress);
			eventPatcher
				.ButtonAWhileFacingUp()
				.DisplayDialogueAndWaitForButton(book.DialogueIndex)
				.EndOrReturn();

			return eventPatcher;
		}


		private static string CreateBookReport(MovedBytesReport freeBytes)
		{
			var writeableBooks = (int)Math.Floor(freeBytes.FreeBytesAmount / 12f);
			string report = 
				$"{freeBytes.FreeSpaceOffset:X}: " +
				$"{freeBytes.FreeBytesAmount} bytes free for " +
				$"{writeableBooks} books.";

			return report;
		}


		private static List<Book> PrepareBooks()
		{
			List<Book> books = new List<Book>();
			books.Add(
				new Book(
					"Empire Soldier Rankings (Narshe School)",
					BlockOf89Bytes.FreeSpaceOffset,
					0xC0C));
			books.Add(
				new Book(
					"Elder's Journal (Narshe)",
					books[0].EventAddress + 12,
					books[0].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 3",
					books[1].EventAddress + 12,
					books[1].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 4",
					books[2].EventAddress + 12,
					books[2].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 5",
					books[3].EventAddress + 12,
					books[3].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 6",
					books[4].EventAddress + 12,
					books[4].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 7",
					books[5].EventAddress + 12,
					books[5].DialogueIndex + 1));

			books.Add(
				new Book(
					"Book 8",
					BlockOf129Bytes.FreeSpaceOffset,
					books[6].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 9",
					books[7].EventAddress + 12,
					books[7].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 10",
					books[8].EventAddress + 12,
					books[8].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 11",
					books[9].EventAddress + 12,
					books[9].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 12",
					books[10].EventAddress + 12,
					books[10].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 13",
					books[11].EventAddress + 12,
					books[11].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 14",
					books[12].EventAddress + 12,
					books[12].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 15",
					books[13].EventAddress + 12,
					books[13].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 16",
					books[14].EventAddress + 12,
					books[14].DialogueIndex + 1));
			books.Add(
				new Book(
					"Book 17",
					books[15].EventAddress + 12,
					books[15].DialogueIndex + 1));

			return books;
		}
		#endregion


		private struct Book
		{
			public string Title;
			public readonly uint EventAddress;
			public readonly ushort DialogueIndex;


			public Book(string title, uint eventAddress, int dialogueIndex)
			{
				this.Title = title;
				this.EventAddress = eventAddress;
				this.DialogueIndex = (ushort)dialogueIndex;
			}
		}
	}
}