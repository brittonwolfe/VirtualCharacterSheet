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
				Console.Write("> ");
				string inp = Console.In.ReadLine();
				if(inp == "quit")
					break;
				if(inp.ToLower() == "help") {
					Help();
					continue;
				}
				try { engine.Execute(inp); }
				catch(Exception e) { Console.WriteLine(e); Console.ReadLine(); break; }
			} while(true);
			Core.HideConsole();
		}

		private static void init() {
			engine.Execute("import clr");

			Action<string> HelpFunc = GetHelp;
			Func<ushort, ushort> RollFunc = Die.Roll;
			Func<byte, ushort, ushort> RollnFunc = Die.Rolln;
			Func<byte, short> ModFunc = Core.Modifier;
			Func<Character> GetCCharFunc = Core.GetCurrentCharacter;
			Func<uint,Item> GetItem = Data.GetItem;
			Action<string> CreateItemFunc = CreateItem;
			Func<uint,Character> GetCharacter = Data.GetCharacter;
			Func<uint,NPC> GetNPC = Data.GetNPC;
			Func<string,RawPyScript> GetPy = Data.GetPy;
			Action<string> OpenScriptEditor = ScriptEditor;
			Action<string> RunScriptFunc = RunScript;

			SetGlobal("help", HelpFunc);
			SetGlobal("roll", RollFunc);
			SetGlobal("rolln", RollnFunc);
			SetGlobal("mod", ModFunc);

			SetGlobal("getopenchar", GetCCharFunc);
			SetGlobal("_i", GetItem);
			SetGlobal("_c", GetCharacter);
			SetGlobal("_n", GetNPC);
			SetGlobal("_py", GetPy);

			SetGlobal("new_i", CreateItemFunc);
			
			SetGlobal("edit_script", OpenScriptEditor);
			SetGlobal("do_script", RunScriptFunc);
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return engine.GetBuiltinModule().GetVariable(n); }

		public static void Help() {
			Console.WriteLine("roll(d)\t\trolln(n,d)\t\tmod(s)\t\tgetopenchar()");
			Console.WriteLine("_i(id)\t\t_c(id)\t\t_n(id)\t\t_py(key)");
			Console.WriteLine("edit_script(key)\t\tdo_script(key)");
		}
		public static void GetHelp(string c) {
			Console.WriteLine();
			switch(c.ToLower()) {
			case "roll":
				Console.WriteLine("roll(d)\n\trolls a [d]-sided die");
				break;
			case "rolln":
				Console.WriteLine("rolln(n,d)\n\trolls a [d]-sided die [n] times.");
				break;
			case "mod":
				Console.WriteLine("mod(s)\n\tgets the modifier for a stat with value [s].");
				break;
			case "getopenchar":
				Console.WriteLine("getopenchar()\n\treturns the character whose sheet is currently open, or null if no such character exists.");
				break;
			case "help":
				Console.WriteLine("help(c)\n\tI take it you figured it out, huh?");
				break;
			case "quit":
				Console.WriteLine("quit\n\texits the Python console");
				break;
			case "":
				Help();
				break;
			default:
				Console.WriteLine("Found no documentation for \"" + c + "\"");
				break;
			}
			Console.WriteLine();
		}

		public static void CreateItem(string n) {
			Item i = new Item();
			i.Name = n;
			ushort id = Data.AddItem(i);
			Console.WriteLine("Created new item \"" + n + "\" at _i(" + id + ")");
		}

		private static void ScriptEditor(string key) {
			bool wantsbreak = false;
			Console.Clear();
			string output = "";
			while(true) {
				string nl = Console.ReadLine();
				if(nl == "")
					if(wantsbreak)
						break;
					else
						wantsbreak = true;
				else
					wantsbreak = false;
				output += nl + "\n";
			}
			Data.SetPy(key, new RawPyScript(output));
			Console.Clear();
			Console.WriteLine("script created at _py(\"" + key + "\")");
		}
		private static void RunScript(string key) { Data.GetPy(key).Run(); }

	}

	public class RawPyScript {
		private string src;

		internal RawPyScript(string py) { src = py; }

		public void Run() { Scripting.engine.Execute(src); }

	}

	public class Script {
		private bool isFile;
		private IO.Path path;
		private RawPyScript raw;

		internal Script(RawPyScript py) {
			isFile = false;
			raw = py;
		}
		public Script(IO.Path fp) {
			isFile = true;
			path = fp;
		}

		internal void Set(IO.Path p) { path = p; }
		internal void Set(RawPyScript py) { raw = py; }

		public void Run() {
			if(isFile)
				Scripting.engine.ExecuteFile(path.ToString());
			else
				raw.Run();
		}

	}

}