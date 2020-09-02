using System;
using System.Collections.Generic;

using Gtk;

using Action = System.Action;

namespace VirtualCharacterSheet.Forms {

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

		public static FileChooserDialog CreateBasicChooser(string title = "Pick a File", string name = "A file", Action<IO.File> callback = null, params string[] filters) {
			var output = new FileChooserDialog(title, null, FileChooserAction.Open, Stock.Cancel, ResponseType.Cancel, Stock.Open, ResponseType.Accept);
			var filter = new FileFilter();
			filter.Name = name;
			foreach(string pattern in filters)
				filter.AddPattern(pattern);
			output.Filter = filter;
			output.Response += (o, e) => {
				if(e.ResponseId == ResponseType.Accept)	
					callback(new IO.File(output.File.Path));
				output.Dispose();
			};
			return output;
		}

		public static implicit operator Window(AbstractGui gui) { return gui.Window; }

	}

	namespace Gui {

		public sealed class Splash : AbstractGui {

			public Splash() : base(name: "VirtualCharacterSheet") {
				Window.Resize(600,400);
				Put(new Component.DefaultMenuBar());
				Pack();
			}

			private void NewCharacter() {
				
			}	

			private void LoadModule(string mod) { Scripting.Brew(new FileScript(new IO.File(mod))); }

		}

		public sealed class BrewListWindow : AbstractGui {

			public BrewListWindow() : base(name: "Brew List") {
				var list = new ListBox();
				foreach(Brew brew in Data.GetAllBrews()) {
					var britem = new Button();
					britem.Label = brew.Name;
					list.Add(britem);
				}
				Put(list);
				Pack(expand: true, fill: true);
			}

		}

		namespace Component {

			internal sealed class DefaultMenuBar : MenuBar {
				private MenuItem
					File,
					Brew;

				internal DefaultMenuBar() {
					this.Name = "vcs";

					File = new MenuItem("File");
					var File_Load = new MenuItem("Load");
					var File_Submenu = new Menu();
					var File_Load_Submenu = new Menu();
					var File_Load_Character = new MenuItem("Character");
					File_Load_Character.ButtonPressEvent += (obj, e) => {
						AbstractGui.CreateBasicChooser(
							"Select a Character",
							"Character File",
							(file) => PlayerCharacter.Deserialize(file.GetBinaryReader()),
							"*.bin", "*.vcschar"
							).Show();
					};
					File_Load_Submenu.Add(File_Load_Character);
					File_Load.Submenu = File_Load_Submenu;
					File.Submenu = File_Submenu;
					File_Submenu.Add(File_Load);
					this.Add(File);

					Brew = new MenuItem("Brew");
					var Brew_Load = new MenuItem("Load");
					Brew_Load.ButtonPressEvent += (obj, e) => {
						AbstractGui.CreateBasicChooser(
							"Select a Brew",
							"Brew Script",
							(file) => Scripting.Brew(new FileScript(file)),
							"brew.py"
						).Show();
					};
					var Brew_List = new MenuItem("List");
					Brew_List.ButtonPressEvent += (obj, e) => {
						var blw = new BrewListWindow();
						blw.Render();
					};
					var Brew_Submenu = new Menu();
					Brew_Submenu.Add(Brew_Load);
					Brew_Submenu.Add(Brew_List);
					Brew.Submenu = Brew_Submenu;
					this.Add(Brew);

				}

			}

		}

	}

}