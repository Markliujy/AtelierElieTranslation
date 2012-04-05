/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 12:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AtelierElieScripter
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			
		}

		enum TextFileBlockTypes
		{
			None,
			Dialogue,
			DialogueChoices
		}
		
		void OpenTextFile(string fileName)
		{
			StreamReader fileStream = new StreamReader(fileName);
			StringWriter blockStream = new StringWriter();
			string fileLine;
			TextFileBlockTypes currentBlockType = TextFileBlockTypes.None;
			TextFileBlockTypes newBlockType = TextFileBlockTypes.None;
			
			const string patternDialogue = @"^(\s)*Block(\s)*(\d){4}(\s)*:$(\s)*";
			const string patternDialogueChoices = @"^Choice(\s)*Main(\s)*Block(\s)*(\d){4}(\s)*:$(\s)*";

			// Loop through all lines of file
			while ((fileLine = fileStream.ReadLine()) != null)
			{
				// Dialogue Section
				if (Regex.IsMatch(fileLine, patternDialogue) && currentBlockType != TextFileBlockTypes.Dialogue)
				{
					newBlockType = TextFileBlockTypes.Dialogue;
				}
				
				// Dialogue Choices Section
				if (Regex.IsMatch(fileLine, patternDialogueChoices) && currentBlockType != TextFileBlockTypes.DialogueChoices)
				{
					newBlockType = TextFileBlockTypes.DialogueChoices;
				}
				
				// If Section changed - Load section
				if (newBlockType != currentBlockType)
				{
					StringReader blockStreamReader = new StringReader(blockStream.ToString());
					OpenTextFileLoadBlock(blockStreamReader, currentBlockType);
					currentBlockType = newBlockType;
					blockStream = new StringWriter();
				}
				
				blockStream.WriteLine(fileLine);
			}
			
			// Process last block
			if (currentBlockType != TextFileBlockTypes.None)
			{
				StringReader blockStreamReader = new StringReader(blockStream.ToString());
				OpenTextFileLoadBlock(blockStreamReader, currentBlockType);
			}		
		}
		
		void OpenTextFileLoadBlock(StringReader blockStream, TextFileBlockTypes blockType)
		{
			switch (blockType)
			{
				case TextFileBlockTypes.Dialogue:
					dialogueControl.LoadTextFileBlock(blockStream);
					break;
				case TextFileBlockTypes.DialogueChoices:
					dialogueChoicesControl.LoadTextFileBlock(blockStream);
					break;
				case TextFileBlockTypes.None:
					break;
			}

		}
		
		void SaveTextFile(string saveFile)
		{
			//TODO: Save function - Working?
			StreamWriter saveFileStream = new StreamWriter(saveFile);
			
			dialogueControl.SaveTextFileBlock(saveFileStream);
			dialogueChoicesControl.SaveTextFileBlock(saveFileStream);
			saveFileStream.Close();
		}
		
		void OpenFileToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFileDialog openfile = new OpenFileDialog();
			openfile.Filter = "Text files|*.txt";
			openfile.Title = "Open Script...";
			openfile.ShowDialog();
			
			if (openfile.FileName != "")
			{
				OpenTextFile(openfile.FileName);
			}
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			SaveFileDialog saveFile = new SaveFileDialog();
			saveFile.Filter = "Text files|*.txt";
			saveFile.Title = "Save Script...";
			saveFile.ShowDialog();
			
			if (saveFile.FileName != "")
			{
				SaveTextFile(saveFile.FileName);
			}			
		}
		
		Dictionary<string, int> GetDTETable(int length)
		{
			Dictionary<string, int> DTEtable = new Dictionary<string, int>();
			
			DTEtable = dialogueControl.GetDTETable(DTEtable, 2);
			DTEtable = dialogueChoicesControl.GetDTETable(DTEtable, 2);
			
			var items = from k in DTEtable.Keys
                    orderby DTEtable[k] descending
                    select k;
			
			Dictionary<string, int> newDTEtable = new Dictionary<string, int>();
			
			int i = 0;
			foreach (string item in items)
			{
				bool repeatItem = false;
				foreach (KeyValuePair<string, int> kvp in newDTEtable)
				{
					if (kvp.Key.Contains(item) || item.Contains(kvp.Key))
						repeatItem = true;
				}
				
				if (repeatItem == false)
				{
					newDTEtable.Add(item, DTEtable[item]);
					i++;
				}
				
				if (i > length)
					break;
				
			}
			
			return newDTEtable;
		}
		
		void DTETableToolStripMenuItemClick(object sender, EventArgs e)
		{
			
			Dictionary<string, int> DTEtable = GetDTETable(165);
			
			string text = string.Empty;
			
			foreach (KeyValuePair<string, int> kvp in DTEtable)
			{
				text += string.Format("{0}| - {1}\n", kvp.Key, kvp.Value.ToString());
			}
			Debug.WriteLine(text);
			MessageBox.Show(text, "test", MessageBoxButtons.OK, MessageBoxIcon.None);
		}
		
		void SaveTestToolStripMenuItemClick(object sender, EventArgs e)
		{
			SaveFileDialog saveFile = new SaveFileDialog();
			saveFile.Title = "Save Script...";
			saveFile.ShowDialog();
			
			if (saveFile.FileName != "")
			{
				Dictionary<string, int> DTEtable = GetDTETable(165);
				
				int i = 0x7B;
				foreach (var key in DTEtable.Keys.ToList())
				{
					if (i > 255)
						i = 0;
					DTEtable[key] = i;
					i++;
				}
				
				dialogueControl.OutputFile(OutFiles.EV1, saveFile.FileName, dialogueChoicesControl, DTEtable);
			}
		}
		
		void SaveDteTestToolStripMenuItemClick(object sender, EventArgs e)
		{
			SaveFileDialog saveFile = new SaveFileDialog();
			saveFile.Title = "Save Script...";
			saveFile.ShowDialog();
			
			if (saveFile.FileName != "")
			{
				Dictionary<string, int> DTEtable = GetDTETable(165);
				
				Encoding enc = new ASCIIEncoding();
				FileStream fs = new FileStream(saveFile.FileName, FileMode.Create);
				BinaryWriter bw = new BinaryWriter(fs, enc);
				
				foreach (KeyValuePair<string, int> kvp in DTEtable)
				{
					bw.Write(kvp.Key.ToCharArray());
				}
				
			}
		}
	}
}
