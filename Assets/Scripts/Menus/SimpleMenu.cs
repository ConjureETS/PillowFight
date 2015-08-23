using UnityEngine;
using System.Collections;

public class SimpleMenu : MonoBehaviour
{
	public int NextLevel;

	// Use this for initialization
	void Start () {
        StartCoroutine(AutoSkip());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            Application.LoadLevel(NextLevel);
        }
	}

    IEnumerator AutoSkip() {

        yield return new WaitForSeconds(5);
        Application.LoadLevel(NextLevel);

    }
}
