using UnityEngine;
using System.Collections;

public class HungerMeter : MonoBehaviour
{
		float timer;
		public float timeOutDuration;
		public GameObject childBar, Text;
		//bool isCompletlyHungry = false;
		// Use this for initialization
		void Start ()
		{
				timer = timeOutDuration;
				GameEventManager.hunger = timeOutDuration;
				InvokeRepeating ("DecreaseHunger", 0, 5.0f);				
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
				Text.gameObject.GetComponent<TextMesh> ().text = "Hunger " + GameEventManager.hunger.ToString ("F0") + "%";
				
				//GameEventManager.hunger = (int)timer;
				if (GameEventManager.hunger > 0) {
						childBar.transform.localScale = new Vector3 (childBar.transform.localScale.x, childBar.transform.localScale.y, (GameEventManager.hunger / timeOutDuration) % 100);						
				}
				if (GameEventManager.hunger >= 100) {
						GameEventManager.hunger = 100; 
				}	
				if (GameEventManager.hunger <= 0) {
						GameEventManager.hunger = 0;
						//isCompletlyHungry = true;
						InvokeRepeating ("DecreaseHealth", 0, 2);
						GameEventManager.hunger = 0.001f;
				}				
				
				switch ((int)GameEventManager.hunger) {				
				case 45:
						HelpText.MainNotification ("I am getting hungry");
						break;	
				case 25:
						HelpText.MainNotification ("I will die soon, feed me");
						break;
				case 5:
						HelpText.MainNotification ("Good bye cruel world, meet you in heaven!!");
						break;
				default:
						break;
				}	
		}
		
		void DecreaseHunger ()
		{
				GameEventManager.hunger --;
		}
		void DecreaseHealth ()
		{
				GameEventManager.health --;
		}

}
