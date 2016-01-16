using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
		//	public GameObject slot;
		Vector3 slotPos;
		public static List<GameObject> quickSlot = new List<GameObject> ();
		public static List<GameObject> craftSlot = new List<GameObject> ();
		//DrawingSlots
		public GameObject drawPos, emptySlot;
		//more button
		public TextMesh label;
		public GameObject InventoryWindow;
		bool isInvUp = true;
		GameObject moreButton;
		public Texture axeTex;
		public static Texture axe;
		
		// Use this for initialization
		void Start ()
		{			
				ShowHideInventory ();
				axe = axeTex;	
				//slot = GameObject.Find ("Inv_Slot");
				moreButton = GameObject.Find ("more");
				label.text = "<<<";
				GameEventManager.SetState (GameEventManager.E_STATES.e_game);
				
				for (int i = 0; i<11; i++) {
						quickSlot.Add (GameObject.Find ("Inv_Slot" + i));
						craftSlot.Add (GameObject.Find ("Craft_Slot" + i));
				}
				
		}	
		// Update is called once per frame
		
		void Update ()
		{
				if (Input.GetMouseButtonUp (0)) {
						ProcessInventoryTouch ();															
				}
				if (Player.canNowPick) {
						Player.canNowPick = true; 									
						//DrawInvSlots ();
						//ShowHideInventory ();						
				}				
		}
		public static void DrawInvSlots ()
		{
				for (int i = 0; i < ItemManager.sortedItemCollected.Count; i++) {						
						quickSlot [i].GetComponentInChildren<TextMesh> ().text = ItemManager.sortedItemCollected [i].itemName;						
						quickSlot [i].gameObject.transform.GetChild (0).GetComponent<Renderer>().material.mainTexture = ItemManager.sortedItemCollected [i].itemIcon.texture;
						print (ItemManager.sortedItemCollected [i].itemName);
				}
				craftSlot [0].gameObject.transform.GetChild (0).GetComponent<Renderer>().material.mainTexture = axe;
		}
		/*void DrawInvSlots ()
		{
//				print (this.transform.position);
				//	GameObject invSlot;				
				//	slotPos = slotPos + new Vector3 (slotPos.x + 0.01f, slotPos.y, slotPos.z); 
				//slotPos = this.transform.position;
				
				//slotPos = new Vector3 (this.transform.position.x - 0.4f, this.transform.position.y + 0.22f, this.transform.position.z); 
				//invSlot = Instantiate (slot, slotPos, this.transform.rotation) as GameObject;
				
				//invSlot.transform.parent = this.transform;
				//(Instantiate (slot, slotPos, slot.transform.rotation) as GameObject).transform.parent = this.transform;				
			
		}*/
		
		void ProcessInventoryTouch ()
		{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {	
						//print (hit.collider.gameObject.transform.parent.name);					
						//if (hit.collider.gameObject.transform.parent.name)
						
						if (hit.collider.gameObject.name == "more") {
								isInvUp = ! isInvUp;
								ShowHideInventory ();
						}
						
						
						
						
						/*switch (hit.collider.gameObject.name) {							
						case "more":
								isInvUp = ! isInvUp;
								ShowHideInventory ();
								break;
						default:
								break;
						}	*/
				}
				
		}
		void ShowHideInventory ()
		{
				if (isInvUp) { 
						//up
						label.text = ">>>";
						GameEventManager.SetState (GameEventManager.E_STATES.e_inventoryWindow);
						Hashtable optional = new Hashtable ();
						optional.Add ("ease", LeanTweenType.easeInOutQuad);
						LeanTween.moveLocal (InventoryWindow, new Vector3 (0, 0, 0.67f), 0.3f, optional);
						LeanTween.moveLocal (Camera.main.gameObject, new Vector3 (-2.5f, 4.34f, -1f), 0.3f, optional);
						LeanTween.scale (InventoryWindow, new Vector3 (1.5f, 1.5f, 1.5f), 0.3f, optional);
				} 
				if (!isInvUp) {
						//Down
						label.text = "<<<";	
						Hashtable optional = new Hashtable ();
						optional.Add ("ease", LeanTweenType.easeInOutQuad);
						LeanTween.moveLocal (InventoryWindow, new Vector3 (0, 0, 0.02f), 0.3f, optional);
						LeanTween.moveLocal (Camera.main.gameObject, new Vector3 (-0.5f, 7f, -2.5f), 0.3f, optional);
						LeanTween.scale (InventoryWindow, new Vector3 (0.8f, 0.8f, 0.8f), 0.3f, optional);
						GameEventManager.SetState (GameEventManager.E_STATES.e_game);
			
				}
		}
}
