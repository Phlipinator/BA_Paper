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

    public float minZoom = 250;
    public float maxZoom = 500;

    private float boundaryX;
    private float boundaryY;
    private float height;
    private float width;

    private bool activateMouseMovement = false;

    private float aspect;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        


        //initialize the starting position of the camera
        Vector2 camInitPos = new Vector2(startScreen.transform.position.x, startScreen.transform.position.y);
        SetCamPos(camInitPos);


        // Gets the height and with of the Background Image
        width = Boundary.GetComponent<SpriteRenderer>().bounds.size.x;
        height = Boundary.GetComponent<SpriteRenderer>().bounds.size.y;

    }

    Vector2 SetCamPos(Vector2 pos)
    {
        cam.transform.position = new Vector3(pos.x, pos.y, -10);

        return GetCamPos();
    }

    private Vector2 GetCamPos()
    {
        return new Vector2(cam.transform.position.x, cam.transform.position.y);
    }

    float Zoom()
    {
        float zoom = cam.orthographicSize;

        float zoomChangeAmount = 800f;

        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime * 10f;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime * 10f;
        }

        return Mathf.Clamp(zoom, minZoom, maxZoom);
    }

    Vector2 ClampPosition(Vector2 pos)
    {
        return new Vector2(
            Mathf.Clamp(pos.x, -boundaryX, boundaryX),
            Mathf.Clamp(pos.y, -boundaryY, boundaryY)
        );
        
    }

    void Update()
    {

        aspect = cam.aspect;

        Vector2 camPos = GetCamPos();

        float zoom = cam.orthographicSize;

        if (enableScrolling && activateMouseMovement)
        {
            zoom = Zoom();
        }

        float camWidth = zoom * aspect * 2;
        float camHeight = zoom * 2;


        boundaryX = (width / 2) - (camWidth / 2);
        boundaryY = (height / 2) - (camHeight / 2);


        Vector2 mouseUnitPos = new Vector2(
            Input.mousePosition.x / Screen.width * 2 - 1,
            Input.mousePosition.y / Screen.height * 2 - 1
        );


        Vector2 mouseWorldPos = mouseUnitPos + camPos;

        Vector2 camMoveDir = (mouseWorldPos - camPos);

       

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

        }
        else
        {
            float a = (magnitude - deadZoneRadius) / (1f - deadZoneRadius);

            moveFactor = a * a;
        }

        Vector2 newCamPos;

        if (activateMouseMovement)
        {
            Vector2 movement = camMoveDir * cameraMoveSpeed * Time.deltaTime * moveFactor * zoom;


            newCamPos = camPos + movement;

        }
        else if (!activateMouseMovement && mouseUnitPos.magnitude < deadZoneRadius)
        {
            activateMouseMovement = true;

            newCamPos = camPos;

        } else
        {
            newCamPos = camPos;
        }

        SetCamPos(ClampPosition(newCamPos));
        cam.orthographicSize = zoom;

    }
}
