using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

		private Transform myTransform;				// this transform
		private Vector3 destinationPosition;		// The destination Point
		private float destinationDistance;			// The distance between myTransform and destinationPosition
		public float moveSpeed = 2;
		private bool isClicked = false;
		GameObject clickedPosition;
		GameObject bar;
		ItemDatabase database;
		public static float waitTime = 0;
		public static bool canNowPick = false;
		
 
		GameObject tempHit;
		public TextMesh weight3DText;
		
		Inventory inventory;
		bool isPickable, isLeftDestination = false, isHitMoreButton = false;

		
		void Start ()
		{
				database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				bar = GameObject.Find ("playerBar");
				ResetPlayerLoadingBar ();
		
				
//				print (bar.transform.position);
				//inventory = GameObject.Find ("Inventory");	
				//	tempHit = GameObject.Find ("Player");				
				clickedPosition = GameObject.Find ("clickedPosition");
				myTransform = transform;							// sets myTransform to this GameObject.transform
				destinationPosition = myTransform.position;	
		}
	
		// Update is called once per frame
		void Update ()
		{
				weight3DText.text = destinationDistance.ToString ();
				destinationDistance = Vector3.Distance (destinationPosition, myTransform.position);
				
				///	print (destinationDistance);
				
				if (Input.GetMouseButtonDown (2)) {
						Application.LoadLevel (0);
				}
				
				if (Input.GetMouseButtonDown (0) && GameEventManager.GetState () == GameEventManager.E_STATES.e_game) {						
						RayCast ();
						isClicked = true;
						isLeftDestination = false;
				}
				
				//Reached Destination
				if (destinationDistance <= 0.1f && isClicked) {						
						clickedPosition.transform.position = Vector3.zero;
						StartCoroutine ("WaitTime");						
						isClicked = false;
						isPickable = false;
				}
				
				//Left Destination
				if (destinationDistance > 1 && !isLeftDestination) {
						
						isLeftDestination = true;
						ResetPlayerLoadingBar ();
						StopCoroutine ("WaitTime");
				}
				myTransform.position = Vector3.MoveTowards (myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
		}
		IEnumerator WaitTime ()
		{		
				Item currentItem = database.items [0];
				for (int i=0; i<database.items.Count; i++) {
						if (database.items [i].itemName == tempHit.name) {
								currentItem = database.items [i];
								break;
						}					
				}
				
				bar.GetComponent<Renderer>().enabled = true;
				bar.transform.localScale = new Vector3 (0.1f, 1, 0);
				
				Hashtable optional = new Hashtable ();
				optional.Add ("ease", LeanTweenType.notUsed);
				LeanTween.scaleZ (bar, 1, currentItem.itemFetchTime, optional);
				
				yield return new WaitForSeconds (currentItem.itemFetchTime);
				
				ResetPlayerLoadingBar ();				
				tempHit.SetActive (false);
				inventory.addItem (currentItem.itemID);
		}
	
		void RayCast ()
		{
				Plane playerPlane = new Plane (Vector3.up, myTransform.position);
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);						
				float hitdist = 0.0f;
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {						
						tempHit = hit.collider.gameObject;
						//						print (tempHit);						
						switch (tempHit.GetComponent<Collider>().tag) {
						case "HUD":													
						case "Craftable":										
						case "Untagged":
								return;	
						case "Pickable":
								isPickable = true;
								destinationPosition = tempHit.transform.position;
								clickedPosition.transform.position = destinationPosition;
								return;
						}					
				}
				if (playerPlane.Raycast (ray, out hitdist)) {
						Vector3 targetPoint = ray.GetPoint (hitdist);
						destinationPosition = ray.GetPoint (hitdist);						
						clickedPosition.transform.position = destinationPosition;
				}	
				/*Plane playerPlane = new Plane (Vector3.up, myTransform.position);
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);						
				float hitdist = 0.0f;
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit) && hit.collider.gameObject.name != "more") {
						tempHit = hit.collider.gameObject;
						print (tempHit.name + "" + destinationPosition + "" + tempHit.transform.position);
							
						clickedPosition.transform.position = destinationPosition = tempHit.transform.position;		
						
						isHitMoreButton = false;		
						//print (tempHit.na);
						switch (tempHit.collider.tag) {
						case "HUD":													
						case "Craftable":										
						case "Untagged":
								return;	
						case "Pickable":
								isPickable = true;
								clickedPosition.transform.position = destinationPosition = tempHit.transform.position;
								break;
						//default:
								//break;
						}
				} else if (hit.collider.name == "more") {
						isHitMoreButton = true;
				}
				if (playerPlane.Raycast (ray, out hitdist) && !isHitMoreButton) {
						Vector3 targetPoint = ray.GetPoint (hitdist);						
						destinationPosition = ray.GetPoint (hitdist);						
						clickedPosition.transform.position = destinationPosition;
				}	*/				
		}

		public void ResetPlayerLoadingBar ()
		{
				bar.GetComponent<Renderer>().enabled = false;
				bar.transform.localScale = new Vector3 (0.1f, 0, 0);
				LeanTween.cancel (bar);
		}
}
