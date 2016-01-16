using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{

		List<string> collectedItems = new List<string> ();
		
		// Use this for initialization
		public static GameObject hitTarget;
		GameObject invSlotGO;
		Vector3 invPos;
		void Start ()
		{
				invSlotGO = GameObject.Find ("InvSlotGO");
				invPos = invSlotGO.transform.position;
				hitTarget = GameObject.Find ("Player");	
		}
	
		// Update is called once per frame
		void Update ()
		{
				CastRay ();
				WaitingForPlayer ();
		}
		
		void CastRay ()
		{
				if (Input.GetMouseButtonDown (0) && GUIUtility.hotControl == 0) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;	
						if (Physics.Raycast (ray, out hit)) {
								hitTarget = hit.collider.gameObject;								
						}
				}
				if (Input.GetMouseButtonDown (1)) {
						GameObject invSlot;
						invSlot = Instantiate (invSlotGO, invPos, invSlotGO.transform.rotation)as GameObject;
						invPos = new Vector3 (invPos.x + 0.2f, invPos.y, invPos.z);
						invSlot.transform.parent = Camera.main.transform;				
				}
		}
		void WaitingForPlayer ()
		{
				if (Player.canNowPick) {
						collectedItems.Add (hitTarget.gameObject.name);
				}
		}
}
