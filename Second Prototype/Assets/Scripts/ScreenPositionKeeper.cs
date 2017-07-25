using UnityEngine;

namespace Assets.Scripts
{
	public class ScreenPositionKeeper : MonoBehaviour
	{

		public float GoalX;
		public float GoalY;

		private Camera _cam;


		// Use this for initialization
		void Start ()
		{
			_cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			gameObject.transform.position = _cam.ScreenToWorldPoint(new Vector3(GoalX, GoalY, 3));
		}
	}
}
