using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool burning;
    public bool hot;

    public GameObject charcoalPrefab;
    public GameObject pulpPrefab;
    public GameObject driedLeavesPrefab;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void Update()
    {
        burning = objectBehaviour.isCurrentlyOnFire;

        if (objectBehaviour.currentTemp >= 100)
        {
            hot = true;
        }
        else if (burning)
        {
            hot = true;
        }
    }



    void FixedUpdate() //all possible craftables
    {
        if (hot)
        {
            Craft("Charcoal");
        }

        if (collidingWithSelectable)
        {
            if (collidingName == "Hot Water" || collidingName == "Hot Chemical")
            {
                Craft("Pulp");
            }
            if (collidingName == "Salt" || collidingName == "Salt(Clone)")
            {
                Craft("Dried Leaves");
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

        if (item == "Charcoal")
        { 
            Instantiate(charcoalPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingcharcoal");
            Destroy(gameObject);
        }
        else if (item == "Pulp")
        {
            Instantiate(pulpPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingpulp");
            DestroyBoth();
        }
        else if (item == "Dried Leaves")
        {
            Instantiate(driedLeavesPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingdriedleaves");
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
