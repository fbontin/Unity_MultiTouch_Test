using System;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class TapHandler : MonoBehaviour
{
	public GameObject GeocommentInput;
	public GameObject MapMarkers;

	private GameObject _parentObject;
	private GameObject _latestPositionObject;
	private GameObject _mapMarker;

	private void OnEnable()
	{
		GetComponent<TapGesture>().Tapped += OnTap;
		_parentObject = GameObject.FindWithTag("ParentObject");
	}

	private void OnDisable()
	{
		GetComponent<TapGesture>().Tapped -= OnTap;
	}

	private void OnTap(object sender, EventArgs e)
	{
		var worldPosition = CreatePositionObject();
		worldPosition = new Vector3(worldPosition.x, 0, worldPosition.z);


		//move click point to center of screen.
		_parentObject.transform.position -= worldPosition;

		SetButtonListeners();
		GeocommentInput.gameObject.SetActive(true);
		GeocommentInput.GetComponentInChildren<InputField>().Select();
		System.Diagnostics.Process.Start("tabtip.exe");
	}

	private void SetButtonListeners()
	{
		var buttons = GeocommentInput.GetComponentsInChildren<Button>();
		buttons[0].onClick.AddListener(CancelGeocomment); //background button
		buttons[1].onClick.AddListener(CancelGeocomment); //remove button
		buttons[2].onClick.AddListener(SaveGeocomment); //save button
	}

	private Vector3 CreatePositionObject()
	{
		var worldPosition = GetWorldPosition();
		var positionObject = new GameObject("EmptyPositionObject");
		positionObject.transform.parent = _parentObject.transform;
		positionObject.transform.position = worldPosition;
		_latestPositionObject = positionObject;

		return worldPosition;
	}

	private Vector3 GetWorldPosition ()
	{
		var ray = GetComponent<Camera>().ScreenPointToRay(GetComponent<TapGesture>().ScreenPosition);
	
		//find distance of raycast when crossing y-plane
		const float height = 25.0f;
		var distance = (height - ray.origin.y) / ray.direction.y;
		var p = ray.GetPoint(distance);
		return p;
	}

	public void SaveGeocomment()
	{
		_mapMarker = Instantiate(Resources.Load("Map Marker")) as GameObject;
		_mapMarker.transform.SetParent(MapMarkers.transform);
		_mapMarker.AddComponent<ScreenSpaceMover>().ObjectToFollow = _latestPositionObject;
		_mapMarker.AddComponent<GeocommentToggler>().GeocommentInput = GeocommentInput;

		var text = GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text;
		_mapMarker.AddComponent<CommentText>().Text = text;

		_mapMarker.GetComponent<Button>().onClick.AddListener(ShowGeocomment);

		RemoveButtonListeners();

		GeocommentInput.gameObject.SetActive(false);
	}

	private void RemoveButtonListeners()
	{
		var buttons = GeocommentInput.GetComponentsInChildren<Button>();
		buttons.ToList().ForEach(b => b.onClick.RemoveAllListeners());
	}

	private void ShowGeocomment()
	{
		_mapMarker.GetComponent<GeocommentToggler>().ShowGeocommentInput();
		Debug.Log("marker clicked in taphandler");
	}

	public void CancelGeocomment()
	{
		Destroy(_latestPositionObject);
		GeocommentInput.gameObject.SetActive(false);
	}
}
