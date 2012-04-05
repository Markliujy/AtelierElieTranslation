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
	/// Description of DialogueChoicesMainBlock.
	/// </summary>
	public class DialogueChoicesMainBlock : Abstract.AbstractTextBlock
	{
		SortedDictionary<int, DialogueChoicesBlock> mainBlocks;
		
		public DialogueChoicesMainBlock()
		{
			patternDivide = @"^Choice\s*Main\s*Block\s*(\d{4})\s*:$\s*";
			mainBlocks = new SortedDictionary<int, DialogueChoicesBlock>();
		}
		
		public int Count
		{
			get{
				return mainBlocks.Count;
			}
		}
		
		public DialogueChoicesBlock this [int index]
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
		
		public DialogueChoicesEntry this [int mainIndex, int textIndex]
		{
			get
			{
				return mainBlocks[mainIndex][textIndex];
			}
			
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			foreach (KeyValuePair<int, DialogueChoicesBlock> kvp in mainBlocks)
			{
				DTEtable = kvp.Value.GetDTETable(DTEtable, DTELen);
			}
			
			return DTEtable;
		}
		
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
					saveFileStream.WriteLine("Choice Main Block " + block.Key.ToString("D4") + ":");
					saveFileStream.WriteLine();
					block.Value.SaveTextFileBlock(saveFileStream);
				}
			}
		}
		#endregion
	}
}
