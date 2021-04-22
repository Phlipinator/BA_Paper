using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject Boundary;
    public float moveAmount = 0.01f;
    public float edgeSize = 0.1f;
    public float cameraMoveSpeed = 1f;

    public bool enableScrolling = true;
    public bool smoothZoom = false;

    public float minZoom = 250;
    public float maxZoom = 500;

    private Vector3 cameraFollowPosition;
    private float boundaryX;
    private float boundaryY;
    private float height;
    private float width;
    private float zoom;

    private bool activateMouseMovement = false;

    private float aspect;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        zoom = cam.orthographicSize;

        aspect = cam.aspect;


        //initialize the starting position of the camera
        Vector2 cameraFollowPosition = new Vector2(startScreen.transform.position.x, startScreen.transform.position.y);
        cam.transform.position = new Vector3(cameraFollowPosition.x, cameraFollowPosition.y, -10f);
        

        // Gets the height and with of the Background Image
        width = Boundary.GetComponent<SpriteRenderer>().bounds.size.x;
        height = Boundary.GetComponent<SpriteRenderer>().bounds.size.y;

    }

    void Update()
    {

        float camWidth = zoom * aspect * 2;
        float camHeight = zoom * 2;

        float dynamicMoveAmount = moveAmount * Time.deltaTime * zoom;

        boundaryX = (width / 2) - (camWidth / 2) ;
        boundaryY = (height / 2) - (camHeight / 2);

        // Debug.LogFormat("BX: {0} BY: {1} CX: {2} CY: {3}", boundaryX, boundaryY, cameraFollowPosition.x, cameraFollowPosition.y);

        Vector2 mouseUnitPos = new Vector2(Input.mousePosition.x / Screen.width * 2 - 1, Input.mousePosition.y / Screen.height * 2 - 1);

        Vector2 camPos2D = new Vector2(cam.transform.position.x, cam.transform.position.y);

        Vector2 mouseWorldPos = mouseUnitPos + camPos2D;


        // correct cam placement RIGHT
        if (mouseWorldPos.x > boundaryX)
        {
            mouseWorldPos.x = boundaryX;
        }

        // correct cam placment LEFT
        if (mouseWorldPos.x < -boundaryX)
        {
            mouseWorldPos.x = -boundaryX;
        }

        // correct cam placement UP
        if (mouseWorldPos.y > boundaryY)
        {
            mouseWorldPos.y = boundaryY;
        }

        // correct cam placement DOWN
        if (mouseWorldPos.y < -boundaryY)
        {
            mouseWorldPos.y = -boundaryY;
        }


        Vector2 cameraMoveDir = (mouseWorldPos - camPos2D);


        float magnitude = mouseUnitPos.magnitude;


        float deadZoneRadius = 0.3f;


        if (magnitude > 1f)
        {
            magnitude = 1f;
        }


        float moveFactor;

        if (magnitude < deadZoneRadius)
        {
            moveFactor = 0f;

        } else
        {
            float a = (magnitude - deadZoneRadius) / (1f - deadZoneRadius);

            moveFactor = a * a;
        }
        

        if (activateMouseMovement)
        {
            Vector3 movement = cameraMoveDir * cameraMoveSpeed * Time.deltaTime * moveFactor;

        
            cam.transform.position += movement;

        } else if (!activateMouseMovement && mouseUnitPos.magnitude < deadZoneRadius)
        {
            activateMouseMovement = true;
        }




        if (enableScrolling)
        {
            float zoomChangeAmount = 800f;

            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

            if (Input.mouseScrollDelta.y > 0)
            {
                zoom -= zoomChangeAmount * Time.deltaTime * 10f;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                zoom += zoomChangeAmount * Time.deltaTime * 10f;
            }

            if (smoothZoom == true)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime);
            }
            else
            {
                cam.orthographicSize = zoom;
            }


        }


    }
}
