using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCharacterSheet {

	class Program {

		static void Main(string[] args) {
			Console.Title = "Virtual Character Sheet";
			Console.OutputEncoding = Encoding.Unicode;
			Console.WriteLine("VCS TUI");
			Scripting.init();

			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

		}

		private static void OnExit(object sender, EventArgs e) {
			foreach(IO.File file in Core.temp_scripts)
				System.IO.File.Delete(file.Path);
		}

	}

	public static class Core {
		private static bool allocated = false;
		private static PlayerCharacter currchar = null;
		private static Thread SandboxThread = new Thread(() => { Scripting.Sandbox(); });
		internal static bool SandboxAwaits = false;
		internal static List<IO.File> temp_scripts = new List<IO.File>();

		static Core() {
			SandboxThread.SetApartmentState(ApartmentState.STA);
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


}
