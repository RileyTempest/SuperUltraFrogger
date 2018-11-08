using UnityEngine;
using System.Collections;

public class CollectPointRow : MonoBehaviour {

	public GameObject item;

	//Extra code for if we need to reference back to this object at a later point
	/*public Inventory backpack;*/

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D entity)
	{
		if (entity.tag == "Player" && item.activeInHierarchy == true)
		{
			print("The Player has entered the trigger.");
			item.SetActive(false);

			//Extra code for if we need to reference back to this object at a later point
			/*if (item.name == "PointRow")
			{
				backpack.PointRow = true;
			} */
		}
	}
}
