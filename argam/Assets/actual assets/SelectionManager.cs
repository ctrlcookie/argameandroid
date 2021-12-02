using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject moveableObject;
    [SerializeField] public Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private string selectableTag = "Selectable";

    private Transform _selection;
    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            moveableObject = selection.gameObject;

            if (selection.CompareTag(selectableTag))
            {
              var selectionRenderer = selection.GetComponent<Renderer>();

              if (selectionRenderer != null)
              { 
                selectionRenderer.material = highlightMaterial;
              }

                _selection = selection;
            }
        }
    }
}
