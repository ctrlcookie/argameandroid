using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulp : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool hot;
    public bool burning;

    public GameObject paperPrefab;

    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void Update()
    {
        burning = objectBehaviour.isCurrentlyOnFire;

        if (objectBehaviour.currentTemp >= 50)
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
            Craft("PaperDry");
        }
        else if (collidingWithSelectable)
        {
            if (collidingName == "Salt" || collidingName == "Salt(Clone)")
            {
                Craft("Paper");
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

        if (item == "PaperDry")
        {
            Instantiate(paperPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingpaper");
            Destroy(gameObject);
        }
        else if (item == "Paper")
        {
            Instantiate(paperPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingpaper");
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
