using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2 : MonoBehaviour
{

    public float speed = 5f;

    private Vector3 startPos;
    private bool isBeingHeld = false;

    void Update()
    {
      if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 direction = mousePos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
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
            float startPosX = mousePos.x - this.transform.localPosition.x;
            float startPosY = mousePos.y - this.transform.localPosition.y;

            startPos = new Vector3(startPosX, startPosY, 0f);

            isBeingHeld = true;

        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
