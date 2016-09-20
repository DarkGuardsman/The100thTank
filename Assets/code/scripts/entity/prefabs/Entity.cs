using UnityEngine;
using System.Collections;
using BuiltBroken.Damage;
using BuiltBroken.Team;
/// <summary>
/// Basic gameobject that can take damage
/// </summary>
public class Entity : MonoBehaviour, IEntity
{
	public float hp = 1;
	public float max_hp = 1;
	public int deathTicks = -10;
	public bool alive = true;

	public TEAM team = TEAM.OTHER;
	protected TeamManager teamManager;

	protected virtual void Awake ()
	{
		teamManager = GameObject.FindWithTag ("World").GetComponent<TeamManager> ();
		switch (team) {
		case TEAM.BLUE:
			SetPrimaryBodyColor (Color.blue);
			SetSecondaryBodyColor (new Color (28, 57, 90));
			break;
		case TEAM.RED:
			SetPrimaryBodyColor (Color.red);
			SetSecondaryBodyColor (Color.black);
			break;
		}
	}

	protected virtual void Start ()
	{
		
	}

	protected virtual void SetPrimaryBodyColor (Color color)
	{

	}

	protected virtual void SetSecondaryBodyColor (Color color)
	{
		
	}

	// Update is called once per frame
	protected virtual void Update ()
	{
		if (hp <= 0 && alive) {
			alive = false;
			OnDeath ();
		}

		if (isDead ()) {
			if (deathTicks != -10) {
				deathTicks--;
				if (deathTicks <= 0) {
					BeforeDestroyed ();
					Destroy (gameObject);
				}
			}
		}
	}

	public virtual float getHeath ()
	{
		return hp;
	}

	public virtual float getMaxHeath ()
	{
		return max_hp;
	}

	public virtual void setHeath (float amount)
	{
		hp = amount;
	}

	public virtual bool damageEntity (DamageSource source, float damage)
	{
		if (alive) {
			setHeath (getHeath () - damage);
			return true;
		}
		return false;
	}

	public virtual bool isDead ()
	{
		return !alive;
	}

	public TEAM getTeam ()
	{
		return team;
	}

	public virtual bool IsValidTarget (GameObject obj)
	{
		if (IsEntity (obj)) {
			IEntity ent = obj.GetComponent<IEntity> ();
			return ent != null && !ent.isDead () && ent.getTeam () != team && ent.getTeam () != TEAM.OTHER;
		}
		return false;
	}

	/// <summary>
	/// Called the first tick after the entity has died
	/// </summary>
	protected virtual void OnDeath ()
	{
		if (gameObject.tag == "Player") {
			//Disable camera
			Camera.main.enabled = false;
			Destroy (gameObject);			
			//Create new camera object for the player
			Instantiate (Resources.Load ("player/camera_dummy"), transform.position, Quaternion.identity);
		}
	}

	//Called by unity before the script is destoryed
	protected virtual void OnDestroy ()
	{
		//print ("Script was destroyed");
	}

	/// <summary>
	/// Called in the death update loop right before the game object is destoryed
	/// </summary>
	protected virtual void BeforeDestroyed ()
	{

	}

	/// <summary>
	/// Attacks the game object threw all of it's IEntity scripts
	/// </summary>
	/// <param name="target">Game object to look for an IEntity script on</param>
	/// <param name="source">Type of damage and it's source object</param>
	/// <param name="damage">amount of damage to doDamage.</param>
	protected virtual bool AttackGameObjectOnly (GameObject target, DamageSource source, float damage)
	{
		return target.GetComponent<IEntity> ().damageEntity (source, damage);
	}

	public static bool IsEntity (GameObject obj)
	{
		return obj.tag == "Entity" || obj.tag == "Player";
	}

	/// <summary>
	/// Iterates threw object & it's parent objects to find an Entity tag. Once it finds
	/// a matching object it attacks the object if it has an IEntity script.
	/// </summary>
	/// <param name="target">Game object to look for an IEntity script on</param>
	/// <param name="source">Type of damage and it's source object</param>
	/// <param name="damage">amount of damage to doDamage.</param>
	protected virtual bool AttackGameObject (GameObject target, DamageSource source, float damage)
	{
		GameObject parent = target;
		while (parent != null) {

			if (IsEntity (parent) && AttackGameObjectOnly (parent, source, damage)) {
                Debug.Log("Attacked Entity: " + parent);
				return true;
			}
			if (parent.transform == null || parent.transform.parent == null) {
				return false;
			}
			parent = parent.transform.parent.gameObject;
		}
		return false;
	}
}
