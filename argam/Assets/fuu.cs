using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fuu : MonoBehaviour
{
    public bool spawn = true;

    public Text text;
    public Image buttonImage;

    public ObjectSpawnder objectSpawn;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }
    public void Button()
    {
        if (spawn)
        {
            text.text = "Start Spawner";
            spawn = false;
            buttonImage.color = Color.green;
        }
        else if (!spawn)
        {
            text.text = "Stop Spawner";
            spawn = true;
            buttonImage.color = Color.red;
            objectSpawn.reset();
        }
    }

}
