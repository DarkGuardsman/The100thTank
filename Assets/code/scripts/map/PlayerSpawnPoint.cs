using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : SpawnPoint
{

	public override GameObject spawn ()
	{
		GameObject spawnedObject = base.spawn ();
		Cannon cannon = FindCannon (spawnedObject);
		//Safty just in case we try to spawn another player while the player is still alive
		if (GameObject.FindWithTag ("Player") != null) {
			Destroy (GameObject.FindWithTag ("Player").gameObject);
		}
		if (cannon != null) {
			//TODO find an easier way to assign this
			GameObject.FindWithTag ("Gui").transform.FindChild ("AmmoText").gameObject.GetComponent<AmmoText> ().cannon = cannon;
		}
		return spawnedObject;
	}

	protected Cannon FindCannon (GameObject obj)
	{
		Cannon cannon = obj.GetComponent<Cannon> ();
		if (cannon == null) {
			cannon = obj.GetComponentInChildren<Cannon> ();
		}
		return cannon;
	}
}
