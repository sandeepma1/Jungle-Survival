using UnityEngine;
using System.Collections;

public class RotateDayNight : MonoBehaviour
{
		float timer = -30;
		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
				timer -= Time.deltaTime;
//				Debug.Log (timer.ToString ("0"));
				this.transform.localRotation = Quaternion.Euler (0f, 0f, timer);
		}
}
