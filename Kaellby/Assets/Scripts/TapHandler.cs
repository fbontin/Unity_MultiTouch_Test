using System;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TapHandler : MonoBehaviour
{
	public GameObject GeocommentInput;
	public GameObject MapMarkers;
	public GameObject ParentObject;

	private GameObject _mapMarker;

	private void OnEnable()
	{
		GetComponent<TapGesture>().Tapped += ShowNewGeocommentInput;
	}

	private void OnDisable()
	{
		GetComponent<TapGesture>().Tapped -= ShowNewGeocommentInput;
	}

	private void ShowNewGeocommentInput(object sender, EventArgs e)
	{
		var worldPosition = GetWorldPosition(GetComponent<TapGesture>().ScreenPosition);
		ParentObject.transform.position -= new Vector3(worldPosition.x, 0, worldPosition.z);

		SetButtonListeners();
		GeocommentInput.gameObject.SetActive(true);
		GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text = "";
		GeocommentInput.GetComponentInChildren<InputField>().Select();
		System.Diagnostics.Process.Start("tabtip.exe");
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
		buttons[0].onClick.AddListener(CancelGeocomment); //background button
		buttons[1].onClick.AddListener(CancelGeocomment); //remove button
		buttons[2].onClick.AddListener(SaveGeocomment); //save button
	}

	private GameObject CreatePositionObject()
	{
		var middleOfScreen = new Vector2((float) Screen.width/2, (float) Screen.height/2);
		var worldPosition = GetWorldPosition(middleOfScreen);
		var positionObject = new GameObject("EmptyPositionObject " + Guid.NewGuid());
		positionObject.transform.parent = ParentObject.transform;
		positionObject.transform.position = worldPosition;
		return positionObject;
	}

	public void SaveGeocomment()
	{
		_mapMarker = Instantiate(Resources.Load("Map Marker")) as GameObject;
		_mapMarker.transform.SetParent(MapMarkers.transform);

		var objectToFollow = CreatePositionObject();
		_mapMarker.AddComponent<ScreenSpaceMover>().ObjectToFollow = objectToFollow;
		_mapMarker.AddComponent<GeocommentToggler>().GeocommentInput = GeocommentInput;
		_mapMarker.GetComponent<GeocommentToggler>().ParentObject = ParentObject;

		var text = GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text;
		_mapMarker.AddComponent<CommentText>().Text = text;
		_mapMarker.GetComponent<Button>().onClick.AddListener(_mapMarker.GetComponent<GeocommentToggler>().ShowGeocommentInput);

		RemoveButtonListeners();
		GeocommentInput.gameObject.SetActive(false);
	}

	private void RemoveButtonListeners()
	{
		var buttons = GeocommentInput.GetComponentsInChildren<Button>();
		buttons.ToList().ForEach(b => b.onClick.RemoveAllListeners());
	}

	public void CancelGeocomment()
	{
		RemoveButtonListeners();
		GeocommentInput.gameObject.SetActive(false);
	}
}
