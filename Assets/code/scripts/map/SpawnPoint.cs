using UnityEngine;
using System.Collections;
/// <summary>
/// Scriped designed to be used to spawn gameobjects from another gameobject
/// </summary>
public class SpawnPoint : MonoBehaviour
{
	public GameObject objectToSpawn;
	public bool shouldSpawn = true;
	public float spawnDelaySeconds = 1f;

	private float currentSpawnTicks = 0f;

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (shouldSpawn) {
			if (currentSpawnTicks >= spawnDelaySeconds) {
				spawn ();
				shouldSpawn = false;
				currentSpawnTicks = 0;
			} else {
				currentSpawnTicks += Time.deltaTime;
			}
		}
	}


	public virtual GameObject spawn ()
	{
		return Instantiate (objectToSpawn, transform.position, transform.rotation) as GameObject;
	}
}
