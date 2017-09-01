using System.Collections;
using UnityEngine;

namespace Assets.TouchScript.Examples._misc.Scripts
{
	/// <exclude />
	public class ShowMe : MonoBehaviour 
	{
		IEnumerator Start () 
		{
			var canvas = GetComponent<Canvas>();
			canvas.enabled = false;
			yield return new WaitForSeconds(.5f);
			canvas.enabled = true;
		}
	}
}
