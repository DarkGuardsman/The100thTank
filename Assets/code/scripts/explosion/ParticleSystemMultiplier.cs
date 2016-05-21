using System;
using UnityEngine;
/// <summary>
/// Scales the particle 
/// </summary>
namespace BuiltBroken.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
		public float multiplier = 1;

        private void Start()
        {
            var systems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in systems)
            {
                system.startSize *= multiplier;
                system.startSpeed *= multiplier;
                system.startLifetime *= Mathf.Lerp(multiplier, 1, 0.5f);
                system.Clear();
                system.Play();
            }
        }
    }
}
