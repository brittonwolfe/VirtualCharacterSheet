using System;
using System.Windows.Forms;

namespace VirtualCharacterSheet.Forms {
	public partial class Splash : Form {
		public Splash() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			new CharacterSheet().Show();
		}
	}
}
