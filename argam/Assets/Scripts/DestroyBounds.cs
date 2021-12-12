using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBounds : MonoBehaviour
{
    public GameObject moving; //currently selected object (found in SelectionManager)
    public bool aboutToDestroySelection = false; //we about to destroy the above?

    void Update()
    {
        moving = GameObject.Find("Selectionmanager").GetComponent<SelectionManager>().moveableObject; //find what object is selected rn
    }

    void OnTriggerEnter(Collider col) //are we colliding with an object that has a trigger?
    {
        Debug.Log("destroy");

        if (col.gameObject == moving)
        {
            aboutToDestroySelection = true; //broadcast to SelectionManager to change it's selection so we can destroy this object 
            Debug.Log("changing selection");
        }

        Destroy(col.gameObject); //destroy object with trigger collider
        //aboutToDestroySelection = false; //just destroyed the object and therefore already broadcast
    }
}