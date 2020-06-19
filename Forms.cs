using System;
using System.Collections.Generic;

namespace VirtualCharacterSheet.Forms {

	public abstract class TerminalForm : ComplexObject {
		protected TerminalGraphic[] Graphics;
		protected dynamic Renderer;
		private Tui Handler;

		public TerminalForm(dynamic renderer, params TerminalGraphic[] graphics) {
			Graphics = graphics;
			Renderer = renderer;
		}

		public void Render() {
			Scripting.locals.graphics = Graphics;
			Renderer();
			Scripting.Remove(Scripting.locals, "graphics");
		}

	}

	public abstract class TerminalGraphic {
		private List<string> Layers = new List<string>();
		protected dynamic Renderer;

		public TerminalGraphic(dynamic render = null, params string[] layers) {
			Renderer = (render != null) ? render : new Func<string>(DefaultRender);
			foreach(string layer in layers)
				Layers.Add(layer);
		}

		public string Draw(object content = null) { return Renderer(); }

		private string DefaultRender() {
			var chars = new List<List<char>>();
			foreach(string l in Layers) {
				ushort line = 0;
				ushort pos = 0;
				foreach(char c in l)
					if(c == '\n') {
						line++;
						pos = 0;
					} else
						chars[line][pos++] = c;
			}
			string output = "";
			foreach(List<char> l in chars) {
				foreach(char c in l)
					output += c;
				output += '\n';
			}
			return output.Trim();
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

		private void NewCharacter() {
			
		}

		private void LoadModule(string mod) { Scripting.Brew(new FileScript(new IO.File(mod))); }

	}

}