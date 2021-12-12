using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class WinScreenText : MonoBehaviour
{
    public float winTime;
    public TMP_Text textComponent;
    [SerializeField] public GameObject myTextgameObject; // gameObject in Hierarchy


    // Start is called before the first frame update
    void Start()
    {
        textComponent = myTextgameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        winTime = GameObject.Find("GameStateManager").GetComponent<TimeChecker>().timeElapsed;

        textComponent.text = "Time elapsed in game: " + Mathf.RoundToInt(winTime) + "s";

    }
}
