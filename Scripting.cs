using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;
using PyList = IronPython.Runtime.List;

using VirtualCharacterSheet.IO;
using VirtualCharacterSheet.Event;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();
		internal static ScriptScope
			MainScope = null,
			BrewScope = null,
			ShellScope = null,
			NetScope = null;
		internal static dynamic locals = new ExpandoObject();
		internal static dynamic homebrew = new ExpandoObject();
		internal static dynamic settings = new ExpandoObject();
		private static bool Initialized = false;

		static Scripting() {
# region main_scope
			MainScope = engine.CreateScope();
			MainScope.SetVariable("local", locals);
			MainScope.SetVariable("brew", homebrew);
# endregion

# region brew_scope
			BrewScope = engine.CreateScope();
			BrewScope.SetVariable("brew", homebrew);
# endregion

# region shell_scope
			ShellScope = engine.CreateScope();
			ShellScope.SetVariable("brew", homebrew);
			ShellScope.SetVariable("_setting", settings);
# endregion

# region netapi_scope
			NetScope = engine.CreateScope();
#endregion
		}

		public static void Sandbox() {
			engine.ExecuteFile(FileLoad.WorkingDirectory().Get(@"core/io.py").Path, ShellScope);
			engine.ExecuteFile(FileLoad.WorkingDirectory().Get(@"core/shell.py").Path, ShellScope);
			try { ShellScope.GetVariable("shell")(); }
			catch(Exception e) {
				Console.WriteLine(e);
			}
		}

		public static void Brew(FileScript src) {

			ICollection<string> paths = engine.GetSearchPaths();
			paths.Add(src.File.Directory.Path);
			engine.SetSearchPaths(paths);

			homebrew.def_brew = new Func<string, Brew>((string n) => { return new Brew(n); });

			homebrew.Path = src.File.Directory;
			homebrew.import_absolute = new Func<string, string, dynamic>(
				(subpath, item) => {
					engine.ExecuteFile(src.File.Directory.Get(subpath).Path, BrewScope);
					return BrewScope.GetVariable(item);
				}
			);

			try { engine.ExecuteFile(src.File.Path, BrewScope); }
			catch(Exception e) { Console.WriteLine(e); }

			Remove(homebrew, "def_brew");
			Remove(homebrew, "Path");
		}

		internal static void init() {
			if(Initialized)
				return;

			engine.GetBuiltinModule().ImportModule("clr");
			ICollection<string> searchpaths = engine.GetSearchPaths();
			string pypath = "";
			switch(Core.platform) {
			case PlatformID.Unix:
				pypath = "/lib/python2.7/";
				if(!new IO.Dir(pypath).Exists())
					pypath = "/usr/lib/python2.7/";
				break;
			case PlatformID.Win32NT:
				pypath = "/Python27/Lib/";
				break;
			}
			searchpaths.Add(pypath);
			engine.SetSearchPaths(searchpaths);

# region configuration
			engine.ExecuteFile(FileLoad.WorkingDirectory().Get(@"core/config.py").Path, ShellScope);
			Data.Config = ShellScope.GetVariable("__config__");
#endregion

# region global variables
			homebrew.load = new Action<string>((string s) => Brew(new FileScript(new File(s))));
# endregion

# region casts
			SetGlobal("byte", new Func<int, byte>((int i) => { return (byte)i; }));
			SetGlobal("short", new Func<int, short>((int i) => { return (short)i; }));
# endregion

# region helper object functions
			SetGlobal("new_mod", new Func<short, Modifier>((short m) => { return new Modifier(m); }));
			SetGlobal("new_roll", new Func<ushort, ushort, Roll>((ushort n, ushort d) => { return new Roll(n, d); }));
# endregion

# region accessors
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
			SetGlobal("view", new Action<object, Brew>(Core.View));
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

		public static T[] PyArray<T>(PyList list) {
			T[] output = new T[list.__len__()];
			uint n = 0;
			foreach(T item in list)
				output[n++] = item;
			return output;
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
			try { Core.Run("code \"" + temp.Path + "\""); }
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

	}

	public static class Util {
# region globals
		public static dynamic local { get => Scripting.locals; }
		public static dynamic brew { get => Scripting.homebrew; }
		public static dynamic _setting { get => Scripting.settings; }

		public static string readl(string prompt) {
			Console.Write(prompt);
			return Console.In.ReadLine();
		}			public static uint roll(ushort sides) { return Die.Roll(sides); }
		public static uint rolln(ushort sides, ushort count) { return Die.Rolln(sides, count); }
# endregion

# region viewers
		public static void view(object obj, Brew brew = null) { Core.View(obj, brew); }
# endregion

# region accessors
		public static Brew _brew(string id) { return Data.GetBrew(id); }
		public static Character _c(string id) { return Data.GetCharacter(id); }

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