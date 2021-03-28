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

    private Vector3 cameraFollowPosition;
    private float boundaryX;
    private float boundaryY;

    void Start()
    {
        //initialize the starting position of the camera
        Vector3 cameraFollowPosition = startScreen.transform.position;

        // Gets the height and with of the Background Image
        float width = Boundary.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = Boundary.GetComponent<SpriteRenderer>().bounds.size.y;

        boundaryX = (width / 2) - (Screen.width / 2);
        boundaryY = height / 2 - (Screen.height / 2);

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
       
    }
}
