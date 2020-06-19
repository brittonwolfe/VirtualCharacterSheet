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

		public void Handle() {
			string inp = Console.In.ReadLine();
			string[] parts = inp.Trim().Split(' ');
			string cmd = parts[0];
			string[] args = new string[parts.Length - 1];
			for(int x = 1; x < parts.Length; x++)
				args[x - 1] = parts[x];
			if(map.ContainsKey(cmd))
				map[cmd]((object)args);
			else
				// quick aside: I want the program to *try* to exec the line as Python if enabled.
				Console.WriteLine("\"" + cmd + "\" is not a valid command");
		}

		private void SetGlobal(string key, dynamic obj) { engine.GetBuiltinModule().SetVariable(key, obj); }

	}

}
