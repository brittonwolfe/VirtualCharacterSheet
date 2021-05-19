using System;
using System.Collections.Generic;

using VirtualCharacterSheet.Forms;

namespace VirtualCharacterSheet.Terminal {

	public interface AbstractCli {

		public abstract void Handle(string input);

		public static void Clear() { Console.Clear(); }

	}

	public class Cli : AbstractCli {
		internal Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();

		public Cli(params (string, dynamic)[] funcs) {
			foreach((string, dynamic) pair in funcs)
				map[pair.Item1] = pair.Item2;
			//Scripting.ShellScope.Import("clr");
			foreach(string key in map.Keys)
				SetGlobal(key, map[key]);
		}

		public void Handle(string input) {
			//try { Scripting.ShellScope.Eval(input.StartsWith(':') ? input.Substring(1) : input); }
			//catch(Exception e) { Console.WriteLine(e); }
		}

		internal void SetGlobal(string key, dynamic obj) { return; }
		internal void RemoveGlobal(string key) { return; }
		internal void SetThis(dynamic obj) { SetGlobal("this", obj); }

	}

	public abstract class AbstractTui : AbstractUi {

	}

}
