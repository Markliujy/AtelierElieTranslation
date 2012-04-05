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

namespace AtelierElieScripter.Lib
{
	public class NoBackgroundPaintPanel : Panel
	{
		
		public NoBackgroundPaintPanel()
		{
	//			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			
		}
		
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			
		}
		
		protected override  CreateParams CreateParams
	
		{
		
		  get
		
		  {
		
		    CreateParams cp=base.CreateParams;
		
		    cp.ExStyle|=0x00000020; //WS_EX_TRANSPARENT
		
		    return cp;
		
		  }
		
		}
		public void  InvalidateEx()
	
		{
		
		  if(Parent==null)
		
		    return;
		
		  Rectangle rc=new Rectangle(this.Location,this.Size);
		
		  Parent.Invalidate(rc,true);
		
		}
	
	}
}
