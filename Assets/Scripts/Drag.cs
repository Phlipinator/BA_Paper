using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    private float orgPosX;
    private float orgPosY;
    private float threshold;

    public bool moveX;
    public bool moveRight;
    public bool moveUp;

    public GameObject hintPosition;
   
    void Start()
    {
        // Get the original Position in the beginning of the programm 
        orgPosX = transform.position.x;
        orgPosY = transform.position.y;

        threshold = GetComponent<SpriteRenderer>().bounds.size.x * 0.3f;

    }


    // Update is called once per frame
    void Update()
    {

       if (isBeingHeld)
        {
            Vector2 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);


            // checks wether to move in X (true) or Y (false) direction
            if (moveX)
            {
                // checks wether to move right (true) or left (false)
                if (moveRight)
                {
                    if ((mousePos.x - startPosX) > orgPosX)
                    {
                        this.gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, orgPosY);
                    }
                        
                       if (transform.position.x >= orgPosX + threshold)
                       {
                            //Hint System interaction here
                            DataScript.interactionMade = true;
                            GameObject.Destroy(hintPosition);
                        }
                        

                } else {

                    if ((mousePos.x - startPosX) < orgPosX)
                    {
                        this.gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, orgPosY);
                    }
                        
                    if (transform.position.x <= orgPosX - threshold)
                    {
                        //Hint System interaction here
                        DataScript.interactionMade = true;
                        GameObject.Destroy(hintPosition);
                    }
                       
                }

            }
            else
            {
                // checks wether to move up (true) or down (false)
                if (moveUp)
                {
                    if ((mousePos.y - startPosY) > orgPosY)
                    {
                        this.gameObject.transform.localPosition = new Vector2(orgPosX, mousePos.y - startPosY);
                    }
                        

                    //Hint System interaction here
                    DataScript.interactionMade = true;
                    GameObject.Destroy(hintPosition);

                } else {

                    if ((mousePos.y - startPosY) < orgPosY)
                    {
                        this.gameObject.transform.localPosition = new Vector2(orgPosX, mousePos.y - startPosY);
                    }
                        

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
