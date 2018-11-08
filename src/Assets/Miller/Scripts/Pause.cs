using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	private bool isPaused;
	private Movement_Thomas_001 movementScript;
    public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		isPaused = false;
		movementScript = GetComponent<Movement_Thomas_001> ();
        pauseMenu.SetActive(false);
       
	}
	
	// Update is called once per frame
	void Update () {
		pauseCheck();
	}

	void pauseCheck() {
		if (Input.GetKeyDown(KeyCode.P)) {
			isPaused = !isPaused;
            
			if (isPaused) {
				Time.timeScale = 0;
				movementScript.enabled = false;
                pauseMenu.SetActive(true);
			} else if (!isPaused) {
				Time.timeScale = 1;
				movementScript.enabled = true;
                pauseMenu.SetActive(false);
			}
		}
	}

    public bool Paused
    {
        get
        {
            return isPaused;
        }
       
    }
}
