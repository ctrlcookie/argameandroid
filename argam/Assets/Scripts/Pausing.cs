using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    public bool paused = false;

    public void Pause()
    {
       paused = true;
       Time.timeScale = 0;

    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
