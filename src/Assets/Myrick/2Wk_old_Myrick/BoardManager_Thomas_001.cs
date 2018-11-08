using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager_Thomas_001 : MonoBehaviour
{
    //Internal Variables
    [SerializeField]
    private List<GameObject> sectionList = new List<GameObject>();
    private int currentSectionIndex = 0;
    private bool isBoardFlipped = false;

    [SerializeField]
    private List<GameObject> waterSectionList = new List<GameObject>();
    private bool isUnderWater = false;                  //init to false when done unit testing!!!!
    private int memSectionIndex = 0;
    private int memRowIndex = 0;

    Transform newSection;
    private bool scrolling = false;
    [SerializeField]
    private float scrollSpeed = 10.0f;
    private bool useOnce = false;

    PlayerRaycastCollision_Thomas_001 PlayerCollision;
    GameObject PlayerObject;

    //Class Variables
    [SerializeField]
    private float widthRow = 14.0f;
    [SerializeField]
    private float sliderBuffer = 7.0f;
    private int rowCount = 0;

    // Use this for initialization
    void Start ()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerCollision = PlayerObject.GetComponent<PlayerRaycastCollision_Thomas_001>();
        rowCount = transform.GetChild(0).childCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForWater();

        #region Using Standard Sections
        if (!isUnderWater)
        {

            if (PlayerCollision.HitRowTransform.tag == "section" && PlayerCollision.HitRowTransform.parent.parent.GetSiblingIndex() != 0)
            {
                SectionChange();
                //AcquireSectionScroll();
                //scrolling = true;
            }
            CheckBoardSection();
            if (scrolling)
            {
                if (useOnce)
                {
                    //ScrollSections();
                }
            }

            if (!scrolling)
            {
                //SectionChange();
                //CheckBoardSection();
            } 
        }
        #endregion

        #region Using Water Sections
        if (isUnderWater)
        {
            LoadWaterList();
        }
        #endregion
    }

    //Methods - standard 
    private void AcquireSectionScroll()
    {
        int oldRowCount = transform.GetChild(0).childCount;

        for (int i = 0; i < transform.childCount; i++)
        {
            //print("destroying child object " + transform.GetChild(i));
            Destroy(transform.GetChild(i).gameObject);
        }



        Vector3 transitionOffset = new Vector3(transform.position.x, oldRowCount, transform.position.z);
        //Vector3 transitionOffsetFlip = new Vector3(transform.position.x, transitionOffsetFlip + doomedSection.position.x, transform.position.z);
        int tempSectionIndex = currentSectionIndex;
        Transform doomedSection;
        
        if (isBoardFlipped)
        {
            tempSectionIndex -= 1;
            tempSectionIndex = Mathf.RoundToInt(Mathf.Clamp(tempSectionIndex, 0.0f, (sectionList.Count - 1.0f)));
            doomedSection = sectionList[tempSectionIndex].transform;
            doomedSection = Instantiate(doomedSection, new Vector3(transform.position.x, oldRowCount + doomedSection.childCount, transform.position.z), transform.rotation, transform);
            for (int i = 0; i < doomedSection.childCount; i++)
            {
                doomedSection.GetChild((doomedSection.childCount - 1)).SetSiblingIndex(i);
                Transform[] childList = doomedSection.GetChild(i).GetComponentsInChildren<Transform>();
                foreach (Transform child in childList)
                {
                    child.transform.position = new Vector3(child.position.x, i + doomedSection.childCount, child.position.z);
                }
            }
            PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, 0.0f, PlayerObject.transform.position.z);


        }
        else
        {
            tempSectionIndex += 1;
            tempSectionIndex = Mathf.RoundToInt(Mathf.Clamp(tempSectionIndex, 0.0f, (sectionList.Count - 1.0f)));
            doomedSection = Instantiate((sectionList[tempSectionIndex].transform), transitionOffset, transform.rotation, transform);
            PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, 0.0f, PlayerObject.transform.position.z);
        }
    }
    private void SectionChange()
    {
            useOnce = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            if (isBoardFlipped)
            {
                currentSectionIndex -= 1;
                currentSectionIndex = Mathf.RoundToInt(Mathf.Clamp(currentSectionIndex, 0.0f, (sectionList.Count - 1.0f)));
                newSection = Instantiate((sectionList[currentSectionIndex].transform), transform.position, transform.rotation, transform);
                PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, 0.0f, PlayerObject.transform.position.z);
            }
            else
            {
                currentSectionIndex += 1;
                currentSectionIndex = Mathf.RoundToInt(Mathf.Clamp(currentSectionIndex, 0.0f, (sectionList.Count - 1.0f)));
                newSection = Instantiate((sectionList[currentSectionIndex].transform), transform.position, transform.rotation, transform);
                PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, 0.0f, PlayerObject.transform.position.z);
            }
    }
    private void ScrollSections()
    {
        print("entered scroll method");
        print(transform.GetChild(transform.childCount - 1).childCount * 0.5);
        print(transform.GetChild(transform.childCount - 1).position);

        for (int i = 0; i < transform.childCount; i++)
        {
            print("entered for");
            transform.GetChild(i).Translate(0.0f, -(Time.deltaTime * scrollSpeed), 0.0f);
        }
        print(transform.GetChild(transform.childCount - 1).position);
        if (!isBoardFlipped && transform.GetChild(transform.childCount - 1).position.y <= 0.0f)
        {
            scrolling = false;
            useOnce = true;
        }

        if (isBoardFlipped && transform.GetChild(transform.childCount - 1).position.y <= transform.GetChild(transform.childCount - 1).childCount * 0.5)
        {
            scrolling = false;
            useOnce = true;
        }
        print("exiting scroll method");
    }

    private void CheckBoardSection()
    {
        if (PlayerCollision.HitRowTransform.tag == "Goal" && PlayerCollision.HitRowTransform.parent.parent.GetSiblingIndex() != 0)
        {
            FlipBoard();
            isBoardFlipped = true;
        }
        else if (PlayerCollision.HitRowTransform.tag == "Home" && PlayerCollision.HitRowTransform.parent.parent.GetSiblingIndex() != 0)
        {
            FlipBoard();
            isBoardFlipped = false;
        }

        if (isBoardFlipped && PlayerCollision.HitRowTransform.tag == "section" && PlayerCollision.HitRowTransform.parent.parent.GetSiblingIndex() != 0)
        {
            FlipBoard();
            isBoardFlipped = true;
        }
    }
    private void FlipBoard()
    {
        rowCount = newSection.childCount;

        for (int i = 0; i < rowCount; i++)
        {
            newSection.GetChild((rowCount - 1)).SetSiblingIndex(i);
            Transform[] childList = newSection.GetChild(i).GetComponentsInChildren<Transform>();
            foreach (Transform child in childList)
            {
                child.transform.position = new Vector3(child.position.x, i, child.position.z);
            }
        }
        isBoardFlipped = true;
        PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, 0.0f, PlayerObject.transform.position.z);
    }


    //Methods - water
    private void CheckForWater()
    {
        if (!isUnderWater && PlayerCollision.HitHazardTransform.tag == "water")
        {
            isUnderWater = true;
            PlayerObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
        if (isUnderWater && PlayerCollision.HitHazardTransform.tag == "section")
        {
            print("turning off water.");
            PlayerObject.transform.position = new Vector3(0.0f, 0.0f, PlayerObject.transform.position.z);
            isUnderWater = false;
            PlayerObject.GetComponent<Rigidbody>().useGravity = false;
        }

        if (!isUnderWater)
        {
            PlayerObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else PlayerObject.GetComponent<Rigidbody>().useGravity = true;
    }
    private void LoadWaterList()
    {
        memSectionIndex = currentSectionIndex;
        memRowIndex = PlayerCollision.HitRowTransform.parent.parent.GetSiblingIndex();

        memSectionIndex = Mathf.RoundToInt(Mathf.Clamp(memSectionIndex, 0.0f, (sectionList.Count - 1.0f))); //index bounds limit

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        newSection = Instantiate((waterSectionList[memSectionIndex].transform), transform.position, transform.rotation, transform);
        



        //pause
        //store water row index from standard sections
        //store current section index before destory
        //destory standard section
        //init water list
        //load section
        //get new player controls
        //set gravity 
        //unpause

        //set exits
        //scroll water level?
        //determine if flipped
        //on exit, retrieve
    }
    //Retrive sectionList from folder here

    //Properties
    public bool IsFlipped
    {
        get
        {
            return isBoardFlipped;
        }
    }
    public float RowMetrics(string _string)
    {
        if (_string == "width")
        {
            return widthRow;
        }
        else if (_string == "buffer")
        {
            return sliderBuffer;
        }
        else if (_string == "added")
        {
            return widthRow + sliderBuffer;
        }
        else if (_string == "count")
        {
            return rowCount = transform.GetChild(0).childCount;
        }
        else if (_string == "currentRow")
        {
            return PlayerCollision.HitRowTransform.parent.parent.GetSiblingIndex();
        }

        else if(_string == "currentSection")
        {
            return currentSectionIndex;
        }
        else return 0.0f;
    }

}
