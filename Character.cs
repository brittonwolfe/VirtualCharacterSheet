using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Scripting.Ast;
using System;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
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
		public dynamic Save = new ExpandoObject();
		public dynamic Meta = new ExpandoObject();
		private DynamicBehaviorSet behavior;

		internal static event InjectionEvent Injection;

		public Character() {
			behavior = new DynamicBehaviorSet(this);
		}

		protected void Inject() { if(Injection != null) Injection.Invoke(this); }

		public bool HasInfo(string name) { return ((IDictionary)Info).Contains(name); }
		public bool HasSave(string name) { return ((IDictionary)Save).Contains(name); }
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

		public PlayerCharacter(string name, string player) {
			Name = name;
			Player = player;
			Data.SetCharacter(this);
			Inject();
		}

	}

	public class NPC : Character {
		public string Module;
		public string Identifier { get { return (Module + ":" + Name); } }

		public NPC(string name, string module) {
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