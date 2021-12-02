using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public bool isSolid; //for boiling and freezing purposes
    public bool isLiquid;
    public bool isGas;
    public bool isConstantHeat;
    [Space]
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
    public float baseTemp;
    public float freezingPoint;
    public float boilingPoint;
    public float dryingPoint;
    public float combustionPoint;
    public float burnPoint;
    [Space]
    public float baseConductivity;
    public float fireConductivity = 1;
    public float wetConductivity;

    [Space]
    public GameObject Fire;
    public GameObject fireInstance;

    [Header("for testing purposes (will be deleted)")]
    public Gradient Materials;

    Material mat;

    //-------------------
    float conductivity;

    bool canConduct = true;
    bool isTouching;
    bool hasaddedFire;
    ObjectBehaviour otherObject;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void FixedUpdate()
    {
        if (isTouching && canConduct)
        {
            StartCoroutine(startTransferringTemp(otherObject));
        }

        float tempNormalized = currentTemp.map(-100, 1000, 0, 1);
        float tempNormalized2 = currentTemp.map(-100, 1000, 0, 1);//Materials.Length - 1);

        if (!gameObject.CompareTag("Plate"))
        {
            mat.color = Materials.Evaluate(tempNormalized);
        }

        //mat.Lerp(mat, Materials[(int)tempNormalized], tempNormalized2);
        //mat.SetColor ("

        if (isFlammable)
        {
            if (currentTemp > combustionPoint)
            {
                isCurrentlyOnFire = true;
                if (!hasaddedFire)
                {
                    hasaddedFire = true;
                    GameObject fireInstance = Instantiate(Fire, transform.position, transform.rotation).gameObject;
                    fireInstance.transform.parent = transform;
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.CompareTag("Fire") == true)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }
            }

            if (isTouching && isCurrentlyOnFire) //fire heats shit up faster
            {
                conductivity = fireConductivity;
            }
            else
            {
                conductivity = baseConductivity; //otherwise normal conductivity
            }
        }

        if (isTouching && otherObject.isCurrentlyWet && isSolid && isWettable) //sets a drying point if made wet
        {
            isCurrentlyWet = true;
            dryingPoint = baseTemp + 20;
        }

        if (isCurrentlyWet && dryingPoint <= currentTemp) //dries objects
        {
            isCurrentlyWet = false;
        }

        if (isBurnable && currentTemp > burnPoint) //destroys burnables
        {
            isCurrentlyDestroyed = true;
        }

        if (currentTemp < freezingPoint) //freezes
        {
            Freeze();
        }

        if (isCurrentlyWet || isLiquid) //changes conductivity
        {
            conductivity = wetConductivity;
        }

        else
        {
            conductivity = baseConductivity;
        }

        if (isConstantHeat) //checks if it's a heater/freezer
        {
            currentTemp = baseTemp;
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
