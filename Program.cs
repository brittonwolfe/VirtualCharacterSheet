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
		internal static bool SandboxAwaits = false;
		internal static List<IO.File> temp_scripts = new List<IO.File>();

		public static PlayerCharacter GetCurrentCharacter() { return currchar; }

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		public static void StartSandbox() { Scripting.Sandbox(); }

	}

}
