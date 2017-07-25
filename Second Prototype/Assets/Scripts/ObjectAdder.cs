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

		public void AddObject(string objectType)
		{
			var primitiveType = ParsePrimitiveType(objectType);
			Debug.Log("Adding " + objectType);

			var plane = GameObject.Find("Plane");
			var go = GameObject.CreatePrimitive(primitiveType);
			go.transform.parent = plane.transform;

			go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
			go.transform.localPosition = new Vector3(1f, 0.5f, 1f);

			AddComponentsToGameObject(go);
		}

		public void AddObjectWithCoordinates(string objectType)
		{
			var primitiveType = ParsePrimitiveType(objectType);

			var plane = GameObject.Find("Plane");
			var go = GameObject.CreatePrimitive(primitiveType);
			go.transform.parent = plane.transform;

			go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

			SetPosition(go, plane);
			AddComponentsToGameObject(go);
			ChangeTouchFocus();
		}

		private static void ChangeTouchFocus()
		{
			//disable scrollview which includes the buttons
			var scrollView = GameObject.Find("Scroll View");
			//var sv = scrollView.GetComponent<ScrollRect>();
			

			scrollView.SetActive(false);
			
			//var button = GameObject.Find("Cube Button").GetComponent<Button>();
			//button.interactable = false;

			//cancel pointer/touch
			var tm = TouchManager.Instance;
			var firstPointerId = tm.PressedPointers[0].Id;
			tm.CancelPointer(firstPointerId, true);
		}

		private static void SetPosition(GameObject go, GameObject plane)
		{
			var tapPosition = FindTapPosition();

			var cam = GameObject.Find("Main Camera").GetComponent<Camera>();
			var distance = Vector3.Distance(cam.transform.position, plane.transform.position);
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
				Debug.Log("Real position: " + pos);
				return pos;
			}
			Debug.Log("Position (0, 0) used");
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
