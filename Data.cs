using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VirtualCharacterSheet {

	public static class Core {
		private static bool allocated = false;
		private static Character? currchar = null;

		public static void AllocateConsole() {
			if(!allocated) {
				AllocConsole();
				allocated = true;
			}
		}
		public static void ShowConsole() { ShowWindow(GetConsoleWindow(),5); }
		public static void HideConsole() { ShowWindow(GetConsoleWindow(),0); }

		public static Character? GetCurrentCharacter() { return currchar; }

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		[DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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



	}

	public class Item {

	}

	public class Feature {

	}

	public class Class {
		public readonly string Name;

	}

}
