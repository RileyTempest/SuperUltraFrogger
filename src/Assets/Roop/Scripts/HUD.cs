//---------------------------------------------------------------------------
// Authors: Tyler Roop
// Date: 2/5/17
// Credit: Michael 8, Rutter: Countdown Timer - Unity Forums: http://answers.unity3d.com/questions/225213/c-countdown-timer.html
// Credit: Full Sail University - PGF2 Experiment 1: Life Icons: https://www.dropbox.com/s/jyg1aaa28fgovg8/PGF2%2001%20Experiment%20-%20Game%20Framework.pdf?dl=0
// Credit: fapawel - Countdown timer score: http://answers.unity3d.com/questions/1037375/how-to-get-remaining-time-in-a-count-down-timer-as.html
// Credit: Statement - Hi-Score Player Prefs: Unity Forum - http://answers.unity3d.com/questions/1086388/displaying-high-score.html
//
// Purpose: Purpose of this script is to update the HUD with changes occuring in game. 
//
//-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public float timeLeft = 0.0f;

    //private float timeLimit = 0;

    [SerializeField]
    Text timerText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text hiScoreText;

    public LifeIcon lifeIconObj;

    private List<LifeIcon> lifeIconInstances;

    //[SerializeField] Movement playerController;

    //----------------------------------------------------------------------------------------------
    

	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {

        UpdateTimer();
		
	}

    //Timer Functions 
    //---------------------------------------------------------------
    void UpdateTimer()
    {
        timeLeft += Time.deltaTime;

        timerText.text = "Time: " + Mathf.Round(timeLeft);

        //if(timeLeft <= timeLimit)
        //{
        //    timeLeft = 0;

        //    //Preliminary ModLive Code Implementation
        //    //playerController.ModLives(-1);

        //    Debug.Log("You ran out of time, you have lost a life!");
        //}
    }

    public float GetTimer()
    {
        return timeLeft;
    }


    //--------------------------------------------------------------------

    //Life Coding
    //-----------------------------------------------------------------

    public void CreateLifeIcons (int numLives)
    {
        if(lifeIconInstances == null)
        {
            lifeIconInstances = new List<LifeIcon>();

            lifeIconInstances.Add(lifeIconObj);

            Vector3 positionOffset = new Vector3(1.0f, 0, 0);

            for(int i = 1; i <numLives; ++i)
            {
                LifeIcon newlifeIcon;

                newlifeIcon = Instantiate(lifeIconObj, lifeIconObj.transform.position, lifeIconObj.transform.rotation) as LifeIcon;

                Vector3 newPosition = newlifeIcon.transform.position;

                newPosition.x += (positionOffset.x * i);

                newlifeIcon.transform.position = newPosition;

                newlifeIcon.transform.localScale = new Vector3(1, 1, 1);

                lifeIconInstances.Add(newlifeIcon);
            }
        }
    }

    public void updateLives( int numLivesRemaining)
    {
        for(int i = 0; i < lifeIconInstances.Count; ++i)
        {
            bool bActive = i < numLivesRemaining;

            lifeIconInstances[i].gameObject.SetActive(bActive);
        }
    }

    //---------------------------------------------------------------------------------

    //Score Information - 
    //----------------------------------------------------------------------

    public void updateScore(int value)
    {
        scoreText.text = "Score: " + value.ToString();
    }

    public void updateTimerScore(int value)
    {
        scoreText.text = "Score: " + Mathf.Round(value + timeLeft);
    }

    public void updateHiScore(int value)
    {
        hiScoreText.text = "Hi-Score: " + value.ToString();
    }
}
