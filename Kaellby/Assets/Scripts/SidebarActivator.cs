using Assets.TouchScript.Scripts;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class SidebarActivator : MonoBehaviour
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
