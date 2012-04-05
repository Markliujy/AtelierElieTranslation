/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 25/03/2010
 * Time: 7:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

namespace AtelierElieScripter.DialogueChoices
{
	
	
	/// <summary>
	/// Description of DialogueChoicesModule.
	/// </summary>
	public class DialogueChoicesModule
	{
		
		DialogueChoicesMainBlock mainBlocks;
		
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
		
		public DialogueChoicesModule()
		{			
			mainBlocks = new DialogueChoicesMainBlock();
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
			int mainBlockNo = 0;
			BinaryReader fileUsed;

			
			// Create all Choice Main Block instances
			for (int i = 1; i < (BLOCKOFFSETLIST.Length); i+=2)
			{
				
				mainBlocks[mainBlockNo] = new DialogueChoicesBlock();
				BlockPointerChoices = BLOCKOFFSETLIST[i];
				
				if (mainBlockNo < 3)
				{
					fileUsed = fileEV1;
				}
				else if (mainBlockNo < 6)
				{
					fileUsed = fileEV4;
				}
				else if (mainBlockNo < 9)
				{
					fileUsed = fileEV7;
				}
				else if (mainBlockNo < 11)
				{
					fileUsed = fileEVCB;
				}
				else
				{
					fileUsed = fileREQUEST;
				}

				InitializeBlock(BlockPointerChoices, fileUsed, mainBlockNo);
				mainBlockNo++;
			}
			
			
			// Close resources
			fileEV1.Close();
			fileEV4.Close();
			fileEV7.Close();
			fileEVCB.Close();
			fileREQUEST.Close();
		}
		
		/// <summary>
		/// Initialize Choice Main Block
		/// </summary>
		/// <param name="pointer">Pointer to Choices</param>
		/// <param name="file">File</param>
		/// <param name="mainBlockNo">Main Block No</param>
		void InitializeBlock(uint pointer, BinaryReader file, int mainBlockNo)
		{
			
			// Seek to Choices Position 
			pointer -= (RAMOFFSET);
			
			string[] textArray = new string[5];
			uint textPointer;
			int blockNo = 0;
			
			// Seek to Start of choices blocks
			
			
			while (true)
			{
				textArray = new string[5];
				file.BaseStream.Seek(pointer, SeekOrigin.Begin);
				
				if (file.BaseStream.Position == file.BaseStream.Length)
					break;
				
				
				textPointer = Lib.Tools.GetPointerFromFile(file);
				if (textPointer < 0x8014B700 || textPointer > 0x80170000)
					break;
				textArray[0] = Lib.Tools.GetTextFromFile(textPointer, RAMOFFSET, file);
				
				
				for (int i = 1; i < 5; i++)
				{
					file.BaseStream.Seek(pointer + (i * 4), SeekOrigin.Begin);
					
					textPointer = Lib.Tools.GetPointerFromFile(file);
					if (textPointer != 0)
						textArray[i] = Lib.Tools.GetTextFromFile(textPointer, RAMOFFSET, file);
				}
				
				pointer += 0x4 * 0x5;
				
				mainBlocks[mainBlockNo][blockNo] = new DialogueChoicesEntry(textArray);
				blockNo += 1;
				
			}
			
			
		}
		#endregion
		#region File Handling
		public void LoadTextFileBlock(StringReader blockStream)
		{
			mainBlocks.LoadTextFileBlock(blockStream);
		}
		
		public void SaveTextFileBlock(StreamWriter saveFileStream)
		{
			mainBlocks.SaveTextFileBlock(saveFileStream);
		}
		#endregion
		#region Return Functions
		public int MainBlocksCount
		{
			get
			{
				return mainBlocks.Count;
			}
		}
		
		public DialogueChoicesMainBlock MainBlocks
		{
			get
			{
				return mainBlocks;
			}
		}
		
		public Dictionary<string, int> GetDTETable(Dictionary<string, int> DTEtable, int DTELen)
		{
			DTEtable = mainBlocks.GetDTETable(DTEtable, DTELen);

			
			return DTEtable;
		}
		#endregion
	}
}
