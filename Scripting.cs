using System;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();

		public static void Sandbox() {
			Core.AllocateConsole();
			do {
				string inp = Console.In.ReadLine();
				if(inp == "quit")
					break;
				try { engine.Execute(inp); }
				catch { Console.WriteLine("An error happen :("); break; }
			} while(true);
			Core.HideConsole();
		}

		private static void init() {
		}

	}

	public class MacroRoll {
		private readonly ScriptSource src;

		public MacroRoll(ScriptSource pysrc) { src = pysrc; }

		public ushort Roll() {
			return 0;
		}

	}

}