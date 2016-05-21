using UnityEngine;
using System.Collections;
using BuiltBroken.Damage;

public class Barrel : Entity {

	public GameObject effectPrefab;
	public override bool damageEntity(DamageSource source, float damage)
	{
		if (damage > 10) {
			GoBoom();
		}
		return base.damageEntity (source, damage);
	}

	void GoBoom()
	{
		//TODO set dead, (change color or create sharpnal) and set destroy timer
		gameObject.GetComponent<AudioSource>().Play();
		Instantiate (effectPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
