 
using UnityEngine;
using System.Collections;
 
public class moveOnMouseClick : MonoBehaviour
{
		private Transform myTransform;				// this transform
		private Vector3 destinationPosition;		// The destination Point
		private float destinationDistance;			// The distance between myTransform and destinationPosition
		public GameObject DebugTextGO;
		public Light pointlight;
		static public float moveSpeed = 2;						// The Speed the character will move
		public GameObject onTouchParticle, destPoint;
		Texture2D myNewTexture2D;
		bool flag = true, flag1, bIsPlane;
		
		bool showLoadingBar = false;
		
		static public bool reached;
		GameObject g1;
		
		public GameObject campfire;
		public Texture mapTexture, player;
		public int miniMapSize = 256;
		public float x, z;
		public Rect sourceRect;
		//bool AIAlert = false;
		public static Vector3 playerPos;
		string touchedObjectsName, touchedObjectsTag;
		int hitCount = 0;
		bool enemyHitting = false;
		float damagePerSeconds = 100;
		GameObject healthText;
		bool taskFinished = false;
		public Texture2D aaa;
		

		
		void Start ()
		{
				miniMapSize = Screen.height / 4;
				myTransform = transform;							// sets myTransform to this GameObject.transform
				destinationPosition = myTransform.position;	
				flag1 = true;
				healthText = GameObject.Find ("HealthText");

		}
 
		void Update ()
		{
				
				healthText.GetComponent<TextMesh> ().text = GameEventManager.health.ToString ();
				if (Input.GetMouseButtonDown (2)) {
						Application.LoadLevel (0);
				}
				
				playerPos = this.gameObject.transform.position;
				destinationDistance = Vector3.Distance (destinationPosition, myTransform.position);
 
				// Moves the Player if the Left Mouse Button was clickedj
				if (Input.GetMouseButtonUp (0)) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
					
						if (Physics.Raycast (ray, out hit)) {

								Vector3 fromPosition = this.transform.position;
								Vector3 toPosition = hit.transform.position;
								Vector3 direction = toPosition - fromPosition;
								
/*								Ray ray1 = new Ray (fromPosition, direction);
								RaycastHit hit1;*/
								
								if (hit.collider.tag != "AI") {
										g1 = hit.collider.gameObject;										
								}						
								if (hit.collider.gameObject.name == "Spider" || hit.collider.gameObject.name == "Bat" && enemyHitting) {
										hitCount ++;												
										hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
										HelpText.currentText = "Die Monster die...";
										HelpText.textTimer = 3;													
										if (hitCount >= 4) {
												hitCount = 0;
												enemyHitting = false;												
												HelpText.currentText = "I killed a it...";
												HelpText.textTimer = 7;													
												Destroy (hit.collider.gameObject);
										}
								}

								
								if (hit.collider.gameObject.tag != "HUD" || hit.collider.gameObject.tag != "AI") {
										
										if (hit.collider.tag == "Ground")																			
												destinationPosition = hit.point;
										else												
												destinationPosition = hit.collider.transform.position;
										
										destPoint.transform.position = destinationPosition;// + new Vector3 (destinationPosition.x, 0.05f, destinationPosition.z);
										flag = false;
										bIsPlane = false;
										reached = true;
										if (hit.collider.tag == "Pickable")
												hit.collider.GetComponent<Renderer>().material.color = Color.yellow;
										return;
								}																																																															
						}		
				} else if (Input.GetMouseButton (0)) {

						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit)) {
								if (hit.collider.tag == "Ground") {
										destinationPosition = hit.point;
										destPoint.transform.position = destinationPosition;
								}
						}
				}

				//Actual Player running
				myTransform.position = Vector3.MoveTowards (myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);

	
				if (destinationDistance <= 0.1) {
						//print ("reached");
						destPoint.transform.position = Vector3.zero;
						if (reached && !bIsPlane) {
								reached = false;
								StartCoroutine ("WaitingTime", 2);
						}
				} else {
						StopCoroutine ("WaitingTime");
							
				}
				
				if (taskFinished == true && g1.tag == "Pickable") {						
						taskFinished = false;
						Destroy (g1.gameObject);
				}
		}
		IEnumerator WaitingTime (int secs)
		{
				showLoadingBar = true;
				yield return new WaitForSeconds (0);				
				if (destinationDistance <= 0.01) {
						taskFinished = true;
				} else
						taskFinished = false;
		}
	
		void LateUpdate ()
		{
				if (enemyHitting == true) {
						damagePerSeconds -= Time.deltaTime * 4;
						this.gameObject.GetComponent<Renderer>().material.color = Color.red;
						GameEventManager.health = (int)damagePerSeconds;
						
				} else {
						this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
				}
						
				healthText.GetComponent<TextMesh> ().text = GameEventManager.health.ToString ();
		}

		
		void OnGUI ()
		{
				GUI.color = new Color32 (128, 128, 128, 255);
				Graphics.DrawTexture (new Rect (Screen.width - miniMapSize, Screen.height - miniMapSize, miniMapSize, miniMapSize), 
		                      mapTexture, 
		                      new Rect (myTransform.position.x / z, myTransform.position.z / z, 0.3f, 0.3f), 		                     
		                     0, 0, 0, 0, 
		                     new Color32 (128, 128, 128, 255));
		
				Graphics.DrawTexture (new Rect (Screen.width - miniMapSize, Screen.height - miniMapSize, miniMapSize, miniMapSize), player);
		}
		void OnCollisionEnter (Collision collisionInfo)
		{	
				if (collisionInfo.gameObject.tag == "AI") {
						enemyHitting = true;
						HelpText.currentText = "Ahhh... Help Me";
						HelpText.textTimer = 3;					
				}
		}
		void OnCollisionExit (Collision collisionInfo)
		{	
				if (collisionInfo.gameObject.tag == "AI") {
						enemyHitting = false;				
				}
		}
		
}