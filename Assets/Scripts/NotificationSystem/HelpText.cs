using UnityEngine;
using System.Collections;

public class HelpText : MonoBehaviour
{

		GameObject helptext, helpTextOutline;
		static public string currentText, tempText;
		static public float textTimer = 4;
		// Use this for initialization
		void Start ()
		{
				tempText = "";
				currentText = "Survive as long as possible";
				helptext = GameObject.Find ("HelpText");
				helpTextOutline = GameObject.Find ("HelpTextOutline");	
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
				helptext.GetComponent<TextMesh> ().text = currentText;
				helpTextOutline.GetComponent<TextMesh> ().text = currentText;
		}
		public static void MainNotification (string text)
		{
				currentText = text;
				textTimer = 4;
		}
		void Update ()
		{	
				if (tempText != currentText) {						
						textTimer -= Time.deltaTime;
				}				
				
				//Removes Text
				if (textTimer <= 0) {
						textTimer = 0;
						currentText = "";
				}
		
		}
}
