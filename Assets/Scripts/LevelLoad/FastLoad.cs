using UnityEngine;
using System.Collections;

//using UnityEngine.SceneManagement;

public class FastLoad : MonoBehaviour
{
	//public int levelNumber = 0;

	void Start ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);

	}
	
}
