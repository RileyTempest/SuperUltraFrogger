using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTadpole : MonoBehaviour {

	public Inventory backpack;

	public GameObject Mouth;

	public GameObject Back;

	public GameObject Tadpole;

	// Use this for initialization
	void Start () {
		
	}
	
	void TakeDamage()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			backpack.Tadpole = false;
			Mouth.SetActive (false);
			Back.SetActive (false);
			Instantiate (Mouth, transform.position, transform.rotation);
		}
	}
}
