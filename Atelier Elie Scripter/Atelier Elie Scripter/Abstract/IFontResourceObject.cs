/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 23/04/2010
 * Time: 1:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace AtelierElieScripter.Abstract
{
	/// <summary>
	/// Description of IFontResourceObject.
	/// </summary>
	public interface IFontResourceObject
	{
		Bitmap GetTextBitmap(string text, int maxWidth, int noLines, int lineSpacing);
	}
}
