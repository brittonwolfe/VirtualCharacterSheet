using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet {

	public static class Tui {
		internal static Dictionary<string, dynamic> func = new Dictionary<string, dynamic>();

		public static void Handle() {
			string inp = Console.In.ReadLine();
			string cmd;
			string[] args;
			{
				string[] parts = inp.Trim().Split(' ');
				cmd = parts[0];
				args = new string[parts.Length - 1];
				for(int x = 1; x < parts.Length; x++)
					args[x - 1] = parts[x];
			}
			if(func.ContainsKey(cmd))
				func[cmd]((object)args);
			else
				Console.WriteLine("\"" + cmd + "\" is not a valid command");
		}

	}

}
