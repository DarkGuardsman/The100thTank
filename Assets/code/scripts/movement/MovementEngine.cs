using UnityEngine;
using System.Collections;
/// <summary>
/// Engine that moves the gameobject using basic physics. In addition allows the
/// center of mass to be relocated, which is optional.
/// </summary>
public abstract class MovementEngine : MonoBehaviour {

	//Attached objects
	public GameObject centerOfMass = null;
	[HideInInspector]
	public Rigidbody rigidbody;

	//Settings
	public float speed = 90f;
	public float turnSpeed = 5f;

	public float powerInput;
	public float turnInput;

	//On load of the object
	protected virtual void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();	
		if (centerOfMass != null) {
			rigidbody.centerOfMass = centerOfMass.transform.localPosition;	
		}
	}
	
	//Called each normal update
	protected virtual void FixedUpdate()
	{	
		//Moves the tank forward or backwards
		rigidbody.AddRelativeForce (0f, 0f, powerInput * speed);
		
		//Rotates the tank
		rigidbody.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
	}
}
