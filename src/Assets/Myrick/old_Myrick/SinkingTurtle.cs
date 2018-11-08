using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingTurtle : MonoBehaviour
{
    private bool isSunk = false;
    [SerializeField]
    private float sinkTimer = 5.0f;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
        gameObject.tag = "turtle";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(sinkTimer <= 0.0f)
        {
            sinkTimer = 5.0f;
            isSunk = !isSunk;
        }
        else
        {
            sinkTimer = sinkTimer - Time.deltaTime;
        }

        if (isSunk)
        {
            spriteRenderer.color = Color.white;
            gameObject.tag = "water";
        }
        else
        {
            spriteRenderer.color = Color.yellow;
            gameObject.tag = "turtle";
        }
    }
}
