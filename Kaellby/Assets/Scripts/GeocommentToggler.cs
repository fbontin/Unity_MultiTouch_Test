using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GeocommentToggler : MonoBehaviour
{

	public GameObject GeocommentInput;
	public GameObject ParentObject;

	public void ShowGeocommentInput()
	{
		ShowText();
		MoveMarkerToMiddle();
		GeocommentInput.gameObject.SetActive(true);
		SetButtonListeners();
	}

	private void ShowText()
	{
		var text = GetComponent<CommentText>().Text;
		GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text = text;
	}

	private void MoveMarkerToMiddle()
	{
		var screenPosition = GetComponent<RectTransform>().anchoredPosition;
		Debug.Log("rect: " + screenPosition + ", local: " + transform.localPosition);
		
		var position = GetWorldPosition(screenPosition);
		ParentObject.transform.position -= new Vector3(position.x, 0, position.z);
		Debug.Log("Marker world pos: " + position + ", parent: " + ParentObject.transform.position);
	}

	private static Vector3 GetWorldPosition(Vector2 screenPosition)
	{
		var ray = Camera.main.ScreenPointToRay(screenPosition);
		const float height = 25.0f;
		var distance = (height - ray.origin.y) / ray.direction.y;
		return ray.GetPoint(distance);
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

	public void HideGeocommentInput()
	{
		RemoveButtonListeners();
		Debug.Log("Obj to follow: " + GetComponent<ScreenSpaceMover>().ObjectToFollow.transform.position);
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
