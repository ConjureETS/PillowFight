using UnityEngine;
using System.Collections;

public class SimpleMenu : MonoBehaviour
{
	public int NextLevel;

	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            Application.LoadLevel(NextLevel);
        }
	}

    IEnumerator AutoSkip() {

        yield return new WaitForSeconds(5);
        

    }
}
