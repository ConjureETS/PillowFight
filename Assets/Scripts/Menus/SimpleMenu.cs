using UnityEngine;
using System.Collections;

public class SimpleMenu : MonoBehaviour
{
	public int NextLevel;
	private float countdown = 1f;

	// Use this for initialization
	void Start () {
		//TODO Use XInput to 
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown < 0)
			Application.LoadLevel(NextLevel);
	}
}
