using UnityEngine;
using System.Collections;

public class Bill : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () {
	 transform.LookAt(Camera.main.transform.position, Vector3.up);
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}
}
