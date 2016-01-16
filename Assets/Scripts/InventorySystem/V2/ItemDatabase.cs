using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
		public List<Item> items = new List<Item> ();
		public List<Item> craftableitems = new List<Item> ();
	
		// Use this for initialization
		void Start ()
		{
				//                   (name, gives, id, desc, fetchTime, power, speed, value, type, stackable, stackAmount, craftable, AName, AValue, BName, BValue)
				items.Add (new Item ("a", "", 0, "some log", 0, 10, 10, 1, Item.ItemType.Resource, true, 10, false, "", 0, "", 0, false));
				items.Add (new Item ("flint", "flint", 1, "Used to create \n tools and weapons", 0.1f, 10, 10, 1, Item.ItemType.Resource, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("Dry Bush", "stick", 2, "Used to create \n tools and weapons", 0.3f, 10, 10, 1, Item.ItemType.Resource, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("berry", "berry", 3, "Decrease Hunger", 1, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("stone", "stone", 4, "Build Structures", 7, 10, 10, 1, Item.ItemType.Resource, true, 0, false, "", 0, "", 0, false));
				items.Add (new Item ("tree", "log", 5, "Tree log", 2, 10, 10, 1, Item.ItemType.Resource, true, 10, false, "", 0, "", 0, false));
				items.Add (new Item ("bed", "bed", 6, "Sleep through night", 0, 10, 10, 1, Item.ItemType.Resource, true, 10, false, "", 0, "", 0, false));
				items.Add (new Item ("grass", "Cut Grass", 7, "Some grass", 0.3f, 10, 10, 1, Item.ItemType.Resource, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("carrot", "rawcarrot", 8, "Decrease Hunger", 1, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("bluemushroom", "bluecap", 9, "Decrease Hunger & Health", 1, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("redmushroom", "redcap", 10, "Decrease Hunger & Health", 1, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("greenmushroom", "greencap", 11, "Decrease Hunger & Health", 1, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				
				items.Add (new Item ("bat", "Bat Meat", 50, "Decrease Hunger \n but poisons", 3, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				items.Add (new Item ("spider", "Spider Meat", 51, "Decrease Hunger \n but poisons", 3, 10, 10, 1, Item.ItemType.Consumable, true, 15, false, "", 0, "", 0, false));
				
				items.Add (new Item ("axe", "axe", 100, "Requires \n 2xStick 2xFlint", 0, 10, 10, 1, Item.ItemType.Weapon, false, 0, true, "stick", 2, "flint", 2, false));
				items.Add (new Item ("pickaxe", "pickaxe", 101, "Requires \n 2xStick 2xFlint", 0, 10, 10, 1, Item.ItemType.Weapon, false, 0, true, "stick", 2, "flint", 2, false));
				items.Add (new Item ("spear", "spear", 102, "Requires \n 2xStick 1xFlint", 0, 10, 10, 1, Item.ItemType.Weapon, false, 0, true, "stick", 2, "flint", 1, false));
				items.Add (new Item ("campfire", "campfire", 103, "Requires \n 4xStone 3xlog", 0, 10, 10, 1, Item.ItemType.Build, false, 0, true, "stone", 4, "log", 3, true));	
				
				
				
				craftableitems.Add (new Item ("axe", "axe", 0, "Requires \n 2xStick 2xFlint", 0, 10, 10, 1, Item.ItemType.Weapon, false, 0, true, "stick", 2, "flint", 2, false));
				craftableitems.Add (new Item ("pickaxe", "pickaxe", 1, "Requires \n 2xStick 2xFlint", 0, 10, 10, 1, Item.ItemType.Weapon, false, 0, true, "stick", 2, "flint", 2, false));
				craftableitems.Add (new Item ("spear", "spear", 2, "Requires \n 2xStick 1xFlint", 0, 10, 10, 1, Item.ItemType.Weapon, false, 0, true, "stick", 2, "flint", 1, false));
				craftableitems.Add (new Item ("campfire", "campfire", 3, "Requires \n 4xStone 3xlog", 0, 10, 10, 1, Item.ItemType.Build, false, 0, true, "stone", 4, "log", 3, true));				
		}		
}
  