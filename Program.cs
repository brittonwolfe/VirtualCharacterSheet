using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Python.Runtime;

namespace VirtualCharacterSheet {

	class Program {
		internal static List<Gtk.Window> Windows = new List<Gtk.Window>();

		static void Main(string[] args) {
			Console.Title = "Virtual Character Sheet";
			Console.OutputEncoding = Encoding.Unicode;
			Scripting.Init();

			var delete_temp			=	Data.GetConfig("main", "delete_temp");
			var keep_log			=	Data.GetConfig("main", "keep_log");
			var prefer_cli			=	Data.GetConfig("main", "prefer_cli");
			var save_open_c			=	Data.GetConfig("main", "save_open_c");

			if(delete_temp != null ? bool.Parse(delete_temp) : true)
				AddExitEvent(DisposeTempFiles);
			if(save_open_c != null ? bool.Parse(save_open_c) : false)
				AddExitEvent(SaveOpenCharacters);

			prefer_cli = (prefer_cli != null ? bool.Parse(prefer_cli) : false) || args.Contains("--nogui");

			if((bool)prefer_cli) {
				Console.WriteLine("VCS CLI");
				Core.StartSandbox();
				Scripting.engine.Dispose();
			} else {
				Gtk.Application.Init("VCS", ref args);
				var sout = new System.IO.StreamWriter(FileLoad.GetTempFile("vcs_log.txt").Path);
				Console.SetOut(sout);
				Console.SetError(sout);
				AddExitEvent((obj, e) => {
					Console.WriteLine($"exited at {System.DateTime.Now.ToString()}");
					sout.Close();
					Scripting.engine.Dispose();
				});
				Console.WriteLine($"started at {System.DateTime.Now.ToString()}");
				var splash = new Forms.Gui.Splash();
				splash.Render();
				Gtk.Application.Run();
			}

		}

		internal static void AddExitEvent(EventHandler eventHandler) { AppDomain.CurrentDomain.ProcessExit += eventHandler; }

# region exit events
		private static void DisposeTempFiles(object sender, EventArgs e) {
			foreach(IO.File file in Core.temp_files)
				System.IO.File.Delete(file.Path);
		}
		private static void SaveOpenCharacters(object sender, EventArgs e) {
			var chars = Data.GetAllCharacters();
			if(chars.Count == 0)
				return;
			Console.Clear();
			Console.Write($"You have {chars.Count} characters open. Would you like to save them before exiting (y/n)? ");
			string GetResponse() { return Console.ReadLine().Trim().ToLower(); }
			if(GetResponse() != "y")
				return;
			foreach(PlayerCharacter pc in Data.GetAllCharacters()) {
				Console.Write($"Would you like to save {pc.__repr__()} (y/n)? ");
				if(GetResponse() != "y")
					continue;
				Console.Write("Please enter a path: ");
				string path = Console.ReadLine();
				PlayerCharacter.Serialize(pc, new IO.File(path).GetBinaryWriter());
			}
		}
# endregion

		internal static void OnWindowClose(object sender, EventArgs e) {
			Gtk.Window obj = (Gtk.Window)sender;
			if(Windows.Contains(obj))
				Windows.Remove(obj);
			if(Windows.Count == 0) {
				//TODO: dispose of Scripting.*Scope
				Gtk.Application.Quit();
			}
		}

	}

	public static class Core {
		internal static bool SandboxAwaits = false;
		internal static List<IO.File> temp_files = new List<IO.File>();
		public readonly static PlatformID platform = Environment.OSVersion.Platform;

		public static short Modifier(byte stat) { return (short)((stat / 2) - 5); }

		public static void StartSandbox() { Scripting.Sandbox(); }

		internal static Process Run(string command) {
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
					tmp += ($"{arg} ");
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
					Console.WriteLine($"{brew.Name} has no viewer for type {T}");
				else
					View(obj);
				return;
			}
			foreach(Brew b in Data.GetAllBrews())
				if(b.CanView(T)){
					b.View(obj);
					return;
				}
			Console.WriteLine($"Cannot view object of type {T}");
		}

	}

	public static class Logger {

	}

}
