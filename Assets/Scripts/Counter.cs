using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject Four;
    public GameObject Five;
    public GameObject Six;
    public GameObject Seven;
    public GameObject Eight;
    public GameObject Nine;
    public GameObject Ten;

    private GameObject[] objectArray;
    private List<GameObject> objList;

    public Text CounterText;



    // Start is called before the first frame update
    void Start()
    {
        objectArray = new GameObject[]
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten
           // Rest go here
        };

        objList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        objList.Clear();

        // Checks if interactable still exists and puts it in a new list if it does
        foreach (GameObject i in objectArray)
        {
            if (i != null)
            {
                // If Interactable has been interacted with it should destroy its position Object making it equal to null
                objList.Add(i);
                //vecList now contains a List of the Positions of all interactables that have not been destroyed yet
            }
        }

        CounterText.text = "Du hast " + (10 - objList.Count) + " von 10 Informationen gefunden";
    }
}
