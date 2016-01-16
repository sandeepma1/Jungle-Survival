using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftableDatabase : MonoBehaviour
{
		public List<Craftable> craftables = new List<Craftable> ();
	
		// Use this for initialization
		void Start ()
		{
				craftables.Add (new Craftable ("axe", 0, "wood Axe", Craftable.ItemType.Weapon));
				craftables.Add (new Craftable ("pickaxe", 1, "wood Pickaxe", Craftable.ItemType.Weapon));
				craftables.Add (new Craftable ("spear", 2, "wood Spear", Craftable.ItemType.Weapon));
				craftables.Add (new Craftable ("campfire", 3, "Campfire", Craftable.ItemType.Weapon));

		}	
}
