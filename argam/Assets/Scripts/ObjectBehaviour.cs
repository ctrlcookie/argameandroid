using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public bool isFlammable;
    public bool isBurnable;
    public bool isWettable;
    public bool conductsTemp;
    [Space]
    public bool isCurrentlyDestroyed;
    public bool isCurrentlyOnFire;
    public bool isCurrentlyWet;
    [Space]
    public float currentTemp;
    public float freezingPoint;
    public float boilingPoint;
    public float dryingPoint;
    public float combustionPoint;
    public float burnPoint;

    public float conductivity;

    [Header("for testing purposes (will be deleted)")]
    public Material[] Materials;

    Material mat;

    //-------------------
    bool canConduct = true;
    bool isTouching;
    ObjectBehaviour otherObject;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void FixedUpdate()
    {
        if (isTouching && canConduct)
        {
            StartCoroutine( startTransferringTemp(otherObject));
        }

        float tempNormalized = currentTemp.map(-100, 1000, 0, Materials.Length - 1);
        float tempNormalized2 = currentTemp.map(-100, 1000, 0, 1);//Materials.Length - 1);
        mat.Lerp(mat, Materials[(int)tempNormalized], tempNormalized2);
        mat.SetColor ("

        if (isFlammable)
        {
            if (currentTemp > combustionPoint)
            {
                isCurrentlyOnFire = true;
            }
        }

        if (isBurnable && currentTemp > burnPoint)
        {
            isCurrentlyDestroyed = true;
        }

        if (currentTemp < freezingPoint)
        {
            Freeze();
        }
    }

    IEnumerator startTransferringTemp(ObjectBehaviour other)
    {
        //get the difference in their temps
        float tempDiff = Mathf.Abs(other.currentTemp - currentTemp);
        if (conductsTemp && other.conductsTemp)
        {
            
            canConduct = false;
            
            if (tempDiff > 1)
            {
                if (currentTemp < other.currentTemp) //if our temp is lower than theirs, then increase ours and reduce theirs
                {
                    currentTemp += conductivity;
                    other.currentTemp -= other.conductivity;
                }
                else
                {
                    currentTemp -= conductivity;
                    other.currentTemp += other.conductivity;
                }
            }

            yield return new WaitForSeconds(0.1f);
            canConduct = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.GetComponent<ObjectBehaviour>() != null)
        {
            isTouching = true;
            otherObject = col.gameObject.GetComponent<ObjectBehaviour>();
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.GetComponent<ObjectBehaviour>() != null)
        {
            isTouching = false;
            otherObject = null;
        }
    }

    public void Freeze()
    {
        //become something new
    }
}
