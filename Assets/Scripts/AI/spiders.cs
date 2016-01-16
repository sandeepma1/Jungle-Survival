using UnityEngine;
using System.Collections;

public class spiders : MonoBehaviour
{		
		public int health = 100;
		bool AIAlert = false;
		GameObject player;
		Vector3 spwanPos;
		float speed = 1f;
		// Use this for initialization
		void Start ()
		{
				spwanPos = this.gameObject.transform.position;
				player = GameObject.Find ("Player");
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (AIAlert) {
						this.gameObject.transform.position = Vector3.MoveTowards (this.gameObject.transform.position, player.transform.position, (speed + Random.Range (0, 0.5f)) * Time.deltaTime);
						//	print (health);
				} else {
						this.gameObject.transform.position = Vector3.MoveTowards (this.gameObject.transform.position, spwanPos, (speed) * Time.deltaTime);
				}			
				
				if (health < 0) {
						Destroy (this.gameObject);
				}
		}
		void OnTriggerEnter (Collider other)
		{
				if (other.GetComponent<Collider>().gameObject.name == "Capsule") {
						AIAlert = true;						
				}
		}
		void OnTriggerExit (Collider other)
		{
				if (other.GetComponent<Collider>().gameObject.name == "Capsule") {
						AIAlert = false;					
				}
		}
}
