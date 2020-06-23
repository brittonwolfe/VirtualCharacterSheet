using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet.Forms {

	public abstract class TerminalForm : ComplexObject {
		protected Dictionary<string, TerminalView> Views;
		protected string CurrentView = "base";
		protected Tui Handler = new Tui();

		public TerminalForm(params (string, TerminalView)[] views) {
			foreach((string, TerminalView) kvp in views)
				Views[kvp.Item1] = kvp.Item2;
		}

		public void Render() { Views[CurrentView].Render(); }

		public void SetupTui(params (string, dynamic)[] funcs) { Handler = new Tui(funcs); }

		public abstract void Close();

	}

	public abstract class TerminalGraphic {
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