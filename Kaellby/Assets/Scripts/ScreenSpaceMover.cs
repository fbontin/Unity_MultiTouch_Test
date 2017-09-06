using UnityEngine;

namespace Assets.Scripts
{
	public class ScreenSpaceMover : MonoBehaviour
	{
		public GameObject ObjectToFollow;
	
		void Update ()
		{
			var screenPosition = Camera.main.WorldToScreenPoint(ObjectToFollow.transform.position);
			transform.position = screenPosition;
		}
	}
}
