using UnityEngine;
using System.Collections;

//Controls the movement of the tank
public class HoverEngine : MovementEngine {

	//Settings
	public float hoverForce = 65f;
	public float hoverHeight = 2.5f;
	public float tiltChangeValue = 0.1f;

	//raycast data
	public GameObject rayCluster;	
	private Transform backLeft;
	private Transform backRight;
	private Transform frontLeft;
	private Transform frontRight;	
	
	private RaycastHit lr;
	private RaycastHit rr;
	private RaycastHit lf;
	private RaycastHit rf;

	//On load of the object
	protected override void Awake()
	{
		base.Awake ();
		//Only populate if empty, allows manual setting of trace points
		backLeft = rayCluster.transform.Find("backLeft");
		backRight = rayCluster.transform.Find("backRight");
		frontLeft = rayCluster.transform.Find("frontLeft");
		frontRight = rayCluster.transform.Find("frontRight");			
	}

	//Called each normal update
	protected override void FixedUpdate()
	{
		base.FixedUpdate ();
		HandleHoverRays();

		//Hover rotation update
		Vector3 start = rayCluster.transform.position;
		Vector3 end = -rayCluster.transform.up;
		Ray ray = new Ray (start, end);
		RaycastHit hit;
		
		Debug.DrawRay (start, end, Color.red);
		
		if (Physics.Raycast (ray, out hit, hoverHeight)) {
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = rayCluster.transform.up * proportionalHeight * hoverForce;
			this.rigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	}
	
	//Handles all hover code
	void HandleHoverRays()
	{
		float yRotation = transform.eulerAngles.y;
		//Raycast all 4 corners strait down
		Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
		Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
		Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
		Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);
 		
		//calculate new up
		Vector3 newUp = (Vector3.Cross (rr.point - Vector3.up, lr.point - Vector3.up) +
			Vector3.Cross (lr.point - Vector3.up, lf.point - Vector3.up) +
			Vector3.Cross (lf.point - Vector3.up, rf.point - Vector3.up) +
			Vector3.Cross (rf.point - Vector3.up, rr.point - Vector3.up)).normalized;

		//Slow rotation to prevent screen rapid motion
		transform.up = Vector3.Slerp(transform.up, newUp, tiltChangeValue);
		//Corrects Y rotation changing due to transfrom.up change
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
		//Debug info
		Debug.DrawRay(rr.point, Vector3.up);
		Debug.DrawRay(lr.point, Vector3.up);
		Debug.DrawRay(lf.point, Vector3.up);
		Debug.DrawRay(rf.point, Vector3.up);
	}
}
