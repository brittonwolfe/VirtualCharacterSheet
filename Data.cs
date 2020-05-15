using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet {

	public static class Data {
		private static Dictionary<uint, Item> item = new Dictionary<uint, Item>();
		private static Dictionary<uint, Character> character = new Dictionary<uint, Character>();
		private static Dictionary<uint, NPC> npc = new Dictionary<uint, NPC>();
		private static Dictionary<string, RawPyScript> py = new Dictionary<string, RawPyScript>();
		private static Dictionary<string, object> pyf = new Dictionary<string, object>();
		private static Dictionary<string, Class> classes = new Dictionary<string, Class>();
		private static Dictionary<string, Feat> feat = new Dictionary<string, Feat>();

		public static Item GetItem(uint id) { return item[id]; }
		public static Character GetCharacter(uint id) { return character[id]; }
		public static NPC GetNPC(uint id) { return npc[id]; }
		public static RawPyScript GetPy(string key) { return py[key]; }
		public static object GetPyF(string key) { return pyf[key]; }
		public static Class GetClass(string key) { return classes[key.ToLower()]; }
		public static Feat GetFeat(string key) { return feat[key.ToLower()]; }

		public static void SetItem(uint id, Item i) { item[id] = i; }
		public static void SetCharacter(uint id, Character c) { character[id] = c; }
		public static void SetNPC(uint id, NPC n) { npc[id] = n; }
		public static void SetPy(string key, RawPyScript src) { py[key] = src; }
		public static void SetPyF(string key, object func) { pyf[key] = func; }
		internal static void SetClass(string key, Class c) { classes[key.ToLower()] = c; }
		public static void SetFeat(string key, Feat f) { feat[key.ToLower()] = f; }

		public static bool HasItem(uint id) { return item.ContainsKey(id); }
		public static bool HasCharacter(uint id) { return character.ContainsKey(id); }
		public static bool HasNPC(uint id) { return npc.ContainsKey(id); }
		public static bool HasPy(string key) { return py.ContainsKey(key); }
		public static bool HasPyF(string key) { return pyf.ContainsKey(key); }
		public static bool HasClass(string key) { return classes.ContainsKey(key.ToLower()); }
		public static bool HasFeat(string key) { return feat.ContainsKey(key.ToLower()); }

		public static uint AddItem(Item i) {
			uint output = 0;
			for(output = 0; output < uint.MaxValue; output++)
				if(!HasItem(output)) {
					item[output] = i;
					return output;
				}
			throw new Exception("item array is full!");
		}

	}

	public class Die {
		public static Random rng = new Random();

		public static uint Roll(ushort d) { return (ushort)rng.Next(1,d); }
		public static uint Rolln(byte n, ushort d) {
			uint output = 0;
			for(byte x = 0; x < n; x++)
				output += Roll(d);
			return output;
		}

	}

	public class Item : ScriptedObject {
		public string Name;
		public string Description;

		public Item() { }
		public Item(string name) {
			Name = name;
		}
		public Item(string name, Script behavior) {
			Name = name;
			Behavior = behavior;
		}

	}

}
