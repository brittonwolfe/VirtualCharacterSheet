using VirtualCharacterSheet.Exceptions;

namespace VirtualCharacterSheet {

	public class HitDice {
		public ushort Die;
		public uint Count;

		public HitDice(ushort d) : this(1, d) {}
		public HitDice(uint n, ushort d) {
			Die = d;
			Count = n;
		}

	}

	public class Character {
		public string Name;
		public string Player;
		protected byte Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma;
		public short STR { get { return Core.Modifier(Strength); } }
		public short DEX { get { return Core.Modifier(Dexterity); } }
		public short CON { get { return Core.Modifier(Constitution); } }
		public short INT { get { return Core.Modifier(Intelligence); } }
		public short WIS { get { return Core.Modifier(Wisdom); } }
		public short CHA { get { return Core.Modifier(Charisma); } }
		public bool Inspiration { get; private set; }

		public Character() { }

	}

	public class NPC {

	}

	public class Feat {
		public string Name;
		public string Description;
		public Script Behavior;

	}

	public class Class {
		public readonly string Name;
		public ushort HitDie;
		public bool[] Saves;

		public Class(string n) {
			Name = n;
			if (Data.HasClass(n))
				throw new ClassAlreadyExistsException(Name);
			Data.SetClass(Name, this);
			Saves = new bool[6];
		}

	}

}