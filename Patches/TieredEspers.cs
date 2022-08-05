namespace FF6Hack
{
	using SnesEditing;


	/// <summary>
	/// Makes it so the fifth spell slot of any esper can't be learned until
	/// a certain event is 
	/// </summary>
	public static class TieredEspers
	{
		public const uint OriginalSpellCheck = 0xC26032;
		public const uint ModifiedSpellCheck = 0xC26580;
		public const uint ModifiedSpellDisplay = 0xC3F210;
		public const uint SpellTaught = 0xD86E01;
		public const ushort LearnSpell = 0x652E;

		private static readonly EventBit UnlockEvent = EventBit.AcquiredTritoch;

		public static void ApplyToRom(byte[] patchedRom)
		{
			FF6Patcher ff6Patcher = new FF6Patcher();
			ff6Patcher.MapBankRange(0xC0, 0xCF, 0x00);

			BuildEdits(ff6Patcher);
			ff6Patcher.Apply(patchedRom);
		}


		private static void BuildEdits(FF6Patcher ff6Patcher)
		{
			ChangeSpellCheckAlgorithm(ff6Patcher);
			ChangeSpellsLearnedDisplay(ff6Patcher);
		}


		/// <summary>
		/// Shows fifth spell slot in Esper menus as greyed out until appropriate
		/// event is triggered.
		/// </summary>
		private static void ChangeSpellsLearnedDisplay(FF6Patcher ff6Patcher)
		{
			ff6Patcher.ChangeOffset(0xC35A1E);
			ff6Patcher
				.JumpToBankSubroutine(ModifiedSpellDisplay);

			ff6Patcher.ChangeOffset(ModifiedSpellDisplay);
			ff6Patcher
				.IncrementDirectPage(0xF5)
				.IncrementDirectPage(0xF5)
				
				// Check if event bit is set.
				.Use8BitAccumulator()
				.BranchAcrossBytesIfEventBitSet(0x11, UnlockEvent)
				
				// Check if this is the fifth spell slot.
				.Use16BitAccumulator()
				.GetDirectPage(0xF5)
				.Compare16(0x0019)
				.Use8BitAccumulator()
				.BranchAcrossBytesIfNotEqual(0x04)
				
				// Set spell to be grey.
				.Set(0x28)
				.StoreValueInDirectPage(0x29)

				// NormalSpellDisplay
				.Use16BitAccumulator()
				.TerminateBankSubroutine();
		}


		/// <summary>
		/// Disables learning on fifth esper spell slot until appropriate event
		/// is triggered.
		/// </summary>
		private static void ChangeSpellCheckAlgorithm(FF6Patcher ff6Patcher)
		{
			ff6Patcher.ChangeOffset(OriginalSpellCheck);
			ff6Patcher
				.JumpToBankSubroutine(ModifiedSpellCheck);

			ff6Patcher.ChangeOffset(ModifiedSpellCheck);
			ff6Patcher
				.CompareY(0x01) // 5th esper spell slot
				.BranchToAddressIfNotEqual(LearnSpell)
				.BranchIfEventBitSet(LearnSpell, UnlockEvent)
				
				// Disable 5th spell slot progress.
				.Set((byte)Spell.Nothing)
				.TerminateBankSubroutine()

				// LearnSpell: Learn spell as usual.
				.GetBankAddressValueWithXIndex(SpellTaught)
				.TerminateBankSubroutine();
		}

	}
}
