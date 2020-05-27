
namespace VirtualCharacterSheet.Forms {
	partial class Splash {
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
			this.CharacterBox = new System.Windows.Forms.GroupBox();
			this.OpenCharacter = new System.Windows.Forms.Button();
			this.NewCharacter = new System.Windows.Forms.Button();
			this.DesignBox = new System.Windows.Forms.GroupBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sandboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GameMasterBox = new System.Windows.Forms.GroupBox();
			this.ModuleBox = new System.Windows.Forms.GroupBox();
			this.BrewBox = new System.Windows.Forms.GroupBox();
			this.CharacterBox.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CharacterBox
			// 
			this.CharacterBox.Controls.Add(this.OpenCharacter);
			this.CharacterBox.Controls.Add(this.NewCharacter);
			this.CharacterBox.Location = new System.Drawing.Point(12, 27);
			this.CharacterBox.Name = "CharacterBox";
			this.CharacterBox.Size = new System.Drawing.Size(178, 83);
			this.CharacterBox.TabIndex = 0;
			this.CharacterBox.TabStop = false;
			this.CharacterBox.Text = "Character";
			// 
			// OpenCharacter
			// 
			this.OpenCharacter.Location = new System.Drawing.Point(6, 48);
			this.OpenCharacter.Name = "OpenCharacter";
			this.OpenCharacter.Size = new System.Drawing.Size(166, 23);
			this.OpenCharacter.TabIndex = 1;
			this.OpenCharacter.Text = "Open an existing Character";
			this.OpenCharacter.UseVisualStyleBackColor = true;
			// 
			// NewCharacter
			// 
			this.NewCharacter.Location = new System.Drawing.Point(6, 19);
			this.NewCharacter.Name = "NewCharacter";
			this.NewCharacter.Size = new System.Drawing.Size(166, 23);
			this.NewCharacter.TabIndex = 0;
			this.NewCharacter.Text = "Create a new Character";
			this.NewCharacter.UseVisualStyleBackColor = true;
			this.NewCharacter.Click += new System.EventHandler(this.NewCharacter_Click);
			// 
			// DesignBox
			// 
			this.DesignBox.Location = new System.Drawing.Point(196, 155);
			this.DesignBox.Name = "DesignBox";
			this.DesignBox.Size = new System.Drawing.Size(171, 283);
			this.DesignBox.TabIndex = 1;
			this.DesignBox.TabStop = false;
			this.DesignBox.Text = "Design";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.consoleToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
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
			// GameMasterBox
			// 
			this.GameMasterBox.Location = new System.Drawing.Point(373, 27);
			this.GameMasterBox.Name = "GameMasterBox";
			this.GameMasterBox.Size = new System.Drawing.Size(415, 411);
			this.GameMasterBox.TabIndex = 3;
			this.GameMasterBox.TabStop = false;
			this.GameMasterBox.Text = "GM";
			// 
			// ModuleBox
			// 
			this.ModuleBox.Location = new System.Drawing.Point(196, 27);
			this.ModuleBox.Name = "ModuleBox";
			this.ModuleBox.Size = new System.Drawing.Size(171, 122);
			this.ModuleBox.TabIndex = 4;
			this.ModuleBox.TabStop = false;
			this.ModuleBox.Text = "Modules";
			// 
			// BrewBox
			// 
			this.BrewBox.Location = new System.Drawing.Point(12, 116);
			this.BrewBox.Name = "BrewBox";
			this.BrewBox.Size = new System.Drawing.Size(178, 322);
			this.BrewBox.TabIndex = 5;
			this.BrewBox.TabStop = false;
			this.BrewBox.Text = "Brews";
			// 
			// Splash
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.BrewBox);
			this.Controls.Add(this.ModuleBox);
			this.Controls.Add(this.GameMasterBox);
			this.Controls.Add(this.DesignBox);
			this.Controls.Add(this.CharacterBox);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Splash";
			this.Text = "Virtual Character Sheet";
			this.CharacterBox.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox CharacterBox;
		private System.Windows.Forms.GroupBox DesignBox;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sandboxToolStripMenuItem;
		private System.Windows.Forms.Button NewCharacter;
		private System.Windows.Forms.Button OpenCharacter;
		private System.Windows.Forms.GroupBox GameMasterBox;
		private System.Windows.Forms.GroupBox ModuleBox;
		private System.Windows.Forms.GroupBox BrewBox;
	}
}