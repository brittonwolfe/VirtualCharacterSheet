using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

using Python.Runtime;

using VirtualCharacterSheet.IO;
using VirtualCharacterSheet.Event;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static Py.GILState engine = Py.GIL();
		internal static PyScope
			MainScope = null,
			BrewScope = null,
			ShellScope = null,
			NetScope = null;
		internal static dynamic locals = new ExpandoObject();
		internal static dynamic homebrew = new ExpandoObject();
		internal static dynamic settings = new ExpandoObject();
		private static bool Initialized = false;

		static Scripting() {
			Py.Import("clr");
			Py.Eval("clr.AddReference(\"vcs\")");
# region scope init
			MainScope = Py.CreateScope("scope_main");
			BrewScope = Py.CreateScope("scope_brew");
			ShellScope = Py.CreateScope("scope_shell");
			NetScope = Py.CreateScope("scope_net");
#endregion

		}

		public static void Sandbox() {
			DoFile("core/shell.py", ShellScope);
			try { ShellScope.Variables["shell"](); }
			catch(Exception e) {
				Console.WriteLine(e);
			}
		}

		internal static dynamic DoFile(string path, PyScope scope = null) {
			var script = engine.Compile("", FileLoad.WorkingDirectory().Get(path).Path);
			var output = (scope ?? MainScope).Execute(script);
			return output;
		}
		internal static dynamic DoFile(File file, PyScope scope = null) {
			var script = engine.Compile("", file.Path);
			var output = (scope ?? MainScope).Execute(script);
			return output;
		}
		internal static dynamic DoString(string src, PyScope scope = null) {
			var output = (scope ?? MainScope).Eval(src);
			return output;
		}

		public static void Brew(FileScript src) {

			ICollection<string> paths = engine.GetSearchPaths();
			var dir = src.File.Directory;
			var parent = src.File.Directory.Parent();
			paths.Add(dir.Path);
			if(!paths.Contains(parent.Path))
				paths.Add(parent.Path);
			engine.SetSearchPaths(paths);

			homebrew.def_brew = new Func<string, Brew>((string n) => { return new Brew(n); });
			homebrew.Path = src.File.Directory;

			try { DoFile(src.File.Path, BrewScope); }
			catch(Exception e) {
				Console.WriteLine(e);
				if(Data.GetConfig("main", "prefer_cli")) {
					Console.Write("Press any key to continue...");
					Console.ReadLine();
				}
			}

			paths.Remove(dir.Path);
			Remove(homebrew, "def_brew");
			Remove(homebrew, "Path");
		}

		internal static void Init() {
			if(Initialized)
				return;
# region python engine
			
# endregion

# region configuration
			Py.Eval("import core.config");
			Data.Config = ShellScope.GetVariable("__config__");
#endregion

# region globals setup
			homebrew.load = new Action<string>((string s) => Brew(new FileScript(new File(s))));
# endregion

# region casts
			SetGlobal("byte", new Func<object, byte>((object num) => {
				if(num is string)
					return byte.Parse(num as string);
				return (byte)num;
			}));
			SetGlobal("short", new Func<object, short>((object num) => {
				if(num is string)
					return short.Parse(num as string);
				return (short)num;
			}));
# endregion

# region finalize
			settings.ShowOutput = false;
# endregion

			Initialized = true;
		}

		public static T[] PyArray<T>(dynamic list) {
			T[] output = new T[list.__len__()];
			uint n = 0;
			foreach(T item in list)
				output[n++] = item;
			return output;
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return null; }//engine.(n); }
		internal static void Remove(dynamic obj, string key) { ((IDictionary<string, object>)obj).Remove(key); }

		internal static void CodeScript(string key) {
			File temp = FileLoad.GetTempFile("vcs_py_" + key + ".py");
			if(Data.HasPy(key))
				temp.WriteText(Data.GetPy(key).src);
			else
				temp.WriteText("");
			string command = (string)(Data.GetConfig("main", "editor") ?? "code");
			try { Core.Run($"{command} \"{temp.Path}\""); }
			catch { ScriptEditor(key); }
			Console.WriteLine("Press enter to resume after editing...");
			Console.ReadLine();
			var script = new RawPyScript(temp.ReadText());
			script.AddTempFile(temp);
			Data.SetPy(key, script);
			Core.temp_files.Add(temp);
		}
		private static void ScriptEditor(string key) {
			bool wantsbreak = false;
			Console.Clear();
			Console.Title = "You can set an editor in your config file";
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

	public static class Util {
# region globals
		public static dynamic local { get => Scripting.locals; }
		public static dynamic brew { get => Scripting.homebrew; }
		public static dynamic _setting { get => Scripting.settings; }

		public static string readl(string prompt) {
			Console.Write(prompt);
			return Console.In.ReadLine();
		}
		public static uint roll(ushort sides) { return Die.Roll(sides); }
		public static uint rolln(ushort sides, ushort count) { return Die.Rolln(sides, count); }
# endregion

# region viewers
		public static void view(object obj, Brew brew = null) { Core.View(obj, brew); }
# endregion

# region accessors
		public static Brew _brew(string id) { return Data.GetBrew(id); }
		public static Character _c(string id) { return Data.GetCharacter(id); }
		public static Class _class(string id) { return Data.GetClass(id); }
		public static Item _i(string id) { return Data.GetItem(id); }
		public static NPC _n(string id) { return Data.GetNPC(id); }
		public static RawPyScript _py(string id) { return Data.GetPy(id); }
		public static dynamic _pyf(string id) { return Data.GetPyF(id); }
# endregion

# region checkers
		public static bool has_brew(string id) { return Data.HasBrew(id); }
		public static bool has_c(string id) { return Data.HasCharacter(id); }
		public static bool has_class(string id) { return Data.HasClass(id); }
		public static bool has_feat(string id) { return Data.HasFeat(id); }
		public static bool has_i(string id) { return Data.HasItem(id); }
		public static bool has_n(string id) { return Data.HasNPC(id); }
		public static bool has_py(string id) { return Data.HasPy(id); }
		public static bool has_pyf(string id) { return Data.HasPyF(id); }
# endregion

# region initializers
		public static PlayerCharacter def_c(string name, string player) { return new PlayerCharacter(name, player); }
		public static Class def_class(string name) { return new Class(name); }
		public static Feat def_feat(string name) { return new Feat(name); }
		public static Item def_i(string name) { return new Item(name); }
# endregion

# region metafunction
		public static void set_pyf(string id, dynamic func) { Data.SetPyF(id, func); }
		public static void set_py(string id, RawPyScript script) { Data.SetPy(id, script); }
# endregion

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

		public void AddTempFile(File file) { Meta.Add("tempfile", file); }

		public override void Run() { Scripting.DoString(src); }

	}

	public class FileScript : Script {
		public File File;

		public FileScript(File file) {
			File = file;
		}

		public override void Run() {
			Scripting.locals.Path = File;
			try { Scripting.DoFile(File); }
			catch(Exception e) { Console.WriteLine(e); }
			finally { Scripting.Remove(Scripting.locals, "Path"); }
		}

	}

	public abstract class Script : DynamicObject {
		protected Dictionary<string, object> Meta = new Dictionary<string, object>();

		public abstract void Run();
		public void Run(dynamic arg) {
			Scripting.locals.arg = arg;
			Run();
			Scripting.Remove(Scripting.locals, "arg");
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result) {
			if(!Meta.ContainsKey(binder.Name))
				return base.TryGetMember(binder, out result);
			result = Meta[binder.Name];
			return true;
		}

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result) {
			result = null;
			if(args.Length > 0)
				Run(args);
			else
				Run();
			return true;
		}

	}

	public abstract class ScriptedObject : DynamicObject {
		protected dynamic Behavior;
		public dynamic Info, Meta;

		protected ScriptedObject() {
			Behavior = new DynamicBehaviorSet(this);
			Info = new ExpandoObject();
			Meta = new ExpandoObject();
		}

		public bool HasInfo(string name) { return ((IDictionary)Info).Contains(name); }
		public bool HasMeta(string name) { return ((IDictionary)Meta).Contains(name); }
		public bool HasBehavior(string name) { return Behavior.Contains(name); }

		public void AddBehavior(string name, dynamic obj) { Behavior.Add(name, obj); }

		public dynamic DoBehavior(string name) { return Behavior.Do(name); }

		public override bool TryGetMember(GetMemberBinder binder, out object result) {
			if (base.TryGetMember(binder, out result))
				return true;
			else if (HasBehavior(binder.Name))
				return Behavior.TryGetMember(binder, out result);
			result = null;
			return false;
		}

	}

	public abstract class ComplexObject {
		public dynamic Info, Meta;
		protected DynamicBehaviorSet Behavior;

	}

}