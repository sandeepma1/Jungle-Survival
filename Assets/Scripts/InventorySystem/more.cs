using UnityEngine;
using System.Collections;

public class more : MonoBehaviour
{
		public TextMesh label;
		public GameObject InventoryWindow;
		bool isInvUp;
		// Use this for initialization
		void Start ()
		{
				label.text = "<<<";
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButtonUp (0)) {
						ShowInventory ();
				}
		}
		
		void ShowInventory ()
		{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == this.gameObject) {												
						isInvUp = ! isInvUp;
//						print (isInvUp);		
				}
				if (isInvUp) {
						//up
						label.text = ">>>";
						GameEventManager.SetState (GameEventManager.E_STATES.e_inventoryWindow);
						Hashtable optional = new Hashtable ();
						optional.Add ("ease", LeanTweenType.easeInOutQuad);
						LeanTween.moveLocalY (InventoryWindow, 0, 0.3f, optional);
				} 
				if (!isInvUp) {
						//Down
						label.text = "<<<";
						GameEventManager.SetState (GameEventManager.E_STATES.e_game);
						Hashtable optional = new Hashtable ();
						optional.Add ("ease", LeanTweenType.easeInOutQuad);
						LeanTween.moveLocalY (InventoryWindow, -0.48f, 0.3f, optional);
				}
		}
}