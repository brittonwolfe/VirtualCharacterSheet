using System;
using System.Collections.Generic;

using Gtk;

using VCS.Terminal;

using PyTuple = IronPython.Runtime.PythonTuple;
using Action = System.Action;

namespace VCS.Forms {

	public abstract class AbstractUiFactory {

		public abstract AbstractUi Create(dynamic content = null);

	}

	public abstract class AbstractUi : ComplexObject {

		public abstract void Render();
		public abstract void Close();

	}

	public readonly struct WrappedWidget {
		public readonly Widget widget;
		public readonly int x;
		public readonly int y;

		public WrappedWidget(Widget widget, int x = 0, int y = 0) {
			this.widget = widget;
			this.x = x;
			this.y = y;
		}

	}

	public abstract class TerminalForm : AbstractUi {
		protected Dictionary<string, TerminalView> Views = new Dictionary<string, TerminalView>();
		protected string CurrentView = "base";
		protected Cli Handler;

		public TerminalForm(params (string, TerminalView)[] views) {
			foreach((string, TerminalView) kvp in views)
				Views[kvp.Item1] = kvp.Item2;
		}

		public override void Render() {
			Views[CurrentView].Render();
		}

		public void SetupCli(params (string, dynamic)[] funcs) {
			Handler = new Cli(funcs);
			Handler.SetThis(this);
		}
		public void SetupTui(params PyTuple[] funcs) {
			var tmp = new (string, dynamic)[funcs.Length];
			for(int x = 0; x < tmp.Length; x++)
				tmp[x] = ((string)funcs[x][0], funcs[x][1]);
			SetupCli(tmp);
		}

		public static (int, int) GetTerminalSize() { return (Console.WindowWidth, Console.WindowHeight); }
		public static int GetTerminalWidth() { return GetTerminalSize().Item1; }
		public static int GetTerminalHeight() { return GetTerminalSize().Item2; }

		public static void SetCursorPosition(int? x = null, int? y = null) {
			if(x != null)
				Console.CursorLeft = (int)x;
			if(y != null)
				Console.CursorTop = (int)y;
		}
		public static (int, int) GetCursorPosition() { return (Console.CursorLeft, Console.CursorTop); }

		public static void SetBackgroundColor(ConsoleColor color) { Console.BackgroundColor = color; }
		public static void SetForegroundColor(ConsoleColor color) { Console.ForegroundColor = color; }
		public static void ResetColor() { Console.ResetColor(); }

		public static void Clear() { Console.Clear(); }

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
		//public CharacterSheet(PyList views) : this(Scripting.PyArray<(string, TerminalView)>(views)) {}

		public void SetCharacter(PlayerCharacter c) {
			DisposeIdentity();
			Character = c;
			//Scripting.viewers[c.Identifier] = this;
			Setup();
		}

		private void DisposeIdentity() {
			if(Character != null)
				return;//Scripting.Remove(Scripting.viewers, Character.Identifier);
		}

		public Cli GetCliHandler() { return Handler; }

		public override void Close() { DisposeIdentity(); }

	}

}
