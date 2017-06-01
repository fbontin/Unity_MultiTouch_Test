using UnityEngine;

namespace Assets.Scripts
{
	public class PutChildrenOnGround : MonoBehaviour {
	
		// Update is called once per frame
		void Update ()
		{
			foreach (Transform child in transform)
			{
				StayOnGround.Stay(child.gameObject);
				Debug.Log("test: " + child.name);
			}
		}
	}
}
