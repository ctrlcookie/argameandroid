using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charcoal : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool wet;

    public GameObject inkPrefab;

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
            Craft("WetInk");
        }
        if (collidingWithSelectable)
        {
            if (collidingName == "Hot Water" || collidingName == "Hot Water(Clone)")
            {
                Craft("Ink");
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

        if (item == "WetInk")
        {

            Instantiate(inkPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingink");
            Destroy(gameObject);
        }
        else if (item == "Ink")
        {
            Instantiate(inkPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingink");
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