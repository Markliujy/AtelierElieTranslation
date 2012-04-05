/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 25/03/2010
 * Time: 7:37 PM
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


namespace AtelierElieScripter.DialogueChoices
{
	/// <summary>
	/// Description of DialogueChoicesControl.
	/// </summary>
	public partial class DialogueChoicesControl : UserControl
	{
		DialogueChoicesModule dialogueChoicesModule;
		
		
		Lib.NoBackgroundPaintPanel[] panelsJap;
		Lib.NoBackgroundPaintPanel[] panelsEng;
		
		TextBox[] textBoxesJap;
		TextBox[] textBoxesEng;
		
		public DialogueChoicesControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			dialogueChoicesModule = new DialogueChoicesModule();
			
			custListViewMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			
			panelsJap = new AtelierElieScripter.Lib.NoBackgroundPaintPanel[5];
			panelsEng = new AtelierElieScripter.Lib.NoBackgroundPaintPanel[5];
			
			panelsJap[0] = panelChoice1;
			panelsJap[1] = panelChoice2;
			panelsJap[2] = panelChoice3;
			panelsJap[3] = panelChoice4;
			panelsJap[4] = panelChoice5;
			
			panelsEng[0] = panelEngChoice1;
			panelsEng[1] = panelEngChoice2;
			panelsEng[2] = panelEngChoice3;
			panelsEng[3] = panelEngChoice4;
			panelsEng[4] = panelEngChoice5;
			
			textBoxesEng = new TextBox[5];
			textBoxesJap = new TextBox[5];
			
			textBoxesJap[0] = textBoxChoices1;
			textBoxesJap[1] = textBoxChoices2;
			textBoxesJap[2] = textBoxChoices3;
			textBoxesJap[3] = textBoxChoices4;
			textBoxesJap[4] = textBoxChoices5;
						
			textBoxesEng[0] = textBoxEngChoices1;
			textBoxesEng[1] = textBoxEngChoices2;
			textBoxesEng[2] = textBoxEngChoices3;
			textBoxesEng[3] = textBoxEngChoices4;
			textBoxesEng[4] = textBoxEngChoices5;
			
			InitializeMainListView();
			DisableInputs();
			
		}
		void InitializeMainListView()
		{
			custListViewMain.Items.Clear();
			ListViewItem listItem;
			
			for (int i = 0; i < dialogueChoicesModule.MainBlocksCount; i++)
			{
				List<string> stringList = new List<string>();
				
				int textDone = dialogueChoicesModule.MainBlocks[i].CountDone;
				int textTotal = dialogueChoicesModule.MainBlocks[i].CountTotal;
				
				int textPerct = 100;
				try
				{
					textPerct = (int)((textDone / textTotal) * 100);
				}
				catch (DivideByZeroException)
				{
					// Do Nothing
				}
				
				int textNotesCount = dialogueChoicesModule.MainBlocks[i].CountNotes;
				string textNotes = "N";
				
				if (textNotesCount > 0)
					textNotes = "Y";
				
				stringList.Add(i.ToString());
				stringList.Add(textDone.ToString());
				stringList.Add(textTotal.ToString());
				stringList.Add(textPerct.ToString());
				stringList.Add(textNotes);
				
						
				
				
				listItem = new ListViewItem(stringList.ToArray());
				listItem.BackColor = Color.Gainsboro;
				custListViewMain.Items.Add(listItem);
			}
		}
		
		void InitializeChoicesBlocks(int mainBlockNo)
		{
			ListViewItem listItem;
			DialogueChoicesEntry choicesEntry;
			
			custListViewBlock.BeginUpdate();
			custListViewBlock.Items.Clear();
			for (int i = 0; i < dialogueChoicesModule.MainBlocks[mainBlockNo].Count; i++)
			{
				choicesEntry = dialogueChoicesModule.MainBlocks[mainBlockNo, i];
				List<string> stringList = new List<string>();
				
				int textDone = choicesEntry.CountDone;
				int textTotal = choicesEntry.CountTotal;
				
				int textPerct = 100;
				try
				{
					textPerct = (int)(((double)textDone / textTotal) * 100);
				}
				catch (DivideByZeroException)
				{
					// Do Nothing
				}
				
				string textNotes = choicesEntry.NotesText.Replace(Environment.NewLine, "  ");
				
				stringList.Add(i.ToString());
				stringList.Add(textDone.ToString());
				stringList.Add(textTotal.ToString());
				stringList.Add(textPerct.ToString());
				stringList.Add(textNotes);
				
				listItem = new ListViewItem(stringList.ToArray());
				listItem.BackColor = Color.Gainsboro;
				custListViewBlock.Items.Add(listItem);
			}
			custListViewBlock.EndUpdate();
		}
		
		void CustListViewMainSelectedIndexChanged(object sender, EventArgs e)
		{
			if (custListViewMain.SelectedIndices.Count > 0)
			{
				InitializeChoicesBlocks(Int32.Parse((custListViewMain.Items[custListViewMain.SelectedIndices[0]].Text)));
				DisableInputs();
			}
			else
			{
				custListViewBlock.SelectedItems.Clear();
				custListViewBlock.Items.Clear();
			}
		}
		
		void DisableInputs()
		{
			foreach (Lib.NoBackgroundPaintPanel panel in panelsJap)
			{
				panel.InvalidateEx();
			}
			
			foreach (Lib.NoBackgroundPaintPanel panel in panelsEng)
			{
				panel.InvalidateEx();
			}
			
			foreach (TextBox textBox in textBoxesEng)
			{
				textBox.Visible = false;
			}
			
			foreach (TextBox textBox in textBoxesJap)
			{
				textBox.Visible = false;
			}
			
			textBoxTransNotes.Enabled = false;
		}
		
		void CustListViewBlockSelectedIndexChanged(object sender, EventArgs e)
		{
			if (custListViewMain.SelectedIndices.Count > 0 && custListViewBlock.SelectedIndices.Count > 0)
			{
				DialogueChoicesEntry choicesEntry = GetCurrentDialogueEntry();
				
				for (int i = 0; i < textBoxesJap.Length; i++)
				{
					if (choicesEntry.JapText[i] != null)
					{
						textBoxesJap[i].Text = choicesEntry.JapText[i];
						textBoxesJap[i].Visible = true;
						
						textBoxesEng[i].Text = choicesEntry.EngText[i];
						textBoxesEng[i].Visible = true;
						
						panelsJap[i].Visible = true;
						panelsEng[i].Visible = true;
					}
					else
					{
						textBoxesJap[i].Text = String.Empty;
						textBoxesJap[i].Visible = false;
						
						textBoxesEng[i].Text = String.Empty;
						textBoxesEng[i].Visible = false;
					}
					
					panelsJap[i].Invalidate();
					panelsEng[i].Invalidate();
				}
				
				textBoxTransNotes.Enabled = true;
			}
			// If no selection, disable input area
			else
			{
				DisableInputs();
			}
		}
		
		DialogueChoicesEntry GetCurrentDialogueEntry()
		{
			int mainBlockNo = Int32.Parse(custListViewMain.Items[custListViewMain.SelectedIndices[0]].Text);
			int textBlockNo = Int32.Parse(custListViewBlock.Items[custListViewBlock.SelectedIndices[0]].Text);
			
			return dialogueChoicesModule.MainBlocks[mainBlockNo][textBlockNo];
		}
		
		void PanelsJapPaint(object sender, PaintEventArgs e)
		{
			if (custListViewMain.SelectedIndices.Count > 0 && custListViewBlock.SelectedIndices.Count > 0)
			{
				DialogueChoicesEntry choicesEntry = GetCurrentDialogueEntry();
				int j = 0;
				
				Lib.NoBackgroundPaintPanel panel = (Lib.NoBackgroundPaintPanel)sender;
				
				for (int i = 0; i < panelsJap.Length; i++)
				{
					if (panel.Name == panelsJap[i].Name)
						j = i;
				}

				
				if (choicesEntry.JapText[j] != null)
				{
					ResourceObjects.JapFontResourceObject resobject = ResourceObjects.JapFontResourceObject.Instance;
					Bitmap buffer = resobject.GetTextWithChoiceBoxBitmap(choicesEntry.JapText[j]);
					panelsJap[j].Size = buffer.Size;
				
				
					//Draws buffer
					Graphics g = e.Graphics;
			
					Point rect = new Point(0, 0);
					g.DrawImage(buffer, rect);
					
				}
				else
				{
					panelsJap[j].Visible = false;
				}
		
			}
		}
		
		void PanelsEngPaint(object sender, PaintEventArgs e)
		{
			if (custListViewMain.SelectedIndices.Count > 0 && custListViewBlock.SelectedIndices.Count > 0)
			{
				DialogueChoicesEntry choicesEntry = GetCurrentDialogueEntry();
				int j = 0;
				
				Lib.NoBackgroundPaintPanel panel = (Lib.NoBackgroundPaintPanel)sender;
				
				for (int i = 0; i < panelsEng.Length; i++)
				{
					if (panel.Name == panelsEng[i].Name)
						j = i;
				}

				
				if (choicesEntry.JapText[j] != null)
				{
					ResourceObjects.EngFontResourceObject resobject = ResourceObjects.EngFontResourceObject.Instance;
					
					string text = choicesEntry.EngText[j];
					if (text == null)
						text = String.Empty;
					
					Bitmap buffer = resobject.GetTextWithChoiceBoxBitmap(text);
					panelsEng[j].Size = buffer.Size;
				
				
					//Draws buffer
					Graphics g = e.Graphics;
			
					Point rect = new Point(0, 0);
					g.DrawImage(buffer, rect);
					
				}
				else
				{
					panelsEng[j].Visible = false;
				}
		
			}
		}
		
		void TextBoxesEngTextChanged (object sender, EventArgs e)
		{
			if (custListViewMain.SelectedIndices.Count > 0 && custListViewBlock.SelectedIndices.Count > 0)
			{
				DialogueChoicesEntry choicesEntry = GetCurrentDialogueEntry();
				int j = 0;
				
				for (int i = 0; i < textBoxesEng.Length; i++)
				{
					if (sender == textBoxesEng[i])
						j = i;
				}
				
				string[] texts = choicesEntry.EngText;
				
				texts[j] = textBoxesEng[j].Text;
				
				choicesEntry.EngText = texts;
				
				panelsEng[j].Visible = true;
				panelsEng[j].Invalidate();
				
				UpdateSelectedMainRow();
				UpdateSelectedBlockRow();
			}
		}
		
		void UpdateSelectedBlockRow()
		{
			int i = Int32.Parse(custListViewMain.SelectedItems[0].SubItems[0].Text);
			int j = Int32.Parse(custListViewBlock.SelectedItems[0].SubItems[0].Text);

			int textDone = dialogueChoicesModule.MainBlocks[i, j].CountDone;
			int textTotal = dialogueChoicesModule.MainBlocks[i, j].CountTotal;
			
			int textPerct = 100;
			try
			{
				textPerct = (int)(((double)textDone / textTotal) * 100);
			}
			catch (DivideByZeroException)
			{
				// Do Nothing
			}
			
			string textNotes = dialogueChoicesModule.MainBlocks[i, j].NotesText.Replace(Environment.NewLine, "  ");
			
			custListViewBlock.Items[j].SubItems[1].Text = textDone.ToString();
			custListViewBlock.Items[j].SubItems[2].Text = textTotal.ToString();
			custListViewBlock.Items[j].SubItems[3].Text = textPerct.ToString();
			custListViewBlock.Items[j].SubItems[4].Text = textNotes;
		}
		
		void UpdateSelectedMainRow()
		{
			int i = Int32.Parse(custListViewMain.SelectedItems[0].SubItems[0].Text);

			int textDone = dialogueChoicesModule.MainBlocks[i].CountDone;
			int textTotal = dialogueChoicesModule.MainBlocks[i].CountTotal;
			
			int textPerct = 100;
			try
			{
				textPerct = (int)(((double)textDone / textTotal) * 100);
			}
			catch (DivideByZeroException)
			{
				// Do Nothing
			}
			
			int textNotesCount = dialogueChoicesModule.MainBlocks[i].CountNotes;
			string textNotes = "N";
			
			if (textNotesCount > 0)
				textNotes = "Y";
				
			
			custListViewMain.Items[i].SubItems[1].Text = textDone.ToString();
			custListViewMain.Items[i].SubItems[2].Text = textTotal.ToString();
			custListViewMain.Items[i].SubItems[3].Text = textPerct.ToString();
			custListViewMain.Items[i].SubItems[4].Text = textNotes;
			
		}
			
		
		void TextBoxTransNotesTextChanged(object sender, EventArgs e)
		{
			if (custListViewMain.SelectedIndices.Count > 0 && custListViewBlock.SelectedIndices.Count > 0)
			{
				DialogueChoicesEntry choicesEntry = GetCurrentDialogueEntry();
				
				choicesEntry.NotesText = textBoxTransNotes.Text;
				
				UpdateSelectedMainRow();
				UpdateSelectedBlockRow();
			}
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			return dialogueChoicesModule.GetDTETable(DTEtable, DTELen);
		}
		
		#region File Handling
		public void LoadTextFileBlock(StringReader blockStream)
		{
			dialogueChoicesModule.LoadTextFileBlock(blockStream);
			InitializeMainListView();
		}
		
		public void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			dialogueChoicesModule.SaveTextFileBlock(saveFileStream);
		}
		#endregion
	}
}
