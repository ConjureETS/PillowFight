using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightOnOff : MonoBehaviour {

	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
			light.enabled = !light.enabled;
	}
}
