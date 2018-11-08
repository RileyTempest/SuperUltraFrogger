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

    private bool inMouth;
    private bool onBack;
    private bool isDropped = false;


    // Use this for initialization
    void Start()
    {
        collision = GetComponent<PlayerRaycastCollision_Thomas_001>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager_Thomas_001>();
    }

    void Update()
    {
        CollectTadpole();

        if (board.RowMetrics("currentSection") != 0 && !isDropped)
        {
            item.SetActive(false);
        }

        if (board.RowMetrics("currentSection") != 0 && isDropped)
        {
            item.SetActive(true);
            ChildSnapToGrid();
        }
    }

    private void CollectTadpole()
    {
       if (collision.HitHazardTransform.tag == "Tadpole" && Input.GetKeyDown(KeyCode.M) && backpack.Tadpole == false)
        {
            isDropped = false;
            item.SetActive(false);
            inMouth = true;
            backpack.Tadpole = true;
            Mouth.SetActive(true);
            item.transform.parent = Frogger.transform;

        }

            if (collision.HitHazardTransform.tag == "Tadpole" && Input.GetKeyDown(KeyCode.B) && backpack.Tadpole == false)
        {
            isDropped = false;
            item.SetActive(false);
            onBack = true;
            backpack.Tadpole = true;
            Back.SetActive(true);
            item.transform.parent = Frogger.transform;
            ChildSnapToGrid();
        }

        if (collision.HitHazardTransform.tag == "car")
        {
            if (inMouth)
            {
                item.SetActive(true);
                inMouth = false;
                Mouth.SetActive(false);
                backpack.Tadpole = false;
                item.transform.parent = null;
                isDropped = true;
                ChildSnapToGrid();
            }

            if (onBack)
            {
                item.SetActive(true);
                onBack = false;
                Back.SetActive(false);
                backpack.Tadpole = false;
                item.transform.parent = null;
                isDropped = true;
                ChildSnapToGrid();
            }
        }

        if (collision.HitHazardTransform.tag == "Goal")
        {
            Back.SetActive(false);
            Mouth.SetActive(false);
            item.transform.parent = null;
        }
    }


    private void ChildSnapToGrid()
    {
        if (collision.HitRowTransform.tag == "safe" || collision.tag == "water")
        {
            item.transform.position = new Vector3((Mathf.Floor(item.transform.position.x)), (Mathf.Floor(item.transform.position.y)), item.transform.position.z);
        }
    }
}