using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour
{		
		public GameObject childBar, Text;
		public float timeOutDuration;

		void Start ()
		{
				GameEventManager.health = (int)timeOutDuration;
				InvokeRepeating ("AutoRegainHealth", 0, 10.0f);
				GameEventManager.health = timeOutDuration;
		}
		// Update is called once per frame
		void LateUpdate ()
		{
				
				Text.gameObject.GetComponent<TextMesh> ().text = "Health " + GameEventManager.health.ToString ("F0") + "%";	
				
				if (GameEventManager.health > 0) {
						childBar.transform.localScale = new Vector3 (childBar.transform.localScale.x, childBar.transform.localScale.y, GameEventManager.health / 100);
				}	
				if (GameEventManager.health <= 0) {
						HelpText.MainNotification ("You Died...");
						Application.LoadLevel (0);
				}						
		}
		void AutoRegainHealth ()
		{
				GameEventManager.health ++;
				if (GameEventManager.health >= 100) {
						GameEventManager.health = 100;
				}
		}
	
		
	
}
