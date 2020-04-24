using System;
using Microsoft.Scripting.Hosting;

using IronPython.Hosting;

namespace VirtualCharacterSheet {

	public static class Scripting {
		internal static ScriptEngine engine = Python.CreateEngine();

		public static void Sandbox() {
			Core.AllocateConsole();
			init();
			do {
				Console.Write("> ");
				string inp = Console.In.ReadLine();
				if(inp == "quit")
					break;
				if(inp.ToLower() == "help") {
					Help();
					continue;
				}
				try { engine.Execute(inp); }
				catch(Exception e) { Console.WriteLine(e); break; }
			} while(true);
			Core.HideConsole();
		}

		private static void init() {
			engine.Execute("import clr");
			Action<string> HelpFunc = GetHelp;
			Func<ushort,ushort> RollFunc = Die.Roll;
			Func<byte,ushort,ushort> RollnFunc = Die.Rolln;
			Func<byte,short> ModFunc = Core.Modifier;
			Func<Character> GetCCharFunc = Core.GetCurrentCharacter;
			Func<ushort,Item> GetItem = Data.GetItem;
			Action<string> CreateItemFunc = CreateItem;
			Func<ushort,Character> GetCharacter = Data.GetCharacter;
			Func<ushort,NPC> GetNPC = Data.GetNPC;
			SetGlobal("help", HelpFunc);
			SetGlobal("roll", RollFunc);
			SetGlobal("rolln", RollnFunc);
			SetGlobal("mod", ModFunc);
			SetGlobal("getopenchar", GetCCharFunc);
			SetGlobal("_i", GetItem);
			SetGlobal("_c", GetCharacter);
			SetGlobal("_n", GetNPC);
			SetGlobal("new_i", CreateItemFunc);
		}

		private static void SetGlobal(string n, object o) { engine.GetBuiltinModule().SetVariable(n, o); }
		private static dynamic GetGlobal(string n) { return engine.GetBuiltinModule().GetVariable(n); }

		public static void Help() {
			Console.WriteLine("roll(d)\t\trolln(n,d)\t\tmod(s)\t\tgetopenchar()");
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
			case "quit":
				Console.WriteLine("quit\n\texits the Python console");
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

		public static void CreateItem(string n) {
			Item i = new Item();
			i.Name = n;
			ushort id = Data.AddItem(i);
			Console.WriteLine("Created new item \"" + n + "\" at _i(" + id + ")");
		}

	}

	public class Script {
		private bool isFile;
		private string src;

		public Script(string py) {
			isFile = false;
			src = py;
		}

		public void Run() {
			if(isFile) {
				
			} else {
				Scripting.engine.Execute<dynamic>(src);
			}
		}

	}

	public class MacroRoll {
		private readonly string src;

		public MacroRoll(string pysrc) { src = pysrc; }

	}

}