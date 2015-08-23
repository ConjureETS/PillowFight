using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
    Transform mainCamera;
	
    
    // Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.rotation = mainCamera.rotation;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
