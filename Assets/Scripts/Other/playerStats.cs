using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {
	float timer;
	GameObject healthGO;
	// Use this for initialization
	void Start () 
	{
		timer = 0;
		healthGO = GameObject.Find ("0HeroHolder/MainCamera/Health");
		Debug.Log (healthGO);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
	
	}
}
