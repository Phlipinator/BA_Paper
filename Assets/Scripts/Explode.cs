using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float targetTime = 2.0f;
    public float timer = 2.0f;
    public double targetClicks = 10;

    private double counter = 0;


    void OnMouseDown()
    {
        // Counter wird für jeden Klick eins hochgezählt
        counter++;
    }

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        if (counter >= targetClicks)
        {
            Destroy(gameObject);
        }

    }



    void timerEnded()
    {
        // Wenn der Timer abläuft wird eins vom Couner abgezogen und der Timer startet neu
        targetTime = timer;

        if (counter >= 0)
        {
            counter--;
        }
 
    }
}
