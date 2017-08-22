using UnityEngine;

public class ScreenSpaceMover : MonoBehaviour
{

	public GameObject ObjectToFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var camera = Camera.main;
		var screenPosition = camera.WorldToScreenPoint(ObjectToFollow.transform.position);

		//Debug.Log("Screen position: " + screenPosition);

		transform.position = screenPosition;

	}
}
