/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/04/2010
 * Time: 12:19 AM
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

namespace AtelierElieScripter.AlchemyItems
{
	/// <summary>
	/// Description of Module.
	/// </summary>
	public class Module
	{
		Collection itemCollection;
		
		public Module()
		{
			itemCollection = new Collection();
			InitializeFromGameFiles();
		}
		
		void InitializeFromGameFiles()
		{
			Encoding enc = Encoding.GetEncoding("shift-jis");
			System.Resources.ResourceManager res = new System.Resources.ResourceManager(
				"AtelierElieScripter.Res.GameFiles", 
				System.Reflection.Assembly.GetExecutingAssembly()
			);
			
			BinaryReader br = new BinaryReader(new MemoryStream((Byte[])res.GetObject("SLPS_017")), enc);
			
			uint pointerPos = CONSTANTS.SLPS_ALCHEMYITEMSPOINTER;
			uint pointerDescPos = CONSTANTS.SLPS_ALCHEMYITEMSDESCPOINTER;
			string text;
			string textDesc;
			
			for (int i = 0; i < 200; i++)
			{
				text = Lib.Tools.GetStringFromFileWithPointer(pointerPos, CONSTANTS.SLPS_RAMOFFSET, br);
				textDesc = Lib.Tools.GetStringFromFileWithPointer(pointerDescPos, CONSTANTS.SLPS_RAMOFFSET, br);
				itemCollection[i] = new Item(text, textDesc, pointerPos, i);
				pointerPos += 0x34;
				pointerDescPos += 0x4;
			}
			
		}
		
		public Collection Items
		{
			get
			{
				return itemCollection;
			}
		}
	}
}
