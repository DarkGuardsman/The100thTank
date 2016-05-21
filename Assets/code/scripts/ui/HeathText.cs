using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeathText : MonoBehaviour
{
	private IEntity entity;

	// Use this for initialization
	void Awake ()
	{
		entity = GameObject.FindWithTag ("Player").GetComponent<IEntity> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (entity != null) {
			gameObject.GetComponent<Text> ().text = "Hp: " + entity.getHeath () + " / " + entity.getMaxHeath ();
		} else {
			gameObject.GetComponent<Text> ().text = "Hp: ----";
			entity = GameObject.FindWithTag ("Player").GetComponent<IEntity> ();
		}
	}
}
