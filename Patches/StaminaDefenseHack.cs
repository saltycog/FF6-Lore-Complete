namespace FF6Hack
{
	using SnesEditing;


	public static class StaminaDefenseHack
	{
		private const uint DefensePlusStaminaOffset = 0xC26530;
		private const ushort DefenseEquipOriginal = 0x301A;
		private const ushort StaminaEquipOriginal = 0x3002;
		private const ushort InBattleDefense = 0x3BB8;
		private const ushort InBattleStamina = 0x3B40;
		private const ushort StatScreenDefense = 0x11BA;
		private const ushort StatScreenStamina = 0x11BA;


		public static void ApplyToRom(byte[] patchedRom)
		{
			FF6Patcher ff6Patcher = new FF6Patcher();
			ff6Patcher.MapBankRange(0xC0, 0xCF, 0x00);
			
			BuildEdits(ff6Patcher);
			ff6Patcher.Apply(patchedRom);
		}


		#region Helper Methods
		private static void BuildEdits(FF6Patcher ff6Patcher)
		{
			ChangeBattleDefenseAlgorithm(ff6Patcher);

			// TODO: Still broken in Stat screen and equip->before.
			//ChangeMenuDefenseAlgorithms(ff6Patcher);
		}


		private static void ChangeBattleDefenseAlgorithm(FF6Patcher ff6Patcher)
		{
			// Replace loaded Defense in battle with new algorithm.
			ff6Patcher.ChangeOffset(0xC20CD1);
			ff6Patcher
				.JumpToBankSubroutine(DefensePlusStaminaOffset);

			// Algorithm that changes how defense is calculated in battle.
			ff6Patcher.ChangeOffset(DefensePlusStaminaOffset);
			ff6Patcher
				.GetAddressValueWithYIndex(InBattleStamina)
				.PushProcessorStatusBits()
				.SetProcessorStatusBits(0x20)
				.DivideBy2()
				.DivideBy2()
				.AddAddressValueAtIndexY(InBattleDefense)
				.BranchAcrossBytesIfOverflowSet(0x04)
				.Compare(0xFF)
				.BranchAcrossBytesIfLessThan(0x02)
				.Set(0xFF)
				.PullProcessorStatusBits()
				.ClearCarry()
				.TerminateBankSubroutine();
		}


		private static void ChangeMenuDefenseAlgorithms(FF6Patcher ff6Patcher)
		{
			// Replace loaded Defense in Stats screen with new algorithm.
			ff6Patcher.ChangeOffset(0xC36019);
			ff6Patcher
				.JumpToSubroutine(0xF1E0);

			// Replace loaded "before" Defense in Equip screen with new algorithm.
			ff6Patcher.ChangeOffset(0xC39192);
			ff6Patcher
				.JumpToSubroutine(0xF200);

			// Replace loaded "after" Defense in Equip screen with new algorithm.
			ff6Patcher.ChangeOffset(0xC392D1);
			ff6Patcher
				.JumpToSubroutine(0xF1E0);

			// Algorithm that changes how Defense is calculated in Stats and "after" Equip sections.
			ff6Patcher.ChangeOffset(0xC3F1E0);
			ff6Patcher
				.GetAddressValue(StatScreenDefense)
				.PushProcessorStatusBits()
				.DivideBy2()
				.DivideBy2()
				.AddAddressValue(StatScreenStamina)
				.BranchAcrossBytesIfOverflowSet(0x04)
				.Compare(0xFF)
				.BranchAcrossBytesIfLessThan(0x02)
				.Set(0xFE)
				.PullProcessorStatusBits()
				.ClearCarry()
				.TerminateSubroutine();

			// Algorithm that changes how Defense is calculated in "before" Equip section.
			ff6Patcher.ChangeOffset(0xC3F200);
			ff6Patcher
				.GetAddressValue(StaminaEquipOriginal)
				.PushProcessorStatusBits()
				.DivideBy2()
				.DivideBy2()
				.AddAddressValue(DefenseEquipOriginal)
				.BranchAcrossBytesIfOverflowSet(0x04)
				.Compare(0xFF)
				.BranchAcrossBytesIfLessThan(0x02)
				.Set(0xFE)
				.PullProcessorStatusBits()
				.ClearCarry()
				.TerminateSubroutine();
		}
		#endregion
	}
}