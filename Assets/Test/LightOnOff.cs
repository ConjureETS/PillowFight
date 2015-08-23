using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightOnOff : MonoBehaviour {

	private Light light;
	private float intensity;
	private float flicker;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		intensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {

		flicker -= Time.deltaTime;
		if (flicker <= 0)
		{
			light.intensity = intensity * 0.8f;
			if (Random.value <= 0.3f)
				flicker = Random.Range(0.4f, 1.5f);
		}
		else
			light.intensity = intensity;

		if(Input.GetKeyDown(KeyCode.Space))
			light.enabled = !light.enabled;
	}
}
