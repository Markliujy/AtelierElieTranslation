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
	/// Description of DialogueChoicesEntry.
	/// </summary>
	public class DialogueChoicesEntry : Abstract.AbstractTextBlock
	{
		string[] textJap = new string[5];
		string[] textEng = new string[5];
		string textNotes = String.Empty;
		const int DTE_MAX = 5;
		
		
		public DialogueChoicesEntry()
		{
			patternDivide = @"^\s*Choice\s*(\d{2})\s*:$\s*";
		}
		
		public DialogueChoicesEntry(string[] textArray)
		{
			textJap = textArray;
			patternDivide = @"^\s*Choice\s*(\d{2})\s*:$\s*";
		}
				
		protected override void LoadTextFileSmallerBlock(StringReader blockStream, int? textBlockNo)
		{
			if (textBlockNo == null)
				return;
			
			int i = (int)textBlockNo;
			
			const string patternJapText = @"^\s*OLD\s*:\s*(.*)\s*$";
			const string patternEngText = @"^\s*NEW\s*:\s*(.*)\s*$";
			const string patternNotes = @"^\s*NOTES\s*:\s*(.*)\s*$";
			
			string searchString = blockStream.ReadToEnd();
			
			Match matchJapText = Regex.Match(searchString, patternJapText, RegexOptions.Multiline);
			Match matchEngText = Regex.Match(searchString, patternEngText, RegexOptions.Multiline);
			Match matchNotes = Regex.Match(searchString, patternNotes, RegexOptions.Multiline);
			
			
			if (matchJapText.Success)
			{
				if (textJap[i] != matchJapText.Groups[1].Value)
				{
					//UNDONE: Jap text not same error catch
				}
				
				textJap[i] = matchJapText.Groups[1].Value.TrimEnd('\r', '\n');
				
			}
			
			if (matchEngText.Success)
				textEng[i] = matchEngText.Groups[1].Value.TrimEnd('\r', '\n');
			
			if (matchNotes.Success)
				textNotes = matchNotes.Groups[1].Value.TrimEnd('\r', '\n');
		}
		
		public override void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			for (int i = 0; i < textJap.Length; i++)
			{
				if (textEng[i] != null && textEng[i] != String.Empty)
				{
					saveFileStream.WriteLine("		Choice " + i.ToString("D2") + ":");
					saveFileStream.WriteLine("		OLD: " + textJap[i]);
					saveFileStream.WriteLine("		NEW: " + textEng[i]);
					saveFileStream.WriteLine();
				}
			}
			
			if (textNotes != String.Empty)
			{
				saveFileStream.WriteLine("		NOTES: " + textNotes);
				saveFileStream.WriteLine();
			}
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			int itemLen;
			int charIndex;
			string newString;
			for (int curLen = DTELen; curLen >= 2; curLen--)
			{
				foreach (string s in textEng)
				{
					if (s != null && s != String.Empty)
					{
						
						itemLen = s.Length;
						
						if (itemLen >= curLen)
						{
							itemLen -= curLen;
							
							for (charIndex = 0; charIndex <= itemLen; charIndex++)
							{
								newString = s.Substring(charIndex, curLen);
								int i = 0;
								while (i + 1 < newString.Length && (newString[i] == newString[i + 1]))
								{
									i++;
								}
								
								if (i < curLen)
									charIndex += (curLen - 1);
								
								if (!DTEtable.ContainsKey(newString))
									DTEtable.Add(newString, curLen - 1);
								else
									DTEtable[newString] += curLen - 1;
							}
						}
					}
				}
			}
			
			return DTEtable;
		}
		
		#region Return Functions
		public bool ToWrite
		{
			get
			{
				bool write = false;
				
				foreach (string text in textEng)
				{
					if (text != null && text != String.Empty)
						write = true;
				}
				return write;
			}
		}
		
		public string NotesText
		{
			get
			{
				return textNotes;
			}
			set
			{
				textNotes = value;
			}
		}
		
		public string[] JapText
		{
			get
			{
				return textJap;
			}
		}
		
		public string[] EngText
		{
			get
			{
				return textEng;
			}
			set
			{
				textEng = value;
			}
		}
		
		public int CountDone
		{
			get
			{
				int count = 0;
				foreach (string str in textEng)
				{
					if (str != null && str != String.Empty)
						count += 1;
				}
				return count;
			}
		}
		
		public int CountTotal
		{
			get
			{
				int count = 0;
				foreach (string str in textJap)
				{
					if (str != null)
						count += 1;
				}
				return count;
			}
		}
		#endregion
	}
}
