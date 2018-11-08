using UnityEngine;
using System.Collections;

public class ResetPointRows : MonoBehaviour {

	public GameObject Row1;
	public GameObject Row2;
	public GameObject Row3;
	public GameObject Row4;
	public GameObject Row5;
	public GameObject Row6;
	public GameObject Row7;
	public GameObject Row8;
	public GameObject Row9;

		//Extra code for if we need to reference back to this object at a later point
	/*public Inventory backpack;*/

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D entity)
	{
		if (entity.tag == "Player")
		{
			print("The Player has entered the trigger.");
			Row1.SetActive(true);
			Row2.SetActive(true);
			Row3.SetActive(true);
			Row4.SetActive(true);
			Row5.SetActive(true);
			Row6.SetActive(true);
			Row7.SetActive(true);
			Row8.SetActive(true);
			Row9.SetActive(true);

			//Extra code for if we need to reference back to this object at a later point
			/*if (item.name == "HomeRow")
			{
				backpack.HomeRow = true;
			} */
		}
	}
}
