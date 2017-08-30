using System;
using System.Linq;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class ObjectsMover : MonoBehaviour
{

	public ScreenTransformGesture TransformGesture;
	public float TransformSpeedCoefficent;

	private GameObject _parentObject;
	private GameObject _superParent;

	private void OnEnable()
	{
		TransformGesture.Transformed += TransformObjects;
		TransformGesture.Transformed += ScaleObjects;
		TransformGesture.Transformed += RotateObjects;

		TransformGesture.TransformStarted += BeginRotateObjects;

		TransformGesture.TransformCompleted += CompleteRotateObjects;

		_parentObject = GameObject.FindWithTag("ParentObject");
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
		//move objects
		/*
		if (TransformGesture.NumPointers == 1)
		{
			var y = transform.position.y;
			var transformSpeed = y / TransformSpeedCoefficent;

			var newX = _parentObject.transform.position.x + TransformGesture.DeltaPosition.x * transformSpeed;
			var newZ = _parentObject.transform.position.z + TransformGesture.DeltaPosition.y * transformSpeed;
			var newPosition = new Vector3(newX, _parentObject.transform.position.y, newZ);
			_parentObject.transform.position = newPosition;
		}
		*/

		//move camera
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
			_parentObject.transform.SetParent(_superParent.transform, true);
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
		_parentObject.transform.parent = null;	
		Destroy(_superParent);
	}
}
