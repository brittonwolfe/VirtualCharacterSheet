using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

using VirtualCharacterSheet.IO;
using VirtualCharacterSheet.Forms;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();
		public static dynamic locals = new ExpandoObject();
		public static dynamic homebrew = new ExpandoObject();
		public static dynamic settings = new ExpandoObject();
		public static Dictionary<string, Form> viewers = new Dictionary<string, Form>();
		private static bool Initialized = false;

		public static void Sandbox() {
			init();
			do {
				Console.Write("> ");
				string inp = Console.In.ReadLine();
				if(inp == null)
					continue;
				if(inp == "exit") {
					Core.HideConsole();
					Console.Clear();
					break;
				}
				if(inp.ToLower() == "cls") {
					Console.Clear();
					continue;
				}
				try {
					dynamic tmp = engine.Execute(inp);
					if(settings.ShowOutput && tmp != null)
						Console.WriteLine(tmp);
				}
				catch(Exception e) {
					Console.WriteLine(e);
					/*Console.Write("Continue (Y/N)? ");
					var choice = Console.ReadKey(true);
					if(choice.Key != ConsoleKey.Y)
						break;*/
					Console.WriteLine();
				}
			} while(true);
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

# region global variables
			SetGlobal("local", locals);
			SetGlobal("brew", homebrew);
			SetGlobal("_setting", settings);
			SetGlobal("_viewer", viewers);
# endregion

# region global functions
			SetGlobal("roll", new Func<ushort, uint>(Die.Roll));
			SetGlobal("rolln", new Func<ushort, ushort, uint>(Die.Rolln));
			SetGlobal("mod", new Func<byte, short>(Core.Modifier));
# endregion

# region casts
			SetGlobal("byte", new Func<object, byte>((object o) => { return (byte)o; }));
			SetGlobal("short", new Func<object, short>((object o) => { return (short)o; }));
# endregion

# region helper object functions
			SetGlobal("new_mod", new Func<short, Modifier>((short m) => { return new Modifier(m); }));
			SetGlobal("new_roll", new Func<ushort, ushort, Roll>((ushort n, ushort d) => { return new Roll(n, d); }));
# endregion

# region accessors
			SetGlobal("_brew", new Func<string, Brew>(Data.GetBrew));
			SetGlobal("_c", new Func<string, Character>(Data.GetCharacter));
			SetGlobal("_class", new Func<string, Class>(Data.GetClass));
			SetGlobal("_feat", new Func<string, Feat>(Data.GetFeat));
			SetGlobal("_i", new Func<string, Item>(Data.GetItem));
			SetGlobal("_n", new Func<string, NPC>(Data.GetNPC));
			SetGlobal("_py", new Func<string, RawPyScript>(Data.GetPy));
			SetGlobal("_pyf", new Func<string, object>(Data.GetPyF));
# endregion

# region initializers and instantiators
			SetGlobal("def_c", new Func<string, string, PlayerCharacter>((string c, string p) => { return new PlayerCharacter(c, p); }));
			SetGlobal("def_class", new Func<string, Class>((string n) => { return new Class(n); }));
			SetGlobal("def_feat", new Func<string, Feat>((string n) => { return new Feat(n); }));
			SetGlobal("def_i", new Func<string, Item>((string n) => { return new Item(n); }));
			#endregion

# region checkers
			SetGlobal("has_c", new Func<string, bool>(Data.HasCharacter));
			SetGlobal("has_class", new Func<string, bool>(Data.HasClass));
			SetGlobal("has_feat", new Func<string, bool>(Data.HasFeat));
			SetGlobal("has_i", new Func<string, bool>(Data.HasItem));
			SetGlobal("has_n", new Func<string, bool>(Data.HasNPC));
			SetGlobal("has_py", new Func<string, bool>(Data.HasPy));
			SetGlobal("has_pyf", new Func<string, bool>(Data.HasPyF));
# endregion

# region viewers
			SetGlobal("view", new Action<object>(ViewObject));
# endregion

# region metaprogrammatical functions
			SetGlobal("set_pyf", new Action<string, object>(Data.SetPyF));
			SetGlobal("edit_py", new Action<string>(CodeScript));
# endregion

# region finalize
			settings.ShowOutput = false;
# endregion

			Initialized = true;
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return engine.GetBuiltinModule().GetVariable(n); }
		internal static void Remove(dynamic obj, string key) { ((IDictionary<string, object>)obj).Remove(key); }

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
			Console.Title = "You should install Visual Studio Code";
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

		private static void ViewObject(object obj) {
			Thread showthread = null;
			switch(obj) {
			case PlayerCharacter player:
				if(viewers.ContainsKey(player.Identifier))
					viewers[player.Identifier].Focus();
				else {
					showthread = new Thread(() => {
						CharacterSheet window = new CharacterSheet();
						window.Show();
						window.SetCharacter(player);
					});
				}
				break;
			default:
				Console.WriteLine(obj.GetType().ToString() + " does not have an associated Form!");
				break;
			}
			if(showthread != null) {
				showthread.SetApartmentState(ApartmentState.STA);
				showthread.IsBackground = true;
				showthread.Start();
			}
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
			try { Scripting.engine.ExecuteFile(File.Path); }
			catch { }
			finally { Scripting.Remove(Scripting.locals, "Path"); }
		}

	}

	public abstract class Script {

		public abstract void Run();
		public void Run(dynamic arg) {
			Scripting.locals.arg = arg;
			Run();
			Scripting.Remove(Scripting.locals, "arg");
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