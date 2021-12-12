using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    public bool paused = false; //we paused right now?

    public void Pause() //"pause" the game. this basically just sets all time based events to not move 
    {
       paused = true;
       //AudioListener.pause = true;
       Time.timeScale = 0;
    }

    public void Resume() //undo the pausing
    {
        paused = false;
        //AudioListener.pause = false;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
