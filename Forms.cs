using System;
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

		public void Render(dynamic content = null) {
			if(content != null)
				Handler.SetGlobal("render", content);
			Views[CurrentView].Render();
			Handler.RemoveGlobal("render");
		}

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

		public static (int, int) GetTerminalSize() { return (Console.WindowWidth, Console.WindowHeight); }
		public static int GetTerminalWidth() { return GetTerminalSize().Item1; }
		public static int GetTerminalHeight() { return GetTerminalSize().Item2; }

	}

	public class TerminalGraphic {
		private List<string> Layers = new List<string>();
		protected dynamic Renderer;
		public ushort Width, Height;

		public TerminalGraphic(dynamic render) { Renderer = render; }

		public string Draw() { return Renderer(); }

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
		public CharacterSheet(PyList views) : this(Scripting.PyArray<(string, TerminalView)>(views)) {}

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
