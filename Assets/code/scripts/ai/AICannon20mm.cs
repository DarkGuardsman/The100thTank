using UnityEngine;
using System.Collections;

public class AICannon20mm : AICannon
{
	public GameObject secondFiringNode;

	protected override GameObject Fire ()
	{
		GameObject temp = firingNode;
		firingNode = secondFiringNode;
		secondFiringNode = temp;
		return base.Fire ();
	}
}
