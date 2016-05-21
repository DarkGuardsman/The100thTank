using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuiltBroken.Damage;
/// <summary>
/// Modified version of Unities's standard explosion
/// </summary>
namespace BuiltBroken.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4f;
		public float explosionDamageScale = 2f;

        private IEnumerator Start()
        {
            // Wait a frame to allow other objects to spawn
            yield return null;

			//Grab the multiplaier from the joint script
            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;
            float radius = 10 * multiplier;

			//Move and attack all objects in the sphere
            var cols = Physics.OverlapSphere(transform.position, radius);
			List<Rigidbody>  rigidbodies = new List<Rigidbody> ();
			List<IEntity>  entities = new List<IEntity> ();

			//Damage source to attack entities with TODO get entity that caused the explosion
			ExplosionDamageSource damageSource = new ExplosionDamageSource (gameObject);

			//Loop threw all collisions in sphere
            foreach (var col in cols)
            {
				//Collect rigid bodies to move
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
					rigidbodies.Add(col.attachedRigidbody);
					col.attachedRigidbody.AddExplosionForce(explosionForce*multiplier, transform.position, radius, multiplier, ForceMode.Impulse);
                }

				//Collect entities to do damage
				IEntity[] ents = col.gameObject.GetComponents<IEntity>();
				foreach(var e in ents)
				{
					if(!entities.Contains(e) && !e.isDead())
					{
						entities.Add (e);
						//TODO scale by distance
						e.damageEntity(damageSource, radius * explosionDamageScale);
					}
				}
            }
        }
    }
}
