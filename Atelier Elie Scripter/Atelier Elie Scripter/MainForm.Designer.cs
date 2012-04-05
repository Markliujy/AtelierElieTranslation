/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 12:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AtelierElieScripter
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.dialogueControl = new AtelierElieScripter.Dialogue.DialogueControl();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPageDialogueText = new System.Windows.Forms.TabPage();
			this.tabPageDialogueChoices = new System.Windows.Forms.TabPage();
			this.dialogueChoicesControl = new AtelierElieScripter.DialogueChoices.DialogueChoicesControl();
			this.tabPageItems = new System.Windows.Forms.TabPage();
			this.control2 = new AtelierElieScripter.AlchemyItems.Control();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDteTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dTETableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControlMain.SuspendLayout();
			this.tabPageDialogueText.SuspendLayout();
			this.tabPageDialogueChoices.SuspendLayout();
			this.tabPageItems.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// dialogueControl
			// 
			this.dialogueControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dialogueControl.Location = new System.Drawing.Point(3, 3);
			this.dialogueControl.Name = "dialogueControl";
			this.dialogueControl.Size = new System.Drawing.Size(848, 585);
			this.dialogueControl.TabIndex = 0;
			// 
			// tabControlMain
			// 
			this.tabControlMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControlMain.Controls.Add(this.tabPageDialogueText);
			this.tabControlMain.Controls.Add(this.tabPageDialogueChoices);
			this.tabControlMain.Controls.Add(this.tabPageItems);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 24);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(862, 620);
			this.tabControlMain.TabIndex = 0;
			// 
			// tabPageDialogueText
			// 
			this.tabPageDialogueText.Controls.Add(this.dialogueControl);
			this.tabPageDialogueText.Location = new System.Drawing.Point(4, 25);
			this.tabPageDialogueText.Name = "tabPageDialogueText";
			this.tabPageDialogueText.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDialogueText.Size = new System.Drawing.Size(854, 591);
			this.tabPageDialogueText.TabIndex = 0;
			this.tabPageDialogueText.Text = "Dialogue";
			this.tabPageDialogueText.UseVisualStyleBackColor = true;
			// 
			// tabPageDialogueChoices
			// 
			this.tabPageDialogueChoices.Controls.Add(this.dialogueChoicesControl);
			this.tabPageDialogueChoices.Location = new System.Drawing.Point(4, 25);
			this.tabPageDialogueChoices.Name = "tabPageDialogueChoices";
			this.tabPageDialogueChoices.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDialogueChoices.Size = new System.Drawing.Size(854, 591);
			this.tabPageDialogueChoices.TabIndex = 1;
			this.tabPageDialogueChoices.Text = "Dialogue Choices";
			this.tabPageDialogueChoices.UseVisualStyleBackColor = true;
			// 
			// dialogueChoicesControl
			// 
			this.dialogueChoicesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dialogueChoicesControl.Location = new System.Drawing.Point(3, 3);
			this.dialogueChoicesControl.Name = "dialogueChoicesControl";
			this.dialogueChoicesControl.Size = new System.Drawing.Size(848, 585);
			this.dialogueChoicesControl.TabIndex = 0;
			// 
			// tabPageItems
			// 
			this.tabPageItems.Controls.Add(this.control2);
			this.tabPageItems.Location = new System.Drawing.Point(4, 25);
			this.tabPageItems.Name = "tabPageItems";
			this.tabPageItems.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageItems.Size = new System.Drawing.Size(854, 591);
			this.tabPageItems.TabIndex = 2;
			this.tabPageItems.Text = "Items";
			this.tabPageItems.UseVisualStyleBackColor = true;
			// 
			// control2
			// 
			this.control2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.control2.Location = new System.Drawing.Point(3, 3);
			this.control2.Name = "control2";
			this.control2.Size = new System.Drawing.Size(848, 585);
			this.control2.TabIndex = 0;
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.editToolStripMenuItem,
									this.viewToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(862, 24);
			this.mainMenuStrip.TabIndex = 2;
			this.mainMenuStrip.Text = "mainMenuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.openFileToolStripMenuItem,
									this.saveToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openFileToolStripMenuItem
			// 
			this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
			this.openFileToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.openFileToolStripMenuItem.Text = "Open...";
			this.openFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItemClick);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.saveToolStripMenuItem.Text = "Save...";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveTestToolStripMenuItem,
									this.saveDteTestToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// saveTestToolStripMenuItem
			// 
			this.saveTestToolStripMenuItem.Name = "saveTestToolStripMenuItem";
			this.saveTestToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.saveTestToolStripMenuItem.Text = "Save test";
			this.saveTestToolStripMenuItem.Click += new System.EventHandler(this.SaveTestToolStripMenuItemClick);
			// 
			// saveDteTestToolStripMenuItem
			// 
			this.saveDteTestToolStripMenuItem.Name = "saveDteTestToolStripMenuItem";
			this.saveDteTestToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.saveDteTestToolStripMenuItem.Text = "save dte test";
			this.saveDteTestToolStripMenuItem.Click += new System.EventHandler(this.SaveDteTestToolStripMenuItemClick);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.dTETableToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// dTETableToolStripMenuItem
			// 
			this.dTETableToolStripMenuItem.Name = "dTETableToolStripMenuItem";
			this.dTETableToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.dTETableToolStripMenuItem.Text = "DTE Table";
			this.dTETableToolStripMenuItem.Click += new System.EventHandler(this.DTETableToolStripMenuItemClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(862, 644);
			this.Controls.Add(this.tabControlMain);
			this.Controls.Add(this.mainMenuStrip);
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.Text = "Atelier Elie Scripter";
			this.tabControlMain.ResumeLayout(false);
			this.tabPageDialogueText.ResumeLayout(false);
			this.tabPageDialogueChoices.ResumeLayout(false);
			this.tabPageItems.ResumeLayout(false);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private AtelierElieScripter.AlchemyItems.Control control2;
		private System.Windows.Forms.ToolStripMenuItem saveDteTestToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveTestToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dTETableToolStripMenuItem;
		private AtelierElieScripter.DialogueChoices.DialogueChoicesControl dialogueChoicesControl;
		private System.Windows.Forms.TabPage tabPageDialogueChoices;
		private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private AtelierElieScripter.Dialogue.DialogueControl dialogueControl;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabPageDialogueText;
		private System.Windows.Forms.TabPage tabPageItems;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
	}
}
