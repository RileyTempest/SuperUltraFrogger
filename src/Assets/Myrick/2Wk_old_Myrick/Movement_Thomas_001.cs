/*
Adam Pitt
Controls the movement of frogger
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement_Thomas_001 : MonoBehaviour
{
    PlayerRaycastCollision_Thomas_001 PlayerCollision;
    BoardManager_Thomas_001 GameBoard;

    //[SerializeField]
    //CollectTadpoleRay collect;

    public HUD playerHud;
    private static int Score;
    static int highscore;

    [SerializeField]
    int maxLives = 3;
    private int curLives;

    [SerializeField]
    Text restartText;
    [SerializeField]
    GameObject pauseMenu;

    //Vector3 pos;

    private int curFrogs = 0;

    private int maxFrogs = 2;

    private bool isDisabled = false;
    private bool isLocked = false;

    // Use this for initialization
    void Start()
    {
        //pos = transform.position;
        PlayerCollision = GetComponent<PlayerRaycastCollision_Thomas_001>();
        GameBoard = GameObject.Find("BoardManager").GetComponent<BoardManager_Thomas_001>();
        ResetStats();
        highscore = PlayerPrefs.GetInt("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        CheckGoal();
        CheckHazardRay();
        CarryPlayer(); //WARNING: Carryplayer must called before player input else will get stuck!
        PlayerMovement();
        ResetPlayer();
        UpdateHighScore();
    }

    private void PlayerMovement()
    {
        if (!isDisabled)
        {
            if (!isLocked)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up);
                    PlayerSnapToGrid();
                    CheckPlayerBounds();
                    isLocked = true;
                    return;   
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down);
                    PlayerSnapToGrid();
                    CheckPlayerBounds();
                    isLocked = true;
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.left);
                    PlayerSnapToGrid();
                    CheckPlayerBounds();
                    isLocked = true;
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right);
                    PlayerSnapToGrid();
                    CheckPlayerBounds();
                    isLocked = true;
                    return;
                }
            }
        }

        if (isLocked)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                isLocked = false;
            }

            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isLocked = false;
            }

            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                isLocked = false;
            }

            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isLocked = false;
            } 
        }
    }

    private void CarryPlayer()
    {
        if (PlayerCollision.HitHazardTransform.tag == "log")
        {
            transform.position = new Vector3(
                PlayerCollision.HitHazardTransform.position.x,
                PlayerCollision.HitHazardTransform.position.y,
                transform.position.z);
        }
        else if (PlayerCollision.HitHazardTransform.tag == "turtle")
        {
            transform.position = new Vector3(
                PlayerCollision.HitHazardTransform.position.x,
                PlayerCollision.HitHazardTransform.position.y,
                transform.position.z);
        }
        CheckPlayerBounds();
    }
    private void CheckHazardRay()   //water hazard is not used anymore
    {
        if (PlayerCollision.HitHazardTransform.tag == "car")
        {
            transform.position = new Vector3(0.0f, 0.0f, 1.0f); //Player spawn point
        }
        else if (PlayerCollision.HitHazardTransform.tag == "reef") 
        {
            // implement collision here
        }
    }
    private void CheckGoal()        //other scripts check the goal. consolidate.
    {
        if (PlayerCollision.HitHazardTransform.tag == "Goal")
        {  
            //if(collect.backpack.Tadpole == true)
            //{
            //    ModScore(500);
            //    curFrogs++;
            //    collect.backpack.Tadpole = false;
            //} 

            if(curFrogs == maxFrogs)
            {
                SceneManager.LoadScene(2);
            }
        }
    }       
    private void CheckPlayerBounds()
    {
        float oldPOSx = transform.position.x;
        float oldPOSy = transform.position.y;
        if (oldPOSx < -(GameBoard.RowMetrics("width")/2)) //negative
        {
            transform.position = new Vector3(-(GameBoard.RowMetrics("width")/2), transform.position.y, transform.position.z);
        }
        else if (oldPOSx > GameBoard.RowMetrics("width")/2)
        {
            transform.position = new Vector3(GameBoard.RowMetrics("width")/2, transform.position.y, transform.position.z);
        }

        if (oldPOSy < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
        else if (oldPOSy > (GameBoard.RowMetrics("count") - 1.0f))
        {
            transform.position = new Vector3(transform.position.x, (GameBoard.RowMetrics("count") - 1.0f), transform.position.z);
        }
    }
    private void PlayerSnapToGrid()
    {
        if (PlayerCollision.HitRowTransform.tag != "water")
        {
            transform.position = new Vector3((Mathf.CeilToInt(transform.position.x)), (Mathf.CeilToInt(transform.position.y)), transform.position.z);
        }
    }

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

    public void WipeScore()
    {
        Score = 0;

        playerHud.updateScore(Score);
    }

    public void UpdateHighScore()
    {
        if (Score > highscore)
        {
            highscore = Score;
            PlayerPrefs.SetInt("highscore", highscore);
            playerHud.updateHiScore(highscore);
        }

        playerHud.updateHiScore(highscore);
    }

    public void ResetPlayer()
    {
        if(curLives <= 0)
        {
            isDisabled = true;
            pauseMenu.SetActive(true);
        }
    }

    public bool Disabled
    {
        get
        {
            return isDisabled;
        }
    }

}
