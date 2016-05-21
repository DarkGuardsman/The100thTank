using UnityEngine;
using System.Collections;

//http://forum.unity3d.com/threads/how-to-make-a-physically-real-stable-car-with-wheelcolliders.50643/
public class AntiRollBar : MonoBehaviour {

	public WheelCollider WheelL;
	public WheelCollider WheelR;
	public Rigidbody rb;
	public float AntiRoll = 5000.0f;
	

	void FixedUpdate ()
	{
		WheelHit hit;
		float travelL = 1.0f;
		float travelR = 1.0f;
		
		bool groundedL = WheelL.GetGroundHit(out hit);
		if (groundedL)
		{
			travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;
		}
		
		bool groundedR = WheelR.GetGroundHit(out hit);
		if (groundedR)
		{
			travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;
		}
		
		float antiRollForce = (travelL - travelR) * AntiRoll;
		
		if (groundedL)
		{
			rb.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
			                             WheelL.transform.position); 
		}
		if (groundedR)
		{
			rb.AddForceAtPosition(WheelR.transform.up * antiRollForce,
			                             WheelR.transform.position); 
		}
	}
}
