//---------------------------------------------------------------------------
// Authors: Thomas Myrick
// Date: 2/07/17
// Purpose: Use ray casting to detect all the objects Frogger can interact with. 
// Report the object's tag/name/layer to other objects that need it using the get/set properties. Keep everything else private.
// Credit:
//-----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastCollision : MonoBehaviour
{

    //Variables
    [SerializeField]
    private LayerMask backgroundMask;
    [SerializeField]
    private LayerMask obstacleMask;

    private RaycastHit hitRow;
    private RaycastHit hitHaz;


    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        HazardRay(); //detect hazards first to determine player death. 
        BoardRay();
    }

    //GetSet Properties
    public Transform HitRowTransform
    {
        get
        {
            return hitHaz.transform;
        }
    }
    public Transform HitHazardTransform
    {
        get
        {
            return hitHaz.transform;
        }
        //bump here is some stuff
    }

    //Methods
    private void BoardRay()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);

        Physics.Raycast(ray, out hitRow, 100.0f, backgroundMask);

        Debug.DrawLine(ray.origin, hitRow.point, Color.white);
        //print(" Boardray " + rayHit.transform.tag.ToString());
        
    }
    private void HazardRay()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);

        Physics.Raycast(ray, out hitHaz, 100.0f, obstacleMask);

        Debug.DrawLine(ray.origin, hitHaz.point, Color.green);
        //print(" HazardRay " + rayHit.transform.tag.ToString());
    }

    /*
    private void HazardRay()
    {
        //Left
        RaycastHit hazHit;
        Ray hazRay = new Ray(new Vector3(transform.position.x - 0.5f,
            transform.position.y,
            transform.position.z),
            Vector3.down);

        Physics.Raycast(hazRay, out hazHit, 100.0f, backgroundMask);

        //rayHitHazName = hazHit.transform.gameObject.tag.ToString();
        Debug.DrawLine(hazRay.origin, hazHit.point, Color.cyan);

        //Right
        hazRay = new Ray(new Vector3(transform.position.x + 0.5f,
             transform.position.y,
             transform.position.z),
             Vector3.down);

        Physics.Raycast(hazRay, out hazHit, 100.0f, backgroundMask);

        //rayHitHazName = hazHit.transform.gameObject.tag.ToString();
        Debug.DrawLine(hazRay.origin, hazHit.point, Color.blue);

        //Top
        hazRay = new Ray(new Vector3(transform.position.x,
             transform.position.y,
             transform.position.z + 0.5f),
             Vector3.down);

        Physics.Raycast(hazRay, out hazHit, 100.0f, backgroundMask);

        //rayHitHazName = hazHit.transform.gameObject.tag.ToString();
        Debug.DrawLine(hazRay.origin, hazHit.point, Color.red);

        //Bottom
        hazRay = new Ray(new Vector3(transform.position.x,
             transform.position.y,
             transform.position.z - 0.5f),
             Vector3.down);

        Physics.Raycast(hazRay, out hazHit, 100.0f, backgroundMask);

        //rayHitHazName = hazHit.transform.gameObject.tag.ToString();
        Debug.DrawLine(hazRay.origin, hazHit.point, Color.yellow);
    }
    */
}
