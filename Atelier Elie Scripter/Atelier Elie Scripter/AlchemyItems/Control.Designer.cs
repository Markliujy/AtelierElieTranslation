/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/04/2010
 * Time: 12:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AtelierElieScripter.AlchemyItems
{
	partial class Control
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
			this.components = new System.ComponentModel.Container();
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.custListViewItemSelection = new AtelierElieScripter.Lib.CustListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.splitContainerNameDesc = new System.Windows.Forms.SplitContainer();
			this.splitContainerNameNotes = new System.Windows.Forms.SplitContainer();
			this.groupBoxName = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBoxPic = new System.Windows.Forms.PictureBox();
			this.panelNameTrans = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxNameTrans = new System.Windows.Forms.TextBox();
			this.textBoxNameOrig = new System.Windows.Forms.TextBox();
			this.panelNameOrig = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.pictureBoxName = new System.Windows.Forms.PictureBox();
			this.groupBoxNotes = new System.Windows.Forms.GroupBox();
			this.textBoxNotes = new System.Windows.Forms.TextBox();
			this.splitContainerDesc = new System.Windows.Forms.SplitContainer();
			this.groupBoxDescOrig = new System.Windows.Forms.GroupBox();
			this.panelDescOrig = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxDescJap = new System.Windows.Forms.TextBox();
			this.groupBoxDescTrans = new System.Windows.Forms.GroupBox();
			this.panelDescTrans = new AtelierElieScripter.Lib.NoBackgroundPaintPanel();
			this.textBoxDescTrans = new System.Windows.Forms.TextBox();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.splitContainerNameDesc.Panel1.SuspendLayout();
			this.splitContainerNameDesc.Panel2.SuspendLayout();
			this.splitContainerNameDesc.SuspendLayout();
			this.splitContainerNameNotes.Panel1.SuspendLayout();
			this.splitContainerNameNotes.Panel2.SuspendLayout();
			this.splitContainerNameNotes.SuspendLayout();
			this.groupBoxName.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPic)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxName)).BeginInit();
			this.groupBoxNotes.SuspendLayout();
			this.splitContainerDesc.Panel1.SuspendLayout();
			this.splitContainerDesc.Panel2.SuspendLayout();
			this.splitContainerDesc.SuspendLayout();
			this.groupBoxDescOrig.SuspendLayout();
			this.groupBoxDescTrans.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMain.Name = "splitContainerMain";
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.custListViewItemSelection);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.splitContainerNameDesc);
			this.splitContainerMain.Panel2MinSize = 0;
			this.splitContainerMain.Size = new System.Drawing.Size(764, 656);
			this.splitContainerMain.SplitterDistance = 222;
			this.splitContainerMain.TabIndex = 0;
			// 
			// custListViewItemSelection
			// 
			this.custListViewItemSelection.BackColor = System.Drawing.Color.Gainsboro;
			this.custListViewItemSelection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3,
									this.columnHeader4,
									this.columnHeader5,
									this.columnHeader6});
			this.custListViewItemSelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.custListViewItemSelection.FullRowSelect = true;
			this.custListViewItemSelection.GridLines = true;
			this.custListViewItemSelection.HideSelection = false;
			this.custListViewItemSelection.Location = new System.Drawing.Point(0, 0);
			this.custListViewItemSelection.MultiSelect = false;
			this.custListViewItemSelection.Name = "custListViewItemSelection";
			this.custListViewItemSelection.Size = new System.Drawing.Size(218, 652);
			this.custListViewItemSelection.SortableColumns = false;
			this.custListViewItemSelection.TabIndex = 0;
			this.custListViewItemSelection.UseCompatibleStateImageBehavior = false;
			this.custListViewItemSelection.UseShowHideColumns = true;
			this.custListViewItemSelection.View = System.Windows.Forms.View.Details;
			this.custListViewItemSelection.SelectedIndexChanged += new System.EventHandler(this.CustListViewItemSelectionSelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "No";
			this.columnHeader1.Width = 80;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Jap";
			this.columnHeader2.Width = 85;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Eng";
			this.columnHeader3.Width = 35;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Jap Desc";
			this.columnHeader4.Width = 85;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Eng Desc";
			this.columnHeader5.Width = 74;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Notes";
			this.columnHeader6.Width = 40;
			// 
			// splitContainerNameDesc
			// 
			this.splitContainerNameDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerNameDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerNameDesc.Location = new System.Drawing.Point(0, 0);
			this.splitContainerNameDesc.Name = "splitContainerNameDesc";
			// 
			// splitContainerNameDesc.Panel1
			// 
			this.splitContainerNameDesc.Panel1.AutoScroll = true;
			this.splitContainerNameDesc.Panel1.AutoScrollMinSize = new System.Drawing.Size(200, 0);
			this.splitContainerNameDesc.Panel1.Controls.Add(this.splitContainerNameNotes);
			// 
			// splitContainerNameDesc.Panel2
			// 
			this.splitContainerNameDesc.Panel2.AutoScroll = true;
			this.splitContainerNameDesc.Panel2.AutoScrollMinSize = new System.Drawing.Size(320, 0);
			this.splitContainerNameDesc.Panel2.Controls.Add(this.splitContainerDesc);
			this.splitContainerNameDesc.Size = new System.Drawing.Size(538, 656);
			this.splitContainerNameDesc.SplitterDistance = 215;
			this.splitContainerNameDesc.TabIndex = 0;
			// 
			// splitContainerNameNotes
			// 
			this.splitContainerNameNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerNameNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerNameNotes.Location = new System.Drawing.Point(0, 0);
			this.splitContainerNameNotes.Name = "splitContainerNameNotes";
			this.splitContainerNameNotes.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerNameNotes.Panel1
			// 
			this.splitContainerNameNotes.Panel1.Controls.Add(this.groupBoxName);
			this.splitContainerNameNotes.Panel1MinSize = 0;
			// 
			// splitContainerNameNotes.Panel2
			// 
			this.splitContainerNameNotes.Panel2.Controls.Add(this.groupBoxNotes);
			this.splitContainerNameNotes.Panel2MinSize = 0;
			this.splitContainerNameNotes.Size = new System.Drawing.Size(215, 656);
			this.splitContainerNameNotes.SplitterDistance = 538;
			this.splitContainerNameNotes.TabIndex = 1;
			// 
			// groupBoxName
			// 
			this.groupBoxName.Controls.Add(this.tableLayoutPanel1);
			this.groupBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxName.Location = new System.Drawing.Point(0, 0);
			this.groupBoxName.Name = "groupBoxName";
			this.groupBoxName.Size = new System.Drawing.Size(211, 534);
			this.groupBoxName.TabIndex = 0;
			this.groupBoxName.TabStop = false;
			this.groupBoxName.Text = "Name";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.pictureBoxPic, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panelNameTrans, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.textBoxNameTrans, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.textBoxNameOrig, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panelNameOrig, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.pictureBoxName, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(205, 515);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// pictureBoxPic
			// 
			this.pictureBoxPic.Location = new System.Drawing.Point(57, 3);
			this.pictureBoxPic.Name = "pictureBoxPic";
			this.pictureBoxPic.Size = new System.Drawing.Size(48, 48);
			this.pictureBoxPic.TabIndex = 7;
			this.pictureBoxPic.TabStop = false;
			// 
			// panelNameTrans
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panelNameTrans, 2);
			this.panelNameTrans.Location = new System.Drawing.Point(3, 157);
			this.panelNameTrans.Name = "panelNameTrans";
			this.panelNameTrans.Size = new System.Drawing.Size(199, 34);
			this.panelNameTrans.TabIndex = 5;
			this.panelNameTrans.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsPaint);
			// 
			// textBoxNameTrans
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxNameTrans, 2);
			this.textBoxNameTrans.Location = new System.Drawing.Point(3, 127);
			this.textBoxNameTrans.Name = "textBoxNameTrans";
			this.textBoxNameTrans.Size = new System.Drawing.Size(199, 20);
			this.textBoxNameTrans.TabIndex = 4;
			this.textBoxNameTrans.TextChanged += new System.EventHandler(this.TextBoxNameTransTextChanged);
			// 
			// textBoxNameOrig
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxNameOrig, 2);
			this.textBoxNameOrig.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxNameOrig.Location = new System.Drawing.Point(3, 57);
			this.textBoxNameOrig.Name = "textBoxNameOrig";
			this.textBoxNameOrig.ReadOnly = true;
			this.textBoxNameOrig.Size = new System.Drawing.Size(199, 23);
			this.textBoxNameOrig.TabIndex = 2;
			// 
			// panelNameOrig
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panelNameOrig, 2);
			this.panelNameOrig.Location = new System.Drawing.Point(3, 87);
			this.panelNameOrig.Name = "panelNameOrig";
			this.panelNameOrig.Size = new System.Drawing.Size(199, 34);
			this.panelNameOrig.TabIndex = 3;
			this.panelNameOrig.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsPaint);
			// 
			// pictureBoxName
			// 
			this.pictureBoxName.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxName.Name = "pictureBoxName";
			this.pictureBoxName.Size = new System.Drawing.Size(48, 16);
			this.pictureBoxName.TabIndex = 6;
			this.pictureBoxName.TabStop = false;
			// 
			// groupBoxNotes
			// 
			this.groupBoxNotes.Controls.Add(this.textBoxNotes);
			this.groupBoxNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxNotes.Location = new System.Drawing.Point(0, 0);
			this.groupBoxNotes.Name = "groupBoxNotes";
			this.groupBoxNotes.Size = new System.Drawing.Size(211, 110);
			this.groupBoxNotes.TabIndex = 0;
			this.groupBoxNotes.TabStop = false;
			this.groupBoxNotes.Text = "Notes";
			// 
			// textBoxNotes
			// 
			this.textBoxNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxNotes.Location = new System.Drawing.Point(3, 16);
			this.textBoxNotes.Multiline = true;
			this.textBoxNotes.Name = "textBoxNotes";
			this.textBoxNotes.Size = new System.Drawing.Size(205, 91);
			this.textBoxNotes.TabIndex = 0;
			this.textBoxNotes.TextChanged += new System.EventHandler(this.TextBoxNotesTextChanged);
			// 
			// splitContainerDesc
			// 
			this.splitContainerDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainerDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerDesc.Location = new System.Drawing.Point(0, 0);
			this.splitContainerDesc.Name = "splitContainerDesc";
			this.splitContainerDesc.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerDesc.Panel1
			// 
			this.splitContainerDesc.Panel1.Controls.Add(this.groupBoxDescOrig);
			this.splitContainerDesc.Panel1MinSize = 0;
			// 
			// splitContainerDesc.Panel2
			// 
			this.splitContainerDesc.Panel2.Controls.Add(this.groupBoxDescTrans);
			this.splitContainerDesc.Panel2MinSize = 0;
			this.splitContainerDesc.Size = new System.Drawing.Size(320, 639);
			this.splitContainerDesc.SplitterDistance = 321;
			this.splitContainerDesc.TabIndex = 1;
			// 
			// groupBoxDescOrig
			// 
			this.groupBoxDescOrig.Controls.Add(this.panelDescOrig);
			this.groupBoxDescOrig.Controls.Add(this.textBoxDescJap);
			this.groupBoxDescOrig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxDescOrig.Location = new System.Drawing.Point(0, 0);
			this.groupBoxDescOrig.Name = "groupBoxDescOrig";
			this.groupBoxDescOrig.Size = new System.Drawing.Size(316, 317);
			this.groupBoxDescOrig.TabIndex = 2;
			this.groupBoxDescOrig.TabStop = false;
			this.groupBoxDescOrig.Text = "Original Description";
			// 
			// panelDescOrig
			// 
			this.panelDescOrig.Location = new System.Drawing.Point(6, 125);
			this.panelDescOrig.Name = "panelDescOrig";
			this.panelDescOrig.Size = new System.Drawing.Size(300, 100);
			this.panelDescOrig.TabIndex = 6;
			this.panelDescOrig.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsPaint);
			// 
			// textBoxDescJap
			// 
			this.textBoxDescJap.AcceptsReturn = true;
			this.textBoxDescJap.Font = new System.Drawing.Font("MS Mincho", 12F);
			this.textBoxDescJap.Location = new System.Drawing.Point(6, 19);
			this.textBoxDescJap.Multiline = true;
			this.textBoxDescJap.Name = "textBoxDescJap";
			this.textBoxDescJap.ReadOnly = true;
			this.textBoxDescJap.Size = new System.Drawing.Size(275, 100);
			this.textBoxDescJap.TabIndex = 0;
			// 
			// groupBoxDescTrans
			// 
			this.groupBoxDescTrans.Controls.Add(this.panelDescTrans);
			this.groupBoxDescTrans.Controls.Add(this.textBoxDescTrans);
			this.groupBoxDescTrans.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxDescTrans.Location = new System.Drawing.Point(0, 0);
			this.groupBoxDescTrans.Name = "groupBoxDescTrans";
			this.groupBoxDescTrans.Size = new System.Drawing.Size(316, 310);
			this.groupBoxDescTrans.TabIndex = 3;
			this.groupBoxDescTrans.TabStop = false;
			this.groupBoxDescTrans.Text = "Translated Description";
			// 
			// panelDescTrans
			// 
			this.panelDescTrans.Location = new System.Drawing.Point(6, 125);
			this.panelDescTrans.Name = "panelDescTrans";
			this.panelDescTrans.Size = new System.Drawing.Size(300, 100);
			this.panelDescTrans.TabIndex = 6;
			this.panelDescTrans.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelsPaint);
			// 
			// textBoxDescTrans
			// 
			this.textBoxDescTrans.AcceptsReturn = true;
			this.textBoxDescTrans.Location = new System.Drawing.Point(6, 19);
			this.textBoxDescTrans.Multiline = true;
			this.textBoxDescTrans.Name = "textBoxDescTrans";
			this.textBoxDescTrans.Size = new System.Drawing.Size(300, 100);
			this.textBoxDescTrans.TabIndex = 0;
			this.textBoxDescTrans.TextChanged += new System.EventHandler(this.TextBoxDescTransTextChanged);
			// 
			// Control
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerMain);
			this.Name = "Control";
			this.Size = new System.Drawing.Size(764, 656);
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel2.ResumeLayout(false);
			this.splitContainerMain.ResumeLayout(false);
			this.splitContainerNameDesc.Panel1.ResumeLayout(false);
			this.splitContainerNameDesc.Panel2.ResumeLayout(false);
			this.splitContainerNameDesc.ResumeLayout(false);
			this.splitContainerNameNotes.Panel1.ResumeLayout(false);
			this.splitContainerNameNotes.Panel2.ResumeLayout(false);
			this.splitContainerNameNotes.ResumeLayout(false);
			this.groupBoxName.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPic)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxName)).EndInit();
			this.groupBoxNotes.ResumeLayout(false);
			this.groupBoxNotes.PerformLayout();
			this.splitContainerDesc.Panel1.ResumeLayout(false);
			this.splitContainerDesc.Panel2.ResumeLayout(false);
			this.splitContainerDesc.ResumeLayout(false);
			this.groupBoxDescOrig.ResumeLayout(false);
			this.groupBoxDescOrig.PerformLayout();
			this.groupBoxDescTrans.ResumeLayout(false);
			this.groupBoxDescTrans.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.PictureBox pictureBoxName;
		private System.Windows.Forms.PictureBox pictureBoxPic;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelNameOrig;
		private System.Windows.Forms.TextBox textBoxNameTrans;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelNameTrans;
		private System.Windows.Forms.TextBox textBoxNameOrig;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBoxName;
		private System.Windows.Forms.TextBox textBoxDescTrans;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelDescTrans;
		private System.Windows.Forms.GroupBox groupBoxDescTrans;
		private AtelierElieScripter.Lib.NoBackgroundPaintPanel panelDescOrig;
		private System.Windows.Forms.SplitContainer splitContainerDesc;
		private System.Windows.Forms.GroupBox groupBoxDescOrig;
		private System.Windows.Forms.TextBox textBoxDescJap;
		private System.Windows.Forms.SplitContainer splitContainerNameNotes;
		private System.Windows.Forms.SplitContainer splitContainerNameDesc;
		private System.Windows.Forms.TextBox textBoxNotes;
		private System.Windows.Forms.GroupBox groupBoxNotes;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.SplitContainer splitContainerMain;
		private AtelierElieScripter.Lib.CustListView custListViewItemSelection;
	}
}
