using TouchScript;
using TouchScript.Behaviors;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class ObjectAdder : MonoBehaviour
	{

		public GameObject Plane;
		public GameObject UiPanel;
		public Camera MainCamera;

		public void AddObject(string objectType)
		{
			var primitiveType = ParsePrimitiveType(objectType);
			var go = GameObject.CreatePrimitive(primitiveType);
			go.transform.parent = Plane.transform;

			go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
			go.transform.localPosition = new Vector3(1f, 0.5f, 1f);

			AddComponentsToGameObject(go);
		}

		public void AddObjectWithCoordinates(GameObject objectType)
		{
			var go = Instantiate(objectType);
			go.transform.parent = Plane.transform;

			go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

			SetPosition(go);
			ChangeTouchFocus();
		}

		public void AddLiveObject(GameObject go)
		{
			go.transform.parent = Plane.transform;
			go.AddComponent<StayOnGround>();

			go.transform.localScale = Vector3.one;
			go.transform.rotation = Quaternion.Euler(0, 0, 0);

			Debug.Log("Local scale: " + go.transform.localScale);
			Debug.Log("Local posit: " + go.transform.localPosition);

			//remove listener which performs this function so that it is only performed the first time
			go.GetComponent<TransformGesture>().OnTransformStart = null;
		}

		public void SetScaleToOne(GameObject go)
		{
			go.transform.localScale.Set(1, 1, 1);
			Debug.Log("Scale should be one");
		}

		private void ChangeTouchFocus()
		{
			UiPanel.SetActive(false);

			//cancel pointer/touch
			var tm = TouchManager.Instance;
			var firstPointerId = tm.PressedPointers[0].Id;
			tm.CancelPointer(firstPointerId, true);

			//var gesture = GetComponent<PressGesture>();
			//if (gesture != null) gesture.Cancel();
		}

		private void SetPosition(GameObject go)
		{
			var tapPosition = FindTapPosition();

			var cam = GameObject.Find("Main Camera").GetComponent<Camera>();
			var distance = Vector3.Distance(cam.transform.position, Plane.transform.position);
			var ray = cam.ScreenPointToRay(new Vector3(tapPosition.x, tapPosition.y, distance));

			var pl = new Plane(Vector3.up, Vector3.zero);
			float hitDistance;
			pl.Raycast(ray, out hitDistance);

			go.transform.position = ray.origin + hitDistance * ray.direction;
		}

		private static Vector2 FindTapPosition()
		{
			var tm = TouchManager.Instance;
			if (tm.PressedPointersCount > 0)
			{
				var pos = tm.PressedPointers[0].Position;
				return pos;
			}
			return new Vector2(0, 0);
		}

		private static PrimitiveType ParsePrimitiveType(string objectType)
		{
			objectType = objectType.ToLower();
			switch (objectType)
			{
				case "cube":
					return PrimitiveType.Cube;
				case "sphere":
					return PrimitiveType.Sphere;
				default:
					return PrimitiveType.Capsule;
			}
		}

		private static void AddComponentsToGameObject(GameObject go)
		{
			go.AddComponent<StayOnGround>();
			go.AddComponent<TransformGesture>();
			go.AddComponent<TapGesture>();
			go.AddComponent<TappedHandler>();

			var tg = go.GetComponent<TransformGesture>();
			tg.Projection = TransformGesture.ProjectionType.Global;
			tg.ProjectionPlaneNormal = new Vector3(0, 1, 0);
			go.AddComponent<Transformer>();
		}
	}
}
