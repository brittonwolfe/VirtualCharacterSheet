using System;
using System.Windows.Forms;

namespace VirtualCharacterSheet.Forms {

	public partial class CharacterSheet : Form {

		public CharacterSheet() {
			InitializeComponent();
		}
		public CharacterSheet(PlayerCharacter pc) : this() {
			CharHeader.Text = pc.Name;
			PlayerName.Text = pc.Player;
		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e) {
			Core.ShowConsole();
		}

		private void sandboxToolStripMenuItem_Click(object sender, EventArgs e) {
			Core.StartSandbox();
		}

	}
}
