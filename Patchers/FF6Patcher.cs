namespace FF6Hack
{
	using SnesEditing;


	public sealed class FF6Patcher : Asm65816Patcher<FF6Patcher>
	{
		/// <summary>
		/// Check if an EventBit is set.
		/// Will set zero flag to 0 if set.
		/// Bytes: 6
		/// </summary>
		/// <param name="eventBit">EventBit to check.</param>
		public FF6Patcher CheckEventBit(EventBit eventBit)
		{
			GetBankAddressValue(eventBit.Offset);
			CheckBit(eventBit.Bit);
			return this;
		}
		

		/// <summary>
		/// Branch to an address in a same bank if a given EventBit is set.
		/// Bytes: 8.
		/// </summary>
		/// <param name="address">Address to branch to.</param>
		/// <param name="eventBit">EventBit to check.</param>
		public FF6Patcher BranchIfEventBitSet(ushort address, EventBit eventBit)
		{
			CheckEventBit(eventBit);
			BranchToAddressIfNotEqual(address);
			return this;
		}


		/// <summary>
		/// Branch given number of bytes if a given EventBit is set.
		/// Bytes: 8.
		/// </summary>
		/// <param name="byteDistance">Number of bytes to branch.</param>
		/// <param name="eventBit">EventBit to check.</param>
		/// <returns></returns>
		public FF6Patcher BranchAcrossBytesIfEventBitSet(sbyte byteDistance, EventBit eventBit)
		{
			byteDistance -= 0x08; // Compensate for the following instructions.
			CheckEventBit(eventBit);
			BranchAcrossBytesIfNotEqual(byteDistance);
			return this;
		}
	}
}