namespace FF6Hack
{
	public struct MovedBytesReport
	{
		public readonly uint FreeSpaceOffset;
		public readonly uint MovedBytesOffset;
		public readonly int FreeBytesAmount;


		public MovedBytesReport(uint freeSpaceOffset, uint movedBytesOffset, int freeBytesAmount)
		{
			this.FreeSpaceOffset = freeSpaceOffset;
			this.MovedBytesOffset = movedBytesOffset;
			this.FreeBytesAmount = freeBytesAmount;
		}
	}
}