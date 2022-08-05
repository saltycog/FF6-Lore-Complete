﻿namespace FF6Hack
{
	public enum Spell : byte
	{
		Fire = 0x00000000,
		Ice = 0x00000001,
		Bolt = 0x00000002,
		Poison = 0x00000002,
		Drain = 0x00000004,
		Fire2 = 0x00000005,
		Ice2 = 0x00000006,
		Bolt2 = 0x00000007,
		Bio = 0x00000008,
		Fire3 = 0x00000009,
		Ice3 = 0x0000000A,
		Bolt3 = 0x0000000B,
		Break = 0x0000000C,
		Doom = 0x0000000D,
		Pearl = 0x0000000E,
		Flare = 0x0000000F,
		Demi = 0x00000010,
		Quartr = 0x00000011,
		XZone = 0x00000012,
		Meteor = 0x00000013,
		Ultima = 0x00000014,
		Quake = 0x00000015,
		WWind = 0x00000016,
		Merton = 0x00000017,
		Scan = 0x00000018,
		Slow = 0x00000019,
		Rasp = 0x0000001A,
		Mute = 0x0000001B,
		Safe = 0x0000001C,
		Sleep = 0x0000001D,
		Muddle = 0x0000001E,
		Haste = 0x0000001F,
		Stop = 0x00000020,
		Bserk = 0x00000021,
		Float = 0x00000022,
		Imp = 0x00000023,
		Rflect = 0x00000024,
		Shell = 0x00000025,
		Vanish = 0x00000026,
		Haste2 = 0x00000027,
		Slow2 = 0x00000028,
		Osmose = 0x00000029,
		Warp = 0x0000002A,
		Quick = 0x0000002B,
		Dispel = 0x0000002C,
		Cure = 0x0000002D,
		Cure2 = 0x0000002E,
		Cure3 = 0x0000002F,
		Life = 0x00000030,
		Life2 = 0x00000031,
		Antdot = 0x00000032,
		Remedy = 0x00000033,
		Regen = 0x00000034,
		Life3 = 0x00000035,
		Ramuh = 0x00000036,
		Ifrit = 0x00000037,
		Shiva = 0x00000038,
		Siren = 0x00000039,
		Terrato = 0x0000003A,
		Shoat = 0x0000003B,
		Maduin = 0x0000003C,
		Bismark = 0x0000003D,
		Stray = 0x0000003E,
		Palador = 0x0000003F,
		Tritoch = 0x00000040,
		Odin = 0x00000041,
		Raiden = 0x00000042,
		Bahamut = 0x00000043,
		Alexandr = 0x00000044,
		Crusader = 0x00000045,
		Ragnarok = 0x00000046,
		Kirin = 0x00000047,
		ZoneSeek = 0x00000048,
		Carbunkl = 0x00000049,
		Phantom = 0x0000004A,
		Sraphim = 0x0000004B,
		Golem = 0x0000004C,
		Unicorn = 0x0000004D,
		Fenrir = 0x0000004E,
		Starlet = 0x0000004F,
		Phoenix = 0x00000050,
		FireSkean = 0x00000051,
		WaterEdge = 0x00000052,
		BoltEdge = 0x00000053,
		Storm = 0x00000054,
		Dispatch = 0x00000055,
		Retort = 0x00000056,
		Slash = 0x00000057,
		QuadraSlam = 0x00000058,
		Empowerer = 0x00000059,
		Stunner = 0x0000005A,
		QuadraSlice = 0x0000005B,
		Cleave = 0x0000005C,
		Pummel = 0x0000005D,
		AuraBolt = 0x0000005E,
		Suplex = 0x0000005F,
		FireDance = 0x00000060,
		Mantra = 0x00000061,
		AirBlade = 0x00000062,
		Spiraler = 0x00000063,
		BumRush = 0x00000064,
		WindSlash = 0x00000065,
		SunBath = 0x00000066,
		Rage = 0x00000067,
		Harvester = 0x00000068,
		SandStorm = 0x00000069,
		Antlion = 0x0000006A,
		ElfFire = 0x0000006B,
		Specter = 0x0000006C,
		LandSlide = 0x00000,
		SonicBoom = 0x0000006E,
		ElNino = 0x0000006F,
		Plasma = 0x00000070,
		Snare = 0x00000071,
		CaveIn = 0x00000072,
		Snowball = 0x00000073,
		Surge = 0x00000074,
		Cokatrice = 0x00000075,
		Wombat = 0x00000076,
		Kitty = 0x00000077,
		Tapir = 0x00000078,
		Whump = 0x00000079,
		WildBear = 0x0000007A,
		PoisFrog = 0x0000007B,
		IceRabbit = 0x0000007C,
		SuperBall = 0x0000007D,
		Flash = 0x0000007E,
		Chocobop = 0x0000007F,
		HBomb = 0x00000080,
		Slot7Flush = 0x00000081,
		Megahit = 0x00000,
		FireBeam = 0x00000083,
		BoltBeam = 0x00000084,
		IceBeam = 0x00000085,
		BioBlast = 0x00000086,
		HealForce = 0x00000087,
		Confuser = 0x00000088,
		XFer = 0x00000089,
		TekMissile = 0x0000008A,
		Condemned = 0x0000008B,
		Roulette = 0x0000008C,
		CleanSweep = 0x0000008D,
		AquaRake = 0x0000008E,
		Aero = 0x0000008F,
		BlowFish = 0x00000090,
		BigGuard = 0x00000091,
		Revenge = 0x00000092,
		PearlWind = 0x00000093,
		L5Doom = 0x00000094,
		L4Flare = 0x00000095,
		L3Muddle = 0x00000096,
		ReflectLore = 0x00000097,
		LNPearl = 0x00000098,
		StepMine = 0x00000099,
		ForceField = 0x0000009A,
		Dischord = 0x0000009B,
		SourMouth = 0x0000009C,
		PepUp = 0x0000009D,
		Rippler = 0x0000009E,
		Stone = 0x0000009F,
		Quasar = 0x000000A0,
		GrandTrain = 0x000000A1,
		Exploder = 0x000000A2,
		ImpSong = 0x000000A3,
		Clear = 0x000000A4,
		Virite = 0x000000A5,
		ChokeSmoke = 0x000000A6,
		Schiller = 0x000000A7,
		Lullaby = 0x000000A8,
		AcidRain = 0x000000A9,
		Confusion = 0x000000AA,
		Megazerk = 0x000000AB,
		MuteAttack = 0x000000AC,
		Net = 0x000000AD,
		Slimer = 0x000000AE,
		DeltaHit = 0x000000AF,
		Entwine = 0x000000B0,
		Blaster = 0x000000B1,
		Cyclonic = 0x000000B2,
		FireBall = 0x000000B3,
		AtomicRay = 0x000000B4,
		TekLaser = 0x000000B5,
		Diffuser = 0x000000B6,
		WaveCannon = 0x000000B7,
		MegaVolt = 0x000000B8,
		GigaVolt = 0x000000B9,
		Blizzard = 0x000000BA,
		Absolute0 = 0x000000BB,
		Magnitude8 = 0x000000BC,
		Raid = 0x000000BD,
		FlashRain = 0x000000BE,
		TekBarrier = 0x000000BF,
		FallenOne = 0x000000C0,
		WallChange = 0x000000C1,
		Escape = 0x000000C2,
		FiftyGs = 0x000000C3,
		MindBlast = 0x000000C4,
		NCross = 0x000000C5,
		FlareStar = 0x000000C6,
		LoveToken = 0x000000C7,
		Seize = 0x000000C8,
		RPolarity = 0x000000C9,
		Targetting = 0x000000CA,
		Sneeze = 0x000000CB,
		SCross = 0x000000CC,
		Launcher = 0x000000CD,
		Charm = 0x000000CE,
		ColdDust = 0x000000CF,
		Tentacle = 0x000000D0,
		HyperDrive = 0x000000D1,
		Train = 0x000000D2,
		EvilToot = 0x000000D3,
		GravBomb = 0x000000D4,
		Engulf = 0x000000D5,
		Disaster = 0x000000D6,
		Shrapnel = 0x000000D7,
		Bomblet = 0x000000D8,
		HeartBurn = 0x000000D9,
		Zinger = 0x000000DA,
		Discard = 0x000000DB,
		Overcast = 0x000000DC,
		Missile = 0x000000DD,
		Goner = 0x000000DE,
		Meteo = 0x000000DF,
		Revenger = 0x000000E0,
		Phantasm = 0x000000E1,
		Dread = 0x000000E2,
		ShockWave = 0x000000E3,
		Blaze = 0x000000E4,
		SoulOut = 0x000000E5,
		GaleCut = 0x000000E6,
		Shimsham = 0x000000E7,
		LodeStone = 0x000000E8,
		ScarBeam = 0x000000E9,
		BabaBreath = 0x000000EA,
		LifeShaver = 0x000000EB,
		FireWall = 0x000000EC,
		Slide = 0x000000ED,
		Battle = 0x000000EE,
		Special = 0x000000EF,
		RiotBlade = 0x000000F0,
		Mirager = 0x000000F1,
		BackBlade = 0x000000F2,
		ShadowFang = 0x000000F3,
		RoyalShock = 0x000000F4,
		TigerBreak = 0x000000F5,
		SpinEdge = 0x000000F6,
		SabreSoul = 0x000000F7,
		StarPrism = 0x000000F8,
		RedCard = 0x000000F9,
		MoogleRush = 0x000000FA,
		XMeteo = 0x000000FB,
		Takedown = 0x000000FC,
		WildFang = 0x000000FD,
		Lagomorph = 0x000000FE,
		Nothing = 0x00000FF
	}
}