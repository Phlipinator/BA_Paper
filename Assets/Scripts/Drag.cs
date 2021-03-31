using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private bool inFixPosition = false;

    private float orgPosX;
    private float orgPosY;
    private float width;
    private float height;
    private float threshold = 3f;

    public bool moveX;
    public bool moveRight;
    public bool moveUp;
    public bool fixPosition;

    public GameObject hintPosition;
   
    void Start()
    {
        // Get the original Position in the beginning of the programm 
        orgPosX = transform.position.x;
        orgPosY = transform.position.y;

        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }


    // Update is called once per frame
    void Update()
    {

       if (isBeingHeld == true)
        {
            Vector2 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            
            // fixes in case "Right"
            if (((this.gameObject.transform.position.x - (orgPosX + width) + threshold) > 1.0) && moveX == true && fixPosition == true)
            {
                inFixPosition = true;
            }

            // fixes in case "Left"
            if (((this.gameObject.transform.position.x - (orgPosX - width) + threshold) < 1.0) && moveX == true && moveRight == false && fixPosition == true)
            {
                inFixPosition = true;
            }

            // fixes in case "Up"
            if (((this.gameObject.transform.position.y - (orgPosY + height) + 2.0) > 1.0) && moveX == false && fixPosition == true)
            {
                inFixPosition = true;
            }

            // fixes in case "Down"
            if (((this.gameObject.transform.position.y - (orgPosY - height) + 2.0) < 1.0) && moveX == false && fixPosition == true)
            {
                inFixPosition = true;
            }



            // checks wether to move in X (true) or Y (false) direction
            if (moveX == true && inFixPosition == false)
            {
                // checks wether to move right (true) or left (false)
                if (moveRight == true)
                {
                    if ((mousePos.x - startPosX) > orgPosX)
                        this.gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, orgPosY);


                        //Hint System interaction here
                        DataScript.interactionMade = true;
                        GameObject.Destroy(hintPosition);

                } else {

                    if ((mousePos.x - startPosX) < orgPosX)
                        this.gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, orgPosY);

                        //Hint System interaction here
                        DataScript.interactionMade = true;
                        GameObject.Destroy(hintPosition);
                }

            }

            // checks wether to move in X (true) or Y (false) direction
            if (moveX == false && inFixPosition == false)
            {

                // checks wether to move up (true) or down (false)
                if (moveUp == true)
                {
                    if ((mousePos.y - startPosY) > orgPosY)
                        this.gameObject.transform.localPosition = new Vector2(orgPosX, mousePos.y - startPosY);

                        //Hint System interaction here
                        DataScript.interactionMade = true;
                        GameObject.Destroy(hintPosition);

                }
                else
                {

                    if ((mousePos.y - startPosY) < orgPosY)
                        this.gameObject.transform.localPosition = new Vector2(orgPosX, mousePos.y - startPosY);

                        //Hint System interaction here
                        DataScript.interactionMade = true;
                        GameObject.Destroy(hintPosition);
                }
                
            }

        }

       
    }

    private void OnMouseDown()
    {
        //makes sure only left Mouse Button works
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //makes sure the Object does not snaps to the cursor
            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;

        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
