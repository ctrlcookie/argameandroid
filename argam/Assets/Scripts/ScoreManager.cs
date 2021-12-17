using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]public GameObject endScreen;
    [SerializeField]public GameObject betterEndScreen;

    public bool spicy = false;
    public bool sailor = false;
    public bool thrill = false;
    public bool noir = false;
    public bool scifi = false;
    public bool botany = false;
    public bool pulp = false;
    public bool putdown = false;
    public bool wild = false;
    public bool journey = false;
    public bool news = false;

    public bool tea = false;
    public bool book = false;


    // Update is called once per frame
    void Update()
    {
        if (spicy || sailor || thrill || noir || scifi || botany || pulp || putdown || wild || journey || news)
        {
            book = true;
        }

        if (tea && book)
        {
            endScreen.SetActive(true);
        }

        if (spicy && sailor && thrill && noir && scifi && botany && pulp && putdown && wild && journey && news && tea)
        {
            betterEndScreen.SetActive(true);
        }
    }
}