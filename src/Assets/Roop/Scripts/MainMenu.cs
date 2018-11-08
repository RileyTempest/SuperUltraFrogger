//-------------------------------------------------------------------
//Author: Tyler Roop
//Date: 2/11/2017
//Credit:
//Purpose: Purpose of this script is to handle all functionality of the main menu and sub menu's present within it. 
//--------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;

    public GameObject howToPlay;

    public GameObject howToPlayPT2;

	// Use this for initialization
	void Start () {

        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        howToPlayPT2.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void Next()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(false);
        howToPlayPT2.SetActive(true);
    }

    public void BackButton()
    {
        howToPlayPT2.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
    }


}
