namespace FF6Hack
{
    public partial class EventPatcher
    {
	    public enum MapMusic : ushort
	    {
		    Default = 0x000,
			ContinuePrevious = 0x400
	    }

	    public enum MapFacing : ushort
	    {
		    Up = 0x0000,
			Right = 0x1000,
			Down = 0x2000,
			Left = 0x3000
	    }

		public enum Character : byte
		{
			Terra = 0x00,
			Locke = 0x01,
			Cyan = 0x02,
			Shadow = 0x03,
			Edgar = 0x04,
			Sabin = 0x05,
			Celes = 0x06,
			Strago = 0x07,
			Relm = 0x08,
			Setzer = 0x09,
			Mog = 0x0A,
			Gau = 0x0B,
			Gogo = 0x0C,
			Umaro = 0x0D,
			TempCharacter1 = 0x0E,
			TempCharacter2 = 0x0F,
			NPC0 = 0x10,
			NPC1 = 0x11,
			NPC2 = 0x12,
			NPC3 = 0x13,
			NPC4 = 0x14,
			NPC5 = 0x15,
			NPC6 = 0x16,
			NPC7 = 0x17,
			NPC8 = 0x18,
			NPC9 = 0x19,
			NPC10 = 0x1A,
			NPC11 = 0x1B,
			NPC12 = 0x1C,
			NPC13 = 0x1D,
			NPC14 = 0x1E,
			NPC15 = 0x1F,
			NPC16 = 0x20,
			NPC17 = 0x21,
			NPC18 = 0x22,
			NPC19 = 0x23,
			NPC20 = 0x24,
			NPC21 = 0x25,
			NPC22 = 0x26,
			NPC23 = 0x27,
			NPC24 = 0x28,
			NPC25 = 0x29,
			NPC26 = 0x2A,
			NPC27 = 0x2B,
			NPC28 = 0x2C,
			NPC29 = 0x2D,
			NPC30 = 0x2E,
			NPC31 = 0x2F,
			Camera = 0x30,
			PartyMember1 = 0x31,
			PartyMember2 = 0x32,
			PartyMember3 = 0x33,
			PartyMember = 0x34
		}
	}
}
