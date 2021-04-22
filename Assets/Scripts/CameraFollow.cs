using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject Boundary;
    public float moveAmount = 0.01f;
    public float edgeSize = 10f;
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

    private float aspect;

    private Camera cam;

    void Start()
    {
        //initialize the starting position of the camera
        Vector3 cameraFollowPosition = startScreen.transform.position;

        // Gets the height and with of the Background Image
        width = Boundary.GetComponent<SpriteRenderer>().bounds.size.x;
        height = Boundary.GetComponent<SpriteRenderer>().bounds.size.y;

        cam = Camera.main;
        zoom = cam.orthographicSize;

        aspect = cam.aspect;

        cam.transform.position = new Vector3(startScreen.transform.position.x, startScreen.transform.position.y, -10f);

    }

    void Update()
    {
        float camWidth = zoom * aspect * 2;
        float camHeight = zoom * 2;

        float dynamicMoveAmount = moveAmount * Time.deltaTime * zoom;

        boundaryX = (width / 2) - (camWidth / 2) ;
        boundaryY = (height / 2) - (camHeight / 2);
        
        // Debug.LogFormat("BX: {0} BY: {1} CX: {2} CY: {3}", boundaryX, boundaryY, cameraFollowPosition.x, cameraFollowPosition.y);

        // move to the right
        if ((Input.mousePosition.x > Screen.width - edgeSize) && cameraFollowPosition.x < boundaryX)
        {
            cameraFollowPosition.x += dynamicMoveAmount;
        }

        // move to the left
        if ((Input.mousePosition.x < edgeSize) && cameraFollowPosition.x > -boundaryX)
        {
            cameraFollowPosition.x -= dynamicMoveAmount;
        }

        // move up
        if ((Input.mousePosition.y > Screen.height - edgeSize) && cameraFollowPosition.y < boundaryY)
        {
            cameraFollowPosition.y += dynamicMoveAmount;
        }

        // move down
        if ((Input.mousePosition.y < edgeSize) && cameraFollowPosition.y > -boundaryY)
        {
            cameraFollowPosition.y -= dynamicMoveAmount;
        }


        // correct cam placement RIGHT
        if (cameraFollowPosition.x > boundaryX)
        {
            cameraFollowPosition.x = boundaryX;
        }

        // correct cam placment LEFT
        if (cameraFollowPosition.x < -boundaryX)
        {
            cameraFollowPosition.x = -boundaryX;
        }

        // correct cam placement UP
        if (cameraFollowPosition.y > boundaryY)
        {
            cameraFollowPosition.y = boundaryY;
        }

        // correct cam placement DOWN
        if (cameraFollowPosition.y < -boundaryY)
        {
            cameraFollowPosition.y = -boundaryY;
        }



        //Sets a static value for z, because we dont need it in 2D
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target

                newCameraPosition = cameraFollowPosition;
            }

            cam.transform.position = newCameraPosition;

        }


        if (enableScrolling == true)
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
