using UnityEngine;
using System.Collections;

public class Item
{
		public string itemName;
		public int itemID;
		public string itemDesc;
		public Texture itemIcon;
		public GameObject itemModel;
		public int itemPower;
		public int itemSpeed;
		public int itemValue;
		public ItemType itemType;
		public bool isItemStackable;
		public bool isItemPlacable;
		public int itemMaxStackAmount;
		public bool isCraftable;
		public string itemA;
		public int itemAValue;
		public string itemB;
		public int itemBValue;
		public string itemGives;
		public float itemFetchTime;
	
		//Not implemented yet
		//public string itemGives;
		//or
		//	public string itemParentName;
		
		public float itemDegrade;
		public bool isDegradeable;
		
		
	
	
		public enum ItemType
		{
				Weapon,
				Resource,
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
		public enum WeaponType
		{
				Blunt,
				Point,
				Blade
		}
		public enum ToolType
		{
				Blunt,
				Point,
				Blade
		}
	
		public Item (string name, string gives, int id, string desc, float fetchTime, int power, int speed, int value, ItemType type, bool stackable, int stackAmount, bool craftable, string AName, int AValue, string BName, int BValue, bool placable)
		{
				itemName = name;
				itemGives = gives;
				itemFetchTime = fetchTime;
				itemID = id;
				itemDesc = desc;
				itemPower = power;
				itemSpeed = speed;
				itemValue = value;
				itemType = type;
				itemIcon = Resources.Load<Texture> ("" + gives);
				isItemStackable = stackable;
				isItemPlacable = placable;
				itemMaxStackAmount = stackAmount;
				isCraftable = craftable;
				itemA = AName;
				itemAValue = AValue;
				itemB = BName;
				itemBValue = BValue;
		  
		}
		public Item ()
		{
				//itemValue = 0;
		}
		
	
}
