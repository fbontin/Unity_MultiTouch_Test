using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snapper : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var goalPosition = GameObject.Find("Goal Plane").transform.position;
		var objectPosition = gameObject.transform.localPosition;
		if (IsInCircle(objectPosition, goalPosition, 0.2f))
		{
			gameObject.transform.localPosition = new Vector3(goalPosition.x, objectPosition.y, goalPosition.z);
		}

		/*
		var corner = gameObject.transform.localScale / 2;
		var c1 = Vector3.Scale(corner, new Vector3(1, -1, 1));
		var c2 = Vector3.Scale(corner, new Vector3(1, -1, -1));
		var c3 = Vector3.Scale(corner, new Vector3(-1, -1, -1));
		var c4 = Vector3.Scale(corner, new Vector3(-1, -1, 1));
		var lowerCorners = new[] { c1, c2, c3, c4 };
		*/
	}

	//2d, only for x and z axes
	private bool IsInCircle(Vector3 objectPosition, Vector3 goalPosition, float radius)
	{
		var deltaX = objectPosition.x - goalPosition.x;
		var deltaZ = objectPosition.z - goalPosition.z;
		var distance = Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);

		return distance < radius;
	}
}
