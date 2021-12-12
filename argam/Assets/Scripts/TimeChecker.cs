using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{

    public float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed = Time.time;
    }
}
