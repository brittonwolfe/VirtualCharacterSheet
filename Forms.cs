using System;
using System.Collections;

namespace VirtualCharacterSheet.Forms {

	public partial class CharacterSheet {
		public PlayerCharacter currChar { get; private set; }

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

		public void SetCharacter(PlayerCharacter c) {
			DisposeIdentity();
			Scripting.viewers[c.Identifier] = this;
			currChar = c;
			return;
			CharHeader.Text = currChar.Name;
			PlayerName.Text = currChar.Player;
			StrengthScore.Text = currChar.Strength.ToString();
			StrengthMod.Text = currChar.STR.ToString();
			DexterityScore.Text = currChar.Dexterity.ToString();
			DexterityMod.Text = currChar.DEX.ToString();
			ConstitutionScore.Text = currChar.Constitution.ToString();
			ConstitutionMod.Text = currChar.CON.ToString();
			IntelligenceScore.Text = currChar.Intelligence.ToString();
			IntelligenceMod.Text = currChar.INT.ToString();
			WisdomScore.Text = currChar.Wisdom.ToString();
			WisdomMod.Text = currChar.WIS.ToString();
			CharismaScore.Text = currChar.Charisma.ToString();
			CharismaMod.Text = currChar.CHA.ToString();
		}

		private void DisposeIdentity() {
			if(currChar != null)
				Scripting.Remove(Scripting.viewers, currChar.Identifier);
		}
	}

	public partial class Splash {

		private void NewCharacter_Click(object sender, EventArgs e) {

		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e) { Core.ShowConsole(); }
		private void sandboxToolStripMenuItem_Click(object sender, EventArgs e) { Core.StartSandbox(); }

	}

}