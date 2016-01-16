using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class EpicCitadelControl : MonoBehaviour
{
		public GameObject clickedPos, button;
		public bool kJoystikEnabled = true;
		float kJoystickSpeed;
		public bool kInverse = false;
		public float kMovementSpeed = 1;
		public ClickToMove clickToMoveScript;
		Transform ownTransform;
		Transform cameraTransform;
		CharacterController characterController;
		Camera _camera;
	
		int leftFingerId = -1;
		int rightFingerId = -1;
		Vector2 leftFingerStartPoint;
		Vector2 leftFingerCurrentPoint;
		Vector2 rightFingerStartPoint;
		Vector2 rightFingerCurrentPoint;
		Vector2 rightFingerLastPoint;
		bool isRotating;
		bool isMovingToTarget = false;
		Vector3 targetPoint;
		Rect joystickRect;
		Rect actualJoyStick;		
		
		ItemDatabase database;
		Inventory inventory;
		GameObject bar, playerBarBorder;
	
		float timer = 0, waitTime = 1;

		private string hitObjectName = "", hitObjectTag = "";
		private GameObject hitObjectGO;
		
		bool canPerformAction = false;		
		Item currentItem;
		
		float DPSTimer = 0;
		
		public GameObject campfire, water, AI, droppedItem;
		public Transform moveToAfterPick;
		public static string debugText;
		
		GameObject colHitGO;
		
		void Start ()
		{
				button.SetActive (false);
//				Physics.IgnoreCollision (AI.collider, collider);
				//button.GetComponentInChildren<TextMesh> ().text = "";
				//joystickRect = new Rect (0, 0, Screen.width, Screen.height);
				joystickRect = new Rect (0, 0, Screen.width * 0.2f, Screen.height * 0.2f);
				//				print (joystickRect);
				actualJoyStick = new Rect (joystickRect.x, joystickRect.y + (Screen.height - Screen.height * 0.2f), joystickRect.width, joystickRect.height);
				ownTransform = transform;
				cameraTransform = Camera.main.transform;
				characterController = GetComponent<CharacterController> ();
				_camera = Camera.main;
				kJoystickSpeed = kMovementSpeed / 10;
				database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				bar = GameObject.Find ("playerBar");
				playerBarBorder = GameObject.Find ("playerBarBorder");
				playerBarBorder.GetComponent<Renderer>().enabled = false;			
		
		}
	
		void OnGUI ()
		{
				if (GameEventManager.GetState () == GameEventManager.E_STATES.e_game || GameEventManager.GetState () == GameEventManager.E_STATES.e_actionButtonPressed) {		
						GUI.Box (actualJoyStick, GameEventManager.GetState ().ToString ());
				}
		}
		
		void Update ()
		{
				RayCastForActionButton ();
				if (GameEventManager.GetState () == GameEventManager.E_STATES.e_inventoryWindow) {
						if (Input.GetMouseButton (1)) {
								inventory.addItem (Random.Range (0, 6));
						}
				}
				if (GameEventManager.GetState () == GameEventManager.E_STATES.e_game) {						
						if (Application.isEditor) {								
								if (Input.GetMouseButtonDown (0)) {
										for (int i=0; i<database.items.Count; i++) {
												if (database.items [i].itemName == hitObjectName) {
														currentItem = database.items [i];
														break;
												}					
										}
										OnTouchBegan (0, Input.mousePosition);
								} else if (Input.GetMouseButtonUp (0))
										OnTouchEnded (0);
								else if (leftFingerId != -1 || rightFingerId != -1)
										OnTouchMoved (0, Input.mousePosition);
						} else {
								int count = Input.touchCount;			
								for (int i = 0; i < count; i++) {	
										Touch touch = Input.GetTouch (i);				
										if (touch.phase == TouchPhase.Began)
												OnTouchBegan (touch.fingerId, touch.position);
										else if (touch.phase == TouchPhase.Moved)
												OnTouchMoved (touch.fingerId, touch.position);
										else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
												OnTouchEnded (touch.fingerId);
								}
						}
						
						
						if (leftFingerId != -1)
								MoveFromJoystick ();
						else if (isMovingToTarget)
								MoveToTarget ();
		
						/*if (rightFingerId != -1 && isRotating)
						Rotate ();*/
				}
				if (GameEventManager.GetState () == GameEventManager.E_STATES.e_inventoryWindow) {	
						button.SetActive (false);
						//button.GetComponentInChildren<TextMesh> ().text = "";
				}
				if (Input.GetMouseButtonDown (2)) {
						Application.LoadLevel (0);
				}
				if (Input.GetKeyDown (KeyCode.P)) {
						PlaceItemOnGround ();					
				}
				if (hitObjectName == "bat") {
						DPSTimer += Time.deltaTime;
						if (DPSTimer > 1) {
								GameEventManager.health = GameEventManager.health - 3f;
								DPSTimer = 0;
						}						
				}
		}
		
		public void PlaceItemOnGround ()
		{
				GameObject placableItem = (GameObject)Instantiate (campfire);
				placableItem.transform.parent = this.gameObject.transform;	
				placableItem.transform.localPosition = new Vector3 (0, -1.25f, -2f);						
				placableItem.transform.rotation = campfire.transform.rotation;	
				placableItem.transform.parent = water.gameObject.transform;					
		}
		void RayCastForActionButton ()
		{
				if (Input.GetMouseButtonDown (0) && GameEventManager.GetState () != GameEventManager.E_STATES.e_inventoryWindow) {
						Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;			
						if (Physics.Raycast (ray, out hit) && hit.collider.name == "ActionButton" || hit.collider.tag == "HUD" && hitObjectGO.GetComponent<Collider>().tag != null) {	
//								print(hit);
								clickToMoveScript.enabled = false;
								CheckForAction ();
						}
				} 
				if (Input.GetMouseButton (0) && GameEventManager.GetState () != GameEventManager.E_STATES.e_inventoryWindow) {
						Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
						RaycastHit hit;			
						if (Physics.Raycast (ray, out hit) && hit.collider.name == "ActionButton" && canPerformAction) {
								clickToMoveScript.enabled = false;
								performAction ();														
						}												
				} else if (Input.GetMouseButtonUp (0) && GameEventManager.GetState () != GameEventManager.E_STATES.e_inventoryWindow) {
						GameEventManager.SetState (GameEventManager.E_STATES.e_game);
						clickToMoveScript.enabled = true;
						clickedPos.transform.position = Vector3.zero;						
				}
		}
		
		void CheckForAction ()
		{						

				switch (hitObjectName) {
				case "tree":
						if (GameEventManager.rightHandWeapon == "axe") {
								canPerformAction = true;
						} else {
								HelpText.MainNotification ("Need Axe!!");
								canPerformAction = false;
								GameEventManager.SetState (GameEventManager.E_STATES.e_actionButtonPressed);							
						}
						break;
				case "stone":
						if (GameEventManager.rightHandWeapon == "pickaxe") {
								canPerformAction = true;
						} else {
								HelpText.MainNotification ("Need Pickaxe!!");
								canPerformAction = false;
								GameEventManager.SetState (GameEventManager.E_STATES.e_actionButtonPressed);
						}
						break;
				default:
						canPerformAction = true;
						break;
				}
				
		}
		
		void performAction ()
		{
				GameEventManager.SetState (GameEventManager.E_STATES.e_actionButtonPressed);				
				if (timer <= 0) {
						for (int i=0; i<database.items.Count; i++) {
								if (database.items [i].itemName == hitObjectName) {
										currentItem = database.items [i];
										waitTime = currentItem.itemFetchTime;
										
										playerBarBorder.GetComponent<Renderer>().enabled = true;
										bar.transform.localScale = new Vector3 (0.1f, 1, 0);
										Hashtable optional = new Hashtable ();
										Hashtable optional1 = new Hashtable ();										
										LeanTween.scaleZ (bar, 1, waitTime, optional);
										break;
								}
						}
				}
				timer += Time.deltaTime;
				if (timer > waitTime) {
						GameObject currentDroppedItem = (GameObject)Instantiate (droppedItem);
						//placableItem.transform.parent = this.gameObject.transform;						
						//print (currentDroppedItem.collider.name);
						//currentDroppedItem.collider.name
						
						float ranpos = 0.3f;
						currentDroppedItem.transform.position = new Vector3 (hitObjectGO.transform.position.x + Random.Range (-ranpos, ranpos), 1.5f, hitObjectGO.transform.position.z + Random.Range (-ranpos, ranpos));
						
						StartCoroutine (WaitForDrop (currentDroppedItem));
						
						currentDroppedItem.transform.rotation = campfire.transform.rotation;	
						currentDroppedItem.name = currentItem.itemGives;
						currentDroppedItem.GetComponent<Renderer>().material.mainTexture = currentItem.itemIcon;		
						
						Destroy (hitObjectGO);
						button.SetActive (false);
						//GameEventManager.SetState (GameEventManager.E_STATES.e_game);
						timer = 0;
						bar.transform.localScale = new Vector3 (0.1f, 1, 0);
						playerBarBorder.GetComponent<Renderer>().enabled = false;
						LeanTween.cancel (bar);	
						hitObjectName = "";			
						//inventory.addItem (currentItem.itemID);						
				}
				if (hitObjectGO.tag == "AI") {
						//	hitObjectGO.transform.parent.gameObject.GetComponent<spiders> ().health --;
						
				}
		}

		IEnumerator WaitForDrop (GameObject fallingItem)
		{
				GameEventManager.SetState (GameEventManager.E_STATES.e_actionButtonPressed);
				Hashtable optional = new Hashtable ();
				optional.Add ("ease", LeanTweenType.easeOutBounce);
				LeanTween.moveY (fallingItem, 0.1f, 0.4f, optional);
				yield return new WaitForSeconds (0.4f);
				LeanTween.move (fallingItem, moveToAfterPick.position + Vector3.down, 0.2f, optional);
				LeanTween.scale (fallingItem, Vector3.zero, 0.3f, optional);
		
				inventory.addItemByName (fallingItem.name);
				Destroy (fallingItem, 0.2f);
				yield return new WaitForSeconds (0.4f);
				GameEventManager.SetState (GameEventManager.E_STATES.e_game);
		}
		void OnCollisionEnter (Collision hit)
		{
				switch (hit.gameObject.tag) {
				case "Pickable":
				case "AI":
						hitObjectGO = hit.collider.transform.parent.gameObject;
						hitObjectName = hit.gameObject.transform.parent.name;
						//hit.collider.transform.parent.renderer.material.color = Color.cyan;	
						//hit.collider.transform.GetComponentInChildren<SpriteRenderer> ().material.color = Color.cyan;				
						button.SetActive (true);
						button.GetComponentInChildren<TextMesh> ().text = hit.collider.transform.parent.name;						
				
						break;
				case "Water":
						kMovementSpeed = 1;
						kJoystickSpeed = kMovementSpeed / 10;
						break;
				case "Ground":
						kMovementSpeed = 2;
						kJoystickSpeed = kMovementSpeed / 10;
						break;
				case "DroppedItem":
						break;
				default:
						break;
				}
		}

		void OnCollisionStay (Collision hit)
		{
				switch (hit.gameObject.tag) {
				case "AI":
						hitObjectGO = hit.collider.transform.parent.gameObject;
						hitObjectName = hit.gameObject.transform.parent.name;
						//hit.collider.transform.parent.renderer.material.color = Color.red;
						break;
				default:
						break;
				}
		}
		void OnCollisionExit (Collision hit)
		{		
				switch (hit.gameObject.tag) {
				case "Pickable":
				case "AI":
						hitObjectName = "";
						//hit.gameObject.transform.parent.renderer.material.color = Color.white;
						//hit.gameObject.transform.renderer.material.color = Color.white;
						button.SetActive (false);
						break;
				default:
						break;
				}								
		}
		
		/*void OnTriggerEnter (Collider other)
		{
				other.isTrigger = false;
				print ("fal");
		}
		void OnTriggerExit (Collider other)
		{
				other.isTrigger = true;
				print ("true");
		}*/
		
		void MoveFromJoystick ()
		{
				//clickToMoveScript.enabled = false;
				isMovingToTarget = false;
				Vector2 offset = leftFingerCurrentPoint - leftFingerStartPoint;
				if (offset.magnitude > 10)
						offset = offset.normalized * 10;
				characterController.SimpleMove (kJoystickSpeed * ownTransform.TransformDirection (new Vector3 (offset.x, 0, offset.y)));

		}
		
		void MoveToTarget ()
		{
				/*Vector3 difference = targetPoint - ownTransform.position;		
				characterController.SimpleMove (difference.normalized * kMovementSpeed);		
				Vector3 horizontalDifference = new Vector3 (difference.x, 0, difference.z);
				if (horizontalDifference.magnitude < 0.01f) {
						isMovingToTarget = false;
						
				}*/
		}
	
		void SetTarget (Vector2 screenPos)
		{
				Ray ray = _camera.ScreenPointToRay (new Vector3 (screenPos.x, screenPos.y));
				RaycastHit hit, hit1;
				int layerMask = 1 << 8; // Ground
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
						targetPoint = hit.point;						
						//clickedPos.transform.position = targetPoint;
						isMovingToTarget = true;
				}
				if (Physics.Raycast (ray, out hit1)) {						
						
						if (hit1.collider.gameObject.tag == "Ground") {
								clickedPos.transform.position = targetPoint;
						}	
						if (hit1.collider.gameObject.tag == "Pickable") {
								clickedPos.transform.position = hit1.collider.transform.parent.position;
								targetPoint = hit1.collider.transform.parent.position;
						}								
				}
		}
		
		void OnTouchBegan (int fingerId, Vector2 pos)
		{
				
				clickedPos.transform.position = Vector3.zero;
				if (leftFingerId == -1 && kJoystikEnabled && joystickRect.Contains (pos)) {
						leftFingerId = fingerId;
						leftFingerStartPoint = leftFingerCurrentPoint = pos;
				} else if (rightFingerId == -1) {
						rightFingerStartPoint = rightFingerCurrentPoint = rightFingerLastPoint = pos;
						rightFingerId = fingerId;
						isRotating = false;
				}
				//GameEventManager.SetState (GameEventManager.E_STATES.e_actionButtonPressed);
		}
		
		void OnTouchEnded (int fingerId)
		{
				if (fingerId == leftFingerId)
						leftFingerId = -1;
				else if (fingerId == rightFingerId) {
						rightFingerId = -1;
						if (false == isRotating) {
								SetTarget (rightFingerStartPoint);
						}
				}
				timer = waitTime = 0;
				bar.transform.localScale = new Vector3 (0.1f, 1, 0);
				LeanTween.cancel (bar);
				playerBarBorder.GetComponent<Renderer>().enabled = false;	
				GameEventManager.SetState (GameEventManager.E_STATES.e_game);
				//clickToMoveScript.destinationPosition = this.transform.position;
				
			
		}
	
		void OnTouchMoved (int fingerId, Vector2 pos)
		{
				if (fingerId == leftFingerId)
						leftFingerCurrentPoint = pos;
				else if (fingerId == rightFingerId) {
						rightFingerCurrentPoint = pos;			
						if ((pos - rightFingerStartPoint).magnitude > 2) {
								isRotating = true;
						}
				}
		}
		
		void Rotate ()
		{
				Vector3 lastDirectionInGlobal = _camera.ScreenPointToRay (rightFingerLastPoint).direction;
				Vector3 currentDirectionInGlobal = _camera.ScreenPointToRay (rightFingerCurrentPoint).direction;
		
				Quaternion rotation = new Quaternion ();
				rotation.SetFromToRotation (lastDirectionInGlobal, currentDirectionInGlobal);
		
				ownTransform.rotation = ownTransform.rotation * Quaternion.Euler (0, kInverse ? rotation.eulerAngles.y : -rotation.eulerAngles.y, 0);
		
				// and now the rotation in the camera's local space
				rotation.SetFromToRotation (cameraTransform.InverseTransformDirection (lastDirectionInGlobal),
		                           cameraTransform.InverseTransformDirection (currentDirectionInGlobal));
				cameraTransform.localRotation = Quaternion.Euler (kInverse ? rotation.eulerAngles.x : -rotation.eulerAngles.x, 0, 0) * cameraTransform.localRotation;
		
				rightFingerLastPoint = rightFingerCurrentPoint;
		}
	
}