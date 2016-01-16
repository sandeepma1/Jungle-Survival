using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{		
		bool AIAlert = false;
		Vector3 tempPos;
		Vector3 randomPos;
		Vector3 moveTowards;
		GameObject player;

		public float health = 100;
		
		void Start ()
		{				
		
				player = GameObject.Find ("Player");
				randomPos = this.transform.position;
				moveTowards = tempPos = this.transform.position;
				InvokeRepeating ("StartCos", 0, 4.5f);				
		}
		
		void Update ()
		{				
				if (AIAlert == true) {				
						this.gameObject.transform.position = Vector3.MoveTowards (this.gameObject.transform.position, player.transform.position, (1) * Time.deltaTime);
//						print (health);
						//	AI2.gameObject.transform.position = Vector3.MoveTowards (AI2.gameObject.transform.position, myTransform.position, (moveSpeed - 0.246f) * Time.deltaTime);
				}
				if (AIAlert == false) {				
						this.gameObject.transform.position = Vector3.MoveTowards (this.gameObject.transform.position, moveTowards, (1) * Time.deltaTime);
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
						tempPos = this.transform.position;					
				}
		}
		void StartCos ()
		{
				if (AIAlert == false) {
						StartCoroutine ("WanderCo");
				}
				if (AIAlert == true) {
						StopCoroutine ("WanderCo");
				}
		}
		IEnumerator WanderCo ()
		{				
				randomPos.x = this.transform.position.x + Random.Range (-1, 1);
				randomPos.z = this.transform.position.z + Random.Range (-1, 1);						
				moveTowards = randomPos;
				yield return new WaitForSeconds (1.5f);
				randomPos.x = this.transform.position.x + Random.Range (-1, 1);
				randomPos.z = this.transform.position.z + Random.Range (-1, 1);						
				moveTowards = randomPos;
				yield return new WaitForSeconds (1.5f);				
				moveTowards = tempPos;
				yield return new WaitForSeconds (1.5f);				
		}
		
}
