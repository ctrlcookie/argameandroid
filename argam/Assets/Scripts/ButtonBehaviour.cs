using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool turnoffsfx = false;
    public GameObject moving;

    void FixedUpdate() //what object are we moving (taken from Selection Manager)
    {
        moving = GameObject.Find("Selectionmanager").GetComponent<SelectionManager>().moveableObject;
    }

    public void OnButtonPress() //this is incredibly subpar but my little brain decided spaghetti code is okay if my braincells aint working -J
    {
        if (gameObject.name == "Upbutton") //move "up"
        {
            moving.transform.Translate(Vector3.forward * Time.deltaTime);
            //Debug.Log("Up clicked ");
        }
        if (gameObject.name == "Downbutton") //move "down"
        {
            moving.transform.Translate(Vector3.back * Time.deltaTime);
            //Debug.Log("Down clicked ");
        }
        if (gameObject.name == "Leftbutton") //move "left"
        {
            moving.transform.Translate(Vector3.left * Time.deltaTime);
            //Debug.Log("Left clicked ");
        }
        if (gameObject.name == "Rightbutton") //move "right"
        {
            moving.transform.Translate(Vector3.right * Time.deltaTime);
            //Debug.Log("Right clicked ");
        }
        
    }

    public void PlayClick()
    {
        if (!turnoffsfx)
        {
            FindObjectOfType<AudioManager>().Play("buttonclick");
        }
    }

    public void PauseMusic()
    {
        FindObjectOfType<AudioManager>().Pause("BackgroundMusic");
    }

    public void PlayMusic()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
    }

    public void TurnOffSfx()
    {
        turnoffsfx = true;
    }

    public void TurnOnSfx()
    {
        turnoffsfx = false;

    }
}
