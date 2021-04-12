using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float targetTime = 2.0f;
    public float timer = 2.0f;
    public double targetClicks = 10;
    public ParticleSystem DestructionEffect;
    public bool changeOpacity = false;

    public bool makeSound = true;
    public GameObject soundManager;
    private AudioSource sound;

    public GameObject hintPosition;

    private double counter = 0;
    private float opacity;
    Color color;
    

    private void Start()
    {
        color = this.gameObject.GetComponent<Renderer>().material.color;

        opacity = Mathf.Clamp(color.a, 0, color.a);
        sound = soundManager.GetComponent<AudioSource>();

    }

    void OnMouseDown()
    {
        // Counter wird f�r jeden Klick eins hochgez�hlt
        counter++;
        opacity -= 0.1f;
    }

    void Update()
    {
        if (changeOpacity == true)
        {
            color.a = opacity;
            this.gameObject.GetComponent<Renderer>().material.color = color;
        }


        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        if (counter >= targetClicks)
        {
            DestroyObbject();

            //Hint System interaction here
            DataScript.interactionMade = true;
            GameObject.Destroy(hintPosition);
        }

    }


    void timerEnded()
    {
        // Wenn der Timer abl�uft wird eins vom Counter abgezogen und der Timer startet neu
        targetTime = timer;

        if (counter >= 0)
        {
            counter--;
            opacity += 0.1f;
        }
 
    }

    private void DestroyObbject()
    {

        if (this.gameObject != null)
        {
           
            GameObject.Destroy(this.gameObject);
            DestructionEffect.Play();
            sound.Play();
          
        }
    }
}
