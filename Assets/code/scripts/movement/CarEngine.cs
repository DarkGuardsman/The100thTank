using UnityEngine;
using System.Collections;

public class CarEngine : MovementEngine {

	public GameObject wheelColliderCluster;
	public GameObject wheelGraphicsCluster;

	public float steer_max = 20;

	//Wheel colliders
	private WheelCollider rearLeftWheel;
	private WheelCollider rearRightWheel;
	private WheelCollider frontLeftWheel;
	private WheelCollider frontRightWheel;

	//Actual wheel objects
	private GameObject rearLeftTire;
	private GameObject rearRightTire;
	private GameObject frontLeftTire;
	private GameObject frontRightTire;

	private bool reverse = false;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		rearLeftWheel = wheelColliderCluster.transform.FindChild ("BL").GetComponent<WheelCollider>();
		rearRightWheel = wheelColliderCluster.transform.FindChild ("BR").GetComponent<WheelCollider>();
		frontLeftWheel = wheelColliderCluster.transform.FindChild ("FL").GetComponent<WheelCollider>();
		frontRightWheel = wheelColliderCluster.transform.FindChild ("FR").GetComponent<WheelCollider>();

		rearLeftTire = wheelGraphicsCluster.transform.FindChild ("BL").gameObject;
		rearRightTire = wheelGraphicsCluster.transform.FindChild ("BR").gameObject;
		frontLeftTire = wheelGraphicsCluster.transform.FindChild ("FL").gameObject;
		frontRightTire = wheelGraphicsCluster.transform.FindChild ("FR").gameObject;
	}
	
	protected override void FixedUpdate () {

		//User controls TODO move to external script to allow AI uses of script
		turnInput = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
		powerInput = Input.GetAxis("Vertical");

		//--Physics	--
		//Apply torque to power wheels
		float forward = Mathf.Clamp(powerInput, 0, 1);
		float back = -1 * Mathf.Clamp(powerInput, -1, 0);
		float motor = 0;
		float brake = 0;	

		//Change gear (only forward and backwards gears)
		if(rearLeftWheel.rpm == 0 || rearRightWheel.rpm == 0) {
			if(back > 0) { reverse = true; }
			if(forward > 0) { reverse = false; }
		}

		//Apply motion and/or brake
		if(reverse) {
			motor = -1 * back;
			brake = forward;
		} else {
			motor = forward;
			brake = back;
		}

		rearLeftWheel.motorTorque = speed * motor;
		rearRightWheel.motorTorque = speed * motor;
		rearLeftWheel.brakeTorque = speed * brake;
		rearRightWheel.brakeTorque = speed * brake;

		//Steer using front wheels
		frontLeftWheel.steerAngle = steer_max * turnInput;
		frontRightWheel.steerAngle = steer_max * turnInput;

		//--Graphics--
		//Rotate front tires to illistrate turning
		frontLeftTire.transform.localEulerAngles = new Vector3(frontLeftTire.transform.localEulerAngles.x, steer_max * turnInput, frontLeftTire.transform.localEulerAngles.z);
		frontRightTire.transform.localEulerAngles = new Vector3(frontRightTire.transform.localEulerAngles.x, steer_max * turnInput, frontRightTire.transform.localEulerAngles.z);

		//Rotate all wheels to illistrate motion
		frontLeftTire.transform.Rotate(frontLeftWheel.rpm * -6 * Time.deltaTime, 0, 0);
		frontRightTire.transform.Rotate(frontRightWheel.rpm * -6 * Time.deltaTime, 0, 0);
		rearLeftTire.transform.Rotate(rearLeftWheel.rpm * -6 * Time.deltaTime, 0, 0);
		rearRightTire.transform.Rotate(rearRightWheel.rpm * -6 * Time.deltaTime, 0, 0);
		
	}
}
