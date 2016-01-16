using UnityEngine;
using System.Collections;

public class TouchZoom : MonoBehaviour {
	float speed = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
  // for Iphone - Detects moving finger
  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
    // Get movement of the finger since last frame
    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
    // move object across XZ plane
    transform.Translate (-touchDeltaPosition.x * speed, 0, -touchDeltaPosition.y * speed);
  }
  transform.position = new Vector3(
           Mathf.Clamp(transform.position.x, -62.0f, 62.0f), 
           Mathf.Clamp(transform.position.y, 8.7f, 8.7f),
           Mathf.Clamp(transform.position.z, -3.5f, -3.5f));
}
}
	
	
