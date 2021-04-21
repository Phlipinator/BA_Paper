using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearReappear : MonoBehaviour
{
    public GameObject disappear;
    public GameObject appear;

    public float speed = 0.005f;

    private bool hasBeenActivated = false;
    private bool activate = false;
    private float opacityOne = 1;
    private float opacityTwo = 0;
    Color colorOne;
    Color colorTwo;
    

    private void Start()
    {
    
        colorOne = disappear.GetComponent<Renderer>().material.color;
        colorTwo = appear.GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {

        colorOne.a = opacityOne;
        colorTwo.a = opacityTwo;

        disappear.GetComponent<Renderer>().material.color = colorOne;
        appear.GetComponent<Renderer>().material.color = colorTwo;


        if (activate == true && hasBeenActivated == false)
        {
            
            if(opacityOne >= 0)
            {

                opacityOne -= speed;

            }
            

            if (opacityOne <= 0 && opacityTwo <= 1)
            {
                opacityTwo += speed;

            }
            

           if(opacityTwo >= 1)
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
