using TouchScript;
using UnityEngine;

namespace Assets.Scripts
{
	public class ActiveToggler : MonoBehaviour
	{

		public GameObject ScrollView;

		void Update()
		{
			var tm = TouchManager.Instance;
			if (tm.PressedPointersCount < 1 && !ScrollView.activeInHierarchy)
			{
				ScrollView.SetActive(true);
			}	
		}
	}
}
