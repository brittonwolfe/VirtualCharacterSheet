using System;

namespace VirtualCharacterSheet.Forms {

	public partial class CharacterSheet {
		public PlayerCharacter currChar;

		private void showToolStripMenuItem_Click(object sender, EventArgs e) { Core.ShowConsole(); }

		private void sandboxToolStripMenuItem_Click(object sender, EventArgs e) { Core.StartSandbox(); }

		private void Strength_Click(object sender, EventArgs e) {
			//do a str check
		}
		private void Dexterity_Click(object sender, EventArgs e) {
			//do a dex check
		}
		private void Constitution_Click(object sender, EventArgs e) {
			//do a con check
		}
		private void Intelligence_Click(object sender, EventArgs e) {
			//do an int check
		}
		private void Wisdom_Click(object sender, EventArgs e) {
			//do a wis check
		}
		private void Charisma_Click(object sender, EventArgs e) {
			//do a cha check
		}

	}

	public partial class Splash {

		private void NewCharacter_Click(object sender, EventArgs e) {
			//create a new character
		}

	}

}