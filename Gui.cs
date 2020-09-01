using Gtk;

namespace VirtualCharacterSheet.Forms.Gtk {

	public sealed class Splash : AbstractGui {

		public Splash() : base("VirtualCharacterSheet") {
			Window.Resize(600,800);
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