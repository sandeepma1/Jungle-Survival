using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour
{
		public GameObject drawPos, emptySlot;
		
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButtonDown (0)) {						
						for (float i = 0; i<9; i++) {								
								for (float j = 0; j<3; j++) {	
										GameObject slots;
										slots = Instantiate (emptySlot, this.transform.position + new Vector3 (i / 10, -j / 10, -0.002f), transform.rotation)as GameObject;										
										slots.transform.parent = drawPos.transform;
										//slots.transform.rotation = this.transform.rotation;								}								
								}
						}
				}
		}
}
