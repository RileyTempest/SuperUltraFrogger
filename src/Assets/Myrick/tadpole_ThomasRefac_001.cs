using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tadpole_ThomasRefac_001 : MonoBehaviour
{
    GameObject Player;
    PlayerRaycastCollision_Thomas_001 PlayerCollision;
    BoardManager_Thomas_001 GameBoard;
    SpriteRenderer SpawnSprite;
    SpriteRenderer PickupSprite;

    [SerializeField]
    Transform tadpoleSpawnPoint;

    private bool pickupAcquiredFromHome = false;
    private bool pickupDropped = false;

    private Transform mouth;
    private Transform back;
    private int toggleMouthBack = 0; //3 position, single throw switch. +/- mouth/back. 0 is both off.


	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerCollision = Player.GetComponent<PlayerRaycastCollision_Thomas_001>();
        GameBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager_Thomas_001>();

        SpawnSprite = tadpoleSpawnPoint.gameObject.GetComponent<SpriteRenderer>();
        PickupSprite = transform.GetComponent<SpriteRenderer>();

        PickupSprite.enabled = false;
        SpawnSprite.enabled = true;

        mouth = Player.transform.FindChild("mouth");
        back = Player.transform.FindChild("back");

        transform.position = new Vector3(Mathf.Floor(transform.root.position.x), Mathf.Floor(transform.root.position.y),3.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {   
        CheckSpawnerAcquisition();
        SpawnerSwitch();
        CheckRescue();
        PickupReveal();
        TransformSnapToGrid();
        CheckGoal();

        if(toggleMouthBack > 0)
        {
            mouth.gameObject.SetActive(true);
            back.gameObject.SetActive(false);
        }
        else if (toggleMouthBack < 0)
        {
            mouth.gameObject.SetActive(false);
            back.gameObject.SetActive(true);
        }
        else
        {
            mouth.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
        }
    }

    private void TransformSnapToGrid()
    {
        transform.position = new Vector3(Mathf.FloorToInt(transform.position.x),
            Mathf.FloorToInt(transform.position.y),
            3.5f); //hard coded z for proper raycast collision
    }

    private void CheckSpawnerAcquisition()
    {
        if (!pickupAcquiredFromHome)
        {
            if (PlayerCollision.HitHazardTransform.tag == "Tadpole" 
                && PlayerCollision.HitRowTransform.tag == "Home"
                && Input.GetKeyDown(KeyCode.M))
            {
                pickupAcquiredFromHome = true;
                pickupDropped = false;
                toggleMouthBack = 1;
            }

            if (PlayerCollision.HitHazardTransform.tag == "Tadpole" 
                && PlayerCollision.HitRowTransform.tag == "Home"
                && Input.GetKeyDown(KeyCode.B))
            {
                pickupAcquiredFromHome = true;
                pickupDropped = false;
                toggleMouthBack = -1;
            } 
        }
    }
    private void SpawnerSwitch()
    {
        if (GameBoard.RowMetrics("currentSection") == 0 
            && !pickupAcquiredFromHome 
            && !GameBoard.IsFlipped)
        {
            SpawnSprite.enabled = true;
        }
        if (GameBoard.RowMetrics("currentSection") != 0 || pickupAcquiredFromHome)
        {
            SpawnSprite.enabled = false;
        }
    }
    private void CheckRescue()
    {
        if (PlayerCollision.HitHazardTransform.tag == "Tadpole"
            && pickupDropped
            && Input.GetKeyDown(KeyCode.M))
        {
            pickupDropped = false;
            toggleMouthBack = 1;
        }
        else if (PlayerCollision.HitHazardTransform.tag == "Tadpole"
            && pickupDropped
            && Input.GetKeyDown(KeyCode.B))
        {
            pickupDropped = false;
            toggleMouthBack = -1;
        }

        if (PlayerCollision.HitHazardTransform.tag == "car" && pickupAcquiredFromHome)
        {
            pickupDropped = true;
            toggleMouthBack = 0;
        }
    }
    private void PickupReveal()
    {
        if (pickupDropped)                                  //dropped, stop carrying pickup
        {
            PickupSprite.enabled = true;
            TransformSnapToGrid();

        }
        else if (!pickupDropped && pickupAcquiredFromHome)  //carry pickup
        {
            PickupSprite.enabled = false;
            transform.position = Player.transform.position;

        }
        else                                                //At spawn. 
        {
            PickupSprite.enabled = false;
            transform.position = transform.root.position;
        }
    }
    private void CheckGoal()
    {
        if (PlayerCollision.HitRowTransform.tag == "Goal" 
            && pickupAcquiredFromHome 
            && !pickupDropped
            && PlayerCollision.HitHazardTransform.tag == "Tadpole")
        {
            print("a froggy was brought to the goal");
            ResetTadpole("reset");
        }
    }

    //For other code to access these bools and tadpole position
    public void ResetTadpole(string command)
    {
        if(command == "reset")
        {
            pickupAcquiredFromHome = false;
            pickupDropped = false;
            toggleMouthBack = 0;
            PickupSprite.enabled = false;
            SpawnSprite.enabled = false;
        }
    }
}
