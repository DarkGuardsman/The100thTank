using UnityEngine;
using System.Collections;

public class PCannon : Cannon
{
	public override bool ShouldFire ()
	{
		return !fullAuto && Input.GetMouseButtonDown (0) || fullAuto && Input.GetMouseButton (0);
	}
}
