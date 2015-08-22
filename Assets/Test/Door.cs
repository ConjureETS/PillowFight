using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private float state = 0f; //From 0 to 1 (closed to open)
	private float goal = -1f; //-1 or 1
	public float angles;
	public float openingSpeed;
	public AnimationCurve curvature;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.A))
		{
			goal *= -1;
		}

		state = Mathf.Clamp(state + goal * openingSpeed * Time.deltaTime, 0f, 1f);

		//transform.rotation = Quaternion.identity;
		//transform.Rotate(Vector3.up * state * angles);
		transform.rotation = Quaternion.Euler(Vector3.up * curvature.Evaluate(state) * -angles);
	}

	public void Open()
	{

	}

	public void Close()
	{

	}
}
