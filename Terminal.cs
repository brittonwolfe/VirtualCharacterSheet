using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet {

	public class Tui {
		internal Dictionary<string, dynamic> func = new Dictionary<string, dynamic>();

		public Tui(params (string, dynamic)[] funcs) {
			foreach((string, dynamic) pair in funcs)
				func[pair.Item1] = pair.Item2;
		}

		public void Handle() {
			string inp = Console.In.ReadLine();
			string[] parts = inp.Trim().Split(' ');
			string cmd = parts[0];
			string[] args = new string[parts.Length - 1];
			for(int x = 1; x < parts.Length; x++)
				args[x - 1] = parts[x];
			if(func.ContainsKey(cmd))
				func[cmd]((object)args);
			else
				// quick aside: I want the program to *try* to exec the line as Python if enabled.
				Console.WriteLine("\"" + cmd + "\" is not a valid command");
		}

	}

}
