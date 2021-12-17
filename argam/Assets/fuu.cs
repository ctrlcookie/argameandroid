using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuu : MonoBehaviour
{
    public bool spawn = true;
    public void Button()
    {
        if (spawn)
        {
            spawn = false;
        }
        else if (!spawn)
        {
          spawn = true;
        }
    }

}
