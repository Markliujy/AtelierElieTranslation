/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 23/01/2010
 * Time: 1:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace AtelierElieScripter.ResourceObjects
{
	/// <summary>
	/// Description of NameResourceObject.
	/// </summary>
	public sealed class NameResourceObject
	{
		private static NameResourceObject instance = new NameResourceObject();
		
		public static NameResourceObject Instance {
			get {
				return instance;
			}
		}
		
		private NameResourceObject()
		{
		}
		
		public Bitmap GetName(int nameId)
		{
			return GetName(nameId, 100);
		}
		
		public Bitmap GetName(int nameId, int zoomPercent)
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameResourceObject));
			string fileName = String.Format("{0:D3}", nameId);
			float zoom = (float)(zoomPercent / 100);
			
			ImageAttributes attr = new ImageAttributes();
			
			Bitmap nameBitmap = (Bitmap)resources.GetObject(fileName);
			
			if (nameBitmap == null)
				return new Bitmap(1, 1);
			
			int sourceWidth = nameBitmap.Width;
			
			int sourceHeight = nameBitmap.Height;
		    int sourceX = 0;
		    int sourceY = 0;
		
		    int destX = 0;
		    int destY = 0; 
		    int destWidth  = (int)(sourceWidth * zoom);
		    int destHeight = (int)(sourceHeight * zoom);
		    
		    PixelFormat format = nameBitmap.PixelFormat;
		
		    Bitmap zoomedBitmap = new Bitmap(destWidth, destHeight, format);
		
		    Graphics g = Graphics.FromImage(zoomedBitmap);
		    g.InterpolationMode = InterpolationMode.NearestNeighbor;
		
		    g.DrawImage(nameBitmap, 
		        new Rectangle(destX,destY,destWidth,destHeight),
		        sourceX,sourceY,sourceWidth,sourceHeight,
		        GraphicsUnit.Pixel, attr);
		
		    
		    g.Dispose();
		    return zoomedBitmap;
		}
	}
}
