using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    
    public float timer = 2.0f;
    public double targetClicks = 10;
    public ParticleSystem DestructionEffect;
    public bool changeOpacity = false;

    public bool makeSound = true;
    public GameObject soundManager;
    private AudioSource sound;

    public GameObject hintPosition;

    private float targetTime;
    private double counter = 0;
    private float opacity = Mathf.Clamp(1, 0, 1);
    Color color;
    

    private void Start()
    {
        color = this.gameObject.GetComponent<Renderer>().material.color;

        sound = soundManager.GetComponent<AudioSource>();

        targetTime = timer;
    }

    void OnMouseDown()
    {
        // Counter wird für jeden Klick eins hochgezählt
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
            DestroyObject();

            //Hint System interaction here
            DataScript.interactionMade = true;
            GameObject.Destroy(hintPosition);
        }

    }


    void timerEnded()
    {
        // Wenn der Timer abläuft wird eins vom Counter abgezogen und der Timer startet neu
        targetTime = timer;

        if (counter >= 0)
        {
            counter--;

            if (opacity < 1)
            {
                opacity += 0.1f;
            }
            
        }
 
    }

    private void DestroyObject()
    {

        if (this.gameObject != null)
        {
           
            GameObject.Destroy(this.gameObject);
            DestructionEffect.Play();
            sound.Play();
          
        }
    }
}
