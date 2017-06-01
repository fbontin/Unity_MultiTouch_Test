using UnityEngine;

namespace Assets.Scripts
{
	public class ToggleActivity : MonoBehaviour {

		public void Toggle()
		{
			gameObject.SetActive(!gameObject.activeInHierarchy);
		}
	}
}
