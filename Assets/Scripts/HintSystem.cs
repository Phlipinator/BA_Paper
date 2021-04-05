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

    private GameObject[] objectArray;
    private List<Vector2> vecList;
    private List<Vector2> vecListNew;



    void Start()
    {
        timer = targetTime;

        objectArray = new GameObject[]
        {
            One,
            Two,
            Three,
            Four,
            Five
           // Rest go here
        };

        vecList = new List<Vector2>();
        vecListNew = new List<Vector2>();

        cam = UnityEngine.Camera.main;
    }

    void Update()
    {
        //TODO: Check if lists are empty!!!!


        vecList.Clear();
        vecListNew.Clear();

        // Checks if interactable still exists and puts it in a new list if it does
        foreach (GameObject i in objectArray)
        {
            if (i != null)
            {
                // If Interactable has been interacted with it should destroy its position Object making it equal to null
                vecList.Add(i.transform.position);
                //vecList now contains a List of the Positions of all interactables that have not been destroyed yet
            }
        }

        
        // Checks if the interatable can be seen by the camera
        foreach (Vector2 j in vecList)
        {
            Vector3 current3 = new Vector3(j.x, j.y, 0f);
            Vector3 screenPoint = cam.WorldToViewportPoint(current3);

            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                vecListNew.Add(j);
            }
        }
        


        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        //Starts Timer again
        targetTime = timer;
   

        if (DataScript.interactionMade == false && !vecListNew.Count.Equals(0))
        {

            showHint();

        } else {

            DataScript.interactionMade = false;
        }

    }

    void showHint()
    {
        int display = Random.Range(0, vecListNew.Count - 1);
        Vector2 selected = vecListNew[display];
        ps.transform.position = selected;
        ps.Play();

        Debug.Log("Showing Hint at: " + display);

    }

}
