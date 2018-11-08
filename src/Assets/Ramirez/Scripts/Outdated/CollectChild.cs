using UnityEngine;
using System.Collections;

public class CollectChild : MonoBehaviour {

	public GameObject item;

	public Inventory backpack;

	public GameObject Mouth;

	public GameObject Back;

	public GameObject Frogger; 

	public float MouthPosition = 0.25f;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void OnTriggerStay2D(Collider2D entity)
	{
		/*if (backpack.Tadpole == false);*/ //Currently this is not working
		{
			if (Input.GetKeyDown (KeyCode.M)) //Currently this is working inconsistently
			{
				print("The Player has put the tadpole in his mouth.");
				//item.SetActive(false);

				if (item.tag == "Tadpole")
				{
					backpack.Tadpole = true;
					//Mouth.SetActive(true);
					gameObject.transform.parent = Frogger.transform;
					gameObject.transform.localPosition = new Vector3 (0, MouthPosition, 0);
				}
			}

			if (Input.GetKeyDown (KeyCode.Space))
			{
				gameObject.transform.parent = null;
			}

			if (Input.GetKeyDown (KeyCode.B))
			{
				print ("The Player has put the tadpole on his back.");
				//item.SetActive (false);

				if (item.tag == "Tadpole")
				{
					backpack.Tadpole = true;
					//Back.SetActive(true);
					gameObject.transform.parent = Frogger.transform;
				}
			}
		}
	}
}