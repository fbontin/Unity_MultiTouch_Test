using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnGround : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
	{
		Stay(gameObject);
	}

	public static void Stay(GameObject go)
	{
		var pos = go.transform.position;
		var height = go.transform.localScale.y;
		go.transform.position = new Vector3(pos.x, height / 2, pos.z);
	}
}
