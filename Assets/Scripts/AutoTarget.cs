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

        Debug.Log("looking direction:" + lookingAngle);


        foreach (Transform t in targets) {
            Vector3 targetDirection = t.transform.position - transform.position;
            
            float realAngle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;
            Debug.Log("real angle:" + realAngle);
            
            float lookAngle = Mathf.Atan2(lookingAngle.z, lookingAngle.x) * Mathf.Rad2Deg;
            Debug.Log("look angle:" + lookAngle);


            if (Mathf.Abs(lookAngle - realAngle) < minAngle) {
                minAngle = lookAngle;
                closest = t;
            }    
        }

        return closest;
    }

}
