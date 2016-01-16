using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
		public Texture2D heightmap;
		public Vector3 size = new Vector3 (100, 10, 100);
		void Update ()
		{
				Debug.Log (heightmap.GetPixel (15, 7));
				//int z = Mathf.RoundToInt (transform.position.z / size.z * heightmap.height);
				//Debug.Log (x + "" + z);
				//transform.position.y = heightmap.GetPixel (x, z).grayscale * size.y;
		}
}