using Assets.TouchScript.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class ObjectInserter : MonoBehaviour
	{

		public GameObject ScrollView;
		public GameObject ParentObject;

		public void InsertFromTap(GameObject objectType)
		{
			var go = Instantiate(objectType);
			go.transform.parent = ParentObject.transform;
			SetPosition(go);
			ChangeTouchFocus();
		}

		private static void SetPosition(GameObject go)
		{
			var tapPosition = FindTapPosition();

			//var distance = Vector3.Distance(Camera.main.transform.position, Plane.transform.position);
			var ray = Camera.main.ScreenPointToRay(new Vector3(tapPosition.x, tapPosition.y));

			var plane = new Plane(Vector3.up, Vector3.zero);
			plane.Translate(new Vector3(0, -25, 0)); //move to ground level from sea level
			float hitDistance;
			plane.Raycast(ray, out hitDistance);

			go.transform.position = ray.origin + hitDistance * ray.direction;
		}

		private static Vector2 FindTapPosition()
		{
			var tm = TouchManager.Instance;
			return tm.PressedPointersCount > 0 ? tm.PressedPointers[0].Position : new Vector2(0, 0);
		}

		private void ChangeTouchFocus()
		{
			ScrollView.SetActive(false);
			var tm = TouchManager.Instance;
			var firstPointerId = tm.PressedPointers[0].Id;
			tm.CancelPointer(firstPointerId, true);
		}
	}
}
