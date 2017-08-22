using System;
using System.Linq;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class ObjectsMover : MonoBehaviour
{

	public ScreenTransformGesture TransformGesture;
	public GameObject AllObjects;
	public float TransformSpeedCoefficent;

	private GameObject _superParent;

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

			var newX = AllObjects.transform.position.x + TransformGesture.DeltaPosition.x * transformSpeed;
			var newZ = AllObjects.transform.position.z + TransformGesture.DeltaPosition.y * transformSpeed;
			var newPosition = new Vector3(newX, AllObjects.transform.position.y, newZ);
			AllObjects.transform.position = newPosition;
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
			_superParent.transform.Rotate(Vector3.up, -deltaRotation);
		}
	}

	private void BeginRotateObjects(object sender, EventArgs e)
	{
		if (TransformGesture.NumPointers >= 2)
		{
			var medianPosition = GetMedianPosition();

			Debug.Log("Median screen position: " + medianPosition);

			var rotationCenter = GetRotationCenter(medianPosition);

			_superParent = new GameObject("SuperParent");
			_superParent.transform.position = rotationCenter;

			AllObjects.transform.SetParent(_superParent.transform, true);
			//AllObjects.transform.parent = _superParent.transform;
		}
	}

	private Vector3 GetRotationCenter(Vector2 medianPosition)
	{
		var ray = GetComponent<Camera>().ScreenPointToRay(medianPosition);

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
		AllObjects.transform.parent = null;	
		Destroy(_superParent);
		Debug.Log("Completed rotation");
	}
}
