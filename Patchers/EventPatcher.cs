namespace FF6Hack
{
	using System;
	using System.Collections.Generic;
	using SnesEditing;


	/// <summary>
	/// Note: Event addresses located at C9xxx.
	/// </summary>
	public partial class EventPatcher : HexPatcher
	{
		#region Constructors
		public EventPatcher(bool logToConsole = true) : base(logToConsole)
		{
			MapBank(0x2E, 0x38);
			MapBank(0xCA, 0x0A);
			MapBank(0xCB, 0x0B);
			MapBank(0xCC, 0x0C);
		}
		#endregion


		/// <summary>
		/// Begin action queue for a character or NPC.
		/// Remember to always end queues with EndCharacterActionQueue(0xFF).
		/// </summary>
		/// <param name="character">Character/NPC to begin queue of.</param>
		/// <param name="queueBytes">Number of bytes in queue (including FF), up to 127.</param>
		public EventPatcher BeginActionQueueAndContinue(Character character, ushort queueBytes)
		{
			AddInstruction(
				new Op(character.ToString(), (byte)character, 2),
				(byte)queueBytes);
			return this;
		}


		/// <summary>
		/// Begin action queue for a character or NPC.
		/// Remember to always end queues with EndCharacterActionQueue(0xFF).
		/// </summary>
		/// <param name="character">Character/NPC to begin queue of.</param>
		/// <param name="queueBytes">Number of bytes in queue (including FF), up to 127.</param>
		public EventPatcher BeginActionQueueAndWait(Character character, ushort queueBytes)
		{
			byte operand = (byte)(queueBytes + 128);
			AddInstruction(
				new Op(character.ToString(), (byte)character, 2),
				operand);
			return this;
		}


		public EventPatcher LoadMapAfterFadeOut(
			ushort mapId, 
			MapFacing mapFacing,
			MapMusic mapMusic,
			byte horizontal,
			byte vertical,
			bool dontfadeIn,
			bool runEntranceEvent,
			bool onAirship,
			bool onChocobo)
		{
			ushort map = (ushort)(mapId + (ushort)mapFacing + (ushort)mapMusic);
			var mapBytes = BitConverter.GetBytes(map);
			byte mapOptions = 0x00;
			if (dontfadeIn)
				mapOptions += 0x40;
			if (runEntranceEvent)
				mapOptions += 0x80;
			if (onAirship)
				mapOptions += 0x01;
			if (onChocobo)
				mapOptions += 0x02;

			AddInstruction(
				new Op("LoadMapAfterFadeOut", 0x6A, 6),
				mapBytes[0],
				mapBytes[1],
				horizontal,
				vertical,
				mapOptions);

			return this;
		}


		public EventPatcher ButtonAWhileFacingUp()
		{
			AddInstruction(
				new Op("ButtonAWhileFacingUp", 0xC1, 8),
				0xB0,
				0x01,
				0xB4,
				0x01,
				0xB3,
				0x5E,
				0x00);
			return this;
		}


		public EventPatcher DisplayDialogueAndWaitForButton(ushort dialogue)
		{
			AddInstruction(
				new Op("DialogueWait", 0x4B, 3),
				dialogue);
			return this;
		}


		/// <summary>
		/// Does absolutely nothing, basically filler to take up space.
		/// Bytes: 1
		/// </summary>
		public EventPatcher DoNothing()
		{
			AddInstruction(new Op("NOP", 0xFD, 1));
			return this;
		}


		/// <summary>
		/// End a script that started with a character action queue.
		/// Bytes: 1.
		/// </summary>
		public EventPatcher EndCharacterActionQueue()
		{
			AddInstruction(new Op("EndActionQueue", 0xFF, 1));
			return this;
		}


		/// <summary>
		/// End the event and return control to the player.
		/// If actually a subroutine, returns from the subroutine instead.
		/// Bytes: 1.
		/// </summary>
		public EventPatcher EndOrReturn()
		{
			AddInstruction(new Op("Return", 0xFE, 1));
			return this;
		}


		/// <summary>
		/// Jump to an address in the event code (aka, subroutine).
		/// Bytes: 4.
		/// </summary>
		/// <param name="address">Address to jump to (absolute offset).</param>
		public EventPatcher JumpToAddress(uint address)
		{
			AddInstruction(
				new Op("JumpToAddress", 0xB2, 4),
				address);
			return this;
		}


		/// <summary>
		/// Move an event from one offset to another.
		/// This will free up a number of bytes equal to (numberOfBytes - 5),
		/// those 5 initial bytes being used for the jump to the new offset.
		/// WARNING: This immediately applies changes and clears the patch buffer, before
		/// and after the rewrite. Do such moves BEFORE applying your other event patches.
		/// </summary>
		/// <param name="oldOffset">Offset of event being moved.</param>
		/// <param name="newOffset">New offset for the event, to be jumped to.</param>
		/// <param name="numberOfBytes">Number of bytes to be moved.</param>
		/// <param name="romBytes">Byte array for ROM being edited.</param>
		/// <returns>The start offset of the newly freed bytes.</returns>
		public MovedBytesReport MoveEvent(
			uint oldOffset,
			uint newOffset,
			int numberOfBytes,
			byte[] romBytes)
		{
			ClearBuffer();

			// Make a copy of the event.
			byte[] movedEvent = GetBytesAtOffset(oldOffset, numberOfBytes, romBytes);

			// Create a byte array of empty bytes of the same length as the event.
			byte[] clearedSpace = new byte[numberOfBytes];
			for (int i = 0; i < numberOfBytes; i++)
			{
				clearedSpace[i] = 0xFF;
			}

			// Replace event with empty bytes.
			this.currentOffset = oldOffset;
			AddInstruction(new Instruction(clearedSpace));

			// Insert the original event at the new location.
			this.currentOffset = newOffset;
			AddInstruction(new Instruction(movedEvent));

			// Apply changes now, because we can't rely on buffer to do things
			// in the correct order.
			Apply(romBytes);
			ClearBuffer();

			// Insert a jump at the old event offset to the new offset.
			this.currentOffset = oldOffset;
			JumpToAddress(newOffset);
			AddInstruction(new Instruction(new byte[] { 0xFE }));

			// Final apply, and then reset everything.
			Apply(romBytes);
			ClearBuffer();
			this.currentOffset = 0;

			return new MovedBytesReport(
				oldOffset + 5,
				newOffset,
				numberOfBytes - 5);
		}
	}
}