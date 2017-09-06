using System;
using System.Linq;
using Assets.TouchScript.Scripts.Gestures.TransformGestures;
using UnityEngine;

namespace Assets.Scripts
{
	public class ObjectTransformer : MonoBehaviour
	{

		public ScreenTransformGesture TransformGesture;
		public float TransformSpeedCoefficent;
		public GameObject GrandParent;
		public GameObject ParentObject;

		private void OnEnable()
		{
			TransformGesture.Transformed += TransformObjects;
			TransformGesture.Transformed += ScaleObjects;
			TransformGesture.Transformed += RotateObjects;

			TransformGesture.TransformStarted += BeginRotateObjects;
			TransformGesture.TransformCompleted += CompleteRotateObjects;
		}

		private void OnDisable()
		{
			TransformGesture.Transformed -= TransformObjects;
			TransformGesture.Transformed -= ScaleObjects;
			TransformGesture.Transformed -= RotateObjects;

			TransformGesture.TransformStarted -= BeginRotateObjects;
			TransformGesture.TransformCompleted -= CompleteRotateObjects;
		}

		private void TransformObjects(object sender, EventArgs e)
		{
			if (TransformGesture.NumPointers == 1)
			{
				var y = transform.position.y;
				var transformSpeed = y / TransformSpeedCoefficent;

				var newX = transform.position.x - TransformGesture.DeltaPosition.x * transformSpeed;
				var newZ = transform.position.z - TransformGesture.DeltaPosition.y * transformSpeed;
				var newPosition = new Vector3(newX, transform.position.y, newZ);
				transform.position = newPosition;
			}
		}

		private void ScaleObjects(object sender, EventArgs e)
		{
			if (TransformGesture.NumPointers >= 2)
			{
				var deltaScale = TransformGesture.DeltaScale;
				var cameraPosition = GetComponent<Camera>().transform.position;
				GetComponent<Camera>().transform.position = new Vector3(cameraPosition.x, cameraPosition.y / deltaScale, cameraPosition.z);
			}
		}

		private void RotateObjects(object sender, EventArgs e)
		{
			if (TransformGesture.NumPointers >= 2)
			{	
				var deltaRotation = TransformGesture.DeltaRotation;
				GrandParent.transform.Rotate(Vector3.up, -deltaRotation);
			}
		}

		private void BeginRotateObjects(object sender, EventArgs e)
		{
			if (TransformGesture.NumPointers >= 2)
			{
				var medianPosition = GetMedianPosition();
				var rotationCenter = GetRotationCenter(medianPosition);
				GrandParent.transform.position = rotationCenter;
				ParentObject.transform.SetParent(GrandParent.transform, true);
			}
		}

		private static Vector3 GetRotationCenter(Vector2 medianPosition)
		{
			var ray = Camera.main.ScreenPointToRay(medianPosition);
			//find distance of raycast when crossing y-plane
			var distance = -ray.origin.y / ray.direction.y;
			return ray.GetPoint(distance);
		}

		private Vector2 GetMedianPosition()
		{
			var pointers = TransformGesture.ActivePointers;
			var startPosition = new Vector2(0, 0);
			pointers.ToList().ForEach(p => startPosition += p.Position);
			return startPosition / TransformGesture.NumPointers;
		}

		private void CompleteRotateObjects(object sender, EventArgs e)
		{
			ParentObject.transform.SetParent(null);
		}
	}
}
