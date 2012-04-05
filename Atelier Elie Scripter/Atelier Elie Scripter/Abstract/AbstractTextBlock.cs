/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 20/04/2010
 * Time: 5:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AtelierElieScripter.Abstract
{
	/// <summary>
	/// Description of AbstractTextBlock.
	/// </summary>
	public abstract class AbstractTextBlock
	{
		
		protected string patternDivide;
		
		public AbstractTextBlock()
		{
			
		}
		
		
		
		public virtual void LoadTextFileBlock(StringReader blockStream)
		{
			if (this.patternDivide == null)
			{
				return;
			}
			
			StringWriter smallerBlockStream = new StringWriter();
			string fileLine;
			StringReader blockStreamReader;
			
			int? lastBlock = null;
			
			// Loop through all lines of file
			while ((fileLine = blockStream.ReadLine()) != null)
			{
				// Dialogue Section
				Match match = Regex.Match(fileLine, this.patternDivide);
				
				if (match.Success)
				{
					
					blockStreamReader = new StringReader(smallerBlockStream.ToString());
					LoadTextFileSmallerBlock(blockStreamReader, lastBlock);
					smallerBlockStream = new StringWriter();
					lastBlock = Int32.Parse(match.Groups[1].Value);
				}
				
				smallerBlockStream.WriteLine(fileLine);
			}
			
			// Process last block
			blockStreamReader = new StringReader(smallerBlockStream.ToString());
			LoadTextFileSmallerBlock(blockStreamReader, lastBlock);
		}
		protected abstract void LoadTextFileSmallerBlock(StringReader blockStream, int? textBlockNo);
		
		public abstract void SaveTextFileBlock(StreamWriter saveFileStream);
		
		
		
	}
}
