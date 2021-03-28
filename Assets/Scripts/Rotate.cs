using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{

    public GameObject Hinge;

    private Vector3 HingePos;
    private float Angle = 0f;

    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;

    private float startPosX;
    private float startPosY;

    private Vector3 In;
    private Vector3 Out;
    private bool isBeingHeld = false;
    

    void Start()
    {
        HingePos = Hinge.transform.position;
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

        mouseStartPos = Input.mousePosition;
        mouseStartPos = Camera.main.ScreenToWorldPoint(mouseStartPos);
        mouseStartPos.z = 0f;
    }


    private void OnMouseUp()
    {
        isBeingHeld = false;

    }


    void Update()
    {
        if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 currentPos = transform.position;

            Vector3 In = mouseStartPos - HingePos;
            Vector3 Out = mousePos - HingePos;

            Vector3 KreuzProdukt = Vector3.Cross(In, Out);

            double AngleDouble = 0;

            //oben links & unten rechts
            if (mousePos.y > currentPos.y && mousePos.x < currentPos.x | mousePos.y < currentPos.y && mousePos.x > currentPos.x)
            {
                AngleDouble = Math.Asin((KreuzProdukt.magnitude) / (In.magnitude * Out.magnitude)) * 360 / (2 * Math.PI);
                Angle = (float)AngleDouble;
            } else
            {
                AngleDouble = Math.Asin((KreuzProdukt.magnitude) / (In.magnitude * Out.magnitude)) * 360 / (2 * Math.PI);
                Angle = (float)AngleDouble * -1f;
            }

            

            



            double newX = Math.Sin(Angle) * currentPos.x - Math.Cos(Angle) * currentPos.y;
            double newY = Math.Cos(Angle) * currentPos.x + Math.Sin(Angle) * currentPos.y;
            Vector3 newPos = new Vector3((float)newX, (float)newY, 0f);

            //this.gameObject.transform.localPosition = newPos;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Angle);

            /*
            Debug.Log("In: " + In);
            Debug.Log("Out: " + Out);
            Debug.Log("Angle: " + Angle);
            Debug.Log("In Magnitude: " + In.magnitude);
            Debug.Log("Out Magnitude: " + Out.magnitude);
            Debug.Log("Cross: " + KreuzProdukt.magnitude);
            */

        }

    }
}
    




