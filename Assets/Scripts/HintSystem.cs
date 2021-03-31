using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour
{

    public float targetTime = 10f;
    private float timer;


    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject Four;
    public GameObject Five;

    public int interactables = 5;

    public ParticleSystem ps;

    private Vector2 OnePos;
    private Vector2 TwoPos;
    private Vector2 ThreePos;
    private Vector2 FourPos;
    private Vector2 FivePos;

    private Vector2[] vecArray;



    void Start()
    {
        timer = targetTime;

        OnePos = One.transform.position;
        TwoPos = Two.transform.position;
        ThreePos = Three.transform.position;
        FourPos = Four.transform.position;
        FivePos = Five.transform.position;

        vecArray = new Vector2[]
        {
            OnePos,
            TwoPos,
            ThreePos,
            FourPos,
            FivePos
        };
    }

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        //Starts Timer again
        targetTime = timer;

        if (DataScript.interactionMade == false)
        {
            

            int display = Random.Range(1, interactables);

            showHint(display);

            Debug.Log("Show Hint " + display);


        } else {

            DataScript.interactionMade = false;
        }

    }

    void showHint(int display)
    {
        Vector2 selected = vecArray[display];
        ps.transform.position = selected;
        ps.Play();

        Debug.Log("I am here");


    }
}
