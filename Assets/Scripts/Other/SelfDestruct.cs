using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour
{
		void OnBecameInvisible ()
		{
				Manager.noObjects--;
				//Destroy (gameObject);
		}
	
}
