using UnityEngine;
using System.Collections;

public class CraftSlotScript : MonoBehaviour
{		
		GameObject itemImage;
		public int slotNumber;
		Inventory inventory;
		ItemDatabase database;
		TextMesh itemAmount;
	
		void Start ()
		{
				itemAmount = this.gameObject.GetComponentInChildren<TextMesh> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				itemImage = transform.FindChild ("icon").gameObject;
		}
	
		void LateUpdate ()
		{
				if (inventory.Items [slotNumber].itemName != null) {
						itemAmount.text = "";
						itemImage.GetComponent<Renderer>().enabled = true;
						itemImage.GetComponent<Renderer>().material.mainTexture = inventory.CraftingItems [slotNumber].itemIcon;						
						if (inventory.CraftingItems [slotNumber].isItemStackable == true) {
								itemAmount.GetComponent<Renderer>().enabled = true;
								itemAmount.text = "" + inventory.CraftingItems [slotNumber].itemValue;
						}
				} else {
						itemImage.GetComponent<Renderer>().enabled = false;
						itemAmount.text = "";
				}
		}
}
