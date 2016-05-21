using UnityEngine;
using System.Collections;

//Controls the movement of the tank
public class PMovement : MonoBehaviour {

	private MovementEngine engine;

	//On load of the object
	void Awake()
	{
		engine = GetComponent<MovementEngine>();
	}

	// Called each frame update
	void Update () 
	{
		//Grabs input from the user
		engine.powerInput = Input.GetAxis ("Vertical");
		engine.turnInput = Input.GetAxis ("Horizontal");
	}
}
