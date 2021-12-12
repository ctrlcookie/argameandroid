using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject moving;

    void FixedUpdate()
    {
        moving = GameObject.Find("Selectionmanager").GetComponent<SelectionManager>().moveableObject;
    }

    //GameObject moving = script.moveableObject;

    public void OnButtonPress()
    {
        if (gameObject.name == "Upbutton")
        {
            moving.transform.Translate(Vector3.forward * Time.deltaTime);
            Debug.Log("Up clicked ");
        }
        if (gameObject.name == "Downbutton")
        {
            moving.transform.Translate(Vector3.back * Time.deltaTime);
            Debug.Log("Down clicked ");
        }
        if (gameObject.name == "Leftbutton")
        {
            moving.transform.Translate(Vector3.left * Time.deltaTime);
            Debug.Log("Left clicked ");
        }
        if (gameObject.name == "Rightbutton")
        {
            moving.transform.Translate(Vector3.right * Time.deltaTime);
            Debug.Log("Right clicked ");
        }
        
    }


}
