var cruiseSpeed = 30.0;
var climbSpeed = 8.0;
var turnspeed = 0.5;
var gravity = 20.0;

static var Global_Offset_x = 1329000.0f;
static var Global_Offset_z = 6172000.0f;

private var controller : CharacterController;
private var moveDirection = Vector3.zero;
private var grounded : boolean = false;

function Awake(){
	controller  = GetComponent(CharacterController);	
	

}


// Smoothly tilts a transform towards a target rotation.
var smooth = 2.0;
var rollAngle = 10.0;
var bankAngle = 5.0;
var roll = 0.0;
var bank = 0.0;
var yaw =0.0;

function LateUpdate () 
{
	if (Input.GetKey ("escape"))
		Application.Quit();
	
	bank = Input.GetAxis("Ascend") * bankAngle;
	roll = Input.GetAxis("Horizontal") * rollAngle ;
	yaw = transform.eulerAngles.y + Input.GetAxis("Horizontal")* turnspeed;
	
	var target = Quaternion.Euler (-bank, yaw, -roll);
	// Dampen towards the target rotation
	transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	//transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.05*smooth);

	
	moveDirection = Vector3( 0 , Input.GetAxis("Ascend") *climbSpeed, Input.GetAxis("Vertical")* cruiseSpeed);
	
	moveDirection = transform.TransformDirection(moveDirection);
	
		
	// Apply gravity
	//moveDirection.y -= gravity * Time.deltaTime;
		
	// Move the controller
	controller.Move(moveDirection * Time.deltaTime);
	//controller.Move(moveDirection *0.05);
	
}

function OnGUI () 
{

	//GUI.Box (Rect (5,30,90,22), "N: "+(transform.localPosition.z ).ToString("f0"));
	GUI.Box (Rect (5,30,90,22), "N: "+(transform.localPosition.z+Global_Offset_z ).ToString("f0"));
	GUI.Box (Rect (5,55,90,22), "E: "+(transform.localPosition.x+Global_Offset_x ).ToString("f0"));
	GUI.Box (Rect (5,80,90,22), "Alt: "+(transform.localPosition.y ).ToString("f0")+" m");
	GUI.Box (Rect (5,105,90,22), "Az: "+(transform.eulerAngles.y ).ToString("f0") );
	
}

