/*
 * Created by SharpDevelop.
 * User: Mark Liu
 * Date: 22/04/2010
 * Time: 12:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AtelierElieScripter.AlchemyItems
{
	/// <summary>
	/// Description of AlchemyItemsCollection.
	/// </summary>
	public class Collection
	{
		
		SortedDictionary<int, Item> itemCollection;
		
		public Collection()
		{
			itemCollection = new SortedDictionary<int, Item>();
		}
		
		public Item this [int index]
		{
			get
			{
				return itemCollection[index];
			}
			
			set
			{
				if (!itemCollection.ContainsKey(index))
					itemCollection.Add(index, value);
			}
		}
		public int Count
		{
			get
			{
				return itemCollection.Count;
			}
		}
	}
}
