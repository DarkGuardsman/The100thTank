using UnityEngine;
using System.Collections;
/// <summary>
/// Helps keep the object's x & z rotation centered. While still allowing it to rotate if needed.
/// </summary>
public class XZRotation : MonoBehaviour {

	private int collisionCount = 0;
	public float xRotationLimit = 5f;
	public float zRotationLimit = 5f;
	
	// Update is called once per frame
	void FixedUpdate () {
		//No rotation if we have collisions
		if (collisionCount <= 0) 
		{
			Vector3 oldRotation = transform.eulerAngles;
			Vector3 newRotation = new Vector3(0, 0, 0);

			bool updateX = oldRotation.x > xRotationLimit || oldRotation.x < xRotationLimit;
			bool updateZ = oldRotation.z > zRotationLimit || oldRotation.z < zRotationLimit;
			if(updateX || updateZ)
			{
				if(updateX && updateZ)
				{
					newRotation = new Vector3(0, oldRotation.y, 0);
				}
				else if(updateX)
				{
					newRotation = new Vector3(0, oldRotation.y, oldRotation.z);
				}
				else if(updateZ)
				{
					newRotation = new Vector3(oldRotation.x, oldRotation.y, 0);
				}
				transform.eulerAngles = Vector3.Slerp(oldRotation, newRotation, Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter()
	{
		collisionCount++;
		Debug.Log ("Collision Detected");
	}
	
	void OnCollisionExit()
	{
		collisionCount--;
	}
}
