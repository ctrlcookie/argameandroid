using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject moveableObject; //current selected object
    [SerializeField]  public GameObject defaultObject; //object to default to when not selecting anything else/deleting old selection

    [SerializeField] public Material highlightMaterial; //material for when we're hovering over an item
    [SerializeField] public Material defaultMaterial; //variable to store what the object's material was before hovering/selection
    [SerializeField] public Material currentSelectionMaterial; //material used to show that an item is a current selection (aka visual of what is the moveableObject)
    [SerializeField] private string selectableTag = "Selectable"; //tag for how we determine what is selectable, ideally we'd be filtering by layer but oh well
    public Transform objectHitDebug; //debug variable showing what we're currently hitting with our raycast
    public bool switchSelect; //boolean taken from DestroyBounds to see if we need to switch selection
    bool selectin = false; //is the player attempting to select right now
    private Transform _selection; // our selection's transform component

    void FixedUpdate()
    {
        var moveableRenderer = moveableObject.GetComponent<Renderer>(); //current selections' renderer
        moveableRenderer.material = currentSelectionMaterial; //change objects' material to show that it's selected


        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;

            _selection = null;

        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //raycast from camera

        RaycastHit hit; //var for what are we hitting

        if (Physics.Raycast(ray, out hit, 1000)) //the raycasting
        {

            objectHitDebug = hit.transform; //what are we hitting right now?
            Debug.DrawLine(ray.origin, hit.point); //debug line in editor that shows visually where the raycast is going

            var selection = hit.transform; //transform of whatever the raycast is hitting


            if (selection.CompareTag(selectableTag)) //does the transform's object have the right tag?
            {
                //Debug.Log("Correct tag");

                if (selectin) //is the player trying to select?
                {
                    //Debug.Log("Selecting");
                    moveableRenderer.material = defaultMaterial; //change previously selected's material to it's stored default ("unselecting" the old object)
                    moveableObject = selection.gameObject; //change the now selected object from the old one
                }

                var selectionRenderer = selection.GetComponent<Renderer>();  //get selection's renderer

                if (selectionRenderer != null) //are we hovering over a selectable object?
                {
                    defaultMaterial = selection.GetComponent<Renderer>().material; //store the object's default material
                    selectionRenderer.material = highlightMaterial; //highlight the material as part of the hover
                }

                _selection = selection; //we selectin?
            }
        }
    }

    void Update()
    {   
        //is the player attempting to select?
        if (Input.GetMouseButtonDown(0))
        {
            selectin = true;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectin = false;
        }

        //check if we're about to delete a selected object
        switchSelect = GameObject.Find("DestroyZone").GetComponent<DestroyBounds>().aboutToDestroySelection;

        //if we are about to delete a selected object, switch the selected object to a hidden on under the stage 
        //to prevent an error of trying to fetch an item that doesnt exist
        if (switchSelect)
        {
            moveableObject = defaultObject;
            GameObject.Find("DestroyZone").GetComponent<DestroyBounds>().aboutToDestroySelection = false;
        }
    }
}
