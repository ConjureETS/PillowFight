using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoTarget : MonoBehaviour
{
    private List<Transform> targets;
    public float minAngleRange = 20f;

	// Use this for initialization
	void Start () 
	{
        targets = new List<Transform>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos) 
		{
            if(!go.Equals(gameObject))
			{
                targets.Add(go.transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public Transform GetTarget(float screenX, float screenZ)
	{
		//Translate into looking angles
		
		Vector3 forwardDir = Camera.main.transform.forward;
		Vector3 rightDir = Camera.main.transform.right;

		forwardDir.y = 0f;
		forwardDir = forwardDir.normalized * screenZ;

		rightDir.y = 0f;
		rightDir = rightDir.normalized * screenX;

		Vector3 movement = forwardDir + rightDir;

		return GetTarget(movement);
	}
	
    public Transform GetTarget(Vector3 lookingAngle) 
	{
        Transform closest = null;
        float minAngle = minAngleRange;

        //Debug.Log("looking direction:" + lookingAngle);
		Debug.DrawRay(transform.position, lookingAngle * 2);


        foreach (Transform t in targets) 
		{
            Vector3 targetDirection = t.transform.position - transform.position;
            
            float realAngle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;

            float lookAngle = Mathf.Atan2(lookingAngle.z, lookingAngle.x) * Mathf.Rad2Deg;
            //Debug.Log("look angle:" + lookAngle);

			float angle = (lookAngle - realAngle + 5*360) % 360;
			if (angle > 180)
				angle -= 360;
			//float angle = lookAngle - realAngle;


			if (Input.GetKeyDown(KeyCode.D))
				Debug.Log("Angle: " + angle + "Looking - " + lookAngle + "\nReal - " + realAngle);
            //Debug.Log("real angle:" + realAngle);
            
            if (Mathf.Abs(angle) < minAngle) 
			{
                minAngle = angle;
                closest = t;
            }
        }

		if (closest != null)
		{
			Debug.DrawRay(transform.position, closest.transform.position - transform.position, Color.blue);
		}

        return closest;
    }
}
