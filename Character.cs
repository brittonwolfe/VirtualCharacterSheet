using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

using VirtualCharacterSheet.Event;
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

	public abstract class Character : DynamicObject {
		public string Name;
		public byte Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma;
		public short STR { get { return Core.Modifier(Strength); } }
		public short DEX { get { return Core.Modifier(Dexterity); } }
		public short CON { get { return Core.Modifier(Constitution); } }
		public short INT { get { return Core.Modifier(Intelligence); } }
		public short WIS { get { return Core.Modifier(Wisdom); } }
		public short CHA { get { return Core.Modifier(Charisma); } }
		public dynamic Info = new ExpandoObject();
		public dynamic Meta = new ExpandoObject();
		private DynamicBehaviorSet behavior;

		internal static event InjectionEvent Injection;

		public Character() {
			behavior = new DynamicBehaviorSet(this);
		}

		protected void Inject() {
			if(Injection != null)
				try { Injection.Invoke(this); }
				catch(Exception e) { Console.WriteLine(e); }
		}

		public bool HasInfo(string name) { return ((IDictionary)Info).Contains(name); }
		public bool HasMeta(string name) { return ((IDictionary)Meta).Contains(name); }
		public bool HasBehavior(string name) { return behavior.Contains(name); }

		public void AddBehavior(string name, dynamic obj) { behavior.Add(name, obj); }

		public override bool TryGetMember(GetMemberBinder binder, out object result) {
			if(base.TryGetMember(binder, out result))
				return true;
			else if(HasBehavior(binder.Name))
				return behavior.TryGetMember(binder, out result);
			result = null;
			return false;
		}

	}

	public class PlayerCharacter : Character {
		public string Player;
		public string Identifier { get { return (Player + ":" + Name); } }
		public bool Inspiration { get; private set; }

		public PlayerCharacter(string name, string player) : base() {
			Name = name;
			Player = player;
			Data.SetCharacter(this);
			Inject();
		}

	}

	public class NPC : Character {
		public string Module;
		public string Identifier { get { return (Module + ":" + Name); } }

		public NPC(string name, string module) : base() {
			Name = name;
			Module = module;
			Inject();
		}

	}

	public class Feat : ScriptedObject {
		public string Name;
		public string Description;

		public Feat(string title) {
			Name = title;
			Data.SetFeat(Name, this);
		}

	}

	public class Class {
		public readonly string Identifier;
		public string Name;
		public ushort HitDie;
		public bool[] Saves;
		public dynamic Info = new ExpandoObject();
		internal List<dynamic> OnLevel = new List<dynamic>();

		public Class(string n) {
			Identifier = n;
			if (Data.HasClass(n))
				throw new ClassAlreadyExistsException(Identifier);
			Data.SetClass(Identifier, this);
			Saves = new bool[6];
		}

		public void AttachInstance(Character c) {
			var tmp = new ClassInstance(this);
		}

	}
	
	public class ClassInstance {
		public ushort Level { get; private set; }
		private Class underlying;
		private Character attached;

		internal ClassInstance(Class c) { underlying = c; }

		internal void Attach(Character character, ushort lvl = 1) {
			attached = character;
			Level = lvl;
		}

		public void LevelUp() { underlying.OnLevel[++Level](attached); }

	}

}