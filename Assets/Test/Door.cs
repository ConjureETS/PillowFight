using UnityEngine;
using System.Collections;
using System;

public class Door : MonoBehaviour
{
	public float MaxAngle = 135f;
    public float OpenDuration = 5f;
    public float CloseDuration = 1f;
    public AudioSource CloseSound;

    /*
	// Update is called once per frame
	void Update ()
    {
		state = Mathf.Clamp(state + goal * openingSpeed * Time.deltaTime, 0f, 1f);

		//transform.rotation = Quaternion.identity;
		//transform.Rotate(Vector3.up * state * angles);
		transform.rotation = Quaternion.Euler(Vector3.up * curvature.Evaluate(state) * -angles);
	}*/

	public void Open(Action callback)
	{
        StartCoroutine("OpenDoor", callback);
	}

    private IEnumerator OpenDoor(object callback)
    {
        Vector3 initialRot = transform.localEulerAngles;
        Vector3 finalRot = new Vector3(initialRot.x, initialRot.y - MaxAngle, initialRot.z);

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / OpenDuration;

            transform.localEulerAngles = Vector3.Lerp(initialRot, finalRot, ratio);

            yield return null;
        }

        if (callback != null)
        {
            ((Action)callback)();
        }
    }

	public void Close(Action callback)
	{
        StartCoroutine("CloseDoor", callback);
	}

    private IEnumerator CloseDoor(object callback)
    {
        Vector3 initialRot = transform.localEulerAngles;
        Vector3 finalRot = new Vector3(initialRot.x, initialRot.y + MaxAngle, initialRot.z);

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / CloseDuration;

            transform.localEulerAngles = Vector3.Lerp(initialRot, finalRot, Mathf.Pow(ratio, 4));

            yield return null;
        }

        CloseSound.Play();

        if (callback != null)
        {
            ((Action)callback)();
        }
    }
}
