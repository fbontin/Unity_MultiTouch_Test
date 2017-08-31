using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardManager : MonoBehaviour, ISelectHandler
{
	public void OnSelect(BaseEventData eventData)
	{
		System.Diagnostics.Process.Start("tabtip.exe");
	}
}
