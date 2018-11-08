using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTadpoleRay : MonoBehaviour
{

	PlayerRaycastCollision_Thomas_001 collision;
    BoardManager_Thomas_001 board;

	public GameObject item;

	public Inventory backpack;

	public GameObject Mouth;

	public GameObject Back;

	public GameObject Frogger;

	private Vector3 Pos;

    private bool isDropped = false;
    private bool inMouth;
    private bool onBack;


	// Use this for initialization
	void Start()
	{
		collision = GetComponent<PlayerRaycastCollision_Thomas_001>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager_Thomas_001>();
		Pos = item.transform.position;
        item.SetActive(true);
	}

	void Update()
	{

		CollectTadpole();

        if (board.RowMetrics("currentSection") != 0 && !isDropped)
        {
            //print("entered method to turn off tadpole spawn " + "currentSectio = " + board.RowMetrics("currentSection"));
            item.SetActive(false);
        }

        if(board.RowMetrics("currentSection") >= 1.0f && isDropped)
        {
            print("Tadpole On " + "currentSection = " + board.RowMetrics("currentSection"));
            item.SetActive(true);
        }
    }

	private void CollectTadpole()
	{
        if(board.RowMetrics("currentSection") == 0.0f && !board.IsFlipped)
        {
            if (collision.HitHazardTransform.tag == "Tadpole" && Input.GetKeyDown(KeyCode.M) && backpack.Tadpole == false)
            {
                item.SetActive (false);
                inMouth = true;
                backpack.Tadpole = true;
                Mouth.SetActive(true);
                item.transform.parent = Frogger.transform; //disable. call the Frogger position on drop only. 
                
            }

            if (collision.HitHazardTransform.tag == "Tadpole" && Input.GetKeyDown(KeyCode.B) && backpack.Tadpole == false)
            {
                item.SetActive(false);
                onBack = true;
                backpack.Tadpole = true;
                Back.SetActive(true);
                item.transform.parent = Frogger.transform;
            }

            if (collision.HitHazardTransform.tag == "car")
            {
                if (inMouth)
                {
                    isDropped = true;
                    item.SetActive(true);
                    inMouth = false;
                    ChildSnapToGrid();
                    Mouth.SetActive(false);
                    backpack.Tadpole = false;
                    item.transform.parent = null;

                }

                if (onBack)
                {
                    isDropped = true;
                    item.SetActive(true);
                    onBack = false;
                    ChildSnapToGrid();
                    Back.SetActive(false);
                    backpack.Tadpole = false;
                    item.transform.parent = null;
                    item.transform.position = Pos;
                    
                }
            }
        }


        if (collision.HitHazardTransform.tag == "Goal")
        {
            //item.SetActive(true);
            Back.SetActive(false);
            Mouth.SetActive(false);
            item.transform.parent = null;
            item.transform.position = Pos;
        }
    }

    private void ChildSnapToGrid()
    {
        if (collision.HitRowTransform.tag == "safe" || collision.HitRowTransform.tag == "water")
        {
            item.transform.position = new Vector3((Mathf.Floor(item.transform.position.x)), item.transform.position.y, item.transform.position.z);
        }
    }
}