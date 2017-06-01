using UnityEngine;

namespace Assets.Scripts
{
	public class SnapToGoalPlane : MonoBehaviour
	{
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update ()
		{
			var goalObject = GameObject.Find("Goal Plane");
			var goalObjectSize = goalObject.transform.localScale.x;
			var goalObjectCorner = goalObject.transform.position -
			                       new Vector3(goalObjectSize / 2, goalObjectSize / 2, goalObjectSize / 2);

			var goalCorners = new[]
			{
				Vector3.Scale(goalObjectCorner, new Vector3(1, -1, 1)),
				Vector3.Scale(goalObjectCorner, new Vector3(1, -1, -1)),
				Vector3.Scale(goalObjectCorner, new Vector3(-1, -1, -1)),
				Vector3.Scale(goalObjectCorner, new Vector3(-1, -1, 1))
			};

			//var isClose = true;

			foreach (var corner in goalCorners)
			{
			}

			/*var goalPosition = goalObject.transform.position;
		var objectPosition = gameObject.transform.localPosition;

		var goalAngle = goalObject.transform.rotation.y;
		var objectAngles = gameObject.transform.rotation;

		var goalSize = goalObject.transform.lossyScale.x;
		var objectSize = gameObject.transform.lossyScale.x;

		var shouldSnap = IsClose(objectPosition, goalPosition, 0.2f) &&
		                 IsRotatedCorrectly(goalAngle, objectAngles.y, 10f, 4) &&
						 IsSameSize(goalSize, objectSize, 0.2f);

		if (shouldSnap)
		{
			gameObject.transform.localPosition = new Vector3(goalPosition.x, objectPosition.y, goalPosition.z);
			gameObject.transform.rotation = new Quaternion(objectAngles.x, goalAngle, objectAngles.z, objectAngles.w);
			gameObject.transform.localScale = goalObject.transform.localScale;
		}*/
		}

		//2d, only for x and z axes
		public static bool IsClose(Vector3 objectPosition, Vector3 goalPosition, float delta)
		{
			var deltaX = objectPosition.x - goalPosition.x;
			var deltaZ = objectPosition.z - goalPosition.z;
			var distance = Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);

			return distance < delta;
		}

		private static bool IsRotatedCorrectly(float objectAngle, float goalAngle, float delta, int numberOfCorners)
		{
			var mod = 360f / numberOfCorners;
			objectAngle = objectAngle % mod;
			goalAngle = goalAngle % mod;
			return Mathf.Abs(objectAngle - goalAngle) < delta;
		}

		private static bool IsSameSize(float objectSize, float goalSize, float delta)
		{
			return Mathf.Abs(objectSize - goalSize) < delta;
		}
	}
}
