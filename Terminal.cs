using System;
using System.Collections.Generic;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

using VirtualCharacterSheet.Forms;

namespace VirtualCharacterSheet.Terminal {

	public interface AbstractCli {

		public abstract void Handle(string input);

	}

	public class Cli : AbstractCli {
		internal Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
		internal ScriptEngine engine = Python.CreateEngine();

		public Cli(params (string, dynamic)[] funcs) {
			foreach((string, dynamic) pair in funcs)
				map[pair.Item1] = pair.Item2;
			engine.GetBuiltinModule().ImportModule("clr");
			foreach(string key in map.Keys)
				SetGlobal(key, map[key]);
		}

		public void Handle(string input) {
			if(input.StartsWith(':'))
				try { Scripting.engine.Execute(input.Substring(1)); }
				catch(Exception e) { Console.WriteLine(e); }
			else
				try { engine.Execute(input); }
				catch(Exception e) { Console.WriteLine(e); }
		}

		internal void SetGlobal(string key, dynamic obj) { engine.GetBuiltinModule().SetVariable(key, obj); }
		internal void RemoveGlobal(string key) { engine.GetBuiltinModule().RemoveVariable(key); }
		internal void SetThis(dynamic obj) { SetGlobal("this", obj); }

	}

	public abstract class AbstractTui : AbstractUi {

	}

}
