using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

using VirtualCharacterSheet.IO;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();
		public static dynamic locals = new ExpandoObject();
		public static dynamic homebrew = new ExpandoObject();
		private static bool Initialized = false;

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

		public static void Brew(FileScript src) {
			init();

			Func<string, Brew> defBrew = (string n) => { return new Brew(n); };

			homebrew.def_brew = defBrew;
			homebrew.Path = src.File.Directory;

			src.Run();

			Remove(homebrew, "def_brew");
			Remove(homebrew, "Path");
		}

		private static void init() {
			if(Initialized)
				return;
			engine.Execute("import clr");

			Action<string> HelpFunc = GetHelp;
			Func<ushort, uint> RollFunc = Die.Roll;
			Func<ushort, ushort, uint> RollnFunc = Die.Rolln;
			Func<byte, short> ModFunc = Core.Modifier;

			Func<PlayerCharacter> GetCCharFunc = Core.GetCurrentCharacter;
			Func<string, Brew> GetBrew = Data.GetBrew;
			Func<string, PlayerCharacter> GetCharacter = Data.GetCharacter;
			Func<string, Class> GetClass = Data.GetClass;
			Func<string, Feat> GetFeat = Data.GetFeat;
			Func<string, Item> GetItem = Data.GetItem;
			Func<string, NPC> GetNPC = Data.GetNPC;
			Func<string, RawPyScript> GetPy = Data.GetPy;
			Func<string, object> GetPyF = Data.GetPyF;

			Func<string, string, PlayerCharacter> DefCharF = (string c, string p) => { return new PlayerCharacter(c, p); };
			Func<string, Class> DefClassF = (string n) => { return new Class(n); };
			Func<string, Feat> DefFeatF = (string n) => { return new Feat(n); };
			Func<string, Item> CreateItemFunc = (string n) => {return new Item(n); };
			Action<string, object> SetScriptFFunc = Data.SetPyF;

			Func<string, bool> HasCharFunc = Data.HasCharacter;
			Func<string, bool> HasClassFunc = Data.HasClass;
			Func<string, bool> HasFeatFunc = Data.HasFeat;
			Func<string, bool> HasItemFunc = Data.HasItem;
			Func<string, bool> HasNPCFunc = Data.HasNPC;
			Func<string, bool> HasPyFunc = Data.HasPy;
			Func<string, bool> HasPyFFunc = Data.HasPyF;

			Func<short, Modifier> NewModifier = (short m) => { return new Modifier(m); };
			Func<ushort, ushort, Roll> NewRoll = (ushort n, ushort d) => { return new Roll(n, d); };

			Action<string> OpenVSCode = CodeScript;

			//global variables
			SetGlobal("local", locals);
			SetGlobal("brew", homebrew);

			//global functions
			SetGlobal("help", HelpFunc);
			SetGlobal("roll", RollFunc);
			SetGlobal("rolln", RollnFunc);
			SetGlobal("mod", ModFunc);

			//helper object functions
			SetGlobal("new_mod", NewModifier);
			SetGlobal("new_roll", NewRoll);

			//accessors
			SetGlobal("getopenchar", GetCCharFunc);
			SetGlobal("_brew", GetBrew);
			SetGlobal("_c", GetCharacter);
			SetGlobal("_class", GetClass);
			SetGlobal("_feat", GetFeat);
			SetGlobal("_i", GetItem);
			SetGlobal("_n", GetNPC);
			SetGlobal("_py", GetPy);
			SetGlobal("_pyf", GetPyF);

			//initializers and instantiators
			SetGlobal("def_c", DefCharF);
			SetGlobal("def_class", DefClassF);
			SetGlobal("def_feat", DefFeatF);
			SetGlobal("def_i", CreateItemFunc);

			//checkers
			SetGlobal("has_c", HasCharFunc);
			SetGlobal("has_class", HasClassFunc);
			SetGlobal("has_feat", HasFeatFunc);
			SetGlobal("has_i", HasItemFunc);
			SetGlobal("has_n", HasNPCFunc);
			SetGlobal("has_py", HasPyFunc);
			SetGlobal("has_pyf", HasPyFFunc);

			//metaprogrammatical functions
			SetGlobal("set_pyf", SetScriptFFunc);
			SetGlobal("edit_py", OpenVSCode);

			Initialized = true;
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return engine.GetBuiltinModule().GetVariable(n); }
		internal static void Remove(dynamic obj, string key) { ((IDictionary<string, object>)obj).Remove(key); }

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

		public static Modifier CreateModifier() { return new Modifier(); }
		public static Modifier CreateModifier(short m) { return new Modifier(m); }
		public static Modifier CreateModifier(Script s) { return new Modifier(s); }

		private static void CodeScript(string key) {
			File temp = FileLoad.GetTempFile("vcs_py_" + key + ".py");
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
			Console.WriteLine("Press enter to resume after editing...");
			Console.ReadLine();
			Data.SetPy(key, new RawPyScript(temp.ReadText()));
			System.IO.File.Delete(temp.Path);
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
		public File File;

		public FileScript(File file) {
			File = file;
		}

		public override void Run() {
			Scripting.locals.Path = File;
			try{ Scripting.engine.ExecuteFile(File.Path); }
			catch { }
			finally { Scripting.Remove(Scripting.locals, "Path"); }
		}

	}

	public abstract class Script {

		public abstract void Run();
		public void Run(dynamic arg) {
			Scripting.locals.arg = arg;
			Run();
			Scripting.locals.arg = null;
		}

	}

	public abstract class ScriptedObject {
		protected Script Behavior;

		public void SetBehavior(Script script) { Behavior = script; }

		public void DoBehavior() {
			Scripting.locals.This = this;
			try{ Behavior.Run(); }
			catch(Exception e) {
				Core.ShowConsole();
				Console.WriteLine("An error occurred in behavior of " + this);
				Console.WriteLine(e);
			} finally { Scripting.Remove(Scripting.locals, "This"); }
		}
		public void DoBehavior(dynamic arg) { Behavior.Run(arg); }

	}

}