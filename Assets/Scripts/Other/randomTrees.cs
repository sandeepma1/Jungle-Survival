using UnityEngine;
using System.Collections;

public class randomTrees : MonoBehaviour {
	public GameObject[] variousTrashPrefabs;  //assign all kinds of trashy objects here via the inspector
	Vector3 pos;
	public float yDistance=0.0f, feildSize = 20.0f;
	int limit = 1;
	public int maxLimit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (limit <= maxLimit) 
		{
			RandomGenerator();
		}
	}
	
	void RandomGenerator()
	{
		for(int i =0; i<10; i++)
			{
				pos.x = Random.Range (-feildSize, feildSize);
				pos.y = yDistance;
				pos.z = Random.Range (-feildSize, feildSize);
        		Instantiate (variousTrashPrefabs[Random.Range(0, variousTrashPrefabs.Length)], pos, transform.rotation);
				limit ++;
			}
	}
	
}
