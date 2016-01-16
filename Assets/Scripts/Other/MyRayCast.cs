using UnityEngine;
using System.Collections;

public class MyRayCast : MonoBehaviour {
	Vector3 pos;
	// Use this for initialization
	void Start () {
		pos = this.gameObject.transform.position;	
		
		Debug.Log (this.gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   			//RaycastHit hit;
    		//if (Physics.Raycast(ray, out hit))
			//{ 
			//	Debug.Log (ray);
			//}
			Plane playerPlane = new Plane(Vector3.up,transform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
 
			if (playerPlane.Raycast(ray, out hitdist)) 
			{
				Vector3 targetPoint = ray.GetPoint(hitdist);
				pos = ray.GetPoint(hitdist);
			}
		
		}	
		
		
		StartCoroutine(MyCoroutine());	
	}
	
	IEnumerator MyCoroutine()
	{		
   	 	this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, pos, 3.0f * Time.deltaTime);
		yield return new WaitForSeconds(1.0f);   	
	}

}
