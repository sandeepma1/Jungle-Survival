using UnityEngine;
using System.Collections;

public class AI_SpiderDen : MonoBehaviour
{
	

		public GameObject spawnPos;
		GameObject spider;
		public float health = 100;
		
	
		void Start ()
		{
				spider = GameObject.Find ("spider");
		}
	
		void Update ()
		{				
		}
		void OnTriggerEnter (Collider other)
		{
				if (other.GetComponent<Collider>().gameObject.name == "Capsule") {							
						print ("trigg");
						InvokeRepeating ("StartCos", 0, 5);
				}
		}
		void OnTriggerExit (Collider other)
		{
				if (other.GetComponent<Collider>().gameObject.name == "Capsule") {
						print ("111trigg");						
						CancelInvoke ();			
				}
		}
		
		void StartCos ()
		{
				GameObject spiderGO = (GameObject)Instantiate (spider);
				spiderGO.name = "spider";	
				spiderGO.transform.position = spawnPos.transform.position;				
				spiderGO.transform.rotation = spawnPos.transform.rotation;	
				
		}
}
