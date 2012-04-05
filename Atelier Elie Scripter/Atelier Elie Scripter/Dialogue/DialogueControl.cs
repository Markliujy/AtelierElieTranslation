/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 12:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AtelierElieScripter.Dialogue
{
	/// <summary>
	/// Description of DialogueControl.
	/// </summary>
	public partial class DialogueControl : UserControl
	{
		#region Members
		Dialogue.DialogueModule dialogueModule;
		#endregion
		
		#region Declarations
		public enum MainBlockColumns
		{
			Blocks,
			Done,
			Notes,
			Perct,
			Total
		}
		#endregion
		
		#region Initialization
		public DialogueControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			dialogueModule = new Dialogue.DialogueModule();
			
			InitializeComponent();
			
			listviewMainBlocks.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			
			
			listviewMainBlocks.BufferUseCurrentHeaders();
			InitializeMainChoices();
			listviewMainBlocks.BufferUpdate();
			
			

			splitContainerBlocksMain.SplitterMoved += delegate(object sender, SplitterEventArgs e) { panelTextBlock.Invalidate(); };
			splitContainerJapEng.SplitterMoved += delegate(object sender, SplitterEventArgs e) {  panelTextBlock.Invalidate(); };
			splitContainertTextBlocks.SplitterMoved += delegate(object sender, SplitterEventArgs e) {  panelTextBlock.Invalidate(); };
			
		}

		/// <summary>
		/// Initialize Main listview values
		/// </summary>
		void InitializeMainChoices()
		{
			ListViewItem listItem;
			listviewMainBlocks.BufferItems.Clear();
			
			for (int i = 0; i < 0x1F9; i++)
			{
				List<string> stringList = new List<string>();
				
				int blocksDone = dialogueModule.GetNoTextBlocksDone(i);
				int blocksNotes = dialogueModule.GetNoTextBlocksNotes(i);
				int blocksTotal = dialogueModule.GetNoTextBlockTotal(i);
				int blocksPerct = 100;
				try
				{
					blocksPerct = (int)((blocksDone / blocksTotal) * 100);
				}
				catch (DivideByZeroException)
				{
					// Do Nothing
				}
				
				
				stringList.Add(i.ToString());
				stringList.Add(blocksDone.ToString());
				stringList.Add(blocksNotes.ToString());
				stringList.Add(blocksPerct.ToString());
				stringList.Add(blocksTotal.ToString());
						
				
				
				listItem = new ListViewItem(stringList.ToArray());
				listItem.BackColor = Color.Gainsboro;
				listviewMainBlocks.BufferItems.Add(listItem);
			}
			
			
			
			// Clear and disable Textblock view area
			listviewTextBlocks.Items.Clear();
			DisableInputs();
				

		}
		
		/// <summary>
		/// Initialize Text Block List View for specified Main Block
		/// </summary>
		/// <param name="mainBlockNo"></param>
		void InitializeTextBlockChoices(int mainBlockNo)
		{
			ListViewItem listItem;
			Dialogue.DialogueEntry dialogueEntry;
			
			listviewTextBlocks.BeginUpdate();
			listviewTextBlocks.Items.Clear();
			for (int i = 0; i < dialogueModule.GetNoTextBlockTotal(mainBlockNo); i++)
			{
				dialogueEntry = dialogueModule.GetDialogueEntry(mainBlockNo, i);
				
				
				listItem = new ListViewItem(i.ToString());
				listItem.SubItems.Add(dialogueEntry.JapText.Replace("￥", " "));
				listItem.SubItems.Add(dialogueEntry.EngText.Replace("\\n", " "));
				listItem.SubItems.Add(dialogueEntry.Notes.Replace("\\n", " "));
				
				
				listviewTextBlocks.Items.Add(listItem);
			}
			listviewTextBlocks.EndUpdate();

		}
		#endregion
		
		#region Updates
	
		/// <summary>
		/// Updates Main Listview's specified row
		/// </summary>
		/// <param name="index">Actual specified row</param>
		/// <param name="sortedIndex">Specified row in sorted Listview</param>
		void UpdateMainChoiceRow(int index, int sortedIndex)
		{
			// Get values
			int blocksDone = dialogueModule.GetNoTextBlocksDone(index);
			int blocksNotes = dialogueModule.GetNoTextBlocksNotes(index);
			int blocksTotal = dialogueModule.GetNoTextBlockTotal(index);
			int blocksPerct = 100;
			try
			{
				blocksPerct = (int)(((double)blocksDone / blocksTotal) * 100);
			}
			catch (DivideByZeroException)
			{
				// Do Nothing
			}
			
			// Update listview buffer
			listviewMainBlocks.BufferItems[index].SubItems[(int)MainBlockColumns.Done].Text = blocksDone.ToString();
			listviewMainBlocks.BufferItems[index].SubItems[(int)MainBlockColumns.Notes].Text = blocksNotes.ToString();
			listviewMainBlocks.BufferItems[index].SubItems[(int)MainBlockColumns.Perct].Text = blocksPerct.ToString();
			
			// Update listview row
			listviewMainBlocks.BufferUpdateSortedRow(index, sortedIndex);
			
		}
		#endregion
		
		#region File Handling
		public void LoadTextFileBlock(StringReader blockStream)
		{
			dialogueModule.LoadTextFileBlock(blockStream);
			InitializeMainChoices();
			listviewMainBlocks.BufferUpdate();
		}
		
		public void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			dialogueModule.SaveTextFileBlock(saveFileStream);
		}
		#endregion

		#region Misc Functions
		/// <summary>
		/// Returns currently selected dialogue entry
		/// </summary>
		/// <returns></returns>
		DialogueEntry GetCurrentDialogueEntry()
		{
			int mainBlockNo = Int32.Parse(listviewMainBlocks.Items[listviewMainBlocks.SelectedIndices[0]].Text);
			int textBlockNo = Int32.Parse(listviewTextBlocks.Items[listviewTextBlocks.SelectedIndices[0]].Text);
			
			return dialogueModule.GetDialogueEntry(mainBlockNo, textBlockNo);
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			return dialogueModule.GetDTETable(DTEtable, DTELen);
		}
		
		void DisableInputs()
		{
			// Blank everything
			textboxTextBlock.Text = String.Empty;
			pictureboxTextBlockName.Image = new Bitmap(1, 1);
			textboxTransText.Text = String.Empty;
			panelTextBlock.InvalidateEx();
			panelTransText.InvalidateEx();
			
			// Disable input text boxes
			
			textboxTransText.Enabled = false;
			textboxTransNotes.Enabled = false;
			textboxTextBlock.Enabled = false;
		}
		
		#endregion
		
		#region Eventhandlers
		
		/// <summary>
		/// Call Initialize Text Block Choices on selection of Main Block
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ListviewMainBlocksSelectedIndexChanged(object sender, EventArgs e)
		{
			if (listviewMainBlocks.SelectedIndices.Count > 0)
			{
				InitializeTextBlockChoices(Int32.Parse((listviewMainBlocks.Items[listviewMainBlocks.SelectedIndices[0]].Text)));
				DisableInputs();
			}
			else
			{
				listviewTextBlocks.SelectedItems.Clear();
				listviewTextBlocks.Items.Clear();
			}
		}
		
		/// <summary>
		/// Load actual entry into input area on Text Block selection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ListviewTextBlocksSelectedIndexChanged(object sender, EventArgs e)
		{
			if (listviewMainBlocks.SelectedIndices.Count > 0 && listviewTextBlocks.SelectedIndices.Count > 0)
			{
				
				// Load Dialogue Entry
				DialogueEntry entry = GetCurrentDialogueEntry();
				
				// Load Original Text
				textboxTextBlock.Text = entry.JapText.Replace("￥", Environment.NewLine);
				
				// Refresh Text Panel
				panelTextBlock.Invalidate();
				
				// Refresh Trans Panel
				panelTransText.Invalidate();
				
				// Load Name Image
				ResourceObjects.NameResourceObject nameobj = ResourceObjects.NameResourceObject.Instance;
				pictureboxTextBlockName.Image = nameobj.GetName(entry.NameId, 200);
				
				// Load Trans Text
				textboxTransText.Text = entry.EngText.Replace("\\n", Environment.NewLine);
				
				// Load Notes
				textboxTransNotes.Text = entry.Notes.Replace("\\n", Environment.NewLine);
				
				// Enable text boxes
				textboxTransText.Enabled = true;
				textboxTransNotes.Enabled = true;
				textboxTextBlock.Enabled = true;

			}
			
			// If no selection, disable input area
			else
			{
				DisableInputs();
			}

		}
		
		/// <summary>
		/// Update data on changing text in Translation textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TextboxTransTextTextChanged(object sender, EventArgs e)
		{
			if (listviewMainBlocks.SelectedIndices.Count > 0 && listviewTextBlocks.SelectedIndices.Count > 0)
			{
				DialogueEntry entry = GetCurrentDialogueEntry();
				
				entry.EngText = textboxTransText.Text.Replace(Environment.NewLine, "\\n");
				
				// Update Listviews
				listviewTextBlocks.SelectedItems[0].SubItems[2].Text = entry.EngText.Replace("\\n", " ");
				
				// Update Main Listview
				UpdateMainChoiceRow(Int32.Parse(listviewMainBlocks.SelectedItems[0].SubItems[0].Text),
				                    listviewMainBlocks.SelectedItems[0].Index);
				
				panelTransText.Invalidate();
			}
		}
		
		/// <summary>
		/// Update data on Translation Notes textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void TextboxTransNotesTextChanged(object sender, EventArgs e)
		{
			if (listviewMainBlocks.SelectedIndices.Count > 0 && listviewTextBlocks.SelectedIndices.Count > 0)
			{
				DialogueEntry entry = GetCurrentDialogueEntry();
				
				entry.Notes = textboxTransNotes.Text.Replace(Environment.NewLine, "\\n");
				
				// Update Listviews
				listviewTextBlocks.SelectedItems[0].SubItems[3].Text = entry.Notes.Replace("\\n", " ");
				
				// Update Main Listview
				UpdateMainChoiceRow(Int32.Parse(listviewMainBlocks.SelectedItems[0].SubItems[0].Text), listviewMainBlocks.SelectedItems[0].Index);
			}
		}
		
		/// <summary>
		/// Paint in-game textbox and English text if entry selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PanelTransTextPaint(object sender, PaintEventArgs e)
		{
			if (listviewMainBlocks.SelectedIndices.Count > 0 && listviewTextBlocks.SelectedIndices.Count > 0)
			{
				DialogueEntry entry = GetCurrentDialogueEntry();
				
				ResourceObjects.EngFontResourceObject resobject = ResourceObjects.EngFontResourceObject.Instance;
				Bitmap buffer = resobject.GetTextWithBoxBitmap(entry.EngText, entry.NameId);
				panelTextBlock.Size = buffer.Size;
			
			
				//Draws buffer
				Graphics g = e.Graphics;
		
				Point rect = new Point(0, 0);
				g.DrawImage(buffer, rect);
			}
		}
		
		/// <summary>
		/// Paint in-game Japanese text and box if entry selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PanelTextBlockPaint(object sender, PaintEventArgs e)
		{
			if (listviewMainBlocks.SelectedIndices.Count > 0 && listviewTextBlocks.SelectedIndices.Count > 0)
			{
				DialogueEntry entry = GetCurrentDialogueEntry();
				
				ResourceObjects.JapFontResourceObject resobject = ResourceObjects.JapFontResourceObject.Instance;
				Bitmap buffer = resobject.GetTextWithBoxBitmap(entry.JapText, entry.NameId);
				panelTextBlock.Size = buffer.Size;
			
			
				//Draws buffer
				Graphics g = e.Graphics;
		
				Point rect = new Point(0, 0);
				g.DrawImage(buffer, rect);
			}
		}
		
		
		#endregion
		
		public void OutputFile(OutFiles type, string fileName, DialogueChoices.DialogueChoicesControl choicesControl, Dictionary<string, int> DTEtable)
		{
			dialogueModule.OutputFile(type, fileName, choicesControl, DTEtable);
		}
	}
}
