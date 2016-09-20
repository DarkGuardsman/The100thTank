using UnityEngine;
using System.Collections;
/// <summary>
/// Basic gun behavior script. Includes firing delay, clip size, and reload time
/// </summary>
public abstract class Cannon : MonoBehaviour
{
	/// <summary>
	/// The game object that will take credit for firing the bullet
	/// </summary>
	public GameObject shooter;
	/// <summary>
	/// Gameobject to be used as the exit point for the bullet. Prevents clipping of the collision box of the gun.
	/// </summary>
	public GameObject firingNode;
	/// <summary>
	/// The bullet prefab to create and fire.
	/// </summary>
	public GameObject bulletPrefab;
	
	[HideInInspector]
	public float
		firingDelayTicks = 0;
	[HideInInspector]
	public float
		reloadDelayTicks = 0;
	[HideInInspector]
	public int
		roundsLeft = 0;


	//Ammount of ammo in the clip
	public int ammoPerReload = 1;
	//delay between rounds firing
	public float firingDelaySeconds = 0.1f;
	//time in seconds it take to reload the clip
	public float reloadSpeedSeconds = 20f;
	//force to apply to the bullet
	public float bulletForce = 1000f;
	public float errorAmount = .01f;
	
	public bool fullAuto = false;
	public bool unlimitedAmmo = false;
	
	// Update is called once per frame
	protected virtual void Update ()
	{        
		if (ShouldFire ()) {
			if (CanFire ()) {
				Fire ();
			} else {
				//TODO play empty audio for effect
			}
		}
	}

	protected virtual void FixedUpdate ()
	{
		if (unlimitedAmmo || roundsLeft > 0) {
			//Firing delay
			if (firingDelayTicks > 0) {
				firingDelayTicks -= Time.deltaTime;
			}	
		} else if (reloadDelayTicks <= 0) {
			roundsLeft = ammoPerReload;
		} else {
			reloadDelayTicks -= Time.deltaTime;
		}
	}

	public virtual bool CanFire ()
	{
		return (unlimitedAmmo || roundsLeft > 0) && firingDelayTicks <= 0;
	}
	
	public abstract bool ShouldFire ();
	
	protected virtual GameObject Fire ()
	{
		if (!unlimitedAmmo && ammoPerReload <= 1) {
			roundsLeft--;
			if (roundsLeft <= 0) {
				reloadDelayTicks = reloadSpeedSeconds;
			}
		}

		//TODO play firing audio
		firingDelayTicks = firingDelaySeconds;
		GameObject bullet = Instantiate (bulletPrefab, firingNode.transform.position, Quaternion.identity) as GameObject;
		bullet.transform.eulerAngles = new Vector3 (firingNode.transform.eulerAngles.x + rollRandomError (), firingNode.transform.eulerAngles.y + rollRandomError (), firingNode.transform.eulerAngles.z + rollRandomError ());
		bullet.GetComponent<Rigidbody> ().AddRelativeForce (0f, 0f, bulletForce);
		bullet.GetComponent<Bullet> ().shooter = shooter;
		return bullet;
	}

	float rollRandomError ()
	{
		return Random.Range (-errorAmount, errorAmount);
	}
}
