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
		private static Dictionary<ushort,Item> item = new Dictionary<ushort, Item>();
		private static Dictionary<ushort,Character> character = new Dictionary<ushort, Character>();
		private static Dictionary<ushort,NPC> npc = new Dictionary<ushort, NPC>();

		public static Item GetItem(ushort id) { return item[id]; }
		public static Character GetCharacter(ushort id) { return character[id]; }
		public static NPC GetNPC(ushort id) { return npc[id]; }

		public static void SetItem(ushort id, Item i) { item[id] = i; }
		public static void SetCharacter(ushort id, Character c) { character[id] = c; }
		public static void SetNPC(ushort id, NPC n) { npc[id] = n; }

		public static bool HasItem(ushort id) { return item.ContainsKey(id); }
		public static bool HasCharacter(ushort id) { return character.ContainsKey(id); }
		public static bool HasNPC(ushort id) { return npc.ContainsKey(id); }

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
