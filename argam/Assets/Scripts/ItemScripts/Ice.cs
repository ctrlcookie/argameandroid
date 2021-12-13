using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public ObjectBehaviour objectBehaviour;
    public string collidingName;


    void Start()
    {
        objectBehaviour = GetComponent<ObjectBehaviour>();
    }

    void Update()
    {
        if (objectBehaviour.currentTemp >= 0)
        {
            Debug.Log("icemelted");
            Destroy(gameObject);
        }

    }

}
