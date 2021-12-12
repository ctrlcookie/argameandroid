using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject moveableObject;
    [SerializeField] public Material highlightMaterial;
    [SerializeField] public Material defaultMaterial;
    [SerializeField] public Material currentSelectionMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    public Transform objectHitDebug;
    bool selectin = false;


    private Transform _selection;
    // Update is called once per frame
    void FixedUpdate()
    {
        var moveableRenderer = moveableObject.GetComponent<Renderer>();
        moveableRenderer.material = currentSelectionMaterial;


        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;

            _selection = null;

        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {

            objectHitDebug = hit.transform;

            Debug.DrawLine(ray.origin, hit.point);

            var selection = hit.transform;


            if (selection.CompareTag(selectableTag))
            {
                Debug.Log("Correct tag");

                if (selectin)
                {
                    Debug.Log("Selecting");
                    moveableRenderer.material = defaultMaterial;
                    moveableObject = selection.gameObject;
                }

                var selectionRenderer = selection.GetComponent<Renderer>();

                if (selectionRenderer != null)
                {
                    defaultMaterial = selection.GetComponent<Renderer>().material;
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectin = true;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectin = false;

        }
    }
}
