using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject waveOne;
    public float speed = 0.1f;
    public float maxSize = 500f;
   

    private Vector3 speedVec;
    private Vector3 newPos;
    private Vector3 orgSize;
    private bool activate = false;


    // Start is called before the first frame update
    void Start()
    {
        speedVec = new Vector3(speed, speed / 3, 0f);

        newPos = waveOne.transform.position;
        orgSize = waveOne.transform.localScale;

        waveOne.SetActive(false);
        
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
   
        if (activate == true)
        {

            makeWave(waveOne);

        }
    }

    private void OnMouseDown()
    {
        activate = true;
    }

    private void OnMouseUp()
    {
        activate = false;
        waveOne.SetActive(false);
        waveOne.transform.localScale = orgSize;

    }

    void makeWave(GameObject wave)
    {
        wave.SetActive(true);


        if (wave.transform.localScale.x <= maxSize)
        {
            wave.transform.localScale += speedVec;

        }
        else
        {
            wave.transform.localScale = orgSize;
            wave.SetActive(false);

        }
    }

}
