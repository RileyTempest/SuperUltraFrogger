/*
Adam Pitt
Controls the movement of frogger
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//TODO:
/*
 * prevent diagonal movement in a single frame. I suggest gating all input until all keys are up. -Thomas
 * Screen clamp needs to be able to push Frogger off a log. Previous solution let frogger ride of screen. -Thomas
 * 
 */

public class Movement_Thomas : MonoBehaviour
{
    PlayerRaycastCollision collision;

    public float speed = 1.0f;
    Vector3 pos;

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

    [SerializeField] CollectTadpoleRay collect;

    private bool isDisabled;

    private bool isLocked;

    // Use this for initialization
    void Start()
    {
        collision = GetComponent<PlayerRaycastCollision>();
        isDisabled = false;
        isLocked = false;
        ResetStats();
        pos = transform.position;//Just use transform.position instead of the variable? -Thomas
        highscore = PlayerPrefs.GetInt("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        CheckGoal();
        CheckHazardKill();
        CarryPlayer(); //WARNING: Carryplayer must call before player input else will get stuck!
        UpdatePosition();
        ResetPlayer();
        UpdateHighScore();
    }

    //Maths looks ugly but it works. Math.Floor() needs to be called immediate after each input.
    void UpdatePosition()
    {
        if (!isDisabled)
        {
            if (!isLocked)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    transform.Translate(Vector3.up * speed);
                    //transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);

                    
                    if (collision.tag == "safe")
                    {
                        transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);
                    }
                    CheckPlayerBounds();
                    isLocked = true;
                    return;   

                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {

                    transform.Translate(Vector3.down * speed);
                    //transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);

                    
                    if (collision.tag == "safe")
                    {
                        transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);
                    }
                    CheckPlayerBounds();
                    isLocked = true;
                    return;

                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    transform.Translate(Vector3.left * speed);
                    //transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);
                    if(collision.tag == "safe")
                    {
                        transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);
                    }
                    CheckPlayerBounds();
                    isLocked = true;
                    return;

                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                    transform.Translate(Vector3.right * speed);
                    //transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);
                    if (collision.tag == "safe")
                    {
                        transform.position = new Vector3((Mathf.Floor(transform.position.x)), transform.position.y, transform.position.z);
                    }
                    CheckPlayerBounds();
                    isLocked = true;
                    return;

                }
            }
        }

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

    //Collision Gets from Thomas
    //----------------------------------------
    private void CarryPlayer()
    {
        if (collision.HitHazardTransform.tag == "log")
        {
            transform.position = new Vector3(
                collision.HitHazardTransform.position.x,
                collision.HitHazardTransform.position.y,
                transform.position.z);
        }
        else if (collision.HitHazardTransform.tag == "turtle")
        {
            transform.position = new Vector3(
                collision.HitHazardTransform.position.x,
                collision.HitHazardTransform.position.y,
                transform.position.z);
        }
        CheckPlayerBounds();
    }
    private void CheckHazardKill()
    {
        if (collision.HitHazardTransform.tag == "water")
        {
            ModLives(-1);
            collect.item.transform.parent = null;
            // playerHud.timeLeft = 60.0f;
            transform.position = pos;
			// WipeScore();
        }

        if (collision.HitHazardTransform.tag == "car")
        {
            ModLives(-1);
            collect.item.transform.parent = null;
           // playerHud.timeLeft = 60.0f;
			transform.position = pos;
            //WipeScore();
        }
    }
    private void CheckGoal()
    {
        if (collision.HitHazardTransform.tag == "Goal")
        {
            ModScore(10);
            //playerHud.timeLeft = 60.0f;
            transform.position = pos;
            print("Player has reached the goal and has received points");
        }
    }
    private void CheckPlayerBounds()
    {
        float oldPOSx = transform.position.x;
        float oldPOSy = transform.position.y;
        if (oldPOSx < -7.0f)
        {
            transform.position = new Vector3(-7.0f, transform.position.y, transform.position.z);
        }
        else if (oldPOSx > 6.0f)
        {
            transform.position = new Vector3(6.0f, transform.position.y, transform.position.z);
        }

        if (oldPOSy < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
        else if (oldPOSy > 12.0f)
        {
            transform.position = new Vector3(transform.position.x, 12.0f, transform.position.z);
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

    public void WipeScore()
    {
        Score = 0;

        playerHud.updateScore(Score);
    }

    public void UpdateHighScore()
    {
        if(Score > highscore)
        {
            highscore = Score;
            PlayerPrefs.SetInt("highscore", highscore);
            playerHud.updateHiScore(highscore);
        }

        playerHud.updateHiScore(highscore);
    }

    public void ResetPlayer()
    {
        //if (playerHud.timeLeft == 0)
        //{
        //    ModLives(-1);

        //    playerHud.timeLeft = 60.0f;

        //    transform.position = pos;
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        if (curLives <= 0)
        {
            isDisabled = true;
            pauseMenu.SetActive(true);
            //restartText.text = "You ran out of lives. Press 'R' to restart!";
            //playerHud.timeLeft = 0.0f;
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
