using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace VirtualCharacterSheet {

	public static class Data {
		private static Dictionary<string, PlayerCharacter> character = new Dictionary<string, PlayerCharacter>();
		private static Dictionary<string, Class> classes = new Dictionary<string, Class>();
		private static Dictionary<string, Feat> feat = new Dictionary<string, Feat>();
		private static Dictionary<string, Item> item = new Dictionary<string, Item>();
		private static Dictionary<string, NPC> npc = new Dictionary<string, NPC>();
		private static Dictionary<string, RawPyScript> py = new Dictionary<string, RawPyScript>();
		private static Dictionary<string, object> pyf = new Dictionary<string, object>();

		private static Dictionary<string, Brew> brew = new Dictionary<string, Brew>();

		public static Item GetItem(string key) { return item[key.ToLower()]; }
		public static PlayerCharacter GetCharacter(string key) { return character[key.ToLower()]; }
		public static NPC GetNPC(string key) { return npc[key.ToLower()]; }
		public static RawPyScript GetPy(string key) { return py[key.ToLower()]; }
		public static object GetPyF(string key) { return pyf[key.ToLower()]; }
		public static Class GetClass(string key) { return classes[key.ToLower()]; }
		public static Feat GetFeat(string key) { return feat[key.ToLower()]; }

		public static void SetItem(Item i) { item[i.Identifier.ToLower()] = i; }
		public static void SetCharacter(PlayerCharacter c) { character[c.Identifier.ToLower()] = c; }
		public static void SetNPC(string key, NPC n) { npc[key.ToLower()] = n; }
		public static void SetPy(string key, RawPyScript src) { py[key.ToLower()] = src; }
		public static void SetPyF(string key, object func) { pyf[key.ToLower()] = func; }
		internal static void SetClass(string key, Class c) { classes[key.ToLower()] = c; }
		public static void SetFeat(string key, Feat f) { feat[key.ToLower()] = f; }

		public static bool HasItem(string key) { return item.ContainsKey(key.ToLower()); }
		public static bool HasCharacter(string key) { return character.ContainsKey(key.ToLower()); }
		public static bool HasNPC(string key) { return npc.ContainsKey(key.ToLower()); }
		public static bool HasPy(string key) { return py.ContainsKey(key.ToLower()); }
		public static bool HasPyF(string key) { return pyf.ContainsKey(key.ToLower()); }
		public static bool HasClass(string key) { return classes.ContainsKey(key.ToLower()); }
		public static bool HasFeat(string key) { return feat.ContainsKey(key.ToLower()); }

		internal static void AddBrew(Brew b) { brew[b.Name] = b; }
		public static Brew GetBrew(string n) { return brew[n]; }

	}

	public class Brew {
		public dynamic Meta = new ExpandoObject();
		public readonly string Name;
		public string Description;
		public string Title;

		public Brew(string name) {
			Name = name;
			Data.AddBrew(this);
		}

		public void DefineSave() {
			
		}

	}

	public class Die {
		public static Random rng = new Random();

		public static uint Roll(ushort d) { return (ushort)rng.Next(1,d); }
		public static uint Rolln(ushort n, ushort d) {
			uint output = 0;
			for(ushort x = 0; x < n; x++)
				output += Roll(d);
			return output;
		}
		public static uint Rolln((ushort, ushort) d) { return Rolln(d.Item1, d.Item2); }

	}

	public struct Roll {
		public List<Modifier> Mods;
		public (ushort, ushort) Dice;

		public Roll(ushort n, ushort d, params Modifier[] mods) {
			Dice = (n, d);
			Mods = new List<Modifier>();
			foreach(Modifier m in mods)
				Add(m);
		}

		public void Add(Modifier m) { Mods.Add(m); }

		public int Do() {
			int output = (int)Die.Rolln(Dice);
			foreach(Modifier m in Mods) { }
			return output;
		}

	}

	public class Modifier : ScriptedObject{

		public Modifier() {}
		public Modifier(short mod) { Behavior = new RawPyScript("return " + mod); }
		public Modifier(Script script) { Behavior = script; }

	}

	public class Item : ScriptedObject {
		public string Name;
		public readonly string Identifier;
		public string Description;

		public Item(string id) {
			Identifier = id;
			Data.SetItem(this);
		}

	}

}
