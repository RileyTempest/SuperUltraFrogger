/*
Adam Pitt
Controls the movement of frogger
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    Vector3 pos;
    //Transform tran;
    public GameObject Frogger;

	public HUD playerHud;

	private int Score;

    [SerializeField]
    int maxLives = 3;

    private int curLives;

	[SerializeField] Text restartText;

    private bool isDisabled;

	// Use this for initialization
    void Start()
    {
		isDisabled = false;
        ResetStats();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        ResetPlayer();
    }

    void UpdatePosition()
    {
		if (!isDisabled) {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				if (transform.position.y >= 17) {
					return;
				} else {
					transform.Translate (Vector3.up * speed);
					ModScore (10);
				}
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				if (transform.position.y <= 6) {
					return;
				} else {
					transform.Translate (Vector3.down * speed);
					ModScore (-10);
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (transform.position.x == -6.5) {
					return;
				} else {
					transform.Translate (Vector3.left * speed);
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (transform.position.x == 6.5) {
					return;
				} else {
					transform.Translate (Vector3.right * speed);
				}
			}
		}
    }

	

    //Stats Section From Tyler and Ramirez
    //---------------------------------------

    void ResetStats()
    {
        Score = 0;

        curLives = maxLives;

        playerHud.CreateLifeIcons(maxLives);

        playerHud.updateLives(curLives);
    }

    public void ModScore(int _value)
    {
        Score += _value;

        playerHud.updateScore(Score);
    }

    public void MultiplyScore(int _value)
    {
        Score += _value;

        playerHud.updateTimerScore(Score);
    }

    public void ModLives(int _value)
    {
        curLives += _value;

        playerHud.updateLives(curLives);
    }

    public void ResetPlayer()
    {
        if(playerHud.timeLeft == 0)
        {
            ModLives(-1);

            playerHud.timeLeft = 60.0f;

            transform.position = pos;
        }
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		if (curLives <= 0) {
			isDisabled = true;
			restartText.text = "You ran out of lives. Press 'R' to restart!";
			playerHud.timeLeft = 0.0f;
		}
    }

}
