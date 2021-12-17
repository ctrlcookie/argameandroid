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
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().spicy = true;

        }
        else if (wet)
        {
            Craft("Sailor's Story");
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().sailor = true;
        }
        else if (cold)
        {
            Craft("Thriller");
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().thrill = true;
        }


        if (collidingWithSelectable && this.name == "Book" || collidingWithSelectable && this.name == "Book(Clone")
        {
            if (collidingName == "Ink" || collidingName == "Ink(Clone)" || collidingName == "Charcoal" || collidingName == "Charcoal(Clone)")
            {
                Craft("Noir");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().noir = true;
            }
            if (collidingName == "Pepper" || collidingName == "Pepper(Clone)" || collidingName == "Ginger" || collidingName == "Ginger(Clone)")
            {
                Craft("Spicy Love Story");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().spicy = true;
            }
            if (collidingName == "Chemical" || collidingName == "Chemical(Clone)")
            {
                Craft("Scifi");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().scifi = true;
            }
            if (collidingName == "Salt" || collidingName == "Salt(Clone)")
            {
                Craft("Sailor's Story");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().sailor = true;
            }
            if (collidingName == "Ice" || collidingName == "Ice(Clone)")
            {
                Craft("Thriller");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().thrill = true;
            }
            if (collidingName == "Seed" || collidingName == "Plant" || collidingName == "Plant(Clone)")
            {
                Craft("Botany Guide");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().botany = true;
            }
            if (collidingName == "Pulp" || collidingName == "Pulp(Clone)")
            {
                Craft("Pulp Magazine");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().pulp = true;
            }
            if (collidingName == "Glue" || collidingName == "Glue(Clone)")
            {
                Craft("Book You Just Can't Put Down");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().putdown = true;
            }
            if (collidingName == "Cactus" || collidingName == "Cactus(Clone)")
            {
                Craft("Wild West Tales");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().wild = true;
            }
            if (collidingName == "Lotus" || collidingName == "Lotus(Clone)")
            {
                Craft("Journey To The West");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().journey = true;
            }
            if (collidingName == "Paper" || collidingName == "Paper(Clone)")
            {
                Craft("Newspaper");
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().news = true;
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
