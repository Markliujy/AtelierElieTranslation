/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 5:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

namespace AtelierElieScripter.Dialogue
{
	/// <summary>
	/// Description of DialogueEntry.
	/// </summary>
	public class DialogueEntry
	{
		#region Members
		
		public int NameId {get; private set;}
		public int VoiceId {get; private set;}
		public int Unknown1 {get; private set;}
		public int Unknown2 {get; private set;}
		public string JapText {get; private set;}
		public string EngText {get; set;}
		public string Notes {get; set;}
		public uint TextPointer {get; private set;}
		#endregion
		
		public DialogueEntry(int nameId, int voiceId, int unknown1, int unknown2, string text, uint textPointer)
		{
			NameId = nameId;
			VoiceId = voiceId;
			Unknown1 = unknown1;
			Unknown2 = Unknown2;
			JapText = text;
			EngText = String.Empty;
			Notes = String.Empty;
			TextPointer = textPointer;
		}
		
		public bool ToWrite
		{
			get{
				if (EngText != String.Empty || Notes != String.Empty)
					return true;
				return false;
			}
		}
		
		public void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			saveFileStream.WriteLine("		OLD: " + JapText);
			if (EngText != String.Empty)
				saveFileStream.WriteLine("		NEW: " + EngText);
			if (Notes != String.Empty)
				saveFileStream.WriteLine("		NOTES: " + Notes);
			saveFileStream.WriteLine();
		}
		
		public void LoadTextFileBlock(StringReader blockStream)
		{
			const string patternJapText = @"^\s*OLD\s*:\s*(.*)\s*$";
			const string patternEngText = @"^\s*NEW\s*:\s*(.*)\s*$";
			const string patternNotes = @"^\s*NOTES\s*:\s*(.*)\s*$";
			
			string searchString = blockStream.ReadToEnd();
			
			Match matchJapText = Regex.Match(searchString, patternJapText, RegexOptions.Multiline);
			Match matchEngText = Regex.Match(searchString, patternEngText, RegexOptions.Multiline);
			Match matchNotes = Regex.Match(searchString, patternNotes, RegexOptions.Multiline);
			
			
			if (matchJapText.Success)
			{
				if (JapText != matchJapText.Groups[1].Value)
				{
					//UNDONE: Jap text not same error catch
				}
				
				JapText = matchJapText.Groups[1].Value.TrimEnd('\r', '\n');
				
			}
			
			if (matchEngText.Success)
				EngText = matchEngText.Groups[1].Value.TrimEnd('\r', '\n');
						
			if (matchNotes.Success)
				Notes = matchNotes.Groups[1].Value.TrimEnd('\r', '\n');
						
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			int itemLen;
			int charIndex;
			string newString;
			for (int curLen = DTELen; curLen >= 2; curLen--)
			{
				itemLen = EngText.Length;
				
				if (itemLen >= curLen)
				{
					itemLen -= curLen;
					
					for (charIndex = 0; charIndex <= itemLen; charIndex++)
					{
						newString = EngText.Substring(charIndex, curLen);
						
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
			
			return DTEtable;
		}
		
	}
}
