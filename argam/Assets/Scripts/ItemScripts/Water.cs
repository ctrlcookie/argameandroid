using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Transform parent;
    public ObjectBehaviour objectBehaviour;
    public string collidingName;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool hot;
    public bool cold;

    public GameObject icePrefab;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void Update()
    {
        if (objectBehaviour.currentTemp >= 40)
        {
            hot = true;
            this.name = "Hot Water";
        }
        else
        {
            hot = false;
            this.name = "Water";
        }
        cold = objectBehaviour.isCurrentlyFrozen;
    }

    void FixedUpdate() //all possible craftables
    {
        if (cold)
        {
            Craft("Ice");
        }
    }

    void OnTriggerEnter(Collider col) //are we tryna craft
    {
        if (col.gameObject.tag == "Selectable")
        {
            collidingName = col.gameObject.name;
        }
    }

    private void Craft(string item)
    {
        Debug.Log("Crafting" + item);

        if (item == "Ice")
        {
            Instantiate(icePrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingice");
            Destroy(gameObject);
        }
  
    }

}
