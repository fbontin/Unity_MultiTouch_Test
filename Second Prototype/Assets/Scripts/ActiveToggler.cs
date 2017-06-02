using UnityEngine;

namespace Assets.Scripts
{
	public class ActiveToggler : MonoBehaviour {

		public void ToggleActivity()
		{
			gameObject.SetActive(!gameObject.activeInHierarchy);
		}
	}
}
