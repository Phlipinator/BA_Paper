using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;

    //Range makes sure the object can only be rotated to a certain amount
    [Range(0, 180)] private float angle;

    private bool isBeingHeld = false;

    public bool rotateOnY = true;

    public GameObject hintPosition;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        //This fires only on the frame the button is clicked
        if (Input.GetMouseButtonDown(0))
        {

            screenPos = myCam.WorldToScreenPoint(transform.position);
            Vector3 v3 = Input.mousePosition - screenPos;
            angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;

        }


        //This fires while the button is pressed down
        if (Input.GetMouseButton(0) && isBeingHeld == true)
        {

            Vector3 v3 = Input.mousePosition - screenPos;
            float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;

            if(rotateOnY == true)
            {
                transform.eulerAngles = new Vector3(0, angle + angleOffset, 0);

                //Hint System interaction here
                DataScript.interactionMade = true;
                GameObject.Destroy(hintPosition);
            } else
            {
                transform.eulerAngles = new Vector3(angle + angleOffset, 0, 0);

                //Hint System interaction here
                DataScript.interactionMade = true;
                GameObject.Destroy(hintPosition);
            }
            
            

        }

        /*
         * Note to Self:
         * To change the Axis the Object is rotated around a custom Pivot can be created in the Sprite Inspector (Import Settings)
         */
    }

    // IsBeingHeld fixes the Hitbox of the Rotation
    void OnMouseDown()
    {
        isBeingHeld = true;
    }

    void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
