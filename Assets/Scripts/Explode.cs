using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float targetTime = 2.0f;
    public float timer = 2.0f;
    public double targetClicks = 10;
    // public ParticleSystem ExplosionParticleSystem;

    private double counter = 0;


    void OnMouseDown()
    {
        // Counter wird f�r jeden Klick eins hochgez�hlt
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
            DestroyObbject();
        }

    }



    void timerEnded()
    {
        // Wenn der Timer abl�uft wird eins vom Couner abgezogen und der Timer startet neu
        targetTime = timer;

        if (counter >= 0)
        {
            counter--;
        }
 
    }

    private void DestroyObbject()
    {
        if (this.gameObject != null)
        {
            // Insert Particle Sytem here
            GameObject.Destroy(this.gameObject);
        }
    }
}
