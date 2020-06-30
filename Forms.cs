﻿using System;
using System.Collections.Generic;

using PyList = IronPython.Runtime.List;
using PyTuple = IronPython.Runtime.PythonTuple;

namespace VirtualCharacterSheet.Forms {

	public abstract class TerminalForm : ComplexObject {
		protected Dictionary<string, TerminalView> Views = new Dictionary<string, TerminalView>();
		protected string CurrentView = "base";
		protected Tui Handler;

		public TerminalForm(params (string, TerminalView)[] views) {
			foreach((string, TerminalView) kvp in views)
				Views[kvp.Item1] = kvp.Item2;
		}

		public void Render() { Views[CurrentView].Render(); }

		public void SetupTui(params (string, dynamic)[] funcs) {
			Handler = new Tui(funcs);
			Handler.SetThis(this);
		}
		public void SetupTui(params PyTuple[] funcs) {
			var tmp = new (string, dynamic)[funcs.Length];
			for(int x = 0; x < tmp.Length; x++)
				tmp[x] = ((string)funcs[x][0], funcs[x][1]);
			SetupTui(tmp);
		}

		public abstract void Close();

		public static (int, int) GetTerminalSize() {
			string[] tmp = Core.Run("stty size").StandardOutput.ReadToEnd().Split(' ');
			return (int.Parse(tmp[0]), int.Parse(tmp[2]));
		}
		public static int GetTerminalWidth() { return GetTerminalSize().Item1; }
		public static int GetTerminalHeight() { return GetTerminalSize().Item2; }

	}

	public class TerminalGraphic {
		private List<string> Layers = new List<string>();
		protected dynamic Renderer;

		public TerminalGraphic(dynamic render = null, params string[] layers) {
			Renderer = (render != null) ? render : new Func<string>(DefaultRender);
			foreach(string layer in layers)
				Layers.Add(layer);
		}

		public string Draw(object content = null) { return Renderer(); }

		private string DefaultRender() {
			var chars = new List<List<char>>();
			foreach(string l in Layers) {
				ushort line = 0;
				ushort pos = 0;
				foreach(char c in l)
					if(c == '\n') {
						line++;
						pos = 0;
					} else
						chars[line][pos++] = c;
			}
			string output = "";
			foreach(List<char> l in chars) {
				foreach(char c in l)
					output += c;
				output += '\n';
			}
			return output.Trim();
		}

	}

	public class TerminalView {
		protected TerminalGraphic[] Graphics;
		protected dynamic Renderer;

		public TerminalView(dynamic renderer = null, params TerminalGraphic[] graphics) {
			Graphics = graphics;
			Renderer = (renderer != null) ? renderer : new Action(DefaultRender);
		}

		public void Render() {
			Scripting.locals.graphics = Graphics;
			Renderer();
			Scripting.Remove(Scripting.locals, "graphics");
		}

		public void DefaultRender() {
			foreach(TerminalGraphic g in Scripting.locals.graphics)
				Console.Write(g.Draw());
		}

	}

	public class CharacterSheet : TerminalForm {
		public PlayerCharacter Character { get; private set; }
		public dynamic Setup;

		public CharacterSheet(params (string, TerminalView)[] views) : base(views) { }
		public CharacterSheet(PyList views) : base() {
			foreach(PyTuple pair in views)
				Views.Add((string)pair[0], (TerminalView)pair[1]);
		}

		public void SetCharacter(PlayerCharacter c) {
			DisposeIdentity();
			Character = c;
			Scripting.viewers[c.Identifier] = this;
			Setup();
		}

		private void DisposeIdentity() {
			if(Character != null)
				Scripting.Remove(Scripting.viewers, Character.Identifier);
		}

		public Tui GetTuiHandler() { return Handler; }

		public override void Close() { DisposeIdentity(); }

	}

	public partial class Splash {

		private void NewCharacter() {
			
		}

		private void LoadModule(string mod) { Scripting.Brew(new FileScript(new IO.File(mod))); }

	}

}