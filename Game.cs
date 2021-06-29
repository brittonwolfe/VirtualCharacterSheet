using System;
using System.Collections.Generic;

namespace VCS {

	public class Die {
		public static Random rng = new Random();

		public static uint Roll(ushort d) { return (ushort)rng.Next(1, d); }
		public static uint Rolln(ushort n, ushort d) {
			uint output = 0;
			for(ushort x = 0; x < n; x++)
				output += Roll(d);
			return output;
		}
		public static uint Rolln((ushort, ushort) d) { return Rolln(d.Item1, d.Item2); }

	}

	public class Item : ScriptedObject {
		public string Name;
		public readonly string Identifier;

		public Item(string id) : base() {
			Identifier = id;
			Data.SetItem(this);
		}

	}

	public struct Roll {
		public List<Modifier> Mods;
		public (ushort, ushort) Dice;

		public Roll(ushort n, ushort d, params Modifier[] mods) {
			Dice = (n, d);
			Mods = new List<Modifier>();
			foreach (Modifier m in mods)
				Add(m);
		}

		public void Add(Modifier m) { Mods.Add(m); }

		public int Do() {
			int output = (int)Die.Rolln(Dice);
			foreach (Modifier m in Mods) { }
			return output;
		}

	}

	public class Modifier : ScriptedObject {

		public Modifier() { }
		public Modifier(short mod) { Behavior = new RawPyScript("return " + mod); }
		public Modifier(Script script) { Behavior = script; }

		public static Modifier Parse(string str) { return new Modifier(short.Parse(str)); }

	}

}