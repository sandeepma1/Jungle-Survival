using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crafting : MonoBehaviour
{

		public GameObject craftSlotTemplate;
		CraftableDatabase Craftdatabase;
		//Transform
		public List<Craftable>CraftingItems = new List<Craftable> ();
		GameObject tempHit;
		public GameObject itemImage;
		public Texture  itemIcon;
		GameObject selectedSlot;
		// Use this for initialization
		void Start ()
		{
				selectedSlot = GameObject.Find ("selectedSlot");
				Craftdatabase = GameObject.FindGameObjectWithTag ("CraftableDatabase").GetComponent<CraftableDatabase> ();
				int slotNumber = 0;		
				
				for (float i=-4; i<0; i++) {
										
						GameObject slot = (GameObject)Instantiate (craftSlotTemplate);
						slot.transform.parent = this.gameObject.transform;						
						slot.transform.localPosition = new Vector3 (i / 10, 0, -0.01f);
						slot.transform.localRotation = new Quaternion (0, 0, 0, 0);	
						CraftingItems.Add (new Craftable ());
						
						itemImage = slot.transform.FindChild ("icon").gameObject;
						itemIcon = itemImage.GetComponent<Renderer>().material.mainTexture;	
						
						itemImage.GetComponent<Renderer>().material.mainTexture = Craftdatabase.craftables [slotNumber].itemIcon;
						
						slot.name = "CraftableSlot" + slotNumber;
						slotNumber++;			
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButtonDown (0)) {
						DrawSlots ();
						RayCast ();
				}
		
				if (Input.GetMouseButtonDown (1)) {
						/*addItem (Random.Range (1, 6));*/
				}
		}
		
		void DrawSlots ()
		{
		
		}
		void RayCast ()
		{			
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
						selectedSlot.transform.localPosition = hit.collider.gameObject.transform.parent.localPosition;
						//tempHit = hit.collider.gameObject;
						print (hit.collider.gameObject.transform.parent);
				}
		}
}

