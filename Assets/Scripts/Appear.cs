using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public GameObject appear;

    public float speed = 0.0005f;

    private bool hasBeenActivated = false;
    private bool activate = false;
    private float opacity = 0;
    Color color;

   
    void Start()
    {
        color = appear.GetComponent<Renderer>().material.color;
    }


    void FixedUpdate()
    {

        color.a = opacity;

        appear.GetComponent<Renderer>().material.color = color;


        if (activate == true && hasBeenActivated == false)
        {
            if (opacity <= 1)
            {
                opacity += speed;

            }


            if (opacity >= 1)
            {
                hasBeenActivated = true;
            }

        }
    }


    private void OnMouseDown()
    {
        activate = true;
    }
}
