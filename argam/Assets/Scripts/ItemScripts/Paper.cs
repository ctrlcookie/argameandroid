using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;


    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool wet;

    public GameObject pulpPrefab;
    public GameObject bookPrefab;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void Update()
    {
        wet = objectBehaviour.isCurrentlyWet;
    }

    void FixedUpdate() //all possible craftables
    {
        if (wet)
        {
            Craft("Pulp");
        }

        if (collidingWithSelectable)
        {
            if (collidingName == "Ink" || collidingName == "Ink(Clone)" || collidingName == "Glue" || collidingName == "Glue(Clone)")
            {
                Craft("Book");
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

        if (item == "Pulp")
        {
            Instantiate(pulpPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingpulp");
            Destroy(gameObject);
        }
        else if (item == "Book")
        {
            Instantiate(bookPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingBook");
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
