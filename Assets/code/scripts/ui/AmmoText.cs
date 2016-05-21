using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoText : MonoBehaviour
{
	public Cannon cannon;
	
	// Update is called once per frame
	void Update ()
	{
		if (cannon != null) {
			gameObject.GetComponent<Text> ().text = "Ammo: " + cannon.roundsLeft + " / " + cannon.ammoPerReload;
		} else {
			gameObject.GetComponent<Text> ().text = "Ammo: ----/----";
		}
	}
}
