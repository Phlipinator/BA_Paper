using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public bool makeSound = true;
    public float timer;
    private float targetTime = 0;
    private AudioSource sound;


    private void Start()
    {
        sound = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (makeSound == true)
        {
            if (targetTime <= 0.0f)
            {
                sound.Play();
                targetTime = timer;
            }
            
        }
        
    }

    private void Update()
    {
        targetTime -= Time.deltaTime;

    }
}
