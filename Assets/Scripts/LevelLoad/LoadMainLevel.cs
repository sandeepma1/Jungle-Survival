using UnityEngine;
using System.Collections;

public class LoadMainLevel : MonoBehaviour
{

	void Start ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (2);

	}

}
