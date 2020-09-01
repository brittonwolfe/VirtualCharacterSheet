using System;
using System.Collections.Generic;

using Gtk;

using VirtualCharacterSheet.Terminal;

using PyList = IronPython.Runtime.List;
using PyTuple = IronPython.Runtime.PythonTuple;
using Action = System.Action;

namespace VirtualCharacterSheet.Forms {

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

	public abstract class AbstractGui : AbstractUi {
		protected Window Window;
		protected Fixed Container;
		protected Layout Layout;
		protected List<WrappedWidget> Components = new List<WrappedWidget>();

		public AbstractGui(string name = "VCS Window") {
			Window = new Window(name);
			Program.Windows.Add(Window);
			Layout = new Layout(new Adjustment(0, 0, 0, 0, 0, 0), new Adjustment(0, 0, 0, 0, 0, 0));
			Window.Destroyed += Program.OnWindowClose;
		}

		public void Add(Widget widget) { Put(widget); }
		public void Put(Widget widget, int x = 0, int y = 0) { Components.Add(new WrappedWidget(widget, x, y)); }
		public void Remove(WrappedWidget widget) { Components.Remove(widget); }

		public void Pack(bool expand = true, bool fill = true, uint padding = 0) {
			if(Container != null)
				Window.Remove(Container);
			Container = new Fixed();
			Container.Add(Layout);
			foreach(Widget child in Container.Children)
				Container.Remove(child);
			foreach(WrappedWidget widget in Components)
				Container.Put(widget.widget, widget.x, widget.y);
			Window.Add(Container);
		}

		public override void Render() { Window.ShowAll(); }
		public override void Close() { Window.Close(); }

		public void Resize(int? width = null, int? height = null) {
			var size = GetSize();
			var w = width ?? size.Item1;
			var h = height ?? size.Item2;
			Window.Resize(w, h);
			Layout.SetSize((uint)w, (uint)h);
		}
		public (int, int) GetSize() {
			int x = 0, y = 0;
			Window.GetSize(out x, out y);
			return (x, y);
		}

		public static implicit operator Window(AbstractGui gui) { return gui.Window; }

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
		public CharacterSheet(PyList views) : this(Scripting.PyArray<(string, TerminalView)>(views)) {}

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

	public sealed class Splash : AbstractGui {

		public Splash() : base("VirtualCharacterSheet") {
			Window.Resize(600,800);
			var testlabel = new Label("label");
			Put(testlabel, y: 40);
			Put(new DefaultMenuBar());
			Pack();
		}

		private void NewCharacter() {
			
		}

		private void LoadModule(string mod) { Scripting.Brew(new FileScript(new IO.File(mod))); }

	}

	internal sealed class DefaultMenuBar : MenuBar {
		private MenuItem
			File,
			Brew;

		internal DefaultMenuBar() {
			this.Name = "vcs";

			File = new MenuItem("File");
			this.Add(File);

			Brew = new MenuItem("Brew");
			var Brew_Load = new MenuItem("Load");
			Brew_Load.ButtonPressEvent += (obj, e) => {
				var fc = new FileChooserDialog("Select a brew", null, FileChooserAction.Open, Stock.Cancel, ResponseType.Cancel, Stock.Open, ResponseType.Accept);
				var ff = new FileFilter();
				ff.Name = "Brew Script";
				ff.AddPattern("brew.py");
				fc.Filter = ff;
				fc.Show();
				fc.Response += (o, e) => {
					if(e.ResponseId == ResponseType.Accept)
						Scripting.Brew(new FileScript(new IO.File(fc.File.Path)));
					fc.Dispose();
				};
			};
			var Brew_List = new MenuItem("List");
			Brew_List.ButtonPressEvent += (obj, e) => {
				//foo
			};
			var Brew_Submenu = new Menu();
			Brew_Submenu.Add(Brew_Load);
			Brew_Submenu.Add(Brew_List);
			Brew.Submenu = Brew_Submenu;
			this.Add(Brew);

		}

	}

}
