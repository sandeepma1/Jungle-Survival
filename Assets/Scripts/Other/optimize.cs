using UnityEngine;
using System.Collections;
using System.IO;

public class optimize : MonoBehaviour
{		
		int realX, realXmin1, realXmax1, realZ, realZmin1, realZmax1;
		public GameObject soil, sea, other, grass;
		public int radius = 1, lastRadius;

		public GameObject quad;
		public TextMesh noOfGameObjects;
		int noOfObjects = 0, onlyBlue = 0;
		Vector3 lastPos, curPos, lastDrawnPos = Vector3.zero;
		
		int renderpos;
		
		private Vector3[] a = new Vector3[9];//1, a2, a3, a4, a5, a6, a7, a8, a9;		
		private int[,] grid = new int[Manager.grid, Manager.grid];

		
		void Start ()
		{			
				for (int i = 0; i <99; i++) {
						
						for (int j = 0; j <99; j++) {
								grid [i, j] = 0;								
						}
				}						
		}
			
		// Update is called once per frame
		void Update ()
		{		
				
				
				//Debug.Log (this.gameObject.transform.position + "" + realX + "" + realZ);	
				for (int i = -(radius); i <= (radius); i++) {				
						for (int j = -(radius); j <= (radius); j++) {
								//grid [realX, realZ] = 0;
								GameObject quadDraw = null;
								
								realX = Mathf.FloorToInt (this.gameObject.transform.position.x);
								realZ = Mathf.FloorToInt (this.gameObject.transform.position.z);
								
								realX = realX - j;										
								realZ = realZ + i;					
								if (grid [realX, realZ] == 0) {										
										quadDraw = Instantiate (sea, new Vector3 (realX, 0, realZ), new Quaternion (90, 0, 0, 90)) as GameObject;		
										grid [realX, realZ] = 1;										
										Manager.noObjects ++;		
								}
								if (grid [realX, realZ] == 1) {
										//quadDraw = Instantiate (soil, new Vector3 (realX, 0, realZ), new Quaternion (90, 0, 0, 90)) as GameObject;
								}
								//Debug.Log ("Stop");
						}						
				}						
				
				noOfGameObjects.text = Manager.noObjects.ToString ();
				
				
				if (Input.GetMouseButtonDown (1)) {
						Application.LoadLevel (0);
						Manager.noObjects = 0;
						
				}
		}
}
