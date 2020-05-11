using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using VirtualCharacterSheet.Exceptions;

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
		private static Dictionary<string, object> pyf = new Dictionary<string, object>();
		private static Dictionary<string, Class> classes = new Dictionary<string, Class>();

		public static Item GetItem(uint id) { return item[id]; }
		public static Character GetCharacter(uint id) { return character[id]; }
		public static NPC GetNPC(uint id) { return npc[id]; }
		public static RawPyScript GetPy(string key) { return py[key]; }
		public static object GetPyF(string key) { return pyf[key]; }
		public static Class GetClass(string key) { return classes[key.ToLower()]; }

		public static void SetItem(uint id, Item i) { item[id] = i; }
		public static void SetCharacter(uint id, Character c) { character[id] = c; }
		public static void SetNPC(uint id, NPC n) { npc[id] = n; }
		public static void SetPy(string key, RawPyScript src) { py[key] = src; }
		public static void SetPyF(string key, object func) { pyf[key] = func; }
		internal static void SetClass(string key, Class c) { classes[key.ToLower()] = c; }

		public static bool HasItem(uint id) { return item.ContainsKey(id); }
		public static bool HasCharacter(uint id) { return character.ContainsKey(id); }
		public static bool HasNPC(uint id) { return npc.ContainsKey(id); }
		public static bool HasPy(string key) { return py.ContainsKey(key); }
		public static bool HasPyF(string key) { return pyf.ContainsKey(key); }
		public static bool HasClass(string key) { return classes.ContainsKey(key.ToLower()); }

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
		private Script Behavior;

		public Item() { }
		public Item(string name) {
			Name = name;
		}
		public Item(string name, Script behavior) {
			Name = name;
			Behavior = behavior;
		}

		public void SetBehavior(Script s) { Behavior = s; }
		public void SetBehavior(RawPyScript s) { SetBehavior(new Script(s)); }
		public void DoBehavior() { Behavior.Run(); }

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
			if(Data.HasClass(n))
				throw new ClassAlreadyExistsException(Name);
			Data.SetClass(Name, this);
			Saves = new bool[6];
		}

	}

}
