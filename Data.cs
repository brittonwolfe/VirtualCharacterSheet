using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VirtualCharacterSheet {

	public static class Core {
		private static bool allocated = false;
		private static Character currchar = null;

		public static void AllocateConsole() {
			if(!allocated) {
				AllocConsole();
				Console.Title = "VCS Python Command Line";
				allocated = true;
			}
		}
		public static void ShowConsole() { ShowWindow(GetConsoleWindow(),5); }
		public static void HideConsole() { ShowWindow(GetConsoleWindow(),0); }

		public static Character GetCurrentCharacter() { return currchar; }

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		[DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	}

	public static class Data {
		private static Dictionary<uint, Item> item = new Dictionary<uint, Item>();
		private static Dictionary<uint, Character> character = new Dictionary<uint, Character>();
		private static Dictionary<uint, NPC> npc = new Dictionary<uint, NPC>();
		private static Dictionary<string, RawPyScript> py = new Dictionary<string, RawPyScript>();

		public static Item GetItem(uint id) { return item[id]; }
		public static Character GetCharacter(uint id) { return character[id]; }
		public static NPC GetNPC(uint id) { return npc[id]; }
		internal static RawPyScript GetPy(string key) { return py[key]; }

		public static void SetItem(uint id, Item i) { item[id] = i; }
		public static void SetCharacter(uint id, Character c) { character[id] = c; }
		public static void SetNPC(uint id, NPC n) { npc[id] = n; }
		internal static void SetPy(string key, RawPyScript src) { py[key] = src; }

		public static bool HasItem(uint id) { return item.ContainsKey(id); }
		public static bool HasCharacter(uint id) { return character.ContainsKey(id); }
		public static bool HasNPC(uint id) { return npc.ContainsKey(id); }
		internal static bool HasPy(string key) { return py.ContainsKey(key); }

		public static ushort AddItem(Item i) {
			ushort output = 0;
			for(output = 0; output < ushort.MaxValue; output++)
				if(!HasItem(output)) {
					item[output] = i;
					return output;
				}
			throw new Exception("item array is full!");
		}

	}

	public class Die {
		public static Random rng = new Random();

		public static ushort Roll(ushort d) { return (ushort)rng.Next(1,d); }
		public static ushort Rolln(byte n, ushort d) {
			ushort output = 0;
			for(byte x = 0; x < n; x++)
				output += Roll(d);
			return output;
		}

	}

	public class Character {
		public string Name;
		protected byte Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma;
		public short STR { get { return Core.Modifier(Strength); } }
		public short DEX { get { return Core.Modifier(Dexterity); } }
		public short CON { get { return Core.Modifier(Constitution); } }
		public short INT { get { return Core.Modifier(Intelligence); } }
		public short WIS { get { return Core.Modifier(Wisdom); } }
		public short CHA { get { return Core.Modifier(Charisma); } }
		public bool Inspiration { get; private set; }

		public Character() {}

	}

	public class NPC {

	}

	public class Item {
		public string Name;

		public Item() { }

	}

	public class Feature {

	}

	public class Class {
		public readonly string Name;

	}

}
