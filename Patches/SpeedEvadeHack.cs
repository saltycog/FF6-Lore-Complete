namespace FF6Hack
{
	public static class SpeedEvadeHack
	{
		private const ushort SpeedPlusEvadeOffset = 0x6550;
		private const ushort InBattleEvade = 0x3B54;
		private const ushort InBattleSpeed = 0x3B19;


		public static void ApplyToRom(byte[] patchedRom)
		{
			FF6Patcher ff6Patcher = new FF6Patcher();
			ff6Patcher.MapBankRange(0xC0, 0xCF, 0x00);

			BuildEdits(ff6Patcher);
			ff6Patcher.Apply(patchedRom);
		}

		private static void BuildEdits(FF6Patcher ff6Patcher)
		{
			ff6Patcher.ChangeOffset(0xC22345);
			ff6Patcher
				.JumpToSubroutine(SpeedPlusEvadeOffset);

			ff6Patcher.ChangeOffset(0xC20000 + SpeedPlusEvadeOffset);
			ff6Patcher
				.GetAddressValueWithYIndex(InBattleSpeed)
				.DivideBy2()
				.DivideBy2()
				.StoreValueInAddress(0x656E)
				.GetAddressValueWithYIndex(InBattleEvade)
				.CompareToAddress(0x656E)
				.BranchToAddressIfLessThan(0x07)
				.SubtractAddressValue(0x656E)
				.Compare(0x00)
				.BranchAcrossBytesIfNotEqual(0x02)
				.Set(0x01)
				.ClearCarry()
				.TerminateSubroutine();
		}
	}
}
