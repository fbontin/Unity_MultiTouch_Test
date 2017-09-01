using UnityEngine;

namespace Assets.TouchScript.Examples._misc.Scripts
{
	/// <exclude />
	public class ExamplesList : MonoBehaviour 
	{

		public RectTransform Content;

		void Start () 
		{
			gameObject.SetActive(false);
		}

		public void ShowHide()
		{
			gameObject.SetActive(!gameObject.activeSelf);
			Content.localPosition = Vector3.zero;
		}

	}
}
