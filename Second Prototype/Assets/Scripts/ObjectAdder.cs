using TouchScript.Behaviors;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

namespace Assets.Scripts
{
	public class ObjectAdder : MonoBehaviour
	{

		public void AddObject(string objectType)
		{
			PrimitiveType primitiveType;

			switch (objectType)
			{
				case "Cube":
					primitiveType = PrimitiveType.Cube;
					break;
				case "Sphere":
					primitiveType = PrimitiveType.Sphere;
					break;
				default:
					primitiveType = PrimitiveType.Capsule;
					break;
			}

			Debug.Log("Adding " + objectType);

			var plane = GameObject.Find("Plane");
			var cube = GameObject.CreatePrimitive(primitiveType);
			//set plane as parent
			cube.transform.parent = plane.transform;

			cube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
			cube.transform.localPosition = new Vector3(1f, 0.5f, 1f);

			cube.AddComponent<StayOnGround>();
			cube.AddComponent<TransformGesture>();
			var tg = cube.GetComponent<TransformGesture>();
			tg.Projection = TransformGesture.ProjectionType.Global;
			tg.ProjectionPlaneNormal = new Vector3(0, 1, 0);
			cube.AddComponent<Transformer>();
		}
	}
}
