using UnityEngine;
using System.Collections;

public class AICannon : Cannon
{
	public bool fire = false;

	protected override GameObject Fire ()
	{
		GameObject bullet = base.Fire ();
		fire = false;
		return bullet;
	}

	public override bool ShouldFire ()
	{
		return fire;
	}
}
