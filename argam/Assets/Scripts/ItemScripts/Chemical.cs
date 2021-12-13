using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemical : MonoBehaviour
{
    private ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;


    Vector3 offset = new Vector3(0f, 5f, 0f);

    public bool hot;
    public bool cold;

    public GameObject saltPrefab;
    public GameObject gluePrefab;
    public GameObject pulpPrefab;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void Update()
    {

        if (objectBehaviour.currentTemp >= 50)
        {
            hot = true;
            this.name = "Hot Chemical";
        }
        else if (objectBehaviour.currentTemp <= -20)
        {
            cold = true;
            this.name = "Cold Chemical";
        }
        else
        {
            this.name = "Chemical";
            hot = false;
            cold = false;
        }
    }


    void FixedUpdate() //all possible craftables
    {
        if (collidingWithSelectable)
        {
            if (this.name == "Hot Chemical" ) //is this item hot
            {
                if (collidingName == "Cold Chemical" || collidingName == "Cold Chemical(Clone)")
                {
                    Craft("Salt");
                }
            }
            else if (this.name == "Chemical" ||  this.name == "Chemical(Clone)")
            {
                if (collidingName == "Salt" || collidingName == "Salt(Clone)" || collidingName == "Hot Water" || collidingName == "Hot Water(Clone)")
                {
                    Craft("Glue");
                }
                else if (collidingName == "Glue" || collidingName == "Glue(Clone)")
                {
                    Craft("Pulp");
                }
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

        if (item == "Salt")
        {
            Instantiate(saltPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingsalt");
            DestroyBoth();
        }
        else if (item == "Glue")
        {
            Instantiate(gluePrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingglue");
            DestroyBoth();
        }
        else if (item == "Pulp")
        {
            Instantiate(pulpPrefab, this.transform.position + offset, Quaternion.identity, parent);
            Debug.Log("makingpulp");
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