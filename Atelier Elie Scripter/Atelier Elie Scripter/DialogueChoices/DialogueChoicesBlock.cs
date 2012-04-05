/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 25/03/2010
 * Time: 8:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AtelierElieScripter.DialogueChoices
{
	/// <summary>
	/// Description of DialogueChoicesBlock.
	/// </summary>
	public class DialogueChoicesBlock :Abstract.AbstractTextBlock
	{
		SortedDictionary<int, DialogueChoicesEntry> choicesBlock;
		
		public DialogueChoicesBlock()
		{
			patternDivide = @"^\s*Choice\s*Block\s*(\d{4})\s*:\s*$";
			choicesBlock = new SortedDictionary<int, DialogueChoicesEntry>();
		}
		
		#region File Handling
		
		public bool ToWrite
		{
			get {
				bool b = false;
				foreach (var textBlock in choicesBlock)
				{
					if (textBlock.Value.ToWrite)
					{
						b = true;
						break;
					}
				}
				return b;
			}
		}
		
		
		protected override void LoadTextFileSmallerBlock(StringReader blockStream, int? textBlockNo)
		{
			if (textBlockNo != null)
			{
				try
				{
					choicesBlock[(int)textBlockNo].LoadTextFileBlock(blockStream);
				}
				catch (KeyNotFoundException)
				{
					//UNDONE: File Load Error catching
				}
			}
		}
		
		public override void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			foreach (var textBlock in choicesBlock)
			{
				if (textBlock.Value.ToWrite)
				{
					saveFileStream.WriteLine("	Choice Block " + textBlock.Key.ToString("D4") + ":");
					saveFileStream.WriteLine();
					textBlock.Value.SaveTextFileBlock(saveFileStream);
				}
			}
		}
		#endregion
		
		public DialogueChoicesEntry this [int index]
		{
			get
			{
				return choicesBlock[index];
			}
			
			set
			{
				if (!choicesBlock.ContainsKey(index))
					choicesBlock.Add(index, value);
			}
		}
		
		
		
		#region Return Functions
		public int Count
		{
			get
			{
				return choicesBlock.Count;
			}
		}
		
		public int CountDone
		{
			get
			{
				int count = 0;
				foreach (var block in choicesBlock)
				{
					count += block.Value.CountDone;
				}
				return count;
			}
		}
		
		public int CountTotal
		{
			get
			{
				int count = 0;
				foreach (var block in choicesBlock)
				{
					count += block.Value.CountTotal;
				}
				return count;
			}
		}
		
		public int CountNotes
		{
			get
			{
				int count = 0;
				foreach (var block in choicesBlock)
				{
					if (block.Value.NotesText != String.Empty)
						count++;
				}
				return count;
			}
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			foreach (KeyValuePair<int, DialogueChoicesEntry> kvp in choicesBlock)
			{
				DTEtable = kvp.Value.GetDTETable(DTEtable, DTELen);
			}
			
			return DTEtable;
		}
		#endregion
	}
}
