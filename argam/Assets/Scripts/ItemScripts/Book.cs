using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool hot;
    public bool cold;
    public bool burning;
    public bool wet;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
    }

    void Update()
    {
        if (objectBehaviour.currentTemp >= 100 || burning)
        {
            hot = true;
        }
        else if (objectBehaviour.currentTemp <= -150)
        {
            cold = true;
        }
        else
        {
            hot = false;
            cold = false;
        }

        wet = objectBehaviour.isCurrentlyWet;
        burning = objectBehaviour.isCurrentlyOnFire;
        cold = objectBehaviour.isCurrentlyFrozen;
    }



    void FixedUpdate() //all possible craftables
    {
        if (hot)
        {
            Craft("Spicy Love Story");
            GameObject.Find("GameScore").GetComponent<GameScore>().spicyLoveStory = true;
        }
        else if (wet)
        {
            Craft("Sailor's Story");
            GameObject.Find("GameScore").GetComponent<GameScore>().sailorsStory = true;
        }
        else if (cold)
        {
            Craft("Thriller");
            GameObject.Find("GameScore").GetComponent<GameScore>().thriller = true;
        }


        if (collidingWithSelectable && this.name == "Book" || collidingWithSelectable && this.name == "Book(Clone")
        {
            if (collidingName == "Ink" || collidingName == "Ink(Clone)" || collidingName == "Charcoal" || collidingName == "Charcoal(Clone)")
            {
                Craft("Noir");
                GameObject.Find("GameScore").GetComponent<GameScore>().noir = true;

            }
            if (collidingName == "Pepper" || collidingName == "Pepper(Clone)" || collidingName == "Ginger" || collidingName == "Ginger(Clone)")
            {
                Craft("Spicy Love Story");
                GameObject.Find("GameScore").GetComponent<GameScore>().spicyLoveStory = true;
            }
            if (collidingName == "Chemical" || collidingName == "Chemical(Clone)")
            {
                Craft("Scifi");
                GameObject.Find("GameScore").GetComponent<GameScore>().sciFi = true;
            }
            if (collidingName == "Salt" || collidingName == "Salt(Clone)")
            {
                Craft("Sailor's Story");
                GameObject.Find("GameScore").GetComponent<GameScore>().sailorsStory = true;
            }
            if (collidingName == "Ice" || collidingName == "Ice(Clone)")
            {
                Craft("Thriller");
                GameObject.Find("GameScore").GetComponent<GameScore>().thriller = true;
            }
            if (collidingName == "Seed" || collidingName == "Plant" || collidingName == "Plant(Clone)")
            {
                Craft("Botany Guide");
                GameObject.Find("GameScore").GetComponent<GameScore>().botanyGuide = true;
            }
            if (collidingName == "Pulp" || collidingName == "Pulp(Clone)")
            {
                Craft("Pulp Magazine");
                GameObject.Find("GameScore").GetComponent<GameScore>().pulpMagazine = true;
            }
            if (collidingName == "Glue" || collidingName == "Glue(Clone)")
            {
                Craft("Book You Just Can't Put Down");
                GameObject.Find("GameScore").GetComponent<GameScore>().cantPutDown = true;
            }
            if (collidingName == "Cactus" || collidingName == "Cactus(Clone)")
            {
                Craft("Wild West Tales");
                GameObject.Find("GameScore").GetComponent<GameScore>().wildWestTales = true;
            }
            if (collidingName == "Lotus" || collidingName == "Lotus(Clone)")
            {
                Craft("Journey To The West");
                GameObject.Find("GameScore").GetComponent<GameScore>().journeyToTheWest = true;
            }
            if (collidingName == "Paper" || collidingName == "Paper(Clone)")
            {
                Craft("Newspaper");
                GameObject.Find("GameScore").GetComponent<GameScore>().newspaper = true;
            }
        }
    }

    void OnTriggerEnter(Collider col) //are we tryna craft
    {
        if (col.gameObject.tag == "Selectable")
        {
            collidingWithSelectable = true;
            collidingName = col.gameObject.name;
            collidingObject = col.gameObject;
        }
        else
        {
            collidingWithSelectable = false;
        }
    }

    private void Craft(string item)
    {
        Debug.Log("Crafting" + item);

        this.name = item;
    }
}
