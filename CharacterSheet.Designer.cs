
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
			this.PlayerName = new System.Windows.Forms.Label();
			this.StatBox = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.characterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sandboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CharHeader.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CharHeader
			// 
			this.CharHeader.Controls.Add(this.PlayerName);
			this.CharHeader.Location = new System.Drawing.Point(12, 27);
			this.CharHeader.Name = "CharHeader";
			this.CharHeader.Size = new System.Drawing.Size(544, 64);
			this.CharHeader.TabIndex = 0;
			this.CharHeader.TabStop = false;
			this.CharHeader.Text = "charname";
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
			this.StatBox.Location = new System.Drawing.Point(13, 97);
			this.StatBox.Name = "StatBox";
			this.StatBox.Size = new System.Drawing.Size(93, 482);
			this.StatBox.TabIndex = 1;
			this.StatBox.TabStop = false;
			this.StatBox.Text = "Stats";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(113, 97);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(444, 482);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(436, 456);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(436, 456);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fooToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.consoleToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(569, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fooToolStripMenuItem
			// 
			this.fooToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
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
			this.characterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
			this.showToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.showToolStripMenuItem.Text = "Show";
			this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
			// 
			// sandboxToolStripMenuItem
			// 
			this.sandboxToolStripMenuItem.Name = "sandboxToolStripMenuItem";
			this.sandboxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.sandboxToolStripMenuItem.Text = "Sandbox";
			this.sandboxToolStripMenuItem.Click += new System.EventHandler(this.sandboxToolStripMenuItem_Click);
			// 
			// CharacterSheet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(569, 595);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.StatBox);
			this.Controls.Add(this.CharHeader);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "CharacterSheet";
			this.Text = "Character Sheet";
			this.CharHeader.ResumeLayout(false);
			this.CharHeader.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox CharHeader;
		private System.Windows.Forms.Label PlayerName;
		private System.Windows.Forms.GroupBox StatBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fooToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem characterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sampleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sandboxToolStripMenuItem;
	}
}

