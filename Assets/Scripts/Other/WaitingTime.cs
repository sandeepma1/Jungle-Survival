using UnityEngine;
using System.Collections;

public class WaitingTime : MonoBehaviour
{
		public GameObject childBar;
		public float timeOutDuration;
		float timer;
		bool start;
		void Start ()
		{				
				this.GetComponent<Renderer>().enabled = true;
		}
		// Update is called once per frame
		void Update ()
		{
				timer += Time.deltaTime; 
				if (timer <= timeOutDuration)
						childBar.transform.localScale = new Vector3 (timer / timeOutDuration, childBar.transform.localScale.y, childBar.transform.localScale.z);
				else {
						this.GetComponent<Renderer>().enabled = false;
						childBar.GetComponent<Renderer>().enabled = false;
				}
		}
		
}
