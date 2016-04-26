using UnityEngine;
using System.Collections;

public class MeshTesting : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GetComponent<MeshFilter>().sharedMesh = new Mesh()
	        {
	            vertices = new[] { new Vector3(0, 0), new Vector3(10, 0), new Vector3(10, 10), new Vector3(0, 10)  },
                triangles = new [] { 0, 1, 2, 0, 2, 3 }
	        };
	}

    // Update is called once per frame
    void Update () {
	
	}
}
