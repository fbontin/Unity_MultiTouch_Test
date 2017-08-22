using System;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class TapHandler : MonoBehaviour
{

	public GameObject ImageCoordinates;
	public Canvas MainCanvas;

	private void OnEnable()
	{
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}

	private void OnDisable()
	{
		GetComponent<TapGesture>().Tapped -= tappedHandler;
	}

	private void tappedHandler(object sender, EventArgs e)
	{
		var tg = GetComponent<TapGesture>();
		var screenPosition = tg.ScreenPosition;

		var worldPosition = GetWorldPosition(screenPosition);


		var empty = new GameObject("EmptyGameObject");
		empty.transform.parent = GetComponent<ObjectsMover>().AllObjects.transform;
		empty.transform.position = worldPosition;

		var uiImage = Instantiate(Resources.Load("Image")) as GameObject;
		uiImage.transform.SetParent(MainCanvas.transform);
		uiImage.AddComponent<ScreenSpaceMover>().ObjectToFollow = empty;




		ImageCoordinates.transform.position = worldPosition;

		Debug.Log("Tap world position: " + worldPosition);
	}

	private Vector3 GetWorldPosition (Vector2 screenPosition)
	{
		var ray = GetComponent<Camera>().ScreenPointToRay(screenPosition);

		//find distance of raycast when crossing y-plane
		var distance = -ray.origin.y / ray.direction.y;
		return ray.GetPoint(distance);
	}
}
