/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 18/01/2010
 * Time: 12:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using Imaging = System.Drawing.Imaging;
using System.Drawing;
using System.Diagnostics;

namespace AtelierElieScripter.Lib
{
	

	
	/// <summary>
	/// Description of Tools.
	/// </summary>
	public class Tools
	{
		public Tools()
		{
		}

		public static string GetStringFromFileWithPointer(uint pointerLoc, uint fileOffset, Byte[] file)
		{
			Encoding enc = Encoding.GetEncoding("shift-jis");
			Stream s = new MemoryStream(file);
			BinaryReader binary = new BinaryReader(s, enc);
			return GetStringFromFileWithPointer(pointerLoc, fileOffset, binary);
		}
		
		public static string GetStringFromFileWithPointer(uint pointerLoc, uint fileOffset, Byte[] file, Encoding enc)
		{
			Stream s = new MemoryStream(file);
			BinaryReader binary = new BinaryReader(s, enc);
			return GetStringFromFileWithPointer(pointerLoc, fileOffset, binary);
		}
		
		public static string GetStringFromFileWithPointer(uint pointerLoc, uint fileOffset,  BinaryReader file)
		{
			uint pointer = GetPointerFromFile(pointerLoc, fileOffset, file);
			return GetTextFromFile(pointer, fileOffset, file);
		}
		
		public static string GetTextFromFile(uint pointer, uint fileOffset, BinaryReader file)
		{
			
			if (pointer > fileOffset)
				pointer -= fileOffset;
			if (pointer >= 0x80000000)
				pointer -= 0x80000000;
			
			
			file.BaseStream.Seek((long)pointer, SeekOrigin.Begin);
			int count = 0;
			
			while (true)
			{
				if (file.BaseStream.Position == file.BaseStream.Length || file.ReadByte() == 0)
					break;
				count += 1;
			}
			count = count / 2;
			file.BaseStream.Seek((long)pointer, SeekOrigin.Begin);
			char[] chars = file.ReadChars(count);
			string text = new string(chars);
			return text;
		}
		
		public static uint GetPointerFromFile(BinaryReader file)
		{
			uint pointer;
			
			pointer = (uint) file.ReadByte();
			pointer |= ((uint) file.ReadByte()) << 8;
		    pointer |= ((uint) file.ReadByte()) << 16;
		    pointer |= ((uint) file.ReadByte()) << 24;
		    
			
		    return pointer;
		}
		
		public static uint GetPointerFromFile(uint pointerLoc, uint fileOffset, BinaryReader file)
		{
			if (pointerLoc > fileOffset)
				pointerLoc -= fileOffset;
			
			return GetPointerFromFile(pointerLoc, file);
		}
		
		public static uint GetPointerFromFile(uint pointerLoc, BinaryReader file)
		{
			if (pointerLoc > 0x80000000)
				pointerLoc -= 0x80000000;
			
			file.BaseStream.Seek((long)pointerLoc, SeekOrigin.Begin);
			
			return GetPointerFromFile(file);
		}		
		
		public static int GetHalfWordFromFile(uint pointerLoc, BinaryReader file)
		{
			return GetHalfWordFromFile(pointerLoc, 0, file);
		}
		
		public static int GetHalfWordFromFile(BinaryReader file)
		{
			
			int halfword = (int) file.ReadByte();
			halfword |= ((int) file.ReadByte()) << 8;
			
			return halfword;
		}
		
		public static int GetHalfWordFromFile(uint pointerLoc, int fileOffset, BinaryReader file)
		{
			pointerLoc -= (uint)fileOffset;
			file.BaseStream.Seek((long)pointerLoc, SeekOrigin.Begin);
			
			return GetHalfWordFromFile(file);
		}
		
		/// <summary>
		/// Returns Inverted Input Image
		/// </summary>
		/// <param name="originalImg">Input Image</param>
		/// <returns></returns>
		public static Bitmap InvertImage(Bitmap originalImg)
        {
            Bitmap invertedBmp = new Bitmap(originalImg.Width, originalImg.Height);

            //Setup color matrix
            Imaging.ColorMatrix clrMatrix = new Imaging.ColorMatrix(new float[][]
                                                    {
                                                    new float[] {-1, 0, 0, 0, 0},
                                                    new float[] {0, -1, 0, 0, 0},
                                                    new float[] {0, 0, -1, 0, 0},
                                                    new float[] {0, 0, 0, 1, 0},
                                                    new float[] {1, 1, 1, 0, 1}
                                                    });

            using (Imaging.ImageAttributes attr = new Imaging.ImageAttributes())
            {
                //Attach matrix to image attributes
                attr.SetColorMatrix(clrMatrix);

                using (Graphics g = Graphics.FromImage(invertedBmp))
                {
                    g.DrawImage(originalImg, new Rectangle(0, 0, originalImg.Width, originalImg.Height),
                                0, 0, originalImg.Width, originalImg.Height, GraphicsUnit.Pixel, attr);
                }
            }

            return invertedBmp;
        }
		
	}
}
