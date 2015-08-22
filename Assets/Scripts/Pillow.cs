using UnityEngine;
using System.Collections;

public class Pillow : MonoBehaviour {
    
    public bool IsThrown = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -10) {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider other) {
        if (IsThrown && other.tag == "Player") {
            
            Debug.Log("A child got hit by a pillow!");

            //other.GetComponent<Child>().takeHit();

            Destroy(this.gameObject);
        }
    }

}
