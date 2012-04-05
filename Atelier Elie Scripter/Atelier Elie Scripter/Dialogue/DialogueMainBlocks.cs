/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 6:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AtelierElieScripter.Dialogue
{
	/// <summary>
	/// Description of DialogueMainBlock.
	/// </summary>
	public class DialogueMainBlocks : Abstract.AbstractTextBlock
	{
		#region Members
		SortedDictionary<int, DialogueTextBlocks> mainBlocks;
		#endregion
		
		#region Initialization
		public DialogueMainBlocks()
		{
			patternDivide = @"^\s*Block\s*(\d{4})\s*:\s*$";
			mainBlocks = new SortedDictionary<int, DialogueTextBlocks>();
		}
		#endregion
		
		#region File Handling
		
		protected override void LoadTextFileSmallerBlock(StringReader blockStream, int? mainBlockNo)
		{
			if (mainBlockNo != null)
			{
				try
				{
					mainBlocks[(int)mainBlockNo].LoadTextFileBlock(blockStream);
				}
				catch (KeyNotFoundException)
				{
					//UNDONE: File Load Error Catching
				}
			}
		}
		
		public override void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			foreach (var block in mainBlocks)
			{
				if (block.Value.ToWrite)
				{
					saveFileStream.WriteLine("Block " + block.Key.ToString("D4") + ":");
					saveFileStream.WriteLine();
					block.Value.SaveTextFileBlock(saveFileStream);
				}
					
			}
		}
		#endregion
			
		#region Return Functions
		public bool Contains(int index)
		{
			return mainBlocks.ContainsKey(index);
		}
		
		public DialogueTextBlocks this [int index]
		{
			get
			{
				return mainBlocks[index];
			}
			
			set
			{
				if (!mainBlocks.ContainsKey(index))
					mainBlocks.Add(index, value);
			}
		}
		
		public DialogueTextBlocks this [int mainIndex, int textIndex]
		{
			get
			{
				return mainBlocks[mainIndex];
			}
			
			set
			{
				if (!mainBlocks.ContainsKey(mainIndex))
					mainBlocks.Add(mainIndex, value);
			}
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			foreach (KeyValuePair<int, DialogueTextBlocks> kvp in mainBlocks)
			{
				DTEtable = kvp.Value.GetDTETable(DTEtable, DTELen);
			}
			
			return DTEtable;
		}
		#endregion
	}
}
