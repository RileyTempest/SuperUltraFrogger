using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail_Thomas_001 : MonoBehaviour
{
    BoardManager_Thomas_001 GameBoard;

    private Time time;
    [SerializeField]
    private float speedFactor = 1.0f;
    [SerializeField]
    private bool directionToggle = false;
    private bool flipped = false;

    private Transform slider;
    private Transform cloneLeft;
    private Transform cloneRight;

    private Transform rowBG;

    // Use this for initialization
    void Start ()
    {

        GameBoard = GetComponentInParent<BoardManager_Thomas_001>();
        slider = transform.FindChild("slider_00");

        cloneLeft = Instantiate(slider, new Vector3(-(GameBoard.RowMetrics("width") + GameBoard.RowMetrics("buffer")), 
            slider.position.y, 3.0f), 
            new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), 
            transform);
        cloneRight = Instantiate(slider, new Vector3((GameBoard.RowMetrics("width") + GameBoard.RowMetrics("buffer")), 
            slider.position.y, 3.0f), 
            new Quaternion(0.0f, 0.0f, 0.0f, 0.0f), 
            transform);
        cloneLeft.name = "slider_01";
        cloneRight.name = "slider_02";
        cloneLeft.SetSiblingIndex(0);
        slider.SetSiblingIndex(1);
        cloneRight.SetSiblingIndex(2);

        rowBG = transform.FindChild("Background_00");
        rowBG.localScale = new Vector3(GameBoard.RowMetrics("width") + 1.0f, 1.0f, 1.0f);

        CheckForNull();
    }

    // Update is called once per frame
    void Update ()
    {
        CheckDirection();
        TranslateAndCheck(cloneLeft);
        TranslateAndCheck(slider);
        TranslateAndCheck(cloneRight);
    }

    //Methods
    private void CheckDirection()
    {
        if(flipped != GameBoard.IsFlipped)
        {
            flipped = GameBoard.IsFlipped;
            directionToggle = !directionToggle;
        }

        if (directionToggle)
        {
            cloneRight.gameObject.SetActive(false);
            cloneLeft.gameObject.SetActive(true);
            speedFactor = -(Mathf.Abs(speedFactor));//negative
        }
        else
        {
            cloneRight.gameObject.SetActive(true);
            cloneLeft.gameObject.SetActive(false);
            speedFactor = (Mathf.Abs(speedFactor));
        }
    }
    private void TranslateAndCheck(Transform currentSlider)
    {
        currentSlider.Translate((Time.deltaTime * speedFactor), 0, 0);

        if (currentSlider.position.x > (GameBoard.RowMetrics("width") + GameBoard.RowMetrics("buffer")))
        {
            currentSlider.position = new Vector3((-GameBoard.RowMetrics("width") + -GameBoard.RowMetrics("buffer")), currentSlider.position.y, currentSlider.position.z);
        }
        else if (currentSlider.position.x < (-GameBoard.RowMetrics("width") + -GameBoard.RowMetrics("buffer")))
        {
            currentSlider.position = new Vector3((GameBoard.RowMetrics("width") + GameBoard.RowMetrics("buffer")), currentSlider.position.y, currentSlider.position.z);
        }
    }

    private void CheckForNull()
    {
        if(rowBG == null)
        {
            print("RowBackground not found! use FindChild() to find Background_00");
        }

        if (slider == null)
        {
            print("Orignal slider not found! use FindChild() to find slider_00");
        }
    }
}
