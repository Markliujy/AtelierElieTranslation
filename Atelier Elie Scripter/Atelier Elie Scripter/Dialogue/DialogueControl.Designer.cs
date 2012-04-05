/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 12:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AtelierElieScripter.Dialogue
{
	partial class DialogueControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.pictureboxTextBlockName = new System.Windows.Forms.PictureBox();
			this.splitContainerBlocksMain = new System.Windows.Forms.SplitContainer();
			this.listviewMainBlocks = new AtelierElieScripter.Lib.CustListView();
			this.columnHeaderMainBlocks1Block = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderMainBlocks2Done = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderMainBlocks3Notes = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderMainBlocks4Perct = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderMainBlocks5Total = new System.Windows.Forms.ColumnHeader();
			this.splitContainertTextBlocks = new System.Windows.Forms.SplitContainer();
			this.listviewTextBlocks = new AtelierElieScripter.Lib.CustListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.splitContainerJapEng = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panelTextBlock = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textboxTextBlock = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.splitContainerTransNotes = new System.Windows.Forms.SplitContainer();
			this.panelTransText = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textboxTransText = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textboxTransNotes = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureboxTextBlockName)).BeginInit();
			this.splitContainerBlocksMain.Panel1.SuspendLayout();
			this.splitContainerBlocksMain.Panel2.SuspendLayout();
			this.splitContainerBlocksMain.SuspendLayout();
			this.splitContainertTextBlocks.Panel1.SuspendLayout();
			this.splitContainertTextBlocks.Panel2.SuspendLayout();
			this.splitContainertTextBlocks.SuspendLayout();
			this.splitContainerJapEng.Panel1.SuspendLayout();
			this.splitContainerJapEng.Panel2.SuspendLayout();
			this.splitContainerJapEng.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.splitContainerTransNotes.Panel1.SuspendLayout();
			this.splitContainerTransNotes.Panel2.SuspendLayout();
			this.splitContainerTransNotes.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureboxTextBlockName
			// 
			this.pictureboxTextBlockName.Location = new System.Drawing.Point(12, 19);
			this.pictureboxTextBlockName.Name = "pictureboxTextBlockName";
			this.pictureboxTextBlockName.Size = new System.Drawing.Size(128, 32);
			this.pictureboxTextBlockName.TabIndex = 4;
			this.pictureboxTextBlockName.TabStop = false;
			// 
			// splitContainerBlocksMain
			// 
			this.splitContainerBlocksMain.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainerBlocksMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerBlocksMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerBlocksMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerBlocksMain.ForeColor = System.Drawing.SystemColors.ControlText;
			this.splitContainerBlocksMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerBlocksMain.Name = "splitContainerBlocksMain";
			// 
			// splitContainerBlocksMain.Panel1
			// 
			this.splitContainerBlocksMain.Panel1.Controls.Add(this.listviewMainBlocks);
			this.splitContainerBlocksMain.Panel1MinSize = 156;
			// 
			// splitContainerBlocksMain.Panel2
			// 
			this.splitContainerBlocksMain.Panel2.Controls.Add(this.splitContainertTextBlocks);
			this.splitContainerBlocksMain.Size = new System.Drawing.Size(819, 624);
			this.splitContainerBlocksMain.SplitterDistance = 160;
			this.splitContainerBlocksMain.TabIndex = 7;
			// 
			// listviewMainBlocks
			// 
			this.listviewMainBlocks.BackColor = System.Drawing.Color.Gainsboro;
			this.listviewMainBlocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderMainBlocks1Block,
									this.columnHeaderMainBlocks2Done,
									this.columnHeaderMainBlocks3Notes,
									this.columnHeaderMainBlocks4Perct,
									this.columnHeaderMainBlocks5Total});
			this.listviewMainBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listviewMainBlocks.FullRowSelect = true;
			this.listviewMainBlocks.GridLines = true;
			this.listviewMainBlocks.HideSelection = false;
			this.listviewMainBlocks.Location = new System.Drawing.Point(0, 0);
			this.listviewMainBlocks.MultiSelect = false;
			this.listviewMainBlocks.Name = "listviewMainBlocks";
			this.listviewMainBlocks.Size = new System.Drawing.Size(156, 620);
			this.listviewMainBlocks.SortableColumns = true;
			this.listviewMainBlocks.TabIndex = 0;
			this.listviewMainBlocks.UseCompatibleStateImageBehavior = false;
			this.listviewMainBlocks.UseShowHideColumns = true;
			this.listviewMainBlocks.View = System.Windows.Forms.View.Details;
			this.listviewMainBlocks.SelectedIndexChanged += new System.EventHandler(this.ListviewMainBlocksSelectedIndexChanged);
			// 
			// columnHeaderMainBlocks1Block
			// 
			this.columnHeaderMainBlocks1Block.Text = "Block";
			this.columnHeaderMainBlocks1Block.Width = 42;
			// 
			// columnHeaderMainBlocks2Done
			// 
			this.columnHeaderMainBlocks2Done.Text = "Done";
			this.columnHeaderMainBlocks2Done.Width = 47;
			// 
			// columnHeaderMainBlocks3Notes
			// 
			this.columnHeaderMainBlocks3Notes.Text = "Notes";
			this.columnHeaderMainBlocks3Notes.Width = 43;
			// 
			// columnHeaderMainBlocks4Perct
			// 
			this.columnHeaderMainBlocks4Perct.Text = "%";
			this.columnHeaderMainBlocks4Perct.Width = 85;
			// 
			// columnHeaderMainBlocks5Total
			// 
			this.columnHeaderMainBlocks5Total.Text = "Total";
			this.columnHeaderMainBlocks5Total.Width = 39;
			// 
			// splitContainertTextBlocks
			// 
			this.splitContainertTextBlocks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainertTextBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainertTextBlocks.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainertTextBlocks.Location = new System.Drawing.Point(0, 0);
			this.splitContainertTextBlocks.Name = "splitContainertTextBlocks";
			// 
			// splitContainertTextBlocks.Panel1
			// 
			this.splitContainertTextBlocks.Panel1.Controls.Add(this.listviewTextBlocks);
			this.splitContainertTextBlocks.Panel1MinSize = 200;
			// 
			// splitContainertTextBlocks.Panel2
			// 
			this.splitContainertTextBlocks.Panel2.Controls.Add(this.splitContainerJapEng);
			this.splitContainertTextBlocks.Panel2MinSize = 0;
			this.splitContainertTextBlocks.Size = new System.Drawing.Size(655, 624);
			this.splitContainertTextBlocks.SplitterDistance = 322;
			this.splitContainertTextBlocks.TabIndex = 5;
			// 
			// listviewTextBlocks
			// 
			this.listviewTextBlocks.BackColor = System.Drawing.Color.Gainsboro;
			this.listviewTextBlocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader5,
									this.columnHeader6,
									this.columnHeader7,
									this.columnHeader8});
			this.listviewTextBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listviewTextBlocks.FullRowSelect = true;
			this.listviewTextBlocks.GridLines = true;
			this.listviewTextBlocks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listviewTextBlocks.HideSelection = false;
			this.listviewTextBlocks.Location = new System.Drawing.Point(0, 0);
			this.listviewTextBlocks.MultiSelect = false;
			this.listviewTextBlocks.Name = "listviewTextBlocks";
			this.listviewTextBlocks.Size = new System.Drawing.Size(318, 620);
			this.listviewTextBlocks.SortableColumns = false;
			this.listviewTextBlocks.TabIndex = 1;
			this.listviewTextBlocks.UseCompatibleStateImageBehavior = false;
			this.listviewTextBlocks.UseShowHideColumns = false;
			this.listviewTextBlocks.View = System.Windows.Forms.View.Details;
			this.listviewTextBlocks.SelectedIndexChanged += new System.EventHandler(this.ListviewTextBlocksSelectedIndexChanged);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Block";
			this.columnHeader5.Width = 42;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Jap";
			this.columnHeader6.Width = 97;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Eng";
			this.columnHeader7.Width = 71;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Note";
			this.columnHeader8.Width = 104;
			// 
			// splitContainerJapEng
			// 
			this.splitContainerJapEng.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerJapEng.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerJapEng.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerJapEng.Location = new System.Drawing.Point(0, 0);
			this.splitContainerJapEng.Name = "splitContainerJapEng";
			this.splitContainerJapEng.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerJapEng.Panel1
			// 
			this.splitContainerJapEng.Panel1.Controls.Add(this.groupBox1);
			this.splitContainerJapEng.Panel1MinSize = 10;
			// 
			// splitContainerJapEng.Panel2
			// 
			this.splitContainerJapEng.Panel2.Controls.Add(this.groupBox2);
			this.splitContainerJapEng.Panel2MinSize = 10;
			this.splitContainerJapEng.Size = new System.Drawing.Size(329, 624);
			this.splitContainerJapEng.SplitterDistance = 260;
			this.splitContainerJapEng.TabIndex = 7;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pictureboxTextBlockName);
			this.groupBox1.Controls.Add(this.panelTextBlock);
			this.groupBox1.Controls.Add(this.textboxTextBlock);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(325, 256);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Original";
			// 
			// panelTextBlock
			// 
			this.panelTextBlock.Location = new System.Drawing.Point(12, 168);
			this.panelTextBlock.Name = "panelTextBlock";
			this.panelTextBlock.Size = new System.Drawing.Size(296, 97);
			this.panelTextBlock.TabIndex = 5;
			this.panelTextBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTextBlockPaint);
			// 
			// textboxTextBlock
			// 
			this.textboxTextBlock.Enabled = false;
			this.textboxTextBlock.Font = new System.Drawing.Font("MS Mincho", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textboxTextBlock.Location = new System.Drawing.Point(12, 57);
			this.textboxTextBlock.Multiline = true;
			this.textboxTextBlock.Name = "textboxTextBlock";
			this.textboxTextBlock.ReadOnly = true;
			this.textboxTextBlock.Size = new System.Drawing.Size(296, 105);
			this.textboxTextBlock.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.splitContainerTransNotes);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(325, 356);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Translation";
			// 
			// splitContainerTransNotes
			// 
			this.splitContainerTransNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerTransNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerTransNotes.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerTransNotes.Location = new System.Drawing.Point(3, 16);
			this.splitContainerTransNotes.Name = "splitContainerTransNotes";
			this.splitContainerTransNotes.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerTransNotes.Panel1
			// 
			this.splitContainerTransNotes.Panel1.Controls.Add(this.panelTransText);
			this.splitContainerTransNotes.Panel1.Controls.Add(this.textboxTransText);
			this.splitContainerTransNotes.Panel1MinSize = 10;
			// 
			// splitContainerTransNotes.Panel2
			// 
			this.splitContainerTransNotes.Panel2.Controls.Add(this.groupBox3);
			this.splitContainerTransNotes.Panel2MinSize = 10;
			this.splitContainerTransNotes.Size = new System.Drawing.Size(319, 337);
			this.splitContainerTransNotes.SplitterDistance = 200;
			this.splitContainerTransNotes.TabIndex = 9;
			// 
			// panelTransText
			// 
			this.panelTransText.Location = new System.Drawing.Point(9, 120);
			this.panelTransText.Name = "panelTransText";
			this.panelTransText.Size = new System.Drawing.Size(296, 83);
			this.panelTransText.TabIndex = 8;
			this.panelTransText.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTransTextPaint);
			// 
			// textboxTransText
			// 
			this.textboxTransText.Enabled = false;
			this.textboxTransText.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textboxTransText.Location = new System.Drawing.Point(9, 3);
			this.textboxTransText.Multiline = true;
			this.textboxTransText.Name = "textboxTransText";
			this.textboxTransText.Size = new System.Drawing.Size(296, 111);
			this.textboxTransText.TabIndex = 7;
			this.textboxTransText.TextChanged += new System.EventHandler(this.TextboxTransTextTextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textboxTransNotes);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(9, 3, 9, 3);
			this.groupBox3.Size = new System.Drawing.Size(315, 129);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Notes";
			// 
			// textboxTransNotes
			// 
			this.textboxTransNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textboxTransNotes.Enabled = false;
			this.textboxTransNotes.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textboxTransNotes.Location = new System.Drawing.Point(9, 16);
			this.textboxTransNotes.Multiline = true;
			this.textboxTransNotes.Name = "textboxTransNotes";
			this.textboxTransNotes.Size = new System.Drawing.Size(297, 110);
			this.textboxTransNotes.TabIndex = 8;
			this.textboxTransNotes.TextChanged += new System.EventHandler(this.TextboxTransNotesTextChanged);
			// 
			// DialogueControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerBlocksMain);
			this.Name = "DialogueControl";
			this.Size = new System.Drawing.Size(819, 624);
			((System.ComponentModel.ISupportInitialize)(this.pictureboxTextBlockName)).EndInit();
			this.splitContainerBlocksMain.Panel1.ResumeLayout(false);
			this.splitContainerBlocksMain.Panel2.ResumeLayout(false);
			this.splitContainerBlocksMain.ResumeLayout(false);
			this.splitContainertTextBlocks.Panel1.ResumeLayout(false);
			this.splitContainertTextBlocks.Panel2.ResumeLayout(false);
			this.splitContainertTextBlocks.ResumeLayout(false);
			this.splitContainerJapEng.Panel1.ResumeLayout(false);
			this.splitContainerJapEng.Panel2.ResumeLayout(false);
			this.splitContainerJapEng.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.splitContainerTransNotes.Panel1.ResumeLayout(false);
			this.splitContainerTransNotes.Panel1.PerformLayout();
			this.splitContainerTransNotes.Panel2.ResumeLayout(false);
			this.splitContainerTransNotes.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ColumnHeader columnHeaderMainBlocks5Total;
		private System.Windows.Forms.ColumnHeader columnHeaderMainBlocks3Notes;
		private System.Windows.Forms.ColumnHeader columnHeaderMainBlocks4Perct;
		private System.Windows.Forms.ColumnHeader columnHeaderMainBlocks1Block;
		private System.Windows.Forms.ColumnHeader columnHeaderMainBlocks2Done;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader5;

		private System.Windows.Forms.TextBox textboxTransNotes;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.SplitContainer splitContainerTransNotes;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelTransText;
		private System.Windows.Forms.TextBox textboxTransText;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.SplitContainer splitContainerJapEng;
		private System.Windows.Forms.GroupBox groupBox1;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelTextBlock;
		private AtelierElieScripter.Lib.CustListView listviewTextBlocks;
		private System.Windows.Forms.SplitContainer splitContainertTextBlocks;
		private AtelierElieScripter.Lib.CustListView listviewMainBlocks;
		private System.Windows.Forms.TextBox textboxTextBlock;
		private System.Windows.Forms.SplitContainer splitContainerBlocksMain;
		private System.Windows.Forms.PictureBox pictureboxTextBlockName;
	}
}
