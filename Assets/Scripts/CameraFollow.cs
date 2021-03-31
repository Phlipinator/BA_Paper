using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject startScreen;
    public float moveAmount = 1f;
    public float edgeSize = 10f;
    public float cameraMoveSpeed = 1f;
    public GameObject Boundary;

    public bool enableScrolling = true;
    public bool smoothZoom = false;

    private Vector3 cameraFollowPosition;
    private float boundaryX;
    private float boundaryY;
    private float height;
    private float width;
    private float zoom;

    private Camera cam;

    void Start()
    {
        //initialize the starting position of the camera
        Vector3 cameraFollowPosition = startScreen.transform.position;

        // Gets the height and with of the Background Image
        width = Boundary.GetComponent<SpriteRenderer>().bounds.size.x;
        height = Boundary.GetComponent<SpriteRenderer>().bounds.size.y;

        boundaryX = (width / 2) - (Screen.width / 2);
        boundaryY = (height / 2) - (Screen.height / 2);

        cam = Camera.main;
        zoom = cam.orthographicSize;

    }

    void Update()
    {
       
            // move to the right
            if ((Input.mousePosition.x > Screen.width - edgeSize) && cameraFollowPosition.x < boundaryX)
            {
                cameraFollowPosition.x += moveAmount * Time.deltaTime;
            }

            // move to the left
            if ((Input.mousePosition.x < edgeSize) && cameraFollowPosition.x > -boundaryX)
            {
                cameraFollowPosition.x -= moveAmount * Time.deltaTime;
            }

            // move up
            if ((Input.mousePosition.y > Screen.height - edgeSize) && cameraFollowPosition.y < boundaryY)
            {
                cameraFollowPosition.y += moveAmount * Time.deltaTime;
            }

            // move down
            if ((Input.mousePosition.y < edgeSize) && cameraFollowPosition.y > -boundaryY)
            {
                cameraFollowPosition.y -= moveAmount * Time.deltaTime;
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

            transform.position = newCameraPosition;

        }

        
        if (enableScrolling == true)
        {
            float zoomChangeAmount = 800f;

            zoom = Mathf.Clamp(zoom, startScreen.GetComponent<SpriteRenderer>().bounds.size.x / 6f, height / 6f);

            if (Input.mouseScrollDelta.y > 0)
            {
                zoom -= zoomChangeAmount * Time.deltaTime * 10f;
            }

            if ( Input.mouseScrollDelta.y < 0)
            {
                zoom += zoomChangeAmount * Time.deltaTime * 10f;
            }

            if(smoothZoom == true)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime);

            } else
            {
                cam.orthographicSize = zoom;
            }
            

        }
        
       
    }
}