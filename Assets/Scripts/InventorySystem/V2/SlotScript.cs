using UnityEngine;
using System.Collections;


/*public class SlotScript : MonoBehaviour
{

		public int slotNumber;
		GameObject childGO;
		// Use this for initialization
		void Start ()
		{
				childGO = transform.FindChild ("icon").gameObject;				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
*/
public class SlotScript : MonoBehaviour
{
		//public Item item;
		GameObject itemImage;
		public int slotNumber;
		Inventory inventory;
		ItemDatabase database;
		TextMesh itemAmount;

		void Start ()
		{
				//itemAmount = gameObject.transform.GetChild (1).GetComponent<TextMesh> ();
				itemAmount = this.gameObject.GetComponentInChildren<TextMesh> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				itemImage = transform.FindChild ("icon").gameObject;
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
				if (inventory.Items [slotNumber].itemName != null) {
						itemAmount.text = "";
						itemImage.GetComponent<Renderer>().enabled = true;
						itemImage.GetComponent<Renderer>().material.mainTexture = inventory.Items [slotNumber].itemIcon;						
						if (inventory.Items [slotNumber].isItemStackable == true) {
								itemAmount.GetComponent<Renderer>().enabled = true;
								itemAmount.text = "" + inventory.Items [slotNumber].itemValue;
						}
				} else {
						itemImage.GetComponent<Renderer>().enabled = false;
						itemAmount.text = "";
				}
		}
		
		
}	


/*
		public void OnPointerUp ( data)
		{
				if (inventory.Items [slotNumber].itemType == Item.ItemType.Consumable) {
						inventory.Items [slotNumber].itemValue --; // consumed consumable items so -- done
						if (inventory.Items [slotNumber].itemValue <= 0) {
								inventory.Items [slotNumber] = new Item ();
								itemAmount.enabled = false;
								inventory.closeTooltip ();
						}
				}
		
		}
		public void OnPointerDown (PointerEventData data)
		{
				print (inventory.Items [slotNumber].itemName);
		
				if (inventory.Items [slotNumber].itemName == null) {
						if (inventory.draggingItem) {
								inventory.Items [slotNumber] = inventory.draggedItem;
								inventory.closeDraggedItem ();
						}
			
				} else {
						if (inventory.draggingItem) {
								if (inventory.Items [slotNumber].itemName != null) {
										inventory.Items [inventory.indexOfDraggedItems] = inventory.Items [slotNumber];
										inventory.Items [slotNumber] = inventory.draggedItem;
										inventory.closeDraggedItem ();
								}
						}
				}
		}
		public void OnPointerEnter (PointerEventData data)
		{
				if (inventory.Items [slotNumber].itemName != null && !inventory.draggingItem) {
						inventory.showTooltip (inventory.Slots [slotNumber].GetComponent<RectTransform> ().localPosition, inventory.Items [slotNumber]);
				}
				OnPointerDown (data);
		
		
		}
		public void OnPointerExit (PointerEventData data)
		{
				if (inventory.Items [slotNumber].itemName != null) {
						inventory.closeTooltip ();
				}
		}
		public void OnDrag (PointerEventData data)
		{
				//****************************************************************
				// Temp Fix because of right click not working..
				if (inventory.Items [slotNumber].itemType == Item.ItemType.Consumable) {
						inventory.Items [slotNumber].itemValue ++;
				}
				//****************************************************************
				if (inventory.Items [slotNumber].itemName != null) {
						inventory.showDraggedItem (inventory.Items [slotNumber], slotNumber);
						inventory.Items [slotNumber] = new Item ();
						itemAmount.enabled = false;
				}
		}
}*/

