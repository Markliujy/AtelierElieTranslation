/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 21/04/2010
 * Time: 5:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace AtelierElieScripter
{
		public struct EmptyRange
		{
			public uint Begin;
			public uint End;
			
			public EmptyRange(uint begin, uint end)
			{
				this.Begin = begin;
				this.End = end;
			}
			
			public static EmptyRange[] Ranges = 
			{
				new EmptyRange(0x4, 0x442a),
				new EmptyRange(0x5efc, 0x95b4),
				new EmptyRange(0xafe8,0xe638)
			};
		}
			
		public struct CONSTANTS
		{
			public static uint EV_RAMOFFSET = 0x8014b700;
			public static uint SLPS_ALCHEMYITEMSPOINTER = 0xdb410;
			public static uint SLPS_ALCHEMYITEMSDESCPOINTER = 0xde254;
			public static uint SLPS_RAMOFFSET = 0x8000F800;
			
			public struct ALCHEMYITEMS
			{
				public static int DESC_MAXWIDTH = 16 * 12;
				public static int DESC_MAXLINES = 4;
				public static int DESC_LINESPACING = 2;
				public static int NAME_MAXWIDTH = 7 * 12;
				public static int NAME_MAXLINES = 1;
				public static int NAME_LINESPACING = 0;
			}
		}
		
		public enum OutFiles
		{
			EV1,
			EV4,
			EV7,
			EVCB,
			EVREQUEST
		}

}