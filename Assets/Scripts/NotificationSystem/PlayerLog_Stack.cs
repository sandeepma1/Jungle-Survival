using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLog_Stack : MonoBehaviour
{
		// Private VARS
		private List<string> Eventlog = new List<string> ();
		private string guiText = "";
		float timer = 0, fadeTimer = 0;

		// Public VARS
		public int maxLines = 10;
		public TextMesh l1, l2, l3;
		public float fadeOutTime = 3;
		bool startFading = false;
		void Start ()
		{				
				Eventlog.Add ("");
				Eventlog.Add ("");
				Eventlog.Add ("");
		}
		/*	void OnGUI ()
		{
				GUI.Label (new Rect (0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3), guiText, GUI.skin.textArea);
		}*/
	
		public  void AddEvent (string eventString)
		{
				Eventlog.Add (eventString);
				if (Eventlog.Count >= maxLines) {
						Eventlog.RemoveAt (0);
				}
//				print (Eventlog.Count);
				l1.text = Eventlog [0];
				l2.text = Eventlog [1];
				l3.text = Eventlog [2];
				timer = 0;
		}

		void Update ()
		{				
				if (Input.GetKeyDown (KeyCode.Space)) {
						AddEvent ("Some" + Random.Range (0, 100));
						HelpText.MainNotification ("aaaaa");
				}
				if (l3.text != "") {
						timer += Time.deltaTime;
				}
				if (timer > fadeOutTime) {
						timer = 0;	
						//Eventlog.Clear ();
						startFading = true;
						StartCoroutine ("StopFading");
				}				
//				print (Eventlog.Count);
				if (startFading) {
						fadeTimer += Time.deltaTime;	
						switch ((fadeTimer.ToString ("F0"))) {
						case ("1"):	
								l1.text = "";								
								break;
						case ("2"):
								l2.text = "";
								break;				
						case ("3"):
								l3.text = "";
								break;
						}
				}
		}
		IEnumerator StopFading ()
		{
//				print ("startFading");
				yield return new WaitForSeconds (3.01f);
				Eventlog.Clear ();
				Eventlog.Add ("");
				Eventlog.Add ("");
				Eventlog.Add ("");
				startFading = false;
				fadeTimer = 0;
		
		
		}

}