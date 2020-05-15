using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VirtualCharacterSheet {

	static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}

	public static class Core {
		private static bool allocated = false;
		private static Character currchar = null;

		public static void AllocateConsole() {
			if (!allocated) {
				AllocConsole();
				Console.Title = "VCS Python Command Line";
				allocated = true;
			}
		}
		public static void ShowConsole() { ShowWindow(GetConsoleWindow(), 5); }
		public static void HideConsole() { ShowWindow(GetConsoleWindow(), 0); }

		public static Character GetCurrentCharacter() { return currchar; }

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		[DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	}

}
