﻿using System;
using System.Windows.Forms;

namespace VirtualCharacterSheet.Forms {

	public partial class CharacterSheet : Form {

		public CharacterSheet() {
			InitializeComponent();
		}

		private void consoleToolStripMenuItem_Click(object sender, EventArgs e) {
			Core.ShowConsole();
		}

	}
}