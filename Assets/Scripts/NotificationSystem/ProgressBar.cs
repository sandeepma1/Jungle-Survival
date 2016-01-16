using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
		static Transform bar, outline;
		float ZAxis = 0;
		static bool startProgressBar = false;
		static float speed = 0;
	
		// Use this for initialization
		void Start ()
		{
				bar = transform.FindChild ("bar");	
				outline = this.transform;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (startProgressBar) {
						ZAxis += Time.deltaTime * speed;
						if (ZAxis < 1 && ZAxis >= 0) {
								bar.transform.localScale = new Vector3 (0.2f, 1, ZAxis);
						} else {
								startProgressBar = false;
								this.transform.GetComponent<Renderer>().enabled = false;
								bar.transform.GetComponent<Renderer>().enabled = false;
								//speed = 0;
								//ZAxis = 0;
								bar.transform.localScale = Vector3.zero;
						}
				}
				if (Input.GetMouseButtonDown (0)) {
						//	StartProgressBar (10); 
				}				
		}
	
		public static void StartProgressBar (float time)
		{
				outline.transform.GetComponent<Renderer>().enabled = true;
				bar.transform.GetComponent<Renderer>().enabled = true;
				speed = time / 10;
//				print (speed);
				startProgressBar = true;
		
		}
}
