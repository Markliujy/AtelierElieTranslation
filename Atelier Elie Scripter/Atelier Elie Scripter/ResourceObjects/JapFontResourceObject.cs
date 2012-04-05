/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 23/01/2010
 * Time: 11:45 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Debug = System.Diagnostics.Debug;
using Imaging = System.Drawing.Imaging;

namespace AtelierElieScripter.ResourceObjects
{
	/// <summary>
	/// Singleton Resource Object to retrieve the Japanese Font
	/// </summary>
	public sealed class JapFontResourceObject : Abstract.IFontResourceObject
	{
		#region Singleton Initialization
		private static JapFontResourceObject instance = new JapFontResourceObject();
		
		public static JapFontResourceObject Instance {
			get {
				return instance;
			}
		}
		#endregion
		
		#region Members
		Dictionary<uint, Bitmap> fontChars = new Dictionary<uint, Bitmap>();
		Bitmap textBox;
		#endregion
		
		#region Constants
		readonly int TEXTBOXTEXTMAXWIDTH = 216;
		readonly int TEXTBOXTEXTLINES = 3;
		readonly int TEXTBOXTEXTLINESPACING = 2;
		readonly int TEXTBOXTEXTX = 72;
		readonly int TEXTBOXTEXTY = 7;
		readonly int TEXTBOXNAMEX = 8;
		readonly int TEXTBOXNAMEY = 6;
		
		readonly int CHOICEBOXMAXWIDTH = 252;
		readonly int CHOICEBOXLINES = 1;
		readonly int CHOICEBOXLINESPACING = 0;
		#endregion
		
		/// <summary>
		/// Initializes All Characters and TextBox
		/// </summary>
		private JapFontResourceObject()
		{
			
			
			// Load Resources
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JapFontResourceObject));
			
			Byte[] sjisPointers = (Byte[])resources.GetObject("sjispointertable");
			Byte[] sjisTable = (Byte[])resources.GetObject("SJIStable");
			Bitmap font = (Bitmap)resources.GetObject("JapFont");
			
			
			System.Resources.ResourceManager miscres = new System.Resources.ResourceManager("AtelierElieScripter.Res.Misc", System.Reflection.Assembly.GetExecutingAssembly());
			textBox = (Bitmap)miscres.GetObject("TextBox");
			
			// Initialize Variables
			uint pos = 0;
			List<uint> pointers = new List<uint>();
			
			// Invert image if bg is black
			if (font.GetPixel(0, 0).ToArgb() == Color.Black.ToArgb())
			{
				font = Lib.Tools.InvertImage(font);
			}
			
			// Read pointers
			while (pos < sjisPointers.Length)
			{
				uint result = ((uint) sjisPointers[pos+3]) << 24;
			    result |= ((uint) sjisPointers[pos+2]) << 16;
			    result |= ((uint) sjisPointers[pos+1]) << 8;
			    result |= (uint)sjisPointers[pos];
			    result -= 0x800C9BC4;
			    
			    pointers.Add(result);
				pos += 4;
			}
			
			
			// Initialize Variables
			pos = 0;
			uint no = 0;
			uint chara;
			
			Imaging.PixelFormat format = font.PixelFormat;	
			
			Imaging.ColorMap transcolorMap = new Imaging.ColorMap();
			
			transcolorMap.OldColor = Color.White;
			transcolorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
			
			Imaging.ImageAttributes attr = new Imaging.ImageAttributes();
			Imaging.ColorMap colorMap = new Imaging.ColorMap();
			colorMap.OldColor = Color.Black;
			colorMap.NewColor = Color.FromArgb(255, 58, 58, 58);
			Imaging.ColorMap[] remapTable = { colorMap , transcolorMap};
			attr.SetRemapTable(remapTable, Imaging.ColorAdjustType.Bitmap);
			
			colorMap.NewColor = Color.FromArgb(255, 140, 124, 91);
			Imaging.ColorMap[] shadowremapTable = { colorMap };
			Imaging.ImageAttributes shadowattr = new Imaging.ImageAttributes();
			shadowattr.SetRemapTable(remapTable, Imaging.ColorAdjustType.Bitmap);
			
			
			// Copy each character into fontChars with Shadow
			for (int i = 0; i < pointers.Count; i++)
			{
				pos = pointers[i];
				while (true)
				{
					if (sjisTable[pos] == 0xff)
					{
						pos += 4;
						break;
					}
					
					chara = (uint)(i << 8) + sjisTable[pos] + 0x8140;
					
					Rectangle rect = new Rectangle((int)no * 12, (int)i * 12, 12, 12);
					Bitmap character = new Bitmap(12, 12);
					
					Bitmap clone = font.Clone(rect, format);
					
					using (Graphics g = Graphics.FromImage(character))
					{
						g.DrawImage(clone, new Rectangle(0, 1, 12, 12), 0, 0, 12, 12, GraphicsUnit.Pixel, shadowattr);
						g.DrawImage(clone, new Rectangle(0, 0, 12, 12), 0, 0, 12, 12, GraphicsUnit.Pixel, attr);
					}
					
					fontChars.Add(chara, character);
					
					no += 1;
					pos += 1;
					//Debug.WriteLine(chara.ToString("X4") + " ");
					
				}
				
				no = 0;
			}
		}
		
		/// <summary>
		/// Returns Bitmap of the specified Character
		/// </summary>
		/// <param name="chara">SJIS Character Number</param>
		/// <returns>Bitmap of Char</returns>
		public Bitmap GetChar(uint chara)
		{
			try{
				return fontChars[chara];
			}
			catch (KeyNotFoundException)
			{
				Debug.WriteLine("Character does not exist: " + chara.ToString("X4"));
				return fontChars[0x8140];
			}

		}
		
		/// <summary>
		/// Returns Bitmap of Text
		/// </summary>
		/// <param name="text">Text to draw</param>
		/// <param name="maxWidth">Maximum Width</param>
		/// <param name="noLines">Lines to Draw</param>
		/// <param name="lineSpacing">Spacing between lines</param>
		/// <returns>Bitmap of Text</returns>
		public Bitmap GetTextBitmap(string text, int maxWidth, int noLines, int lineSpacing)
		{
			Imaging.ImageAttributes attr = new Imaging.ImageAttributes();
			attr.SetColorKey(Color.White, Color.White);
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding("shift-jis"); 
			
			Byte[] textBytes = (enc.GetBytes(text));
			
			int w = maxWidth;
			int h = noLines * 12 + (noLines - 1) * lineSpacing;
				
			
			Bitmap textBitmap = new Bitmap(w, h);
			int x = 0;
			int y = 0;
			
			
			
			using (Graphics g = Graphics.FromImage(textBitmap))
			{
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				
				for (int i = 0; i < textBytes.Length; i += 2)
				{
					
					uint textChar;
					try 
					{
						textChar = (uint)(textBytes[i] << 8) + textBytes[i+1];
					}
					catch (IndexOutOfRangeException)
					{
						return textBitmap;
					}
					
					if (y > h)
					{
						break;
					}
					else if (textChar == (enc.GetBytes("￥")[0] << 8) + enc.GetBytes("￥")[1])
					{
						x = 0;
						y += 12 + lineSpacing;
					}
					else
					{
						if (x >= maxWidth)
						{
							x = 0;
							y += 12 + lineSpacing;
						}
						
						Rectangle drawRect = new Rectangle(x, y, 12, 12);
						Bitmap bmp = GetChar(textChar);
						g.DrawImage(bmp, drawRect, 0, 0, 12, 12, GraphicsUnit.Pixel, attr);
						
						x += 12;
						
					}
				}
			}
			
			return textBitmap;
		}

		/// <summary>
		/// GetTextWithBoxBitmap
		/// </summary>
		/// <param name="text">Text String</param>
		/// <param name="nameId">Name Id int</param>
		/// <returns>Text Box with Text, Name and Box</returns>
		public Bitmap GetTextWithBoxBitmap(string text, int nameId)
		{
			Bitmap textBitmap = GetTextBitmap(text, TEXTBOXTEXTMAXWIDTH, TEXTBOXTEXTLINES, TEXTBOXTEXTLINESPACING);
			Bitmap textWithBoxBitmap = new Bitmap(textBox.Width, textBox.Height);
			
			Imaging.ImageAttributes attr = new Imaging.ImageAttributes();
			attr.SetColorKey(Color.Black, Color.Black);
			
			using (Graphics g = Graphics.FromImage(textWithBoxBitmap))
			{
				g.InterpolationMode = InterpolationMode.NearestNeighbor;
				
				// Draw textbox
				Rectangle textboxRect = new Rectangle(0, 0, textBox.Width, textBox.Height);
				g.DrawImage(textBox, textboxRect, 0, 0, textBox.Width, textBox.Height, GraphicsUnit.Pixel, attr);
				
				
				// Draw Name
				NameResourceObject nameRes = NameResourceObject.Instance;
				Bitmap nameBitmap = nameRes.GetName(nameId);
				Rectangle nameRect = new Rectangle(TEXTBOXNAMEX, TEXTBOXNAMEY, nameBitmap.Width, nameBitmap.Height);
				g.DrawImage(nameBitmap, nameRect, 0, 0, nameBitmap.Width, nameBitmap.Height, GraphicsUnit.Pixel, attr);	
				
				// Draw Text
				Rectangle textRect = new Rectangle(TEXTBOXTEXTX, TEXTBOXTEXTY, textBitmap.Width, textBitmap.Height);
				g.DrawImage(textBitmap, textRect, 0, 0, textBitmap.Width, textBitmap.Height, GraphicsUnit.Pixel, attr);	
				
			}
			
			return textWithBoxBitmap;
		}
		
		public Bitmap GetTextWithChoiceBoxBitmap(string text)
		{
			Bitmap textBitmap = GetTextBitmap(text, CHOICEBOXMAXWIDTH, CHOICEBOXLINES, CHOICEBOXLINESPACING);
			
			
			Bitmap textWithBoxBitmap = GetChoiceBox(12 * text.Length);
			
			Imaging.ImageAttributes attr = new Imaging.ImageAttributes();
			attr.SetColorKey(Color.Black, Color.Black);
			
			using (Graphics g = Graphics.FromImage(textWithBoxBitmap))
			{
				g.InterpolationMode = InterpolationMode.NearestNeighbor;
				// Draw Text
				Rectangle textRect = new Rectangle(1, 1, textBitmap.Width, textBitmap.Height);
				g.DrawImage(textBitmap, textRect, 0, 0, textBitmap.Width, textBitmap.Height, GraphicsUnit.Pixel, attr);	
			}
			
			return textWithBoxBitmap;
		}
		
		public Bitmap GetChoiceBox(int w)
		{
			Bitmap result = new Bitmap(252, 14);
			
			using (Graphics g = Graphics.FromImage(result))
			{
				g.Clear(Color.White);
				g.DrawRectangle(Pens.Blue, new Rectangle(0, 0, 251, 13));
				g.DrawRectangle(Pens.Red, new Rectangle(0, 0, w + 1, 13));
			}
			
			return result;
		}
		
	}
}
