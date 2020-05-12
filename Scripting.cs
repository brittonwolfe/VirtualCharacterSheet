using System;
using System.Dynamic;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();
		public static dynamic locals = new ExpandoObject();

		public static void Sandbox() {
			Core.AllocateConsole();
			init();
			do {
				Console.Write("> ");
				string inp = Console.In.ReadLine();
				if(inp == null)
					continue;
				if(inp == "exit")
					break;
				if(inp.ToLower() == "help") {
					Help();
					continue;
				}
				try { engine.Execute(inp); }
				catch(Exception e) {
					Console.WriteLine(e);
					Console.Write("Continue (Y/N)? ");
					var choice = Console.ReadKey(true);
					if(choice.Key != ConsoleKey.Y)
						break;
					Console.WriteLine();
				}
			} while(true);
			Core.HideConsole();
		}

		private static void init() {
			engine.Execute("import clr");

			Action<string> HelpFunc = GetHelp;
			Func<ushort, uint> RollFunc = Die.Roll;
			Func<byte, ushort, uint> RollnFunc = Die.Rolln;
			Func<byte, short> ModFunc = Core.Modifier;

			Func<Character> GetCCharFunc = Core.GetCurrentCharacter;
			Func<uint, Item> GetItem = Data.GetItem;
			Action<string, object> SetScriptFFunc = SetScriptF;
			Func<uint, Character> GetCharacter = Data.GetCharacter;
			Func<string, Class> GetClass = Data.GetClass;
			Func<uint, NPC> GetNPC = Data.GetNPC;
			Func<string, RawPyScript> GetPy = Data.GetPy;
			Func<string, object> GetPyF = Data.GetPyF;

			Action<string> CreateItemFunc = CreateItem;
			Func<string, Class> DefClassF = DefineClass;

			Action<string> OpenVSCode = CodeScript;
			Action<string> RunScriptFunc = RunScript;

			SetGlobal("local", locals);

			SetGlobal("help", HelpFunc);
			SetGlobal("roll", RollFunc);
			SetGlobal("rolln", RollnFunc);
			SetGlobal("mod", ModFunc);

			SetGlobal("getopenchar", GetCCharFunc);
			SetGlobal("_i", GetItem);
			SetGlobal("_c", GetCharacter);
			SetGlobal("_class", GetClass);
			SetGlobal("_n", GetNPC);
			SetGlobal("_py", GetPy);
			SetGlobal("_pyf", GetPyF);

			SetGlobal("def_class", DefClassF);
			SetGlobal("new_i", CreateItemFunc);

			SetGlobal("set_pyf", SetScriptFFunc);
			
			SetGlobal("edit_py", OpenVSCode);
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return engine.GetBuiltinModule().GetVariable(n); }

		public static void Help() {
			Console.WriteLine("roll(d)\t\trolln(n,d)\t\tmod(s)\t\tgetopenchar()");
			Console.WriteLine("_i(id)\t\t_c(id)\t\t_n(id)\t\t_py(key)");
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
			case "exit":
				Console.WriteLine("exit\n\texits the Python console");
				break;
			case "type":
			case "types":
				Console.WriteLine("-- Data Types --");
				Console.WriteLine("_c(id)\tCharacter\n\tUses a numeric key to return a reference to a character.");
				Console.WriteLine("_i(id)\tItem\n\tUses a numeric key to return a reference to an item.");
				Console.WriteLine("_n(id)\tNPC\n\tUses a numeric key to return a reference to an NPC.");
				Console.WriteLine("_py(key)\tPython Script\n\tUses a string key to return a reference to a dynamically loaded script.");
				Console.WriteLine("_pyf(key)\tPython Function\n\tUses a string key to return a reference to a python function.");
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
			uint id = Data.AddItem(i);
			Console.WriteLine("Created new item \"" + n + "\" at _i(" + id + ")");
		}

		public static Class DefineClass(string n) { return new Class(n); }

		private static void CodeScript(string key) {
			IO.File temp = FileLoad.GetTempFile("vcs_py_" + key + ".py");
			if(Data.HasPy(key))
				temp.WriteText(Data.GetPy(key).src);
			else
				temp.WriteText("");
			var process = new System.Diagnostics.Process();
			var info = new System.Diagnostics.ProcessStartInfo();
			info.FileName = "CMD.exe";
			info.Arguments = ("/C code \"" + temp.Path + "\"");
			process.StartInfo = info;
			try { process.Start(); }
			catch { ScriptEditor(key); }
			Console.WriteLine("Press enter to resume...");
			Console.ReadLine();
			Data.SetPy(key, new RawPyScript(temp.ReadText()));
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

		private static void SetScriptF(string key, object f) { Data.SetPyF(key, f); }

	}

	public class PyFScript : Script {
		private dynamic Object;

		public PyFScript(dynamic o) { Object = o; }

		public override void Run() { Object(); }

		public static PyFScript FromKey(string key) { return new PyFScript(Data.GetPyF(key)); }

	}

	public class RawPyScript : Script {
		internal string src;

		internal RawPyScript(string py) { src = py; }

		public override void Run() { Scripting.engine.Execute(src); }

	}

	public class FileScript : Script {
		public IO.File File;

		public FileScript(IO.File file) {
			File = file;
		}

		public override void Run() { }

	}

	public abstract class Script {

		public abstract void Run();
		public void Run(dynamic arg) {
			Scripting.locals.arg = arg;
			Run();
			Scripting.locals.arg = null;
		}

	}

}