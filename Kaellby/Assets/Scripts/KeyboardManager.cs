using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardManager : MonoBehaviour, ISelectHandler
{
	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log(this.gameObject.name + " was selected");
		ShowKeyboard();
	}

	public void ShowKeyboard()
	{
		Debug.Log("Keyboard should be visible");
		System.Diagnostics.Process.Start("tabtip.exe");

		/* Starting the keyboard using Unity, does not work for the moment.
		var keyboard = TouchScreenKeyboard.Open("test");

		if (keyboard.active)
		{
			Debug.Log("Keyboard is active");
		}
		*/
	}
}
