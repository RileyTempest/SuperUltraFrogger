//--------------------------------
// Author: Tyler Roop
// Date: 2/11/2017
// Credit:
//
// Purpose: The purpose of this script is to handle pause menu functionality.
//--------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    //public GameObject pauseMenu;

	// Use this for initialization
	void Start () {

        //pauseMenu.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

   
}
