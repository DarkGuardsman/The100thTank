using UnityEngine;
using System.Collections;

public class grid_creator : MonoBehaviour {

	public GameObject prefab;
	public int x = 20;
	public int y = 20;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				GameObject newObject = Instantiate(prefab, new Vector3(i - (x / 2), 0, j - (y / 2)), Quaternion.identity) as GameObject;
				newObject.transform.parent = gameObject.transform;
				//Even and zero I values
				if(i == 0 || i % 2 == 0)
				{
					if(j == 0 || j % 2 == 0)
					{
						((GameObject)newObject).GetComponent<Renderer>().material.color = Color.white;
					}
					else
					{
						((GameObject)newObject).GetComponent<Renderer>().material.color = Color.black;
					}
				}
				// Odd i values
				else
				{
					if(j == 0 || j % 2 == 0)
					{
						((GameObject)newObject).GetComponent<Renderer>().material.color = Color.black;
					}
					else
					{
						((GameObject)newObject).GetComponent<Renderer>().material.color = Color.white;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
