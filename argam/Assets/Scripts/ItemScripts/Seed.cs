using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool hot;
    public bool cold;
    public bool burning;
    public bool wet;

    public GameObject plantPrefab;
    public GameObject mintPrefab;
    public GameObject gingerPrefab;
    public GameObject pepperPrefab;
    public GameObject lemonPrefab;
    public GameObject lotusPrefab;
    public GameObject cactusPrefab;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void Update()
    {
        if(objectBehaviour.currentTemp >= 50)
        {
            hot = true;
        }
        else if (objectBehaviour.currentTemp <= -20)
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
    }



    void FixedUpdate() //all possible craftables
    {
        if (collidingWithSelectable)
        {
            if (collidingName == "Water" || collidingName == "Water(Clone)")
            {
                Craft("Water");
            }
            if (collidingName == "Chemical" || collidingName == "Chemical(Clone)")
            {
                Craft("Chemical");
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

        if (item == "Water")
        {
            if (hot && wet)
            {
                Instantiate(cactusPrefab, this.transform.position + offset, Quaternion.identity, parent);
                Debug.Log("makingcactus");
                DestroyBoth();
            }
            else if(hot)
            {
                Instantiate(gingerPrefab, this.transform.position + offset, Quaternion.identity, parent);
                Debug.Log("makingginger");
                DestroyBoth();
            }
            else if (cold)
            {
                Instantiate(mintPrefab, this.transform.position + offset, Quaternion.identity, parent);
                Debug.Log("makingmint");
                DestroyBoth();
            }
            else if (burning)
            {
                Instantiate(pepperPrefab, this.transform.position + offset, Quaternion.identity, parent);
                Debug.Log("makingpepper");
                DestroyBoth();
            }
            else if (wet)
            {
                Instantiate(lotusPrefab, this.transform.position + offset, Quaternion.identity, parent);
                Debug.Log("makinglotus");
                DestroyBoth();
            }
            else
            {
                Instantiate(plantPrefab, this.transform.position + offset, Quaternion.identity, parent);
                Debug.Log("makingplant");
                DestroyBoth();
            }
        } 
        else if (item == "Chemical")
        {
            Instantiate(lemonPrefab, this.transform.position + offset, Quaternion.identity, parent);
            DestroyBoth();
        }
        else
        {
            Debug.Log("Can't craft" + item);
        }
    }

    public void DestroyBoth()
    {
        GameObject.Find("DestroyZone").GetComponent<DestroyBounds>().aboutToDestroySelection = true;
        Destroy(collidingObject);
        Destroy(gameObject);
    }
}
