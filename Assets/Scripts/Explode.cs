using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float targetTime = 2.0f;
    public float timer = 2.0f;
    public double targetClicks = 10;
    public ParticleSystem DestructionEffect;

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
            DestroyObbject();
        }

    }



    void timerEnded()
    {
        // Wenn der Timer abläuft wird eins vom Counter abgezogen und der Timer startet neu
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



            //Instantiate our one-off particle system
            ParticleSystem explosionEffect = Instantiate(DestructionEffect) as ParticleSystem;

            explosionEffect.transform.position = transform.position;
            //play it
            explosionEffect.loop = false;
            explosionEffect.Play();

            //destroy the particle system when its duration is up, right
            //it would play a second time.
            Destroy(explosionEffect.gameObject, explosionEffect.duration);
            GameObject.Destroy(this.gameObject);
        }
    }
}
