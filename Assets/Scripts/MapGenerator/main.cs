using UnityEngine;
using System.Collections;

public class main : MonoBehaviour
{
	
		public bool renderAll = true;
		public GameObject soil, sea, rocky, sand, forest, tree, berry, stone, drybush, flint, moai, grass, carrot, redmushroom, greenmushroom, bluemushroom;
		public Texture2D aaa;
	
		Vector3 pos, pos1;	
		void Start ()
		{		
				for (int i = 0; i < aaa.width; i++) {
						for (int j = 0; j < aaa.height; j++) {				
								//GameObject objectInstance;								
								pos1.x = pos.x = i;
								pos1.z = pos.z = j;				
								pos1.x += Random.Range (0.2f, 0.8f);
								pos1.z -= Random.Range (0.0f, 0.8f);
								//Soil=> Trees, bush, berries
								if (aaa.GetPixel (i, j) == new Color (0, 1, 0)) {
										AutoInstantiate (soil, pos);
										int x = Random.Range (0, 100);										
										switch (x) {
										case 1:
												AutoInstantiate (bluemushroom, pos1);
												break;
										case 2:
												AutoInstantiate (carrot, pos1);
												break;
										case 3:									
												AutoInstantiate (berry, pos1);
												break;										
										case 4:	
												AutoInstantiate (redmushroom, pos1);
												break;
										case 5:	
												AutoInstantiate (greenmushroom, pos1);
												break;
										case 6:												
										case 7:	
										case 8:
												AutoInstantiate (grass, pos1);
												break;
										case 9:
										case 10:
										case 11:
										case 12:	
						
												AutoInstantiate (drybush, pos1);
												
												break;
										case 31:	
										case 32:	
										case 33:
										case 34:	
										case 35:
										case 36:	
																				
												AutoInstantiate (flint, pos1);
												break;
										case 41:										
											
										default:
												break;
										}							
								} 				
								if (aaa.GetPixel (i, j) == new Color (0, 1, 1)) {																		
										AutoInstantiate (forest, pos);					
										int x = Random.Range (0, 5);
										switch (x) {
										case 0:												
												AutoInstantiate (tree, pos1);
												break;										
										case 1:										
											//	AutoInstantiate (drybush, pos1);
												break;
										default:
												break;					
										}	
								} 
								if (aaa.GetPixel (i, j) == new Color (1, 0, 0)) {
								
										AutoInstantiate (rocky, pos);																																							
										int x = Random.Range (0, 5);
										switch (x) {
										case 0:	
												AutoInstantiate (stone, pos1);
												break;
										case 1:																	
												//AutoInstantiate (drybush, pos1);
												break;
										
										default:
												break;					
										}								
								} 
								if (aaa.GetPixel (i, j) == new Color (1, 1, 0)) {										
										AutoInstantiate (sand, pos);					
								} 
								if (aaa.GetPixel (i, j) == new Color (0, 0, 1)) {									
										AutoInstantiate (sea, pos);										
								}
						}
				}		
		}
		void AutoInstantiate (GameObject aa, Vector3 posaa)
		{				
				GameObject objectInstance;				
				objectInstance = Instantiate (aa, posaa, aa.transform.rotation) as GameObject;	
				objectInstance.name = aa.name;
				//transform.LookAt (objectInstance.transform.position + Camera.main.transform.rotation * Vector3.forward);	
				objectInstance.transform.parent = this.transform;		
		}
}
