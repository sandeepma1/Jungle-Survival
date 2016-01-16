using UnityEngine;
using System.Collections;

public class Craftable
{
		public string itemName;
		public int itemID;
		public string itemDesc;
		public Texture itemIcon;
		public GameObject itemModel;	
		
		public ItemType itemType;	
	
		public enum ItemType
		{
				Weapon,
				Consumable,
				Quest,
				Survival,
				Build,
				Head,
				Shoes,
				Chest,
				Trousers,
				Necklace,
				Rings,
				Hands
		}
	
		public Craftable (string name, int id, string desc, ItemType type)
		{
				itemName = name;
				itemID = id;
				itemDesc = desc;
				itemType = type;
				itemIcon = Resources.Load<Texture> ("" + name);	
		}
		public Craftable ()
		{
		
		}
	
}
