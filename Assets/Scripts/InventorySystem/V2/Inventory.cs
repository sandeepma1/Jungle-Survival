 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

		public GameObject slotTemplate, CsoltTemplate;
		public static ItemDatabase database;
		public List<Item>Items = new List<Item> ();
		public List<Item>CraftingItems = new List<Item> ();
		bool isInvUp;
		public TextMesh label;
		GameObject selectedSlot;
		GameObject itemDescription;
		GameObject eatButton;
		GameObject sleepButton;
		GameObject playerRightHand;
		//GameObject playerLog;
		PlayerLog_Stack mainTextNotification;
		Texture blank;
		string selectedItemName = "";
		int hitSlotNumber = -1, hitCraftSlotNumber = -1;
		GameObject sun;
		EpicCitadelControl epicCitadelControl;

		void Start ()
		{
				epicCitadelControl = GameObject.FindGameObjectWithTag ("Player").GetComponent<EpicCitadelControl> ();
				sleepButton = GameObject.Find ("sleepButton");
				sleepButton.SetActive (true);
				sun = GameObject.Find ("0SUN");
//				print (sun.GetComponent<DayNightController> ().currentPhase);
				blank = Resources.Load<Texture> ("blank");
				playerRightHand = GameObject.Find ("PlayerRightHand");
				//craftButton = GameObject.Find ("Craft");
				eatButton = GameObject.Find ("EatButton");
				itemDescription = GameObject.Find ("ItemDescription");
				itemDescription.GetComponentInChildren<TextMesh> ().text = ""; 
				selectedSlot = GameObject.Find ("selectedSlot");
				isInvUp = false;
				ShowHideInventory ();
				
				label.text = "<<<";	
				GameEventManager.SetState (GameEventManager.E_STATES.e_game);
				
				database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();			
				
				mainTextNotification = GameObject.Find ("Notification").GetComponent<PlayerLog_Stack> ();
				
				//mainTextNotification = playerLog.GetComponent<PlayerLog_Stack> ();
				
				int slotNumber = 0, cslotNumber = 0;
				label.text = "<<<";
				float tempi;
				for (float i=-4; i<12; i++) {
						GameObject slot = (GameObject)Instantiate (slotTemplate);
						slot.transform.parent = this.gameObject.transform;						
						
						slot.transform.localPosition = new Vector3 (i / 10, 0.2f, -0.01f);						
						slot.transform.localRotation = new Quaternion (0, 0, 0, 0);
						if (i >= 4) {
								slot.transform.localPosition = new Vector3 ((i - 8) / 10, 0.2f - 0.101f, -0.01f);						
								slot.transform.localRotation = new Quaternion (0, 0, 0, 0);
						}
						
						slot.GetComponent<SlotScript> ().slotNumber = slotNumber;
						slot.name = "Slot" + slotNumber;
						
						Items.Add (new Item ());
						slotNumber++;				
				}			
				for (float i=-4; i<0; i++) {
						GameObject Cslot = (GameObject)Instantiate (CsoltTemplate);
						Cslot.transform.parent = this.gameObject.transform;						
			
						Cslot.transform.localPosition = new Vector3 (i / 10, -0.1f, -0.01f);						
						Cslot.transform.localRotation = new Quaternion (0, 0, 0, 0);
						if (i >= 4) {
								Cslot.transform.localPosition = new Vector3 ((i - 8) / 10, 0.2f - 0.101f, -0.01f);						
								Cslot.transform.localRotation = new Quaternion (0, 0, 0, 0);
						}
			
						Cslot.GetComponent<CraftSlotScript> ().slotNumber = cslotNumber;
						Cslot.name = "CraftSlot" + cslotNumber;
			
						CraftingItems.Add (new Item ());
						cslotNumber++;				
				}
				
				addCraftingItems ();				
				
				/*addItem (1);
				addItem (2);*/
				addItem (3);
/*				addItem (4);
				addItem (5);*/	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButtonDown (0) && GameEventManager.GetState () == GameEventManager.E_STATES.e_game || GameEventManager.GetState () == GameEventManager.E_STATES.e_actionButtonPressed) {
						MoreButtonTouch ();
				}
				if (Input.GetMouseButtonUp (0) && GameEventManager.GetState () == GameEventManager.E_STATES.e_inventoryWindow) {
						RayCast ();
						MoreButtonTouch ();
				}
		}
		
		void LateUpdate ()
		{
				/*	if (sun.GetComponent<DayNightController> ().currentPhase == DayNightController.DayPhase.Night) {
						sleepButton.SetActive (true);
				} 
				if (sun.GetComponent<DayNightController> ().currentCycleTime >= 20 && sun.GetComponent<DayNightController> ().currentCycleTime <= 21) {
						sleepButton.SetActive (false);
						Time.timeScale = 1;
				}*/
		}
	
		void MoreButtonTouch ()
		{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name == "more") {
						isInvUp = ! isInvUp;
						ShowHideInventory ();
				}
		
		}
		void RayCast ()
		{			
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
						if (hit.collider.gameObject.GetComponent<SlotScript> () != null) {
								hitSlotNumber = hit.collider.gameObject.GetComponent<SlotScript> ().slotNumber;
								selectedSlot.transform.localPosition = hit.collider.transform.localPosition;	
							
								if (Items [hitSlotNumber].itemName != "") {							
										itemDescription.transform.FindChild ("ItemName").GetComponent<TextMesh> ().text = Items [hitSlotNumber].itemGives;							
										itemDescription.transform.FindChild ("ItemDesc").GetComponent<TextMesh> ().text = Items [hitSlotNumber].itemDesc;
								}
						
								if (Items [hitSlotNumber].itemType == Item.ItemType.Consumable) {
										eatButton.transform.FindChild ("name").GetComponent<TextMesh> ().text = "Eat";
										selectedItemName = Items [hitSlotNumber].itemGives;
								} else {
										eatButton.transform.FindChild ("name").GetComponent<TextMesh> ().text = "";
								}
								if (Items [hitSlotNumber].itemType == Item.ItemType.Weapon) {
										playerRightHand.GetComponent<Renderer>().material.mainTexture = Items [hitSlotNumber].itemIcon;
										GameEventManager.rightHandWeapon = Items [hitSlotNumber].itemName;
								} 
								if (Items [hitSlotNumber].itemName == null) {
										playerRightHand.GetComponent<Renderer>().material.mainTexture = blank;
										GameEventManager.rightHandWeapon = "";
								}
						}

						if (hit.collider.gameObject.GetComponent<CraftSlotScript> () != null) {								
								hitCraftSlotNumber = hit.collider.gameObject.GetComponent<CraftSlotScript> ().slotNumber;
								selectedSlot.transform.localPosition = hit.collider.transform.localPosition;
								if (CraftingItems [hitCraftSlotNumber].itemName != "") {							
										itemDescription.transform.FindChild ("ItemName").GetComponent<TextMesh> ().text = "Craft " + CraftingItems [hitCraftSlotNumber].itemName;							
										itemDescription.transform.FindChild ("ItemDesc").GetComponent<TextMesh> ().text = CraftingItems [hitCraftSlotNumber].itemDesc;
								}				
								if (CraftingItems [hitCraftSlotNumber].isCraftable == true) {
										eatButton.transform.FindChild ("name").GetComponent<TextMesh> ().text = "Craft";
										selectedItemName = CraftingItems [hitCraftSlotNumber].itemName;
								} else {
										eatButton.transform.FindChild ("name").GetComponent<TextMesh> ().text = "";
								}							
						}
						switch (hit.collider.gameObject.name) {
						case "sleepButton":
								Time.timeScale = 10;	//10x GameSpeed	
								HelpText.MainNotification ("Sleeping...");								
								break;
						case "EatButton":
								if (eatButton.transform.FindChild ("name").GetComponent<TextMesh> ().text == "Eat")
										removeItem (selectedItemName);
								if (eatButton.transform.FindChild ("name").GetComponent<TextMesh> ().text == "Craft")
										CraftItem (selectedItemName);
								break;
						default:
								Time.timeScale = 1;								
								break;
						}
				} 
		}
		
		void CraftItem (string itemName)
		{
				for (int i =0; i<database.craftableitems.Count; i++) {
						if (database.craftableitems [i].itemName == itemName) {
								searchForItems (itemName, database.craftableitems [i].itemA, database.craftableitems [i].itemB, database.craftableitems [i].itemAValue, database.craftableitems [i].itemAValue);
								break;
						}
				}
		}
		
		void searchForItems (string crafter, string aname, string bname, int avalue, int bvalue)
		{
				string itemA = "x", itemB = "x";
				for (int i=0; i<Items.Count; i++) {
						if (Items [i].itemGives == aname && Items [i].itemValue >= avalue) {	
								itemA = Items [i].itemGives;
						}
						if (Items [i].itemGives == bname && Items [i].itemValue >= bvalue) {				
								itemB = Items [i].itemGives;
						}
				}
				if (itemA != "x" && itemB != "x") {
						for (int i=0; i<=avalue; i++) {
								removeItem (itemA);								
						}
						for (int i=0; i<=bvalue; i++) {
								removeItem (itemB);
						}
						for (int i =0; i<database.craftableitems.Count; i++) {	
								if (database.craftableitems [i].itemName == crafter && database.craftableitems [i].isItemPlacable == true) {
										epicCitadelControl.PlaceItemOnGround ();
										//PlaceItemOnGround (database.craftableitems [i].itemName);
										break;
								}
								if (database.craftableitems [i].itemName == crafter && database.craftableitems [i].isItemPlacable == false) {
										addItemByName (crafter);
										break;
								}
						}
				} else {
						mainTextNotification.AddEvent ("Need more ingredients");
				}				
		}
		
		
		
		void ShowHideInventory ()
		{
				//print (isInvUp);
				if (isInvUp) { 
						//up
						label.text = ">>>";
						GameEventManager.SetState (GameEventManager.E_STATES.e_inventoryWindow);
						Hashtable optional = new Hashtable ();
						optional.Add ("ease", LeanTweenType.easeInOutQuad);
						LeanTween.moveLocal (this.gameObject, new Vector3 (0, 0, 0.6f), 0.3f, optional);
						//LeanTween.moveLocal (Camera.main.gameObject, new Vector3 (-2.5f, 4.34f, -1f), 0.3f, optional);
						LeanTween.scale (this.gameObject, new Vector3 (1f, 1f, 1f), 0.3f, optional);
						GameEventManager.SetState (GameEventManager.E_STATES.e_inventoryWindow);
						
				} 
				if (!isInvUp) {
						//Down
						label.text = "<<<";	
						Hashtable optional = new Hashtable ();
						optional.Add ("ease", LeanTweenType.easeInOutQuad);
						LeanTween.moveLocal (this.gameObject, new Vector3 (0, -0.6f, 0.78f), 0.3f, optional);
						//LeanTween.moveLocal (Camera.main.gameObject, new Vector3 (-0.5f, 7f, -2.5f), 0.3f, optional);
						LeanTween.scale (this.gameObject, new Vector3 (1f, 1f, 1f), 0.3f, optional);
						GameEventManager.SetState (GameEventManager.E_STATES.e_game);
				}
		}
				
		/*public void convertNameToID (string name)
		{
				switch (name) {
				case "tree":
						name = "log";
						break;
				case "dryBush":
						name = "stick";
						break;
				case "stone":
						name = "stone";
						break;
				}
				for (int i=0; i<database.items.Count; i++) {
						if (database.items [i].itemName == name) {
								int id;
								id = database.items [i].itemID;								
								addItem (id);	
						}
				}				
		}*/
		public void addItem (int id)
		{
//				print ("Items Added with index: " + id);
				for (int i=0; i<database.items.Count; i++) {
						if (database.items [i].itemID == id) {
								
								mainTextNotification.AddEvent ("You got 1+" + database.items [i].itemGives);
								Item item = database.items [i];								
								if (database.items [i].isItemStackable == true) {
										checkIfItemExists (id, item);
										break;
								} else {
										addItemAtEmptySlot (item);
								}								
						}
				}
		}
		public void  checkIfItemExists (int itemID, Item item)
		{				
				for (int i=0; i<Items.Count; i++) {
						if (Items [i].itemID == itemID) {
								Items [i].itemValue = Items [i].itemValue + 1;								
								break;
						} else if (i == Items.Count - 1) {
								addItemAtEmptySlot (item);
						}
				}
		}
		public void addItemAtEmptySlot (Item item)
		{
				for (int i = 0; i<Items.Count; i++) {
						if (Items [i].itemGives == null) {
								Items [i] = item;
								Items [i].itemValue = 1;
								break;
						}
				}
		}
		public void addCraftingItems ()
		{
				for (int i=0; i<4; i++) {
						Item item = database.craftableitems [i];
						CraftingItems [i] = item;
				}
		}
		void removeItem (string name)
		{
				for (int i = 0; i<Items.Count; i++) {
						if (Items [i].itemGives == name) {//								
								Items [i].itemValue --;
								mainTextNotification.AddEvent ("-1 " + Items [i].itemGives);
								if (Items [i].itemType == Item.ItemType.Consumable) {
										GameEventManager.hunger += 10;
								}
								if (Items [i].itemValue <= 0) {			
										Items [i] = new Item ();							
								}
								break;
						}
				}
		}
		
		public void addItemByName (string name)
		{
				for (int i = 0; i<Items.Count; i++) {
						if (database.items [i].itemGives == name) {
								addItem (database.items [i].itemID);
								break;
						} 
				}
		}
}
