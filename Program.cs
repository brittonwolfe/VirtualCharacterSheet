using IronPython.Compiler.Ast;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VirtualCharacterSheet {

	static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Console.Title = "VCS Console";
			Core.HideConsole();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Forms.Splash());
		}
	}

	public static class Core {
		private static bool allocated = false;
		private static PlayerCharacter currchar = null;
		private static Thread SandboxThread = new Thread(() => { Scripting.Sandbox(); });
		internal static bool SandboxAwaits = false;

		static Core() {
			SandboxThread.SetApartmentState(ApartmentState.STA);
		}

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

		public static void StartSandbox() {
			if (!SandboxThread.IsAlive)
				SandboxThread.Start();
		}

	}

}
