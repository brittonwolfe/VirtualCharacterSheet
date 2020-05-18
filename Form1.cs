using System.Windows.Forms;

namespace VirtualCharacterSheet {

	public partial class Form1 : Form {

		public Form1() {
			InitializeComponent();
			Scripting.Sandbox();
		}

	}
}
