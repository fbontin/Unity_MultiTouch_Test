using UnityEngine;

namespace Assets.Scripts
{
	public class StayOnGround : MonoBehaviour {
	
		void Update ()
		{
			var goPosition = gameObject.transform.localPosition;
			var goY = gameObject.transform.localScale.y;
			var parentPosition = gameObject.transform.parent.localPosition;

			//set the objects lowest point to the same as the planes height
			gameObject.transform.localPosition = new Vector3(goPosition.x, goY / 2 + parentPosition.y, goPosition.z);
		}
	}
}
