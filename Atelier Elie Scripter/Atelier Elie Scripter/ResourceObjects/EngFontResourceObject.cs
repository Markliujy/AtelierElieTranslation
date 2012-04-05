/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 23/01/2010
 * Time: 4:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace AtelierElieScripter.ResourceObjects
{
	/// <summary>
	/// Description of EngFontResourceObject.
	/// </summary>
	public sealed class EngFontResourceObject : Abstract.IFontResourceObject
	{
		#region Singleton Initialization
		private static EngFontResourceObject instance = new EngFontResourceObject();
		
		public static EngFontResourceObject Instance {
			get {
				return instance;
			}
		}
		#endregion
		
		#region Constants
		readonly int TEXTBOXTEXTMAXWIDTH = 216;
		readonly int TEXTBOXTEXTLINES = 3;
		readonly int TEXTBOXTEXTLINESPACING = 2;
		readonly int TEXTBOXTEXTX = 72;
		readonly int TEXTBOXTEXTY = 7;
		readonly int TEXTBOXNAMEX = 8;
		readonly int TEXTBOXNAMEY = 6;
		
		readonly int FONTOFFSET = 0x20;
		readonly int FONTTILEWIDTH = 12;
		readonly int FONTTILEHEIGHT = 12;
		
		readonly int CHOICEBOXMAXWIDTH = 252;
		readonly int CHOICEBOXLINES = 1;
		readonly int CHOICEBOXLINESPACING = 0;
		#endregion

		#region Members
		Bitmap[] fontChars;
		int[] fontWidths;
		Bitmap textBox;
		#endregion
		
		
		private EngFontResourceObject()
		{
			// Load Resources
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngFontResourceObject));
			
			Byte[] widthTable = (Byte[])resources.GetObject("WidthTable");
			Bitmap fontBitmap = (Bitmap)resources.GetObject("Font");
			
			System.Resources.ResourceManager miscres = new System.Resources.ResourceManager("AtelierElieScripter.Res.Misc", System.Reflection.Assembly.GetExecutingAssembly());
			textBox = (Bitmap)miscres.GetObject("TextBox");
			
			if (fontBitmap.GetPixel(0, 0).ToArgb() == Color.Black.ToArgb())
				fontBitmap = Lib.Tools.InvertImage(fontBitmap);
			
			int xCount = fontBitmap.Width / FONTTILEWIDTH;
			int yCount = fontBitmap.Height / FONTTILEHEIGHT;
			
			int charCount = xCount * yCount;
			
			fontChars = new Bitmap[charCount];
			
			Imaging.PixelFormat format = fontBitmap.PixelFormat;	
			
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
			
			for (int i = 0; i < yCount; i++)
			{
				for (int j = 0; j < xCount; j++)
				{
					int charcount = i * xCount + j;
					Rectangle rect = new Rectangle(j * FONTTILEWIDTH, i * FONTTILEHEIGHT, FONTTILEWIDTH, FONTTILEHEIGHT);
					Bitmap character = new Bitmap(FONTTILEWIDTH, FONTTILEHEIGHT);
					
					Bitmap clone = fontBitmap.Clone(rect, format);
					using (Graphics g = Graphics.FromImage(character))
					{
						g.DrawImage(clone, new Rectangle(0, 1, FONTTILEWIDTH, FONTTILEHEIGHT), 0, 0, FONTTILEWIDTH, FONTTILEHEIGHT, GraphicsUnit.Pixel, shadowattr);
						g.DrawImage(clone, new Rectangle(0, 0, FONTTILEWIDTH, FONTTILEHEIGHT), 0, 0, FONTTILEWIDTH, FONTTILEHEIGHT, GraphicsUnit.Pixel, attr);
					}
					
					fontChars[charcount] = character;
				}
			}
			
			fontBitmap.Dispose();
			
			
			fontWidths = new int[charCount];
			for (int i = 0; i < charCount; i++)
			{
				fontWidths[i] = widthTable[i];
			}			
		}
		
		public Bitmap GetChar(int character)
		{
			
			if (character < FONTOFFSET || character > (fontChars.Length + FONTOFFSET))
			{
				return new Bitmap(1, 1);
			}
			
			
			return fontChars[character - FONTOFFSET];
		}
		
		public int GetWidth(int character)
		{
			if (character < FONTOFFSET || character > (fontChars.Length + FONTOFFSET))
			{
				return 0;
			}
			
			
			return fontWidths[character - FONTOFFSET];
		}
		
		public int GetWidth(string text)
		{
			System.Text.Encoding enc = System.Text.Encoding.ASCII;
			Byte[] textBytes = (enc.GetBytes(text));
			int width = 0;
			
			for (int i = 0; i < textBytes.Length; i += 1)
			{
				int textChar = (int)textBytes[i];
				width += GetWidth(textChar);
			}
			return width;
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
			System.Text.Encoding enc = System.Text.Encoding.ASCII;
			
			Byte[] textBytes = (enc.GetBytes(text));
			
			int w = maxWidth;
			int h = noLines * 12 + (noLines - 1) * lineSpacing;
			
			Bitmap textBitmap = new Bitmap(w, h);
			int x = 0;
			int y = 0;
			
			using (Graphics g = Graphics.FromImage(textBitmap))
			{
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				
				for (int i = 0; i < textBytes.Length; i += 1)
				{
				
					int textChar = (int)textBytes[i];
					
					if (y > h)
					{
						break;
					}
					// Linebreak (\n is 2 bytes, so needs to check next byte as well)
					else if ((i + 1) < textBytes.Length && textChar == enc.GetBytes("\\n")[0]
					         && (int)textBytes[i+1] == enc.GetBytes("\\n")[1])
					{
						x = 0;
						y += 12 + lineSpacing;
						i++;
					}
					else
					{
						// HACK: ASM in-game uses + 12 instead of + width due to constraints for now
						if ((x + FONTTILEWIDTH) >= maxWidth)
						{
							x = 0;
							y += 12 + lineSpacing;
						}
						
						Rectangle drawRect = new Rectangle(x, y, FONTTILEWIDTH, FONTTILEHEIGHT);
						Bitmap bmp = GetChar(textChar);
						g.DrawImage(bmp, drawRect, 0, 0, FONTTILEWIDTH, FONTTILEHEIGHT, GraphicsUnit.Pixel, attr);
						
						x += GetWidth(textChar);
						
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
			
			
			Bitmap textWithBoxBitmap = GetChoiceBox(GetWidth(text));
			
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
