using System;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class GeocommentToggler : MonoBehaviour
{

	public GameObject GeocommentInput;

	public void ShowGeocommentInput()
	{
		ShowText();
		GeocommentInput.gameObject.SetActive(true);
		SetButtonListeners();
	}

	private void SetButtonListeners()
	{
		var buttons = GeocommentInput.GetComponentsInChildren<Button>();
		buttons[0].onClick.AddListener(HideGeocommentInput); //background button
		buttons[1].onClick.AddListener(RemoveGeocomment); //remove button
		buttons[2].onClick.AddListener(SaveGeocomment); //save button
	}

	private void RemoveButtonListeners()
	{
		var buttons = GeocommentInput.GetComponentsInChildren<Button>();
		buttons.ToList().ForEach(b => b.onClick.RemoveAllListeners());
	}


	private void ShowText()
	{
		var text = GetComponent<CommentText>().Text;
		GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text = text;
	}

	public void HideGeocommentInput()
	{
		RemoveButtonListeners();
		Debug.Log(GetComponent<ScreenSpaceMover>().ObjectToFollow.transform.position);
		GeocommentInput.gameObject.SetActive(false);
	}

	private void SaveGeocomment()
	{
		var text = GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text;
		GetComponent<CommentText>().Text = text;
		RemoveButtonListeners();
		GeocommentInput.gameObject.SetActive(false);
	}

	private void RemoveGeocomment()
	{
		Destroy(GetComponent<ScreenSpaceMover>().ObjectToFollow);
		Destroy(gameObject);
		RemoveButtonListeners();
		GeocommentInput.gameObject.SetActive(false);
	}
}
