namespace FF6Hack
{
	public struct EventBit
	{
		public readonly uint Offset;
		public readonly byte Bit;


		public EventBit(uint offset, byte bit)
		{
			this.Offset = offset;
			this.Bit = bit;
		}

		public static readonly EventBit AcquiredTheFalcon = new EventBit(0x7E1E99, 0x05);
		public static readonly EventBit AcquiredTritoch = new EventBit(0x7E1ED3, 0x06);
	}
}
