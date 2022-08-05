namespace FF6Hack
{
	using SnesEditing;


	public static class RestrictedEsperHack
	{
		public static void ApplyToRom(byte[] patchedRom)
		{
			Asm65816Patcher asm65816Patcher = new Asm65816Patcher();
			asm65816Patcher.MapBankRange(0xC0, 0xCF, 0x00);
			BuildEdits(asm65816Patcher);
			asm65816Patcher.Apply(patchedRom);
		}


		private static void BuildEdits(Asm65816Patcher asm65816Patcher)
		{
			asm65816Patcher.ChangeOffset(0xC31B61);
			asm65816Patcher
				.JumpToSubroutine(0xF091);

			// 
			asm65816Patcher.ChangeOffset(0xC3F091);
			asm65816Patcher
				.StoreValueInX()
				.GetAddressValueWithXIndex(0x69)
				.StoreValueInAddress(0x1CF8)
				.TerminateSubroutine();

			// Check Esper
			asm65816Patcher.ChangeOffset(0xCF098);
			asm65816Patcher
				.PushXToStack()
				.StoreValueInAddress(0xE0)
				.GetDirectPage()
				.GetAddressValue(0x1CF8)
				.MultiplyBy2()
				.StoreValueInX()
				.GetAddressValue(0xE0)
				.MultiplyBy2();
			//.Ju
		}
	}
}
