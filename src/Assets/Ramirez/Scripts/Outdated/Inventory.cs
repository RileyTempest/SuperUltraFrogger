using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public bool Tadpole;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		Tadpole = false;
	}
}
