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
    public Camera cam;

    private Vector2 OnePos;
    private Vector2 TwoPos;
    private Vector2 ThreePos;
    private Vector2 FourPos;
    private Vector2 FivePos;

    private Vector2[] vecArray;
    private List<Vector2> vecList;
    private List<Vector2> vecListNew;



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
           // Rest go here
        };

        vecList = new List<Vector2>();
    }

    void Update()
    {

        foreach (Vector2 i in vecArray)
        {
            if (i != null)
            {
                // Add in new Array or List?
                // If Interactable has been interacted with it should destroy its position Object making it equal to null
                vecList.Add(i);
                //vecList now contains a List of the Positions of all interactables that have not been destroyed yet
                // vectorList[display];
            }
        }

        foreach (Vector2 i in vecList)
        {
            Vector3 current3 = new Vector3(i.x, i.y, 0f);
            Vector3 currentPoint = cam.WorldToViewportPoint(current3);

            if (currentPoint.x >= 0 && currentPoint.x <= 1 && currentPoint.y >= 0 && currentPoint.y <= 1)
            {
                vecListNew.Add(i);
            }
        }



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
            

            int display = Random.Range(1, vecListNew.Count);

            showHint(display);

            Debug.Log("Show Hint " + display);


        } else {

            DataScript.interactionMade = false;
        }

    }

    void showHint(int display)
    {
        Vector2 selected = vecListNew[display];
        ps.transform.position = selected;
        ps.Play();

        Debug.Log("I am here");


    }

}
