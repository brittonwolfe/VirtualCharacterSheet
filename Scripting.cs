using System;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();

		public static void Sandbox() {
			Core.AllocateConsole();
			init();
			do {
				string inp = Console.In.ReadLine();
				if(inp == "quit")
					break;
				try { engine.Execute(inp); }
				catch(Exception e) { Console.WriteLine(e); break; }
			} while(true);
			Core.HideConsole();
		}

		private static void init() {
			engine.Execute("import clr");
			Func<ushort,ushort> RollFunc = Die.Roll;
			Func<byte,ushort,ushort> RollnFunc = Die.Rolln;
			Func<byte,short> ModFunc = Core.Modifier;
			SetGlobal("roll", RollFunc);
			SetGlobal("rolln", RollnFunc);
			SetGlobal("mod", ModFunc);
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return engine.GetBuiltinModule().GetVariable(n); }

	}

	public class MacroRoll {
		private readonly ScriptSource src;

		public MacroRoll(ScriptSource pysrc) { src = pysrc; }

		public ushort Roll() {
			return 0;
		}

	}

}