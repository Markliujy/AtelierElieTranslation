/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/01/2010
 * Time: 7:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Data;
using System.ComponentModel;

using System.Diagnostics;


namespace AtelierElieScripter.Lib
{
	/// <summary>
	/// Description of CustListView.
	/// </summary>
	public class CustListView : ListView
	{
		
		List<ListViewItem> bufferItemCollection;
		List<ColumnHeader> bufferHeaderCollection;
		List<bool> bufferActiveColumns;
		bool showHideColumns;
		ContextMenuStrip bufferContextMenu;
		
		bool sortableColumns;
		private Lib.ListViewColumnSorter lvwColumnSorter;
		
		public CustListView()
		{
			DoubleBuffered = true;
			showHideColumns = false;
			sortableColumns = false;
		}
		
		/// <summary>
		/// showHideColumns: Copy current headers into buffer and create context menu
		/// 					Also reset active columns
		/// </summary>
		public void BufferUseCurrentHeaders()
		{
			bufferContextMenu.Items.Clear();
			bufferActiveColumns.Clear();
			
			foreach (ColumnHeader head in this.Columns)
			{
				bufferHeaderCollection.Add(head);
				if (head.Index != 0)
				{
					ToolStripMenuItem menuitem = new ToolStripMenuItem(head.Text);
					menuitem.Checked = true;
					menuitem.CheckState = CheckState.Checked;
					menuitem.Tag = head.Index;
					menuitem.Click += new EventHandler(this.BufferContextMenuClick);
					bufferContextMenu.Items.Add(menuitem);
				}
				bufferActiveColumns.Add(true);
			}
		}
		
		/// <summary>
		/// Updates current items and headers according to buffer and active columns
		/// </summary>
		public void BufferUpdate()
		{
			this.BeginUpdate();
			
			// Display active columns
			this.Columns.Clear();
			for (int i = 0; i < bufferActiveColumns.Count; i++)
			{
				if (bufferActiveColumns[i])
					this.Columns.Add(bufferHeaderCollection[i]);
			}
			
			this.Items.Clear();
			foreach (ListViewItem item in bufferItemCollection)
			{
				List<ListViewItem.ListViewSubItem> subItemList = new List<ListViewItem.ListViewSubItem>();
				ListViewItem newitem = (ListViewItem)item.Clone();
				int j = 0;
				for (int i = 0; i < bufferActiveColumns.Count; i++)
				{
					if (!bufferActiveColumns[i])
					{
						newitem.SubItems.RemoveAt(i-j);
						j++;
							
					}
				}
				newitem.BackColor = Color.Gainsboro;
				this.Items.Add(newitem);
			}
			this.EndUpdate();
		}
		
		/// <summary>
		/// Updated sorted row only
		/// </summary>
		/// <param name="index">Actual index in buffer</param>
		/// <param name="sortedIndex">Current sorted index</param>
		public void BufferUpdateSortedRow(int index, int sortedIndex)
		{
			int j = 0;
			this.BeginUpdate();
			for (int i = 0; i < bufferActiveColumns.Count; i++)
			{
				if (bufferActiveColumns[i])
				{
					if (this.Items[sortedIndex].SubItems[j] != this.BufferItems[index].SubItems[i])
						this.Items[sortedIndex].SubItems[j] = this.BufferItems[index].SubItems[i];
					j++;
				}
			}
			this.EndUpdate();
			
		}
		
		/// <summary>
		/// showHideColumns: Toggle columns function
		/// </summary>
		/// <param name="i">Column ID</param>
		public void ShowHideColumn(int i)
		{
			ShowHideColumn(i, false);
		}
		
		public void ShowHideColumn(int i, bool refresh)
		{
			if (showHideColumns == true)
			{
				bufferActiveColumns[i] ^= true;
				if (refresh == true)
				{
					this.BufferUpdate();
					this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);		
				}
			}
		}
		
		/// <summary>
		/// showHideColumns: Toggle columns event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BufferContextMenuClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			
			int i = Int32.Parse(item.Tag.ToString());
			item.Checked ^= true;
			bufferActiveColumns[i] ^= true;
			
			this.BufferUpdate();
			this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);		
		}
		
		void SortableColumnClick(object sender, ColumnClickEventArgs  e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if ( e.Column == lvwColumnSorter.SortColumn )
			{
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
			
		
			// Perform the sort with these new sort options.
			Sort();
		}
			

		/// <summary>
		/// showHideColumns: Returns list of Buffer Items
		/// </summary>
		public List<ListViewItem> BufferItems
		{
			get
			{
				return bufferItemCollection;
			}
		}
		
		/// <summary>
		/// showHideColumns: Returns list of Column Headers
		/// </summary>
		public List<ColumnHeader> BufferHeaders
		{
			get
			{
				return bufferHeaderCollection;
			}
		}
		
		/// <summary>
		/// showHideColumns: Allow show/hiding of columns
		/// </summary>
		public bool UseShowHideColumns
		{
			get
			{
				return showHideColumns;
			}
			set
			{
				// Initialize variables used by show/hide
				if (value)
				{
					bufferActiveColumns = new List<bool>();
					bufferItemCollection = new List<ListViewItem>();
					bufferHeaderCollection = new List<ColumnHeader>();
					bufferContextMenu = new ContextMenuStrip();
					this.ContextMenuStrip = bufferContextMenu;
				}
				showHideColumns = value;
			}
		}
		
		public bool SortableColumns
		{
			get
			{
				return sortableColumns;
			}
			set
			{
				if (value)
				{
					lvwColumnSorter = new Lib.ListViewColumnSorter();
					ListViewItemSorter = lvwColumnSorter;
					ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SortableColumnClick);
				}
				sortableColumns = value;
			}
		}
		
		/// <summary>
		/// Auto resize last column to fit
		/// </summary>
		/// <param name="message"></param>
		protected override void WndProc( ref Message message )
		{
		    const int WM_PAINT = 0xf ;
		    
		    switch ( message.Msg )
		    {
		    case WM_PAINT:
		        if ( this.View == View.Details && this.Columns.Count > 1 )
		            this.Columns[this.Columns.Count - 1].Width = -2 ;
		        break ;
		    }
		    

		    base.WndProc( ref message ) ;
		}
	}
		
}
