/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/04/2010
 * Time: 12:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace AtelierElieScripter.AlchemyItems
{
	/// <summary>
	/// Description of AlchemyItemsControl.
	/// </summary>
	public partial class Control : UserControl
	{
		Module controlModule;
		
		#region Initialization
		public Control()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// Initialize module
			controlModule = new Module();
			
			// Initialize custom list view
			custListViewItemSelection.BufferUseCurrentHeaders();
			InitializeSelections();
			
			// Hide columns for performance
			custListViewItemSelection.ShowHideColumn(3);
			custListViewItemSelection.ShowHideColumn(4);
			custListViewItemSelection.ShowHideColumn(5);
			
			custListViewItemSelection.BufferUpdate();
			custListViewItemSelection.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			DisableInputs();
			
		}
		
		/// <summary>
		/// Initialize Listview's values
		/// </summary>
		void InitializeSelections()
		{
			custListViewItemSelection.BufferItems.Clear();
			ListViewItem listItem;
			for (int i = 0; i < controlModule.Items.Count; i++)
			{
				string textJap = controlModule.Items[i].textJap;
				string textEng = controlModule.Items[i].textEng;
				string textDescJap = controlModule.Items[i].textDescJap;
				string textDescEng = controlModule.Items[i].textDescEng;
				string textNotes = controlModule.Items[i].textNote;
				
				listItem = new ListViewItem(new string[]{i.ToString(),
				                            textJap,
				                            textEng,
				                            textDescJap,
				                            textDescEng,
				                            textNotes
				                            });
				listItem.BackColor = Color.Gainsboro;
				custListViewItemSelection.BufferItems.Add(listItem);
			}
		}
			
		#endregion
		
		#region Event Handling
		/// <summary>
		/// List view's item selected has changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CustListViewItemSelectionSelectedIndexChanged(object sender, EventArgs e)
		{
			if (custListViewItemSelection.SelectedIndices.Count > 0)
			{
				int itemNo = Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text);
				Item itemEntry = controlModule.Items[itemNo];
				
				System.Resources.ResourceManager res = new System.Resources.ResourceManager(
				"AtelierElieScripter.Res.AlchemyItems", 
				System.Reflection.Assembly.GetExecutingAssembly()
				);
				
				pictureBoxName.Image = (Bitmap)res.GetObject(String.Format("{0:000}_1", itemNo));
				pictureBoxPic.Image = (Bitmap)res.GetObject(String.Format("{0:000}_0", itemNo));
				
				textBoxDescJap.Enabled = true;
				textBoxDescTrans.Enabled = true;
				textBoxNameOrig.Enabled = true;
				textBoxNameTrans.Enabled = true;
				textBoxNotes.Enabled = true;
				
				textBoxDescJap.Text = itemEntry.textDescJap;
				textBoxDescTrans.Text = itemEntry.textDescEng.Replace("\\n", Environment.NewLine);
				textBoxNameOrig.Text = itemEntry.textJap;
				textBoxNameTrans.Text = itemEntry.textEng;
				textBoxNotes.Text = itemEntry.textNote;
				
				panelDescOrig.InvalidateEx();
				panelDescTrans.InvalidateEx();
				panelNameOrig.InvalidateEx();
				panelNameTrans.InvalidateEx();
			}
			else
			{
				DisableInputs();
			}
		}
				
		/// <summary>
		/// Paint panels
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PanelsPaint(object sender, PaintEventArgs e)
		{
			if (custListViewItemSelection.SelectedIndices.Count > 0)
			{
				
				
				int itemNo = Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text);
				Item itemEntry = controlModule.Items[itemNo];
				
				Abstract.IFontResourceObject resobject;
				
				if (sender == panelDescOrig || sender == panelNameOrig)
					resobject = ResourceObjects.JapFontResourceObject.Instance;
				else
					resobject = ResourceObjects.EngFontResourceObject.Instance;
				
				
				
				string text = string.Empty;
				if (sender == panelDescOrig)
					text = itemEntry.textDescJap;
				else if (sender == panelDescTrans)
					text = itemEntry.textDescEng;
				else if (sender == panelNameOrig)
					text = itemEntry.textJap;
				else if (sender == panelNameTrans)
					text = itemEntry.textEng;
				Bitmap buffer;
				if (sender == panelDescOrig || sender == panelDescTrans)
					buffer = resobject.GetTextBitmap(text,
					                                        CONSTANTS.ALCHEMYITEMS.DESC_MAXWIDTH, 
					                                        CONSTANTS.ALCHEMYITEMS.DESC_MAXLINES,  
					                                        CONSTANTS.ALCHEMYITEMS.DESC_LINESPACING);
				else
					buffer = resobject.GetTextBitmap(text,
					                                        CONSTANTS.ALCHEMYITEMS.NAME_MAXWIDTH, 
					                                        CONSTANTS.ALCHEMYITEMS.NAME_MAXLINES,  
					                                        CONSTANTS.ALCHEMYITEMS.NAME_LINESPACING);
				
				//Draws buffer
				Graphics g = e.Graphics;
		
				Point rect = new Point(0, 0);
				g.DrawImage(buffer, rect);
			}
		}
		
		void TextBoxDescTransTextChanged(object sender, EventArgs e)
		{
			if (custListViewItemSelection.SelectedIndices.Count > 0)
			{
				int itemNo = Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text);
				Item itemEntry = controlModule.Items[itemNo];
				
				itemEntry.textDescEng = textBoxDescTrans.Text.Replace(Environment.NewLine, "\\n");
				
				
				custListViewItemSelection.BufferItems[itemNo].SubItems[4].Text = itemEntry.textDescEng.Replace("\\n", " ");
				custListViewItemSelection.BufferUpdateSortedRow(Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text),
				                                                custListViewItemSelection.SelectedItems[0].Index);
				
				panelDescTrans.InvalidateEx();
			}
		}
		
		void TextBoxNameTransTextChanged(object sender, EventArgs e)
		{
			if (custListViewItemSelection.SelectedIndices.Count > 0)
			{
				int itemNo = Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text);
				Item itemEntry = controlModule.Items[itemNo];
				
				itemEntry.textEng = textBoxNameTrans.Text.Replace(Environment.NewLine, "\\n");
				
				
				custListViewItemSelection.BufferItems[itemNo].SubItems[2].Text = itemEntry.textEng.Replace("\\n", " ");
				custListViewItemSelection.BufferUpdateSortedRow(Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text),
				                                                custListViewItemSelection.SelectedItems[0].Index);
				panelNameTrans.InvalidateEx();
			}
		}
		
		void TextBoxNotesTextChanged(object sender, EventArgs e)
		{
			if (custListViewItemSelection.SelectedIndices.Count > 0)
			{
				int itemNo = Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text);
				Item itemEntry = controlModule.Items[itemNo];
				
				itemEntry.textNote = textBoxNotes.Text.Replace(Environment.NewLine, "\\n");
				
				
				custListViewItemSelection.BufferItems[itemNo].SubItems[5].Text = itemEntry.textNote.Replace("\\n", " ");
				custListViewItemSelection.BufferUpdateSortedRow(Int32.Parse(custListViewItemSelection.Items[custListViewItemSelection.SelectedIndices[0]].Text),
				                                                custListViewItemSelection.SelectedItems[0].Index);
			}
		}
		#endregion
		
		#region Misc. Functions
		/// <summary>
		/// Disable inputs
		/// </summary>
		void DisableInputs()
		{
			textBoxDescJap.Enabled = false;
			textBoxDescTrans.Enabled = false;
			textBoxNameOrig.Enabled = false;
			textBoxNameTrans.Enabled = false;
			textBoxNotes.Enabled = false;
			
			textBoxDescJap.Text = String.Empty;
			textBoxDescTrans.Text = String.Empty;
			textBoxNameOrig.Text = String.Empty;
			textBoxNameTrans.Text = String.Empty;
			textBoxNotes.Text = String.Empty;
			
			pictureBoxName.Image = new Bitmap(1, 1);
			pictureBoxPic.Image = new Bitmap(1, 1);
			
			panelDescOrig.InvalidateEx();
			panelDescTrans.InvalidateEx();
			panelNameOrig.InvalidateEx();
			panelNameTrans.InvalidateEx();
		}
		#endregion
	}
}
