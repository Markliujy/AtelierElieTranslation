/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/04/2010
 * Time: 12:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AtelierElieScripter.AlchemyItems
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		public string textEng {get; set;}
		public string textJap {get; set;}
		public string textNote {get; set;}
		public int number {get; set;}
		public uint pointerPos {get; private set;}
		public string textDescEng {get; set;}
		public string textDescJap {get; set;}
		
		public Item()
		{
		}
		
		public Item(string japText, string japDescText, uint posPointer, int no)
		{
			textJap = japText;
			textDescJap = japDescText;
			pointerPos = posPointer;
			number = no;
			
			textEng = String.Empty;
			textNote = String.Empty;
			textDescEng = String.Empty;
			
		}
	}
}
