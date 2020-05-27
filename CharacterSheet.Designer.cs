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
			this.CharismaBox = new System.Windows.Forms.GroupBox();
			this.CharismaMod = new System.Windows.Forms.Label();
			this.WisdomScore = new System.Windows.Forms.TextBox();
			this.WisdomBox = new System.Windows.Forms.GroupBox();
			this.WisdomMod = new System.Windows.Forms.Label();
			this.IntelligenceScore = new System.Windows.Forms.TextBox();
			this.IntelligenceBox = new System.Windows.Forms.GroupBox();
			this.IntelligenceMod = new System.Windows.Forms.Label();
			this.ConstitutionScore = new System.Windows.Forms.TextBox();
			this.ConstitutionBox = new System.Windows.Forms.GroupBox();
			this.ConstitutionMod = new System.Windows.Forms.Label();
			this.DexterityScore = new System.Windows.Forms.TextBox();
			this.DexterityBox = new System.Windows.Forms.GroupBox();
			this.DexterityMod = new System.Windows.Forms.Label();
			this.StrengthScore = new System.Windows.Forms.TextBox();
			this.StrengthBox = new System.Windows.Forms.GroupBox();
			this.StrengthMod = new System.Windows.Forms.Label();
			this.InfoPage = new System.Windows.Forms.TabControl();
			this.BasicTab = new System.Windows.Forms.TabPage();
			this.BasicView = new System.Windows.Forms.WebBrowser();
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
			this.CharHeader.SuspendLayout();
			this.StatBox.SuspendLayout();
			this.CharismaBox.SuspendLayout();
			this.WisdomBox.SuspendLayout();
			this.IntelligenceBox.SuspendLayout();
			this.ConstitutionBox.SuspendLayout();
			this.DexterityBox.SuspendLayout();
			this.StrengthBox.SuspendLayout();
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
			this.StatBox.Controls.Add(this.CharismaBox);
			this.StatBox.Controls.Add(this.WisdomScore);
			this.StatBox.Controls.Add(this.WisdomBox);
			this.StatBox.Controls.Add(this.IntelligenceScore);
			this.StatBox.Controls.Add(this.IntelligenceBox);
			this.StatBox.Controls.Add(this.ConstitutionScore);
			this.StatBox.Controls.Add(this.ConstitutionBox);
			this.StatBox.Controls.Add(this.DexterityScore);
			this.StatBox.Controls.Add(this.DexterityBox);
			this.StatBox.Controls.Add(this.StrengthScore);
			this.StatBox.Controls.Add(this.StrengthBox);
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
			// CharismaBox
			// 
			this.CharismaBox.Controls.Add(this.CharismaMod);
			this.CharismaBox.Location = new System.Drawing.Point(24, 367);
			this.CharismaBox.Name = "CharismaBox";
			this.CharismaBox.Size = new System.Drawing.Size(47, 50);
			this.CharismaBox.TabIndex = 11;
			this.CharismaBox.TabStop = false;
			this.CharismaBox.Text = "CHA";
			this.CharismaBox.Click += new System.EventHandler(this.Charisma_Click);
			// 
			// CharismaMod
			// 
			this.CharismaMod.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CharismaMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CharismaMod.Location = new System.Drawing.Point(7, 16);
			this.CharismaMod.Name = "CharismaMod";
			this.CharismaMod.Size = new System.Drawing.Size(35, 25);
			this.CharismaMod.TabIndex = 1;
			this.CharismaMod.Text = "0";
			this.CharismaMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// WisdomBox
			// 
			this.WisdomBox.Controls.Add(this.WisdomMod);
			this.WisdomBox.Location = new System.Drawing.Point(24, 298);
			this.WisdomBox.Name = "WisdomBox";
			this.WisdomBox.Size = new System.Drawing.Size(47, 50);
			this.WisdomBox.TabIndex = 9;
			this.WisdomBox.TabStop = false;
			this.WisdomBox.Text = "WIS";
			this.WisdomBox.Click += new System.EventHandler(this.Wisdom_Click);
			// 
			// WisdomMod
			// 
			this.WisdomMod.Cursor = System.Windows.Forms.Cursors.Hand;
			this.WisdomMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WisdomMod.Location = new System.Drawing.Point(6, 16);
			this.WisdomMod.Name = "WisdomMod";
			this.WisdomMod.Size = new System.Drawing.Size(35, 25);
			this.WisdomMod.TabIndex = 1;
			this.WisdomMod.Text = "0";
			this.WisdomMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// IntelligenceBox
			// 
			this.IntelligenceBox.Controls.Add(this.IntelligenceMod);
			this.IntelligenceBox.Location = new System.Drawing.Point(24, 229);
			this.IntelligenceBox.Name = "IntelligenceBox";
			this.IntelligenceBox.Size = new System.Drawing.Size(47, 50);
			this.IntelligenceBox.TabIndex = 7;
			this.IntelligenceBox.TabStop = false;
			this.IntelligenceBox.Text = "INT";
			this.IntelligenceBox.Click += new System.EventHandler(this.Intelligence_Click);
			// 
			// IntelligenceMod
			// 
			this.IntelligenceMod.Cursor = System.Windows.Forms.Cursors.Hand;
			this.IntelligenceMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.IntelligenceMod.Location = new System.Drawing.Point(6, 16);
			this.IntelligenceMod.Name = "IntelligenceMod";
			this.IntelligenceMod.Size = new System.Drawing.Size(35, 25);
			this.IntelligenceMod.TabIndex = 1;
			this.IntelligenceMod.Text = "0";
			this.IntelligenceMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// ConstitutionBox
			// 
			this.ConstitutionBox.Controls.Add(this.ConstitutionMod);
			this.ConstitutionBox.Location = new System.Drawing.Point(24, 160);
			this.ConstitutionBox.Name = "ConstitutionBox";
			this.ConstitutionBox.Size = new System.Drawing.Size(47, 50);
			this.ConstitutionBox.TabIndex = 5;
			this.ConstitutionBox.TabStop = false;
			this.ConstitutionBox.Text = "CON";
			this.ConstitutionBox.Click += new System.EventHandler(this.Constitution_Click);
			// 
			// ConstitutionMod
			// 
			this.ConstitutionMod.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ConstitutionMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ConstitutionMod.Location = new System.Drawing.Point(6, 15);
			this.ConstitutionMod.Name = "ConstitutionMod";
			this.ConstitutionMod.Size = new System.Drawing.Size(35, 25);
			this.ConstitutionMod.TabIndex = 1;
			this.ConstitutionMod.Text = "0";
			this.ConstitutionMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// DexterityBox
			// 
			this.DexterityBox.Controls.Add(this.DexterityMod);
			this.DexterityBox.Location = new System.Drawing.Point(24, 91);
			this.DexterityBox.Name = "DexterityBox";
			this.DexterityBox.Size = new System.Drawing.Size(47, 50);
			this.DexterityBox.TabIndex = 3;
			this.DexterityBox.TabStop = false;
			this.DexterityBox.Text = "DEX";
			this.DexterityBox.Click += new System.EventHandler(this.Dexterity_Click);
			// 
			// DexterityMod
			// 
			this.DexterityMod.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DexterityMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DexterityMod.Location = new System.Drawing.Point(6, 16);
			this.DexterityMod.Name = "DexterityMod";
			this.DexterityMod.Size = new System.Drawing.Size(35, 25);
			this.DexterityMod.TabIndex = 1;
			this.DexterityMod.Text = "0";
			this.DexterityMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// StrengthBox
			// 
			this.StrengthBox.Controls.Add(this.StrengthMod);
			this.StrengthBox.Location = new System.Drawing.Point(24, 22);
			this.StrengthBox.Name = "StrengthBox";
			this.StrengthBox.Size = new System.Drawing.Size(47, 50);
			this.StrengthBox.TabIndex = 0;
			this.StrengthBox.TabStop = false;
			this.StrengthBox.Text = "STR";
			this.StrengthBox.Click += new System.EventHandler(this.Strength_Click);
			// 
			// StrengthMod
			// 
			this.StrengthMod.Cursor = System.Windows.Forms.Cursors.Hand;
			this.StrengthMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StrengthMod.Location = new System.Drawing.Point(7, 16);
			this.StrengthMod.Name = "StrengthMod";
			this.StrengthMod.Size = new System.Drawing.Size(35, 25);
			this.StrengthMod.TabIndex = 0;
			this.StrengthMod.Text = "0";
			this.StrengthMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.CharismaBox.ResumeLayout(false);
			this.WisdomBox.ResumeLayout(false);
			this.IntelligenceBox.ResumeLayout(false);
			this.ConstitutionBox.ResumeLayout(false);
			this.DexterityBox.ResumeLayout(false);
			this.StrengthBox.ResumeLayout(false);
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
		private System.Windows.Forms.GroupBox StrengthBox;
		private System.Windows.Forms.TextBox StrengthScore;
		private System.Windows.Forms.TabPage BioPage;
		private System.Windows.Forms.TextBox CharismaScore;
		private System.Windows.Forms.GroupBox CharismaBox;
		private System.Windows.Forms.TextBox WisdomScore;
		private System.Windows.Forms.GroupBox WisdomBox;
		private System.Windows.Forms.TextBox IntelligenceScore;
		private System.Windows.Forms.GroupBox IntelligenceBox;
		private System.Windows.Forms.TextBox ConstitutionScore;
		private System.Windows.Forms.GroupBox ConstitutionBox;
		private System.Windows.Forms.TextBox DexterityScore;
		private System.Windows.Forms.GroupBox DexterityBox;
		private System.Windows.Forms.ToolStripMenuItem planToolStripMenuItem;
		private System.Windows.Forms.WebBrowser BasicView;
		private System.Windows.Forms.Label CharismaMod;
		private System.Windows.Forms.Label WisdomMod;
		private System.Windows.Forms.Label IntelligenceMod;
		private System.Windows.Forms.Label ConstitutionMod;
		private System.Windows.Forms.Label DexterityMod;
		private System.Windows.Forms.Label StrengthMod;
	}
}

