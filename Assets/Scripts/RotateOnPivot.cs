using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPivot : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;

    private bool isBeingHeld = false;

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
            transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);

            DataScript.interactionMade = true;
            GameObject.Destroy(hintPosition);

}



        /*
         * Note to Self:
         * To change the Hinge the Object is rotated around a custom Pivot can be created in the Sprite Inspector (Import Settings)
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
