using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public ParticleSystem water;

    private bool activate = false;


    void Update()
    {
        if (activate == true)
        {
            water.Play();
        }
    }

    private void OnMouseDown()
    {
        activate = true;
    }

    private void OnMouseUp()
    {
        activate = false;
    }
}
