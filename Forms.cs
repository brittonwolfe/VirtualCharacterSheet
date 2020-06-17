using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet.Forms {

	public abstract class TerminalForm : ComplexObject {
		
	}

	public abstract class TerminalGraphic {
		private List<string> Layers = new List<string>();

		public TerminalGraphic(params string[] layers) {
			foreach(string layer in layers)
				Layers.Add(layer);
		}

	}

	public partial class CharacterSheet {
		public PlayerCharacter currChar { get; private set; }

		public void SetCharacter(PlayerCharacter c) {
			DisposeIdentity();
			//Scripting.viewers[c.Identifier] = this;
			currChar = c;
			/*CharHeader.Text = currChar.Name;
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
			CharismaMod.Text = currChar.CHA.ToString();*/
		}

		private void DisposeIdentity() {
			/*if(currChar != null)
				Scripting.Remove(Scripting.viewers, currChar.Identifier);*/
		}

	}

	public partial class Splash {

		private void NewCharacter(object sender, EventArgs e) {
			
		}

		private void LoadModule(string mod) {
			//TODO
			Scripting.Brew(new FileScript(new IO.File(mod)));
		}


	}

}