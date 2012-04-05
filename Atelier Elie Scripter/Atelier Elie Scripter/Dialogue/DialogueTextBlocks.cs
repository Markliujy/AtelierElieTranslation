/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 6:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AtelierElieScripter.Dialogue
{
	/// <summary>
	/// Description of DialogueTextBlock.
	/// </summary>
	public class DialogueTextBlocks : Abstract.AbstractTextBlock
	{
		SortedDictionary<int, DialogueEntry> textBlocks;
		
		public DialogueTextBlocks()
		{
			patternDivide = @"^\s*Textblock\s*(\d{4})\s*:\s*$";
			textBlocks = new SortedDictionary<int, DialogueEntry>();
		}
		
		public int Count()
		{
			return textBlocks.Count;
		}
		
		public DialogueEntry this [int index]
		{
			get
			{
				return textBlocks[index];
			}
			
			set
			{
				if (!textBlocks.ContainsKey(index))
					textBlocks.Add(index, value);
			}
		}
		
		/// <summary>
		/// Get number of text blocks with Eng Text
		/// </summary>
		/// <returns>int Done</returns>
		public int GetNoTextBlocksDone()
		{
			int doneCount = 0;
			
			foreach (KeyValuePair<int, DialogueEntry> kvp in textBlocks)
			{
				if (kvp.Value.EngText != String.Empty)
					doneCount += 1;
			}
			
			return doneCount;
		}
		
		public int GetNoTextBlocksNotes()
		{
			int doneCount = 0;
			
			foreach (KeyValuePair<int, DialogueEntry> kvp in textBlocks)
			{
				if (kvp.Value.Notes != String.Empty)
					doneCount += 1;
			}
			
			return doneCount;
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			foreach (KeyValuePair<int, DialogueEntry> kvp in textBlocks)
			{
				DTEtable = kvp.Value.GetDTETable(DTEtable, DTELen);
			}
			
			return DTEtable;
		}
		
		#region File Handling
		public bool ToWrite
		{
			get {
				bool b = false;
				foreach (var textBlock in textBlocks)
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
					textBlocks[(int)textBlockNo].LoadTextFileBlock(blockStream);
				}
				catch (KeyNotFoundException)
				{
					//UNDONE: File Load Error catching
				}
					
			}
		}
		
		public override void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			foreach (var textBlock in textBlocks)
			{
				if (textBlock.Value.ToWrite)
				{
					saveFileStream.WriteLine("	Textblock " + textBlock.Key.ToString("D4") + ":");
					textBlock.Value.SaveTextFileBlock(saveFileStream);
				}
			}
		}
		#endregion
		
		#region Output
		public void OutputFile(BinaryWriter bw, int no, uint pos, Dictionary<string, int> DTEtable)
		{
			uint writePos = pos;
			uint writeMax = EmptyRange.Ranges[no].End;

			string toWrite;
			short blankByte = 0;
			
			foreach (KeyValuePair<int, DialogueEntry> kvp in textBlocks)
			{
				toWrite = kvp.Value.EngText;
				//TODO: Newline output file
				
				foreach (KeyValuePair<string, int> kvp2 in DTEtable)
				{
					toWrite = toWrite.Replace(kvp2.Key, Convert.ToChar(kvp2.Value).ToString());
				}
				
				
				if ((toWrite.Length + writePos + 2) < writeMax)
				{
					//Write pointer
					bw.Seek((int)kvp.Value.TextPointer, SeekOrigin.Begin);
					
					byte[] byteArray = BitConverter.GetBytes(writePos + CONSTANTS.EV_RAMOFFSET);
					bw.Write(BitConverter.ToUInt32(byteArray, 0));
					
					//Write Text
					bw.Seek((int)writePos, SeekOrigin.Begin);
					
					bw.Write(toWrite.ToCharArray());
					bw.Write(blankByte);
					
					writePos = (uint)bw.BaseStream.Position;
				}
				else
					return;
			}
			
		}
		#endregion
	}
}
