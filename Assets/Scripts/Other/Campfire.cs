using UnityEngine;
using System.Collections;

public class Campfire : MonoBehaviour
{

		float timer = 120;
		GameObject light;
		// Use this for initialization
		void Start ()
		{
				light = GameObject.Find ("CampfireLight");
				print (light.GetComponent<Light> ().intensity);
		}
	
		// Update is called once per frame
		void Update ()
		{
				timer -= Time.deltaTime;
				
				if (timer <= 0) {
						timer = 0;
						light.GetComponent<Light> ().enabled = false;		
				}	
		}
}
