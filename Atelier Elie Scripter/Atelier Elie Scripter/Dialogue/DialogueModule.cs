/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 12:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AtelierElieScripter.Dialogue
{
	/// <summary>
	/// Description of DialogueModule.
	/// </summary>
	public class DialogueModule
	{
		
		#region Members
		DialogueMainBlocks mainBlocks;
		#endregion
		
		#region Constants
		readonly uint[] BLOCKOFFSETLIST = {
								0x8015134c,
								0x80151520,
								0x80156304,
								0x80156670,
								0x8015af40,
								0x8015b528,
								0x8014fa94,
								0x8015028c,
								0x80155350,
								0x80155d40,
								0x8015a710,
								0x8015b3dc,
								0x80151188,
								0x801520f4,
								0x80156a34,
								0x80157bb0,
								0x8015c5b0,
								0x8015d8c4,
								0x8014e864,
								0x8014fd64,
								0x8014f92c,
								0x801510b4,
								0x80159294,
								0x8015aa40};
		
		readonly uint RAMOFFSET = 0x8014b700;
		
		#endregion
		
		public DialogueModule()
		{
			mainBlocks = new DialogueMainBlocks();
			InitializeFromGameFiles();
		}
		
		#region Initialization
		void InitializeFromGameFiles()
		{
			
			// Load Resources
			Encoding enc = Encoding.GetEncoding("shift-jis");
			System.Resources.ResourceManager res = new System.Resources.ResourceManager("AtelierElieScripter.Res.GameFiles", System.Reflection.Assembly.GetExecutingAssembly());
			BinaryReader fileEV1 = new BinaryReader(new MemoryStream((Byte[])res.GetObject("EV_001")), enc);
			BinaryReader fileEV4 = new BinaryReader(new MemoryStream((Byte[])res.GetObject("EV_004")), enc);
			BinaryReader fileEV7 = new BinaryReader(new MemoryStream((Byte[])res.GetObject("EV_007")), enc);
			BinaryReader fileEVCB = new BinaryReader(new MemoryStream((Byte[])res.GetObject("EV_CB")), enc);
			BinaryReader fileREQUEST = new BinaryReader(new MemoryStream((Byte[])res.GetObject("REQUEST")), enc);
			
			// Initialize Variables
			uint BlockPointerChoices;
			uint BlockPointer;
			int textBlockNo;
			BinaryReader fileUsed;

			
			// Create all TextBlock instances
			for (int i = 0; i < 0x1F9; i++)
			{
				mainBlocks[i] = new DialogueTextBlocks();
				
				
				
				if (i < 0x27)
				{
					BlockPointer = BLOCKOFFSETLIST[0];
					BlockPointerChoices = BLOCKOFFSETLIST[1];
					textBlockNo = i;
					fileUsed = fileEV1;
				}
				else if (i < 0x49)
				{
					BlockPointer = BLOCKOFFSETLIST[2];
					BlockPointerChoices = BLOCKOFFSETLIST[3];
					textBlockNo = i - 0x27;
					fileUsed = fileEV1;
				}
				else if (i < 0x7e)
				{
					BlockPointer = BLOCKOFFSETLIST[4];
					BlockPointerChoices = BLOCKOFFSETLIST[5];
					textBlockNo = i - 0x49;
					fileUsed = fileEV1;
				}
				else if (i < 0xaa)
				{
					BlockPointer = BLOCKOFFSETLIST[6];
					BlockPointerChoices = BLOCKOFFSETLIST[7];
					textBlockNo = i - 0x7e;
					fileUsed = fileEV4;
				}
				else if (i < 0xd4)
				{
					BlockPointer = BLOCKOFFSETLIST[8];
					BlockPointerChoices = BLOCKOFFSETLIST[9];
					textBlockNo = i - 0xaa;
					fileUsed = fileEV4;
				}
				else if (i < 0x111)
				{
					BlockPointer = BLOCKOFFSETLIST[10];
					BlockPointerChoices = BLOCKOFFSETLIST[11];
					textBlockNo = i - 0xd4;
					fileUsed = fileEV4;
				}
				else if (i < 0x149)
				{
					BlockPointer = BLOCKOFFSETLIST[12];
					BlockPointerChoices = BLOCKOFFSETLIST[13];
					textBlockNo = i - 0x111;
					fileUsed = fileEV7;
				}
				else if (i < 0x175)
				{
					BlockPointer = BLOCKOFFSETLIST[14];
					BlockPointerChoices = BLOCKOFFSETLIST[15];
					textBlockNo = i - 0x149;
					fileUsed = fileEV7;
				}
				else if (i < 0x197)
				{
					BlockPointer = BLOCKOFFSETLIST[16];
					BlockPointerChoices = BLOCKOFFSETLIST[17];
					textBlockNo = i - 0x175;
					fileUsed = fileEV7;
				}
				else if (i < 0x1c0)
				{
					BlockPointer = BLOCKOFFSETLIST[18];
					BlockPointerChoices = BLOCKOFFSETLIST[19];
					textBlockNo = i - 0x197;
					fileUsed = fileEVCB;
				}
				else if (i < 0x1f6)
				{
					BlockPointer = BLOCKOFFSETLIST[20];
					BlockPointerChoices = BLOCKOFFSETLIST[21];
					textBlockNo = i - 0x1c0;
					fileUsed = fileEVCB;
				}
				else
				{
					BlockPointer = BLOCKOFFSETLIST[22];
					BlockPointerChoices = BLOCKOFFSETLIST[23];
					textBlockNo = i - 0x1f6;
					fileUsed = fileREQUEST;
				}
				
				InitializeTextBlock(BlockPointer, fileUsed, i, textBlockNo);
			}
			
			
			// Close resources
			fileEV1.Close();
			fileEV4.Close();
			fileEV7.Close();
			fileEVCB.Close();
			fileREQUEST.Close();
			
		}
		
		void InitializeTextBlock(uint pointer, BinaryReader file, int blockNo, int textBlockNo)
		{
			uint commandPointer = (uint)(pointer + blockNo * 12);
			uint textBlocksPointer = (uint)(pointer + blockNo * 12 + 4);
			
			commandPointer = Lib.Tools.GetPointerFromFile(commandPointer, RAMOFFSET, file);
			commandPointer -= (RAMOFFSET);
			
			textBlocksPointer = Lib.Tools.GetPointerFromFile(textBlocksPointer, RAMOFFSET, file);
			textBlocksPointer -= (RAMOFFSET);
			
			
			//Debug.WriteLine(blockNo.ToString() + " - " + (commandPointer).ToString("X2") + " - " + textBlocksPointer.ToString("X2"));
			
			
			
			file.BaseStream.Seek(textBlocksPointer, SeekOrigin.Begin);
			
			int nameNo;
			int unknown1;
			int voiceNo;
			int unknown2;
			uint textPointer;
			string text;
			
			textBlockNo = 0;
			
			while (true)
			{
				file.BaseStream.Seek(textBlocksPointer, SeekOrigin.Begin);
				
				if (file.BaseStream.Position == file.BaseStream.Length)
					break;
				
				nameNo = Lib.Tools.GetHalfWordFromFile(file);
				unknown1 = Lib.Tools.GetHalfWordFromFile(file);
				voiceNo = Lib.Tools.GetHalfWordFromFile(file);
				unknown2 = Lib.Tools.GetHalfWordFromFile(file);
				textPointer = Lib.Tools.GetPointerFromFile(file);
				
				textBlocksPointer = (uint)file.BaseStream.Position;
				
				if (textPointer < 0x8014B700 || textPointer > 0x80170000)
					break;
				
				text = Lib.Tools.GetTextFromFile(textPointer, RAMOFFSET, file);
				
				mainBlocks[blockNo][textBlockNo] = new DialogueEntry(nameNo, voiceNo, unknown1, unknown2, text, textBlocksPointer - 4);
				textBlockNo += 1;
				
			}
			
			
		}
		#endregion
		
		#region File Handling
		enum TextFileBlockTypes
		{
			None,
			Text
		}
		
		public void LoadTextFileBlock(StringReader blockStream)
		{
			StringWriter smallerBlockStream = new StringWriter();
			string fileLine;
			TextFileBlockTypes currentBlockType = TextFileBlockTypes.None;
			TextFileBlockTypes newBlockType = TextFileBlockTypes.None;
			
			const string patternText = @"^(\s)*Block(\s)*(\d){4}(\s)*:(\s)*$";

			// Loop through all lines of file
			while ((fileLine = blockStream.ReadLine()) != null)
			{
				// Dialogue Section
				if (Regex.IsMatch(fileLine, patternText) && currentBlockType != TextFileBlockTypes.Text)
					newBlockType = TextFileBlockTypes.Text;
				
				// If Section changed - Load section
				if (newBlockType != currentBlockType)
				{
					StringReader blockStreamReader = new StringReader(smallerBlockStream.ToString());
					LoadTextFileSmallerBlock(blockStreamReader, currentBlockType);
					currentBlockType = newBlockType;
					smallerBlockStream = new StringWriter();
				}
				
				smallerBlockStream.WriteLine(fileLine);
			}
			
			// Process last block
			if (currentBlockType != TextFileBlockTypes.None)
			{
				StringReader blockStreamReader = new StringReader(smallerBlockStream.ToString());
				LoadTextFileSmallerBlock(blockStreamReader, currentBlockType);
			}		
		}
		
		void LoadTextFileSmallerBlock(StringReader blockStream, TextFileBlockTypes blockType)
		{
			switch (blockType)
			{
				case TextFileBlockTypes.Text:
					mainBlocks.LoadTextFileBlock(blockStream);
					break;
				case TextFileBlockTypes.None:
					break;
			}

		}
		
		public void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			mainBlocks.SaveTextFileBlock(saveFileStream);
		}
		#endregion
		
		#region Return Functions		
		public int GetNoTextBlockTotal(int mainBlockNo)
		{
			return mainBlocks[mainBlockNo].Count();
		}
		
		public int GetNoTextBlocksDone(int mainBlockNo)
		{
			return mainBlocks[mainBlockNo].GetNoTextBlocksDone();
		}
		
		public int GetNoTextBlocksNotes(int mainBlockNo)
		{
			return mainBlocks[mainBlockNo].GetNoTextBlocksNotes();
		}
		
		public DialogueEntry GetDialogueEntry(int mainBlockNo, int textBlockNo)
		{
			return mainBlocks[mainBlockNo][textBlockNo];
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			return mainBlocks.GetDTETable(DTEtable, DTELen);
		}
		#endregion
		
		#region Output
	
		

		
		public void OutputFile(OutFiles type, string fileName, DialogueChoices.DialogueChoicesControl choicesControl, Dictionary<string, int> DTEtable)
		{
			System.Resources.ResourceManager res = new System.Resources.ResourceManager("AtelierElieScripter.Res.GameFiles", System.Reflection.Assembly.GetExecutingAssembly());
			Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
			
			switch (type)
			{
				case OutFiles.EV1:
					FileStream fs = new FileStream(fileName, FileMode.Create);
					BinaryWriter bw = new BinaryWriter(fs, enc);
					bw.Write((Byte[])res.GetObject("EV_001"));
					uint writePos;
					writePos = EmptyRange.Ranges[0].Begin;
					for (int i = 0; i < 0x27; i++)
					{
						mainBlocks[i].OutputFile(bw, 0, writePos, DTEtable);
						writePos = (uint)bw.BaseStream.Position;
					}
					writePos = EmptyRange.Ranges[1].Begin;
					for (int i = 27; i < 0x49; i++)
					{
						mainBlocks[i].OutputFile(bw, 1, writePos, DTEtable);
						writePos = (uint)bw.BaseStream.Position;
					}
					writePos = EmptyRange.Ranges[2].Begin;
					for (int i = 49; i < 0x7e; i++)
					{
						mainBlocks[i].OutputFile(bw, 2, writePos, DTEtable);
						writePos = (uint)bw.BaseStream.Position;
					}
					
					break;
				case OutFiles.EV4:
					break;
				case OutFiles.EV7:
					break;
				case OutFiles.EVCB:
					break;
				case OutFiles.EVREQUEST:
					break;
			}
		}
		#endregion
	}
}