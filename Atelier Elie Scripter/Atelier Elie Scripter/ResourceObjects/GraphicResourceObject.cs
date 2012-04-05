/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 19/11/2009
 * Time: 3:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Debug = System.Diagnostics.Debug;
using Imaging = System.Drawing.Imaging;

namespace AtelierElieScripter.ResourceObjects
{
	public class GraphicResourceObject
	{
		Image TextBox;
		Image Name;
		Bitmap ChoiceBox;
		
		public GraphicResourceObject(System.Resources.ResourceManager resources)
		{
			TextBox = ((System.Drawing.Image)(resources.GetObject("TextBox")));
			Name = ((System.Drawing.Image)(resources.GetObject("NameSample")));
			ChoiceBox = ((System.Drawing.Bitmap)(resources.GetObject("choicebox")));
		}
		
		
//		public Image GetChoiceBox(int w)
//		{
//			w += 8 * 2;
//			
//			Bitmap scaled = new Bitmap(w, 24);
//			
//			Imaging.PixelFormat format = ChoiceBox.PixelFormat;
//			
//			Bitmap left = ChoiceBox.Clone(new Rectangle(0, 0, 8, 24), format);
//			Bitmap right = ChoiceBox.Clone(new Rectangle(16, 0, 8, 24), format);
//			Bitmap top = ChoiceBox.Clone(new Rectangle(8, 0, 8, 8), format);
//			Bitmap bot = ChoiceBox.Clone(new Rectangle(8, 16, 8, 8), format);
//			
//			Color colour = Color.FromArgb(255, 248, 232, 160);
//			
//			using (Graphics g = Graphics.FromImage(scaled))
//			{
//				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;	
//				g.Clear(colour);
//				
//				g.DrawImage(top, new Rectangle(0, 0, w, 8));
//				g.DrawImage(bot, new Rectangle(0, 16, w, 8));
//				g.DrawImage(left, new Rectangle(0, 0, 8, 24));
//				g.DrawImage(right, new Rectangle(w - 8, 0, 8, 24));
//				
//
//
//			}
//			
//			return scaled;
//			
//		}
		
		public Image GetChoiceBox(int w)
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
		
		public Image GetTextBox()
		{
			return TextBox;
		}
		
		public Image GetName(int nameno, int scale)
		{
			Bitmap name;
			Bitmap scaled = new Bitmap(64 * scale, 16 * scale);
			
			
			
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager("AtelierElieScripter.res.NameGFX", System.Reflection.Assembly.GetExecutingAssembly());

			name = (Bitmap)resources.GetObject("008_" + nameno.ToString("D3"));
			
			if (name != null)
			{
				Imaging.ImageAttributes attr = new Imaging.ImageAttributes();
				attr.SetColorKey(Color.Black, Color.Black);
				
				using (Graphics g = Graphics.FromImage(scaled))
				{
					g.DrawImage(name, new Rectangle(0, 0, 64 * scale, 16 * scale), 0, 0, 64, 16, GraphicsUnit.Pixel, attr);
				}
				
			}
			
			return scaled;
		}
		
		public Image GetName(int nameno)
		{
			Bitmap name;
			Bitmap scaled = new Bitmap(64, 16);
			
			
			
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager("AtelierElieScripter.res.NameGFX", System.Reflection.Assembly.GetExecutingAssembly());

			name = (Bitmap)resources.GetObject("008_" + nameno.ToString("D3"));
			
			if (name != null)
			{
				Imaging.ImageAttributes attr = new Imaging.ImageAttributes();
				attr.SetColorKey(Color.Black, Color.Black);
				
				using (Graphics g = Graphics.FromImage(scaled))
				{
					g.DrawImage(name, new Rectangle(0, 0, 64, 16), 0, 0, 64, 16, GraphicsUnit.Pixel, attr);
				}
				
			}
			
			return scaled;
		}
	}
}
