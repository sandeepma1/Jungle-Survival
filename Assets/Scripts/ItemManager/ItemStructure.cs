using UnityEngine;
using System.Collections;

public class ItemStructure
{		
		public string itemName;
		public int itemID;
		public string itemDesc;
		public Sprite itemIcon;
		public GameObject itemModel;
		public int itemPower;
		public int itemSpeed;
		public int itemValue;
		public ItemType itemType;
		public bool isItemStackable;
		public int itemMaxStackAmount;
	
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
	
		public ItemStructure (string name, int id, string desc, int power, int speed, int value, ItemType type, bool stackable, int stackAmount)
		{
				itemName = name;
				itemID = id;
				itemDesc = desc;
				itemPower = power;
				itemSpeed = speed;
				itemValue = value;
				itemType = type;
				itemIcon = Resources.Load<Sprite> ("" + name);
				isItemStackable = stackable;
				itemMaxStackAmount = stackAmount;
		
		
		}
		public ItemStructure (string newName)
		{				
				itemName = newName;		
				itemIcon = Resources.Load<Sprite> ("" + itemName);		
		}
		public ItemStructure ()
		{
		
		}
}
