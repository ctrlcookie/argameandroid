using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBounds : MonoBehaviour
{
    public GameObject moving;

    void Update()
    {
        moving = GameObject.Find("Selectionmanager").GetComponent<SelectionManager>().moveableObject;

    }


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("destroy");

        if (col.gameObject == moving)
        {
            Debug.Log("oops");
        }

        Destroy(col.gameObject);
    }
}
