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
		var screenPosition = Camera.main.WorldToScreenPoint(ObjectToFollow.transform.position);
		transform.position = screenPosition;

		//FIX
		//Debug.Log("cube: " + ObjectToFollow.transform.position);
	}
}
