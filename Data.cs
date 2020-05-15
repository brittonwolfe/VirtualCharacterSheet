using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet {

	public static class Data {
		private static Dictionary<string, Item> item = new Dictionary<string, Item>();
		private static Dictionary<string, Character> character = new Dictionary<string, Character>();
		private static Dictionary<string, NPC> npc = new Dictionary<string, NPC>();
		private static Dictionary<string, RawPyScript> py = new Dictionary<string, RawPyScript>();
		private static Dictionary<string, object> pyf = new Dictionary<string, object>();
		private static Dictionary<string, Class> classes = new Dictionary<string, Class>();
		private static Dictionary<string, Feat> feat = new Dictionary<string, Feat>();

		public static Item GetItem(string id) { return item[id]; }
		public static Character GetCharacter(string id) { return character[id]; }
		public static NPC GetNPC(string id) { return npc[id]; }
		public static RawPyScript GetPy(string key) { return py[key]; }
		public static object GetPyF(string key) { return pyf[key]; }
		public static Class GetClass(string key) { return classes[key.ToLower()]; }
		public static Feat GetFeat(string key) { return feat[key.ToLower()]; }

		public static void SetItem(string id, Item i) { item[id] = i; }
		public static void SetCharacter(string id, Character c) { character[id] = c; }
		public static void SetNPC(string id, NPC n) { npc[id] = n; }
		public static void SetPy(string key, RawPyScript src) { py[key] = src; }
		public static void SetPyF(string key, object func) { pyf[key] = func; }
		internal static void SetClass(string key, Class c) { classes[key.ToLower()] = c; }
		public static void SetFeat(string key, Feat f) { feat[key.ToLower()] = f; }

		public static bool HasItem(string id) { return item.ContainsKey(id); }
		public static bool HasCharacter(string id) { return character.ContainsKey(id); }
		public static bool HasNPC(string id) { return npc.ContainsKey(id); }
		public static bool HasPy(string key) { return py.ContainsKey(key); }
		public static bool HasPyF(string key) { return pyf.ContainsKey(key); }
		public static bool HasClass(string key) { return classes.ContainsKey(key.ToLower()); }
		public static bool HasFeat(string key) { return feat.ContainsKey(key.ToLower()); }

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
		public readonly string Identifier;
		public string Description;

		public Item(string id) {
			Identifier = id;

		}

	}

}
