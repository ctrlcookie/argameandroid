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
        }
        else if (wet)
        {
            Craft("Sailor's Story");
        }
        else if (cold)
        {
            Craft("Thriller");
        }


        if (collidingWithSelectable && this.name == "Book" || collidingWithSelectable && this.name == "Book(Clone")
        {
            if (collidingName == "Ink" || collidingName == "Ink(Clone)" || collidingName == "Charcoal" || collidingName == "Charcoal(Clone)")
            {
                Craft("Noir");
            }
            if (collidingName == "Pepper" || collidingName == "Pepper(Clone)" || collidingName == "Ginger" || collidingName == "Ginger(Clone)")
            {
                Craft("Spicy Love Story");
            }
            if (collidingName == "Chemical" || collidingName == "Chemical(Clone)")
            {
                Craft("Scifi");
            }
            if (collidingName == "Salt" || collidingName == "Salt(Clone)")
            {
                Craft("Sailor's Story");
            }
            if (collidingName == "Ice" || collidingName == "Ice(Clone)")
            {
                Craft("Thriller");
            }
            if (collidingName == "Seed" || collidingName == "Plant" || collidingName == "Plant(Clone)")
            {
                Craft("Botany Guide");
            }
            if (collidingName == "Pulp" || collidingName == "Pulp(Clone)")
            {
                Craft("Pulp Magazine");
            }
            if (collidingName == "Glue" || collidingName == "Glue(Clone)")
            {
                Craft("Book You Just Can't Put Down");
            }
            if (collidingName == "Cactus" || collidingName == "Cactus(Clone)")
            {
                Craft("Wild West Tales");
            }
            if (collidingName == "Lotus" || collidingName == "Lotus(Clone)")
            {
                Craft("Journey To The West");
            }
            if (collidingName == "Paper" || collidingName == "Paper(Clone)")
            {
                Craft("Newspaper");
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
