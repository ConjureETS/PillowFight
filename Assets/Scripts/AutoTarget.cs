using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoTarget : MonoBehaviour {

    private List<Transform> targets;
    public float minAngleRange = 60f;

	// Use this for initialization
	void Start () {
        targets = new List<Transform>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos) {
            if( !go.Equals(gameObject) ){
                targets.Add(go.transform);
            }
        }
                
	}
	
	// Update is called once per frame
	void Update () {

	}

    public Transform GetTarget(Vector3 lookingAngle) {

        Transform closest = null;
        float minAngle = minAngleRange;

        foreach (Transform t in targets) {
            Vector3 targetDirection = t.transform.position - transform.position;

            float dot = Vector3.Dot(targetDirection, lookingAngle);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            
            if (angle < minAngle) {
                minAngle = angle;
                closest = t;
            }
            Debug.Log("angle: " + angle);

            
        }

        Debug.Log("min angle:" + minAngle);
        
        return closest;
    }

}
