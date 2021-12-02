using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] public SelectionManager manager;


    public void OnButtonPress()
    {
        if (gameObject.name == "Upbutton")
        {
            Debug.Log("Up clicked ");
        }
        if (gameObject.name == "Downbutton")
        {
            Debug.Log("Down clicked ");
        }
        if (gameObject.name == "Leftbutton")
        {
            Debug.Log("Left clicked ");
        }
        if (gameObject.name == "Rightbutton")
        {
            Debug.Log("Right clicked ");
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
