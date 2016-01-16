using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
		public static List <ItemStructure> itemCollected = new List<ItemStructure> ();	
		public static List <ItemStructure> sortedItemCollected = new List<ItemStructure> ();	
		//Adds Item
		public static void AddItem (string name)
		{
				if (itemCollected.Count < 11) {
						itemCollected.Add (new ItemStructure (name));				
				} else {
						print ("Out of slots");
				}
//				print ("added: " + name);
		}
		
		//Removes Item at
		public static void RemoveItem (int index)
		{
				itemCollected.RemoveAt (index);
		}
		
		//Displays all items
		public static void DisplayItem ()
		{
				SortCollectedItem ();
				for (int i = 0; i<itemCollected.Count; i++) {
//						print (sortedItemCollected [i].name);
//						print (sortedItemCollected.Count);
				}
		}
		public static void SortCollectedItem ()
		{
				sortedItemCollected.Clear ();
				for (int i = 0; i<itemCollected.Count; i++) {
						sortedItemCollected.Add (itemCollected [i]);
				}		
		}
}
