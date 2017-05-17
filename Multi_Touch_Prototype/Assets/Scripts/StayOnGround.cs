using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//set gameObject to always stay at ground level
		var pos = gameObject.transform.position;
		var height = gameObject.transform.localScale.y;
		Debug.Log("height: " + height);
		gameObject.transform.position = new Vector3(pos.x, height / 2, pos.z);
	}
}
