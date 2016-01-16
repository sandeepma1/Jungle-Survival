using UnityEngine;
using System.Collections;

public class Streamer : MonoBehaviour
{
	
		public bool renderAll = true;
		public GameObject soil, sea, rocky, sand, forest;//, tree1, tree2, tree3, berry1, berry2, stone1, stone2, stone3, drybush, flint, AI_Spider;
		public Texture2D aaa;
		int direction;
		Vector3 pos, pos1;
		public GameObject player;
		Vector3 tempPos;
		int instances;
		
	
		void Start ()
		{
				tempPos = player.transform.position = new Vector3 (30, 0, 30);		
				for (int i = (int)tempPos.x -1; i <= (int)tempPos.x + 1; i++) {
						for (int j = (int)tempPos.z -1; j <= (int)tempPos.x + 1; j++) {											
								pos.x = i;
								pos.z = j;												
								if (aaa.GetPixel (i, j) == new Color (0, 1, 0)) {										
										AutoInstantiate (soil, pos);				
								} 				
								if (aaa.GetPixel (i, j) == new Color (0, 1, 1)) {																
										AutoInstantiate (forest, pos);					
								} 
								if (aaa.GetPixel (i, j) == new Color (1, 0, 0)) {										
										AutoInstantiate (rocky, pos);
								} 
				
								if (aaa.GetPixel (i, j) == new Color (1, 1, 0)) {										
										AutoInstantiate (sand, pos);
								} 
								if (aaa.GetPixel (i, j) == new Color (0, 0, 1)) {										
										AutoInstantiate (sea, pos);										
								}
								player.transform.position = player.transform.position;
								
						}
				}		
		}
		void Update ()
		{			
				
				if (Input.GetKeyDown (KeyCode.W)) {	
						player.transform.position = player.transform.position + Vector3.forward;
						direction = 3;	
						print (instances + 3);
						StartStreaming ();
										
				}
				if (Input.GetKeyDown (KeyCode.A)) {
						player.transform.position = player.transform.position + Vector3.left;
						direction = 1;	
						print (instances + 3);
						StartStreaming ();					
				}
				if (Input.GetKeyDown (KeyCode.S)) {
						player.transform.position = player.transform.position + Vector3.back;
						direction = 4;
						print (instances + 3);
						StartStreaming ();
				}
				if (Input.GetKeyDown (KeyCode.D)) {
						player.transform.position = player.transform.position + Vector3.right;
						direction = 2;
						print (instances + 3);
						StartStreaming ();
				}
		}
		void StartStreaming ()
		{
				//print (player.transform.position);
						
			
				//left
				if (direction == 1) {
						AutoInstantiate (soil, new Vector3 (player.transform.position.x - 1, 0, player.transform.position.z + 1));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x - 1, 0, player.transform.position.z));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x - 1, 0, player.transform.position.z - 1));
				}				
				//right
				if (direction == 2) {
						AutoInstantiate (soil, new Vector3 (player.transform.position.x + 1, 0, player.transform.position.z + 1));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x + 1, 0, player.transform.position.z));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x + 1, 0, player.transform.position.z - 1));
				}
				//up
				if (direction == 3) {
						AutoInstantiate (soil, new Vector3 (player.transform.position.x - 1, 0, player.transform.position.z + 1));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x, 0, player.transform.position.z + 1));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x + 1, 0, player.transform.position.z + 1));
				}
				//down
				if (direction == 4) {
						AutoInstantiate (soil, new Vector3 (player.transform.position.x - 1, 0, player.transform.position.z - 1));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x, 0, player.transform.position.z - 1));
						AutoInstantiate (soil, new Vector3 (player.transform.position.x + 1, 0, player.transform.position.z - 1));
				}
				
				
				/*for (int i = (int)player.transform.position.x -1; i <= (int)player.transform.position.x + 1; i++) {
						for (int j = (int)player.transform.position.z -1; j <= (int)player.transform.position.z + 1; j++) {											
								pos.x = i;
								pos.z = j;
				
								if (aaa.GetPixel (i, j) == new Color (0, 1, 0)) {										
										AutoInstantiate (soil, pos);					
								} 				
								if (aaa.GetPixel (i, j) == new Color (0, 1, 1)) {																
										AutoInstantiate (forest, pos);					
								} 
								if (aaa.GetPixel (i, j) == new Color (1, 0, 0)) {										
										AutoInstantiate (rocky, pos);
								} 				
								if (aaa.GetPixel (i, j) == new Color (1, 1, 0)) {										
										AutoInstantiate (sand, pos);
								} 
								if (aaa.GetPixel (i, j) == new Color (0, 0, 1)) {										
										AutoInstantiate (sea, pos);										
								}
								//print (pos);
						}
				}*/
		}
		void AutoInstantiate (GameObject aa, Vector3 posaa)
		{
				GameObject objectInstance;				
				objectInstance = Instantiate (aa, posaa, aa.transform.rotation) as GameObject;		
				objectInstance.transform.parent = this.transform;		
		}
}
