/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 25/03/2010
 * Time: 7:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AtelierElieScripter.DialogueChoices
{
	partial class DialogueChoicesControl
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
			this.splitContainerChoicesMain = new System.Windows.Forms.SplitContainer();
			this.custListViewMain = new AtelierElieScripter.Lib.CustListView();
			this.columnHeaderMainBlock = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDone = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderTotal = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderPercent = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderNotes = new System.Windows.Forms.ColumnHeader();
			this.splitContainerBlock = new System.Windows.Forms.SplitContainer();
			this.custListViewBlock = new AtelierElieScripter.Lib.CustListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.panelInputs = new System.Windows.Forms.Panel();
			this.splitContainerTransNotes = new System.Windows.Forms.SplitContainer();
			this.splitContainerJapTrans = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panelChoice1 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.panelChoice2 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.panelChoice3 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.panelChoice4 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.panelChoice5 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxChoices1 = new System.Windows.Forms.TextBox();
			this.textBoxChoices2 = new System.Windows.Forms.TextBox();
			this.textBoxChoices3 = new System.Windows.Forms.TextBox();
			this.textBoxChoices4 = new System.Windows.Forms.TextBox();
			this.textBoxChoices5 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panelEngChoice1 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.panelEngChoice2 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.panelEngChoice3 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxEngChoices5 = new System.Windows.Forms.TextBox();
			this.panelEngChoice4 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxEngChoices4 = new System.Windows.Forms.TextBox();
			this.panelEngChoice5 = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxEngChoices3 = new System.Windows.Forms.TextBox();
			this.textBoxEngChoices1 = new System.Windows.Forms.TextBox();
			this.textBoxEngChoices2 = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxTransNotes = new System.Windows.Forms.TextBox();
			this.splitContainerChoicesMain.Panel1.SuspendLayout();
			this.splitContainerChoicesMain.Panel2.SuspendLayout();
			this.splitContainerChoicesMain.SuspendLayout();
			this.splitContainerBlock.Panel1.SuspendLayout();
			this.splitContainerBlock.Panel2.SuspendLayout();
			this.splitContainerBlock.SuspendLayout();
			this.panelInputs.SuspendLayout();
			this.splitContainerTransNotes.Panel1.SuspendLayout();
			this.splitContainerTransNotes.Panel2.SuspendLayout();
			this.splitContainerTransNotes.SuspendLayout();
			this.splitContainerJapTrans.Panel1.SuspendLayout();
			this.splitContainerJapTrans.Panel2.SuspendLayout();
			this.splitContainerJapTrans.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerChoicesMain
			// 
			this.splitContainerChoicesMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerChoicesMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerChoicesMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerChoicesMain.Name = "splitContainerChoicesMain";
			// 
			// splitContainerChoicesMain.Panel1
			// 
			this.splitContainerChoicesMain.Panel1.Controls.Add(this.custListViewMain);
			// 
			// splitContainerChoicesMain.Panel2
			// 
			this.splitContainerChoicesMain.Panel2.Controls.Add(this.splitContainerBlock);
			this.splitContainerChoicesMain.Size = new System.Drawing.Size(824, 628);
			this.splitContainerChoicesMain.SplitterDistance = 202;
			this.splitContainerChoicesMain.TabIndex = 0;
			// 
			// custListViewMain
			// 
			this.custListViewMain.BackColor = System.Drawing.Color.Gainsboro;
			this.custListViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderMainBlock,
									this.columnHeaderDone,
									this.columnHeaderTotal,
									this.columnHeaderPercent,
									this.columnHeaderNotes});
			this.custListViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.custListViewMain.FullRowSelect = true;
			this.custListViewMain.GridLines = true;
			this.custListViewMain.HideSelection = false;
			this.custListViewMain.Location = new System.Drawing.Point(0, 0);
			this.custListViewMain.MultiSelect = false;
			this.custListViewMain.Name = "custListViewMain";
			this.custListViewMain.Size = new System.Drawing.Size(198, 624);
			this.custListViewMain.SortableColumns = true;
			this.custListViewMain.TabIndex = 0;
			this.custListViewMain.UseCompatibleStateImageBehavior = false;
			this.custListViewMain.UseShowHideColumns = false;
			this.custListViewMain.View = System.Windows.Forms.View.Details;
			this.custListViewMain.SelectedIndexChanged += new System.EventHandler(this.CustListViewMainSelectedIndexChanged);
			// 
			// columnHeaderMainBlock
			// 
			this.columnHeaderMainBlock.Text = "Block";
			this.columnHeaderMainBlock.Width = 58;
			// 
			// columnHeaderDone
			// 
			this.columnHeaderDone.Text = "Done";
			this.columnHeaderDone.Width = 42;
			// 
			// columnHeaderTotal
			// 
			this.columnHeaderTotal.Text = "Total";
			this.columnHeaderTotal.Width = 39;
			// 
			// columnHeaderPercent
			// 
			this.columnHeaderPercent.Text = "%";
			this.columnHeaderPercent.Width = 25;
			// 
			// columnHeaderNotes
			// 
			this.columnHeaderNotes.Text = "Notes";
			this.columnHeaderNotes.Width = 43;
			// 
			// splitContainerBlock
			// 
			this.splitContainerBlock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerBlock.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerBlock.Location = new System.Drawing.Point(0, 0);
			this.splitContainerBlock.Name = "splitContainerBlock";
			// 
			// splitContainerBlock.Panel1
			// 
			this.splitContainerBlock.Panel1.Controls.Add(this.custListViewBlock);
			// 
			// splitContainerBlock.Panel2
			// 
			this.splitContainerBlock.Panel2.Controls.Add(this.panelInputs);
			this.splitContainerBlock.Size = new System.Drawing.Size(618, 628);
			this.splitContainerBlock.SplitterDistance = 177;
			this.splitContainerBlock.TabIndex = 0;
			// 
			// custListViewBlock
			// 
			this.custListViewBlock.BackColor = System.Drawing.Color.Gainsboro;
			this.custListViewBlock.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3,
									this.columnHeader4,
									this.columnHeader5});
			this.custListViewBlock.Dock = System.Windows.Forms.DockStyle.Fill;
			this.custListViewBlock.FullRowSelect = true;
			this.custListViewBlock.GridLines = true;
			this.custListViewBlock.HideSelection = false;
			this.custListViewBlock.Location = new System.Drawing.Point(0, 0);
			this.custListViewBlock.MultiSelect = false;
			this.custListViewBlock.Name = "custListViewBlock";
			this.custListViewBlock.Size = new System.Drawing.Size(173, 624);
			this.custListViewBlock.SortableColumns = true;
			this.custListViewBlock.TabIndex = 1;
			this.custListViewBlock.UseCompatibleStateImageBehavior = false;
			this.custListViewBlock.UseShowHideColumns = false;
			this.custListViewBlock.View = System.Windows.Forms.View.Details;
			this.custListViewBlock.SelectedIndexChanged += new System.EventHandler(this.CustListViewBlockSelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Block";
			this.columnHeader1.Width = 58;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Done";
			this.columnHeader2.Width = 42;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Total";
			this.columnHeader3.Width = 39;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "%";
			this.columnHeader4.Width = 30;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Notes";
			this.columnHeader5.Width = 43;
			// 
			// panelInputs
			// 
			this.panelInputs.Controls.Add(this.splitContainerTransNotes);
			this.panelInputs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelInputs.Location = new System.Drawing.Point(0, 0);
			this.panelInputs.Name = "panelInputs";
			this.panelInputs.Size = new System.Drawing.Size(433, 624);
			this.panelInputs.TabIndex = 0;
			// 
			// splitContainerTransNotes
			// 
			this.splitContainerTransNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerTransNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerTransNotes.Location = new System.Drawing.Point(0, 0);
			this.splitContainerTransNotes.Name = "splitContainerTransNotes";
			this.splitContainerTransNotes.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerTransNotes.Panel1
			// 
			this.splitContainerTransNotes.Panel1.Controls.Add(this.splitContainerJapTrans);
			// 
			// splitContainerTransNotes.Panel2
			// 
			this.splitContainerTransNotes.Panel2.Controls.Add(this.groupBox3);
			this.splitContainerTransNotes.Size = new System.Drawing.Size(433, 624);
			this.splitContainerTransNotes.SplitterDistance = 421;
			this.splitContainerTransNotes.TabIndex = 20;
			// 
			// splitContainerJapTrans
			// 
			this.splitContainerJapTrans.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerJapTrans.Location = new System.Drawing.Point(0, 0);
			this.splitContainerJapTrans.Name = "splitContainerJapTrans";
			this.splitContainerJapTrans.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerJapTrans.Panel1
			// 
			this.splitContainerJapTrans.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainerJapTrans.Panel2
			// 
			this.splitContainerJapTrans.Panel2.Controls.Add(this.groupBox2);
			this.splitContainerJapTrans.Size = new System.Drawing.Size(429, 417);
			this.splitContainerJapTrans.SplitterDistance = 208;
			this.splitContainerJapTrans.TabIndex = 23;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.panelChoice1);
			this.groupBox1.Controls.Add(this.panelChoice2);
			this.groupBox1.Controls.Add(this.panelChoice3);
			this.groupBox1.Controls.Add(this.panelChoice4);
			this.groupBox1.Controls.Add(this.panelChoice5);
			this.groupBox1.Controls.Add(this.textBoxChoices1);
			this.groupBox1.Controls.Add(this.textBoxChoices2);
			this.groupBox1.Controls.Add(this.textBoxChoices3);
			this.groupBox1.Controls.Add(this.textBoxChoices4);
			this.groupBox1.Controls.Add(this.textBoxChoices5);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(390, 249);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Original";
			// 
			// panelChoice1
			// 
			this.panelChoice1.Location = new System.Drawing.Point(6, 16);
			this.panelChoice1.Name = "panelChoice1";
			this.panelChoice1.Size = new System.Drawing.Size(252, 14);
			this.panelChoice1.TabIndex = 0;
			this.panelChoice1.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsJapPaint);
			// 
			// panelChoice2
			// 
			this.panelChoice2.Location = new System.Drawing.Point(6, 36);
			this.panelChoice2.Name = "panelChoice2";
			this.panelChoice2.Size = new System.Drawing.Size(252, 14);
			this.panelChoice2.TabIndex = 1;
			this.panelChoice2.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsJapPaint);
			// 
			// panelChoice3
			// 
			this.panelChoice3.Location = new System.Drawing.Point(6, 56);
			this.panelChoice3.Name = "panelChoice3";
			this.panelChoice3.Size = new System.Drawing.Size(252, 14);
			this.panelChoice3.TabIndex = 2;
			this.panelChoice3.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsJapPaint);
			// 
			// panelChoice4
			// 
			this.panelChoice4.Location = new System.Drawing.Point(6, 76);
			this.panelChoice4.Name = "panelChoice4";
			this.panelChoice4.Size = new System.Drawing.Size(252, 14);
			this.panelChoice4.TabIndex = 3;
			this.panelChoice4.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsJapPaint);
			// 
			// panelChoice5
			// 
			this.panelChoice5.Location = new System.Drawing.Point(6, 96);
			this.panelChoice5.Name = "panelChoice5";
			this.panelChoice5.Size = new System.Drawing.Size(252, 14);
			this.panelChoice5.TabIndex = 4;
			this.panelChoice5.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsJapPaint);
			// 
			// textBoxChoices1
			// 
			this.textBoxChoices1.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxChoices1.Location = new System.Drawing.Point(6, 116);
			this.textBoxChoices1.Name = "textBoxChoices1";
			this.textBoxChoices1.ReadOnly = true;
			this.textBoxChoices1.Size = new System.Drawing.Size(376, 23);
			this.textBoxChoices1.TabIndex = 5;
			// 
			// textBoxChoices2
			// 
			this.textBoxChoices2.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxChoices2.Location = new System.Drawing.Point(6, 142);
			this.textBoxChoices2.Name = "textBoxChoices2";
			this.textBoxChoices2.ReadOnly = true;
			this.textBoxChoices2.Size = new System.Drawing.Size(376, 23);
			this.textBoxChoices2.TabIndex = 6;
			// 
			// textBoxChoices3
			// 
			this.textBoxChoices3.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxChoices3.Location = new System.Drawing.Point(6, 168);
			this.textBoxChoices3.Name = "textBoxChoices3";
			this.textBoxChoices3.ReadOnly = true;
			this.textBoxChoices3.Size = new System.Drawing.Size(376, 23);
			this.textBoxChoices3.TabIndex = 7;
			// 
			// textBoxChoices4
			// 
			this.textBoxChoices4.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxChoices4.Location = new System.Drawing.Point(6, 194);
			this.textBoxChoices4.Name = "textBoxChoices4";
			this.textBoxChoices4.ReadOnly = true;
			this.textBoxChoices4.Size = new System.Drawing.Size(376, 23);
			this.textBoxChoices4.TabIndex = 8;
			// 
			// textBoxChoices5
			// 
			this.textBoxChoices5.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxChoices5.Location = new System.Drawing.Point(6, 220);
			this.textBoxChoices5.Name = "textBoxChoices5";
			this.textBoxChoices5.ReadOnly = true;
			this.textBoxChoices5.Size = new System.Drawing.Size(376, 23);
			this.textBoxChoices5.TabIndex = 9;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.panelEngChoice1);
			this.groupBox2.Controls.Add(this.panelEngChoice2);
			this.groupBox2.Controls.Add(this.panelEngChoice3);
			this.groupBox2.Controls.Add(this.textBoxEngChoices5);
			this.groupBox2.Controls.Add(this.panelEngChoice4);
			this.groupBox2.Controls.Add(this.textBoxEngChoices4);
			this.groupBox2.Controls.Add(this.panelEngChoice5);
			this.groupBox2.Controls.Add(this.textBoxEngChoices3);
			this.groupBox2.Controls.Add(this.textBoxEngChoices1);
			this.groupBox2.Controls.Add(this.textBoxEngChoices2);
			this.groupBox2.Location = new System.Drawing.Point(3, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(390, 250);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Translation";
			// 
			// panelEngChoice1
			// 
			this.panelEngChoice1.Location = new System.Drawing.Point(6, 19);
			this.panelEngChoice1.Name = "panelEngChoice1";
			this.panelEngChoice1.Size = new System.Drawing.Size(252, 14);
			this.panelEngChoice1.TabIndex = 10;
			this.panelEngChoice1.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsEngPaint);
			// 
			// panelEngChoice2
			// 
			this.panelEngChoice2.Location = new System.Drawing.Point(6, 39);
			this.panelEngChoice2.Name = "panelEngChoice2";
			this.panelEngChoice2.Size = new System.Drawing.Size(252, 14);
			this.panelEngChoice2.TabIndex = 11;
			this.panelEngChoice2.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsEngPaint);
			// 
			// panelEngChoice3
			// 
			this.panelEngChoice3.Location = new System.Drawing.Point(6, 59);
			this.panelEngChoice3.Name = "panelEngChoice3";
			this.panelEngChoice3.Size = new System.Drawing.Size(252, 14);
			this.panelEngChoice3.TabIndex = 12;
			this.panelEngChoice3.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsEngPaint);
			// 
			// textBoxEngChoices5
			// 
			this.textBoxEngChoices5.Location = new System.Drawing.Point(6, 223);
			this.textBoxEngChoices5.Name = "textBoxEngChoices5";
			this.textBoxEngChoices5.Size = new System.Drawing.Size(376, 20);
			this.textBoxEngChoices5.TabIndex = 19;
			this.textBoxEngChoices5.TextChanged += new System.EventHandler(this.TextBoxesEngTextChanged);
			// 
			// panelEngChoice4
			// 
			this.panelEngChoice4.Location = new System.Drawing.Point(6, 79);
			this.panelEngChoice4.Name = "panelEngChoice4";
			this.panelEngChoice4.Size = new System.Drawing.Size(252, 14);
			this.panelEngChoice4.TabIndex = 13;
			this.panelEngChoice4.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsEngPaint);
			// 
			// textBoxEngChoices4
			// 
			this.textBoxEngChoices4.Location = new System.Drawing.Point(6, 197);
			this.textBoxEngChoices4.Name = "textBoxEngChoices4";
			this.textBoxEngChoices4.Size = new System.Drawing.Size(376, 20);
			this.textBoxEngChoices4.TabIndex = 18;
			this.textBoxEngChoices4.TextChanged += new System.EventHandler(this.TextBoxesEngTextChanged);
			// 
			// panelEngChoice5
			// 
			this.panelEngChoice5.Location = new System.Drawing.Point(6, 99);
			this.panelEngChoice5.Name = "panelEngChoice5";
			this.panelEngChoice5.Size = new System.Drawing.Size(252, 14);
			this.panelEngChoice5.TabIndex = 14;
			this.panelEngChoice5.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsEngPaint);
			// 
			// textBoxEngChoices3
			// 
			this.textBoxEngChoices3.Location = new System.Drawing.Point(6, 171);
			this.textBoxEngChoices3.Name = "textBoxEngChoices3";
			this.textBoxEngChoices3.Size = new System.Drawing.Size(376, 20);
			this.textBoxEngChoices3.TabIndex = 17;
			this.textBoxEngChoices3.TextChanged += new System.EventHandler(this.TextBoxesEngTextChanged);
			// 
			// textBoxEngChoices1
			// 
			this.textBoxEngChoices1.Location = new System.Drawing.Point(6, 119);
			this.textBoxEngChoices1.Name = "textBoxEngChoices1";
			this.textBoxEngChoices1.Size = new System.Drawing.Size(376, 20);
			this.textBoxEngChoices1.TabIndex = 15;
			this.textBoxEngChoices1.TextChanged += new System.EventHandler(this.TextBoxesEngTextChanged);
			// 
			// textBoxEngChoices2
			// 
			this.textBoxEngChoices2.Location = new System.Drawing.Point(6, 145);
			this.textBoxEngChoices2.Name = "textBoxEngChoices2";
			this.textBoxEngChoices2.Size = new System.Drawing.Size(376, 20);
			this.textBoxEngChoices2.TabIndex = 16;
			this.textBoxEngChoices2.TextChanged += new System.EventHandler(this.TextBoxesEngTextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBoxTransNotes);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(429, 195);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Notes";
			// 
			// textBoxTransNotes
			// 
			this.textBoxTransNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxTransNotes.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBoxTransNotes.Location = new System.Drawing.Point(3, 16);
			this.textBoxTransNotes.Multiline = true;
			this.textBoxTransNotes.Name = "textBoxTransNotes";
			this.textBoxTransNotes.Size = new System.Drawing.Size(423, 176);
			this.textBoxTransNotes.TabIndex = 0;
			this.textBoxTransNotes.TextChanged += new System.EventHandler(this.TextBoxTransNotesTextChanged);
			// 
			// DialogueChoicesControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerChoicesMain);
			this.Name = "DialogueChoicesControl";
			this.Size = new System.Drawing.Size(824, 628);
			this.splitContainerChoicesMain.Panel1.ResumeLayout(false);
			this.splitContainerChoicesMain.Panel2.ResumeLayout(false);
			this.splitContainerChoicesMain.ResumeLayout(false);
			this.splitContainerBlock.Panel1.ResumeLayout(false);
			this.splitContainerBlock.Panel2.ResumeLayout(false);
			this.splitContainerBlock.ResumeLayout(false);
			this.panelInputs.ResumeLayout(false);
			this.splitContainerTransNotes.Panel1.ResumeLayout(false);
			this.splitContainerTransNotes.Panel2.ResumeLayout(false);
			this.splitContainerTransNotes.ResumeLayout(false);
			this.splitContainerJapTrans.Panel1.ResumeLayout(false);
			this.splitContainerJapTrans.Panel2.ResumeLayout(false);
			this.splitContainerJapTrans.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TextBox textBoxTransNotes;
		private System.Windows.Forms.SplitContainer splitContainerJapTrans;
		private System.Windows.Forms.SplitContainer splitContainerTransNotes;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeaderNotes;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBoxEngChoices5;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelEngChoice1;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelEngChoice2;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelEngChoice3;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelEngChoice4;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelEngChoice5;
		private System.Windows.Forms.TextBox textBoxEngChoices1;
		private System.Windows.Forms.TextBox textBoxEngChoices2;
		private System.Windows.Forms.TextBox textBoxEngChoices3;
		private System.Windows.Forms.TextBox textBoxEngChoices4;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelChoice1;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelChoice2;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelChoice3;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelChoice4;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelChoice5;
		private System.Windows.Forms.TextBox textBoxChoices1;
		private System.Windows.Forms.TextBox textBoxChoices2;
		private System.Windows.Forms.TextBox textBoxChoices3;
		private System.Windows.Forms.TextBox textBoxChoices4;
		private System.Windows.Forms.TextBox textBoxChoices5;
		private System.Windows.Forms.Panel panelInputs;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeaderPercent;
		private System.Windows.Forms.ColumnHeader columnHeaderTotal;
		private System.Windows.Forms.ColumnHeader columnHeaderDone;
		private System.Windows.Forms.ColumnHeader columnHeaderMainBlock;
		private AtelierElieScripter.Lib.CustListView custListViewBlock;
		private System.Windows.Forms.SplitContainer splitContainerBlock;
		private AtelierElieScripter.Lib.CustListView custListViewMain;
		private System.Windows.Forms.SplitContainer splitContainerChoicesMain;
	}
}
