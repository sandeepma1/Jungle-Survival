using UnityEngine;
using System.Collections;

public class FastLoad : MonoBehaviour
{
		public int levelNumber = 0;
	
		void Start ()
		{
				Application.LoadLevel (levelNumber);
		}
	
}
