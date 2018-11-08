using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailEdge : MonoBehaviour
{
    private Transform respawn;

	// Use this for initialization
	void Start ()
    {
        respawn = transform.FindChild("respawn");
        //print(respawn.GetInstanceID());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent.position = respawn.position;
        //print("collided with " + collision);
    }

}
