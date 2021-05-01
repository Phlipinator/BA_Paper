using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    public GameObject car;
    public GameObject boundry;
    public GameObject soundManager;
    public bool goRight = false;
    public float speed = 1;

    private float stopR;
    private float stopL;
    private bool activate = false;
    private bool playSound = false;
    private Vector3 pos;
    private Vector3 newPos;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        pos = car.transform.position;

        newPos = pos;

        float bWidth = boundry.GetComponent<SpriteRenderer>().bounds.size.x;
        float cWidth = car.GetComponent<SpriteRenderer>().bounds.size.x;

        stopR = (bWidth / 2) + (cWidth / 2);
        stopL = -(bWidth / 2) - (cWidth / 2);

        sound = soundManager.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.gameObject.GetComponent<Renderer>().material.color.a == 0)
        {
            activate = true;

            if (!playSound)
            {
                sound.Play();

                playSound = true;
            }
        }

        if (activate)
        {

            pos = car.transform.position;

            car.transform.position = newPos;

            if (goRight)
            {
                if (newPos.x >= stopR)
                {
                    car.SetActive(false);
                }
                else
                {
                    newPos.x += speed;
                }


            }
            else
            {
                if (newPos.x <= stopL)
                {
                    car.SetActive(false);
                }
                else
                {
                    newPos.x -= speed;
                }

            }
        }
    }
}

   
