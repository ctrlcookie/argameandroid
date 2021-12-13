using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public Transform parent;

    private bool collidingWithSelectable;
    public string collidingName;
    private GameObject collidingObject;

    Vector3 offset = new Vector3(0f, 5f, 0f);

    [SerializeField]public string teaName;

    public GameObject teaPrefab;

    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
        parent = GameObject.Find("items").transform;
    }

    void FixedUpdate() //all possible craftables
    {
        if (collidingWithSelectable)
        {
            if (this.name == "Ice" || this.name == "Ice(Clone)")
            {
                if (collidingName == "Tea Bag" || collidingName == "Tea Bag(Clone)")
                {
                    Craft("Tea");
                }
            }
            else if (collidingName == "Hot Water" || collidingName == "Hot Water(Clone)" )
            {
                Craft("Tea");
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

        if (item == "Tea")
        {
            GameObject newTea = Instantiate(teaPrefab, this.transform.position + offset, Quaternion.identity, parent);
            newTea.name = teaName;
            Debug.Log("makingtea");
            DestroyBoth();
        }
    }

    public void DestroyBoth()
    {
        GameObject.Find("DestroyZone").GetComponent<DestroyBounds>().aboutToDestroySelection = true;
        Destroy(collidingObject);
        Destroy(gameObject);
    }
}
