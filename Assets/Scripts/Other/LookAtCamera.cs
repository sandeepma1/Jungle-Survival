using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
		Quaternion camRotation;		
		void Update ()
		{
				
		
				transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward);
		}
}
