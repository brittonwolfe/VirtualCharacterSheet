using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCharacterSheet {

	class Program {

		static void Main(string[] args) {
			Console.Title = "Virtual Character Sheet";
			Console.OutputEncoding = Encoding.Unicode;
			Console.WriteLine("VCS TUI");
			Scripting.init();

			Core.StartSandbox();

			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

		}

		private static void OnExit(object sender, EventArgs e) {
			foreach(IO.File file in Core.temp_files)
				System.IO.File.Delete(file.Path);
		}

	}

	public static class Core {
		private static PlayerCharacter currchar = null;
		internal static bool SandboxAwaits = false;
		internal static List<IO.File> temp_files = new List<IO.File>();
		public readonly static PlatformID platform = Environment.OSVersion.Platform;

		public static PlayerCharacter GetCurrentCharacter() { return currchar; }

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		public static void StartSandbox() { Scripting.Sandbox(); }

		public static Process Run(string command) {
			var process = new Process();
			var info = new ProcessStartInfo();
			if(platform == PlatformID.Unix) {
				info.FileName = "/bin/bash";
				info.Arguments = command;
			} else {
				var parts = command.Split();
				info.FileName = parts[0];
				string tmp = "";
				foreach(string arg in parts.Skip(1).ToArray())
					tmp += (arg + " ");
				info.Arguments = tmp.Trim();
			}
			process.StartInfo = info;
			process.Start();
			return process;
		}

		public static void View(object obj, Brew brew = null) {
			var T = obj.GetType();
			if(brew != null) {
				if(!brew.CanView(T))
					Console.WriteLine(brew.Name + " has no viewer for type " + T);
				else
					View(obj);
				return;
			}
			foreach(Brew b in Data.GetAllBrews())
				if(b.CanView(T)){
					b.View(obj);
					return;
				}
			Console.WriteLine("Cannot view object of type " + T + ".");
		}

	}

}
