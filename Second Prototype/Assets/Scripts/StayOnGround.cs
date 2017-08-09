using UnityEngine;

namespace Assets.Scripts
{
	public class StayOnGround : MonoBehaviour {
	
		void Update ()
		{
			var position = gameObject.transform.localPosition;
			var yScale = gameObject.transform.localScale.y;
			var parentPosition = gameObject.transform.parent.localPosition;

			//set the objects lowest point to the same as the planes height
			gameObject.transform.localPosition = new Vector3(position.x, yScale / 2 + parentPosition.y, position.z);
		}
	}
}
