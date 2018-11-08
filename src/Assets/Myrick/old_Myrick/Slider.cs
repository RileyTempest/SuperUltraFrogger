using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    private Time time;

    [SerializeField]
    private float speedFactor = 1.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float translation = Time.deltaTime * speedFactor;
        transform.Translate(-translation, 0, 0);
    }
}
