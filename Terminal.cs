using System;
using System.Collections.Generic;

using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

namespace VirtualCharacterSheet {

	public class Tui {
		internal Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
		private ScriptEngine engine = Python.CreateEngine();

		public Tui(params (string, dynamic)[] funcs) {
			foreach((string, dynamic) pair in funcs)
				map[pair.Item1] = pair.Item2;
			engine.GetBuiltinModule().ImportModule("clr");
			foreach(string key in map.Keys)
				SetGlobal(key, map[key]);
		}

		public void Handle(string input) {
			try { engine.Execute(input); }
			catch {
				try { Scripting.engine.Execute(input); }
				catch(Exception e2) {
					throw e2;
				}
			}
		}

		private void SetGlobal(string key, dynamic obj) { engine.GetBuiltinModule().SetVariable(key, obj); }

	}

}
