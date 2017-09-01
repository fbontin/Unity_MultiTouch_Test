using UnityEngine;

namespace Assets.Scripts
{
	public class FlightControls : MonoBehaviour {

		public float maxAirSpeed = 50.0f;
		//public float climbSpeed = 8.0f;
		public float maxYaw = 20.0f;
		public float gravity = 20.0f;

		public float airSpeed = 0.0f;

		// Smoothly tilts a transform towards a target rotation.
		public float smooth = 2.0f;
		public float rollAngle = 10.0f;
		public float bankAngle = 10.0f;
		private float roll = 0.0f;
		private float bank = 0.0f;
		private float yaw = 0.0f;
		private float climb = 0.0f;

		private float Global_Offset_x = 1329000.0f;
		private float Global_Offset_z = 6172000.0f;

		private CharacterController controller;
		private Vector3 moveDirection = Vector3.zero;


		void Start () {
			controller  = GetComponent<CharacterController>();	
			Cursor.visible = false;
			airSpeed = 0.0f;
		}
	

		void Update () {
			if (Input.GetKey ("escape"))
				Application.Quit();
		
			bank = Input.GetAxis("Vertical") * bankAngle;
			roll = Input.GetAxis("Horizontal") * rollAngle ;
			yaw = transform.eulerAngles.y + Input.GetAxis("Horizontal")* maxYaw;

			var target = Quaternion.Euler (-bank, yaw, -roll);
			// Dampen towards the target rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

			airSpeed = (Input.GetAxis("Power") +1.0f) * maxAirSpeed;

			moveDirection = new Vector3( 0 , climb, airSpeed );

			moveDirection = transform.TransformDirection(moveDirection);


			// Apply gravity
			//moveDirection.y -= gravity * Time.deltaTime;

			// Move the controller
			controller.Move(moveDirection * Time.deltaTime);
		}



		void OnGUI () 
		{


			GUI.Box (new Rect (5,30,90,22), "N: "+(transform.localPosition.z+Global_Offset_z ).ToString("f0"));
			GUI.Box (new Rect (5,55,90,22), "E: "+(transform.localPosition.x+Global_Offset_x ).ToString("f0"));
			GUI.Box (new Rect (5,80,90,22), "Höjd: "+(transform.localPosition.y ).ToString("f0")+" m");
			GUI.Box (new Rect (5,105,90,22), "Bäring: "+(transform.eulerAngles.y ).ToString("f0") );

		}
	}
}
