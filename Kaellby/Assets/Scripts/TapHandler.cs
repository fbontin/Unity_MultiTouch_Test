using System;
using System.Linq;
using Assets.TouchScript.Scripts.Gestures;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class TapHandler : MonoBehaviour
	{
		public GameObject GeocommentInput;
		public GameObject MapMarkers;
		public GameObject ParentObject;

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
			var mapMarker = Instantiate(Resources.Load<GameObject>("Map Marker"));
			mapMarker.transform.SetParent(MapMarkers.transform);

			var objectToFollow = CreatePositionObject();
			mapMarker.AddComponent<ScreenSpaceMover>().ObjectToFollow = objectToFollow;
			mapMarker.AddComponent<GeocommentToggler>().GeocommentInput = GeocommentInput;
			mapMarker.GetComponent<GeocommentToggler>().ParentObject = ParentObject;

			var text = GeocommentInput.GetComponentInChildren<InputField>().GetComponentInChildren<Text>().text;
			mapMarker.AddComponent<CommentText>().Text = text;
			mapMarker.GetComponent<Button>().onClick.AddListener(mapMarker.GetComponent<GeocommentToggler>().ShowGeocommentInput);

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
}
