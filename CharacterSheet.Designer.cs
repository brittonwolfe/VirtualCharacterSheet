using System;
using System.Diagnostics.Tracing;

namespace VirtualCharacterSheet.Forms {
	partial class CharacterSheet {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.CharHeader = new System.Windows.Forms.GroupBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.PlayerName = new System.Windows.Forms.Label();
			this.StatBox = new System.Windows.Forms.GroupBox();
			this.CharismaScore = new System.Windows.Forms.TextBox();
			this.CharismaMod = new System.Windows.Forms.GroupBox();
			this.WisdomScore = new System.Windows.Forms.TextBox();
			this.WisdomMod = new System.Windows.Forms.GroupBox();
			this.IntelligenceScore = new System.Windows.Forms.TextBox();
			this.IntelligenceMod = new System.Windows.Forms.GroupBox();
			this.ConstitutionScore = new System.Windows.Forms.TextBox();
			this.ConstitutionMod = new System.Windows.Forms.GroupBox();
			this.DexterityScore = new System.Windows.Forms.TextBox();
			this.DexterityMod = new System.Windows.Forms.GroupBox();
			this.StrengthScore = new System.Windows.Forms.TextBox();
			this.StrengthMod = new System.Windows.Forms.GroupBox();
			this.InfoPage = new System.Windows.Forms.TabControl();
			this.BasicTab = new System.Windows.Forms.TabPage();
			this.InventoryPage = new System.Windows.Forms.TabPage();
			this.SpellPage = new System.Windows.Forms.TabPage();
			this.BioPage = new System.Windows.Forms.TabPage();
			this.ObjectPage = new System.Windows.Forms.TabPage();
			this.ObjectTree = new System.Windows.Forms.TreeView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.planToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.characterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sandboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BasicView = new System.Windows.Forms.WebBrowser();
			this.CharHeader.SuspendLayout();
			this.StatBox.SuspendLayout();
			this.InfoPage.SuspendLayout();
			this.BasicTab.SuspendLayout();
			this.ObjectPage.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CharHeader
			// 
			this.CharHeader.Controls.Add(this.progressBar1);
			this.CharHeader.Controls.Add(this.PlayerName);
			this.CharHeader.Location = new System.Drawing.Point(12, 27);
			this.CharHeader.Name = "CharHeader";
			this.CharHeader.Size = new System.Drawing.Size(544, 64);
			this.CharHeader.TabIndex = 0;
			this.CharHeader.TabStop = false;
			this.CharHeader.Text = "charname";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(441, 22);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(100, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// PlayerName
			// 
			this.PlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PlayerName.AutoSize = true;
			this.PlayerName.Location = new System.Drawing.Point(503, 48);
			this.PlayerName.Name = "PlayerName";
			this.PlayerName.Size = new System.Drawing.Size(35, 13);
			this.PlayerName.TabIndex = 0;
			this.PlayerName.Text = "player";
			// 
			// StatBox
			// 
			this.StatBox.Controls.Add(this.CharismaScore);
			this.StatBox.Controls.Add(this.CharismaMod);
			this.StatBox.Controls.Add(this.WisdomScore);
			this.StatBox.Controls.Add(this.WisdomMod);
			this.StatBox.Controls.Add(this.IntelligenceScore);
			this.StatBox.Controls.Add(this.IntelligenceMod);
			this.StatBox.Controls.Add(this.ConstitutionScore);
			this.StatBox.Controls.Add(this.ConstitutionMod);
			this.StatBox.Controls.Add(this.DexterityScore);
			this.StatBox.Controls.Add(this.DexterityMod);
			this.StatBox.Controls.Add(this.StrengthScore);
			this.StatBox.Controls.Add(this.StrengthMod);
			this.StatBox.Location = new System.Drawing.Point(13, 97);
			this.StatBox.Name = "StatBox";
			this.StatBox.Size = new System.Drawing.Size(93, 439);
			this.StatBox.TabIndex = 1;
			this.StatBox.TabStop = false;
			this.StatBox.Text = "Stats";
			// 
			// CharismaScore
			// 
			this.CharismaScore.Enabled = false;
			this.CharismaScore.Location = new System.Drawing.Point(29, 410);
			this.CharismaScore.Name = "CharismaScore";
			this.CharismaScore.ReadOnly = true;
			this.CharismaScore.Size = new System.Drawing.Size(37, 20);
			this.CharismaScore.TabIndex = 12;
			// 
			// CharismaMod
			// 
			this.CharismaMod.Location = new System.Drawing.Point(24, 367);
			this.CharismaMod.Name = "CharismaMod";
			this.CharismaMod.Size = new System.Drawing.Size(47, 50);
			this.CharismaMod.TabIndex = 11;
			this.CharismaMod.TabStop = false;
			this.CharismaMod.Text = "CHA";
			this.CharismaMod.Click += new System.EventHandler(this.Charisma_Click);
			// 
			// WisdomScore
			// 
			this.WisdomScore.Enabled = false;
			this.WisdomScore.Location = new System.Drawing.Point(29, 341);
			this.WisdomScore.Name = "WisdomScore";
			this.WisdomScore.ReadOnly = true;
			this.WisdomScore.Size = new System.Drawing.Size(37, 20);
			this.WisdomScore.TabIndex = 10;
			// 
			// WisdomMod
			// 
			this.WisdomMod.Location = new System.Drawing.Point(24, 298);
			this.WisdomMod.Name = "WisdomMod";
			this.WisdomMod.Size = new System.Drawing.Size(47, 50);
			this.WisdomMod.TabIndex = 9;
			this.WisdomMod.TabStop = false;
			this.WisdomMod.Text = "WIS";
			this.WisdomMod.Click += new System.EventHandler(this.Wisdom_Click);
			// 
			// IntelligenceScore
			// 
			this.IntelligenceScore.Enabled = false;
			this.IntelligenceScore.Location = new System.Drawing.Point(29, 272);
			this.IntelligenceScore.Name = "IntelligenceScore";
			this.IntelligenceScore.ReadOnly = true;
			this.IntelligenceScore.Size = new System.Drawing.Size(37, 20);
			this.IntelligenceScore.TabIndex = 8;
			// 
			// IntelligenceMod
			// 
			this.IntelligenceMod.Location = new System.Drawing.Point(24, 229);
			this.IntelligenceMod.Name = "IntelligenceMod";
			this.IntelligenceMod.Size = new System.Drawing.Size(47, 50);
			this.IntelligenceMod.TabIndex = 7;
			this.IntelligenceMod.TabStop = false;
			this.IntelligenceMod.Text = "INT";
			this.IntelligenceMod.Click += new System.EventHandler(this.Intelligence_Click);
			// 
			// ConstitutionScore
			// 
			this.ConstitutionScore.Enabled = false;
			this.ConstitutionScore.Location = new System.Drawing.Point(29, 203);
			this.ConstitutionScore.Name = "ConstitutionScore";
			this.ConstitutionScore.ReadOnly = true;
			this.ConstitutionScore.Size = new System.Drawing.Size(37, 20);
			this.ConstitutionScore.TabIndex = 6;
			// 
			// ConstitutionMod
			// 
			this.ConstitutionMod.Location = new System.Drawing.Point(24, 160);
			this.ConstitutionMod.Name = "ConstitutionMod";
			this.ConstitutionMod.Size = new System.Drawing.Size(47, 50);
			this.ConstitutionMod.TabIndex = 5;
			this.ConstitutionMod.TabStop = false;
			this.ConstitutionMod.Text = "CON";
			this.ConstitutionMod.Click += new System.EventHandler(this.Constitution_Click);
			// 
			// DexterityScore
			// 
			this.DexterityScore.Enabled = false;
			this.DexterityScore.Location = new System.Drawing.Point(29, 134);
			this.DexterityScore.Name = "DexterityScore";
			this.DexterityScore.ReadOnly = true;
			this.DexterityScore.Size = new System.Drawing.Size(37, 20);
			this.DexterityScore.TabIndex = 4;
			// 
			// DexterityMod
			// 
			this.DexterityMod.Location = new System.Drawing.Point(24, 91);
			this.DexterityMod.Name = "DexterityMod";
			this.DexterityMod.Size = new System.Drawing.Size(47, 50);
			this.DexterityMod.TabIndex = 3;
			this.DexterityMod.TabStop = false;
			this.DexterityMod.Text = "DEX";
			this.DexterityMod.Click += new System.EventHandler(this.Dexterity_Click);
			// 
			// StrengthScore
			// 
			this.StrengthScore.Enabled = false;
			this.StrengthScore.Location = new System.Drawing.Point(29, 65);
			this.StrengthScore.Name = "StrengthScore";
			this.StrengthScore.ReadOnly = true;
			this.StrengthScore.Size = new System.Drawing.Size(37, 20);
			this.StrengthScore.TabIndex = 2;
			// 
			// StrengthMod
			// 
			this.StrengthMod.Location = new System.Drawing.Point(24, 22);
			this.StrengthMod.Name = "StrengthMod";
			this.StrengthMod.Size = new System.Drawing.Size(47, 50);
			this.StrengthMod.TabIndex = 0;
			this.StrengthMod.TabStop = false;
			this.StrengthMod.Text = "STR";
			this.StrengthMod.Click += new System.EventHandler(this.Strength_Click);
			// 
			// InfoPage
			// 
			this.InfoPage.Controls.Add(this.BasicTab);
			this.InfoPage.Controls.Add(this.InventoryPage);
			this.InfoPage.Controls.Add(this.SpellPage);
			this.InfoPage.Controls.Add(this.BioPage);
			this.InfoPage.Controls.Add(this.ObjectPage);
			this.InfoPage.Location = new System.Drawing.Point(113, 97);
			this.InfoPage.Name = "InfoPage";
			this.InfoPage.SelectedIndex = 0;
			this.InfoPage.Size = new System.Drawing.Size(444, 486);
			this.InfoPage.TabIndex = 2;
			// 
			// BasicTab
			// 
			this.BasicTab.Controls.Add(this.BasicView);
			this.BasicTab.Location = new System.Drawing.Point(4, 22);
			this.BasicTab.Name = "BasicTab";
			this.BasicTab.Padding = new System.Windows.Forms.Padding(3);
			this.BasicTab.Size = new System.Drawing.Size(436, 460);
			this.BasicTab.TabIndex = 0;
			this.BasicTab.Text = "Basic";
			this.BasicTab.UseVisualStyleBackColor = true;
			// 
			// InventoryPage
			// 
			this.InventoryPage.Location = new System.Drawing.Point(4, 22);
			this.InventoryPage.Name = "InventoryPage";
			this.InventoryPage.Padding = new System.Windows.Forms.Padding(3);
			this.InventoryPage.Size = new System.Drawing.Size(436, 460);
			this.InventoryPage.TabIndex = 1;
			this.InventoryPage.Text = "Inventory";
			this.InventoryPage.UseVisualStyleBackColor = true;
			// 
			// SpellPage
			// 
			this.SpellPage.Location = new System.Drawing.Point(4, 22);
			this.SpellPage.Name = "SpellPage";
			this.SpellPage.Padding = new System.Windows.Forms.Padding(3);
			this.SpellPage.Size = new System.Drawing.Size(436, 460);
			this.SpellPage.TabIndex = 2;
			this.SpellPage.Text = "Spells";
			this.SpellPage.UseVisualStyleBackColor = true;
			// 
			// BioPage
			// 
			this.BioPage.Location = new System.Drawing.Point(4, 22);
			this.BioPage.Name = "BioPage";
			this.BioPage.Padding = new System.Windows.Forms.Padding(3);
			this.BioPage.Size = new System.Drawing.Size(436, 460);
			this.BioPage.TabIndex = 4;
			this.BioPage.Text = "Bio";
			this.BioPage.UseVisualStyleBackColor = true;
			// 
			// ObjectPage
			// 
			this.ObjectPage.Controls.Add(this.ObjectTree);
			this.ObjectPage.Location = new System.Drawing.Point(4, 22);
			this.ObjectPage.Name = "ObjectPage";
			this.ObjectPage.Padding = new System.Windows.Forms.Padding(3);
			this.ObjectPage.Size = new System.Drawing.Size(436, 460);
			this.ObjectPage.TabIndex = 3;
			this.ObjectPage.Text = "Object View";
			this.ObjectPage.UseVisualStyleBackColor = true;
			// 
			// ObjectTree
			// 
			this.ObjectTree.Cursor = System.Windows.Forms.Cursors.Default;
			this.ObjectTree.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.ObjectTree.Location = new System.Drawing.Point(3, 3);
			this.ObjectTree.Name = "ObjectTree";
			this.ObjectTree.Size = new System.Drawing.Size(427, 447);
			this.ObjectTree.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fooToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.consoleToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(570, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fooToolStripMenuItem
			// 
			this.fooToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.planToolStripMenuItem});
			this.fooToolStripMenuItem.Name = "fooToolStripMenuItem";
			this.fooToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
			this.fooToolStripMenuItem.Text = "Character";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// planToolStripMenuItem
			// 
			this.planToolStripMenuItem.Name = "planToolStripMenuItem";
			this.planToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
			this.planToolStripMenuItem.Text = "Plan";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.characterToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// characterToolStripMenuItem
			// 
			this.characterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleToolStripMenuItem});
			this.characterToolStripMenuItem.Name = "characterToolStripMenuItem";
			this.characterToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.characterToolStripMenuItem.Text = "Character";
			// 
			// sampleToolStripMenuItem
			// 
			this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
			this.sampleToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.sampleToolStripMenuItem.Text = "Sample";
			// 
			// consoleToolStripMenuItem
			// 
			this.consoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.sandboxToolStripMenuItem});
			this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
			this.consoleToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.consoleToolStripMenuItem.Text = "Console";
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.showToolStripMenuItem.Text = "Show";
			this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
			// 
			// sandboxToolStripMenuItem
			// 
			this.sandboxToolStripMenuItem.Name = "sandboxToolStripMenuItem";
			this.sandboxToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.sandboxToolStripMenuItem.Text = "Sandbox";
			this.sandboxToolStripMenuItem.Click += new System.EventHandler(this.sandboxToolStripMenuItem_Click);
			// 
			// BasicView
			// 
			this.BasicView.AllowNavigation = false;
			this.BasicView.AllowWebBrowserDrop = false;
			this.BasicView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BasicView.IsWebBrowserContextMenuEnabled = false;
			this.BasicView.Location = new System.Drawing.Point(3, 3);
			this.BasicView.MinimumSize = new System.Drawing.Size(20, 20);
			this.BasicView.Name = "BasicView";
			this.BasicView.Size = new System.Drawing.Size(430, 454);
			this.BasicView.TabIndex = 0;
			// 
			// CharacterSheet
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(570, 594);
			this.Controls.Add(this.InfoPage);
			this.Controls.Add(this.StatBox);
			this.Controls.Add(this.CharHeader);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "CharacterSheet";
			this.Text = "Character Sheet";
			this.CharHeader.ResumeLayout(false);
			this.CharHeader.PerformLayout();
			this.StatBox.ResumeLayout(false);
			this.StatBox.PerformLayout();
			this.InfoPage.ResumeLayout(false);
			this.BasicTab.ResumeLayout(false);
			this.ObjectPage.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox CharHeader;
		private System.Windows.Forms.Label PlayerName;
		private System.Windows.Forms.GroupBox StatBox;
		private System.Windows.Forms.TabControl InfoPage;
		private System.Windows.Forms.TabPage BasicTab;
		private System.Windows.Forms.TabPage InventoryPage;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fooToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem characterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sampleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sandboxToolStripMenuItem;
		private System.Windows.Forms.TabPage SpellPage;
		private System.Windows.Forms.TabPage ObjectPage;
		private System.Windows.Forms.TreeView ObjectTree;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.GroupBox StrengthMod;
		private System.Windows.Forms.TextBox StrengthScore;
		private System.Windows.Forms.TabPage BioPage;
		private System.Windows.Forms.TextBox CharismaScore;
		private System.Windows.Forms.GroupBox CharismaMod;
		private System.Windows.Forms.TextBox WisdomScore;
		private System.Windows.Forms.GroupBox WisdomMod;
		private System.Windows.Forms.TextBox IntelligenceScore;
		private System.Windows.Forms.GroupBox IntelligenceMod;
		private System.Windows.Forms.TextBox ConstitutionScore;
		private System.Windows.Forms.GroupBox ConstitutionMod;
		private System.Windows.Forms.TextBox DexterityScore;
		private System.Windows.Forms.GroupBox DexterityMod;
		private System.Windows.Forms.ToolStripMenuItem planToolStripMenuItem;
		private System.Windows.Forms.WebBrowser BasicView;
	}
}

