using UnityEngine;
using System.Collections;
namespace BuiltBroken.Damage
{
	/// <summary>
	/// Used to describe the type of damage and the source of the damage
	/// </summary>
	public class DamageSource {

		public string name;
		public GameObject source;

		public DamageSource(string name)
		{
			this.name = name;
		}

		//TODO add bool types(explosion, fire, projectile, majic, bypass armor, bypass all)
	}

	public class BulletDamageSource : DamageSource
	{
		public BulletDamageSource (GameObject shooter) : base("Bullet")
		{
			this.source = shooter;
		}
	}

	public class ExplosionDamageSource : DamageSource
	{
		public ExplosionDamageSource (GameObject shooter) : base("Explosion")
		{
			this.source = shooter;
		}
	}
}
