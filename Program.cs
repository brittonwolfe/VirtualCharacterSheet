using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace VirtualCharacterSheet {

	static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Core.HideConsole();
			new Thread(() => { Scripting.Sandbox(); }).Start();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Forms.CharacterSheet());
		}
	}

	public static class Core {
		private static bool allocated = false;
		private static PlayerCharacter currchar = null;

		public static void AllocateConsole() {
			if (!allocated) {
				AllocConsole();
				Console.Title = "VCS Python Command Line";
				allocated = true;
			}
		}
		public static void ShowConsole() { ShowWindow(GetConsoleWindow(), 5); }
		public static void HideConsole() { ShowWindow(GetConsoleWindow(), 0); }

		public static PlayerCharacter GetCurrentCharacter() { return currchar; }

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		[DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	}

}
