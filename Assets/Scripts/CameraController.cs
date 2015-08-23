using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour 
{
	private List<Transform> players;
	private Camera cam;
	private float nextUpdate = 0f;
	private const float UpdateTime = 0.1f;
	private bool movingLeft = false;
	private bool movingRight = false;
	private bool movingUp = false;
	private bool movingDown = false;
	private bool zoomingIn = false;
	private bool zoomingOut = false;

	private bool debug = false;

	private float XOffset = 0.25f;
	private float YOffset = 0.15f;

	private Vector3 upMove;

	// Use this for initialization
	void Start () 
	{
		cam = GetComponent<Camera>();
		players = new List<Transform>();
		
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in gos)
		{
			if (!go.Equals(gameObject))
			{
				players.Add(go.transform);
			}
		}

		upMove = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
	}
	
	// Update is called once per frame
	void Update ()
	{
		nextUpdate -= Time.deltaTime;
		if (nextUpdate < 0)
		{
			CalculateCameraMovement();
			nextUpdate += UpdateTime;
		}

		if (Input.GetKeyDown(KeyCode.Z))
			debug = true;

		if (movingUp)
			transform.Translate(upMove * -Time.deltaTime, Space.World);

		if (movingDown)
			transform.Translate(upMove * Time.deltaTime, Space.World);

		if (movingLeft)
			transform.Translate(-Time.deltaTime, 0, 0, Space.Self);

		if (movingRight)
			transform.Translate(Time.deltaTime, 0, 0, Space.Self);

		int mod = 0;
		if (zoomingIn)
			mod = -10;
		if (zoomingOut)
			mod = 10;

		//cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + mod * Time.deltaTime, 30, 60);
	}

	void CalculateCameraMovement()
	{
		List<Vector3> screenPos = GetScreenPositions();

		float minX = 2, maxX = -2, minY = 2, maxY = -2;
		foreach (Vector3 pos in screenPos)
		{
			if (pos.x > maxX)
				maxX = pos.x;
			if (pos.x < minX)
				minX = pos.x;

			if (pos.y > maxY)
				maxY = pos.y;
			if (pos.y < minY)
				minY = pos.y;
		}

		float distX = maxX - minX;
		float distY = maxY - minY;

		if (debug)
		{
			Debug.Log("Dist X - " + distX + "\nDist Y - " + distY);
			debug = false;
		}

		if (movingLeft)
		{
			if(minX > 0.5f - XOffset / 2f)
				movingLeft = false;
		}
		else
		{
			if(minX < 0.5f - XOffset)
				movingLeft = true;
		}

		if (movingRight)
		{
			if(maxX < 0.5f + XOffset / 2f)
				movingRight = false;
		}
		else
		{
			if(maxX > 0.5f + XOffset)
				movingRight = true;
		}

		if (movingUp)
		{
			if (minY > 0.5f - YOffset / 2f)
				movingUp = false;
		}
		else
		{
			if (minY < 0.5f - YOffset)
				movingUp = true;
		}

		if (movingDown)
		{
			if (maxY < 0.5f + YOffset / 2f)
				movingDown = false;
		}
		else
		{
			if (maxY > 0.5f + YOffset)
				movingDown = true;
		}

		if (zoomingOut)
		{
			if (distX < 0.65f)
			//if (Mathf.Min(distX, distY) < 0.8f)
				zoomingOut = false;
		}
		else
		{
			if (distX > 0.75f)
			//if (Mathf.Max(distX, distY) > 0.9f)
				zoomingOut = true;
		}

		if (zoomingIn)
		{
			if (distX > 0.7f)
			//if (Mathf.Max(distX, distY) > 0.7f)
				zoomingIn = false;
		}
		else
		{
			if (distX < 0.6f)
			//if (Mathf.Min(distX, distY) < 0.6f)
				zoomingIn = true;
		}
	}

	private List<Vector3> GetScreenPositions()
	{
		List<Vector3> list = new List<Vector3>();
		foreach (Transform t in players)
			list.Add(cam.WorldToViewportPoint(t.position));
		return list;
	}
}
