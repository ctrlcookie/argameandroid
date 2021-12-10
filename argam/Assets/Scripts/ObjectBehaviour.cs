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
    public float fireConductivityMuliplier = 5; // how much to affect our normal conductvity
    public float wetConductivityMultiplier = 0.5f;
    float fireConductivity;
    float wetConductivity;

    [Space]
    public GameObject Fire;
    public GameObject fireInstance;

    [Header("for testing purposes (will be deleted)")]
    public Gradient Materials;
    public AnimationCurve emmisionAmount;
    public AnimationCurve tempDifferenceMulitplerCurve;

    Material mat;

    //-------------------
    [SerializeField] float conductivity;
    [SerializeField] float tempDifferenceMultiplier;
    [SerializeField] float ambientTemperatureLossAmount;

    bool canConduct = true;
    bool isTouching;
    bool hasaddedFire;
    ObjectBehaviour otherObject;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_EMISSION");

        currentTemp = baseTemp;
    }

    void FixedUpdate()
    {
        if (!isConstantHeat)
        {
            ambientTemperatureLossAmount = (tempDifferenceMulitplerCurve.Evaluate(Mathf.Abs(currentTemp - 20)) * 200) / 100000;
            //ambient temperature loss
            if (currentTemp > 20)
            {
                currentTemp -= ambientTemperatureLossAmount;
                //currentTemp -= 0.001f;
            }
            else
            {
                currentTemp += ambientTemperatureLossAmount;
            }//room temp
            

            fireConductivity = baseConductivity * fireConductivityMuliplier;
            wetConductivity = baseConductivity * wetConductivityMultiplier;
            if (isTouching && canConduct)
            {
                StartCoroutine(startTransferringTemp(otherObject));
            }

            float tempNormalized = currentTemp.map(-200, 1300, 0, 1);
            float tempNormalized2 = currentTemp.map(-100, 1000, 0, 1);//Materials.Length - 1);


            mat.color = Materials.Evaluate(tempNormalized);
            mat.SetColor("_EmissionColor", Materials.Evaluate(tempNormalized) * emmisionAmount.Evaluate(currentTemp));


            //mat.Lerp(mat, Materials[(int)tempNormalized], tempNormalized2);
            //mat.SetColor ("

            //create flames
            if (isFlammable)
            {
                if (currentTemp > combustionPoint)
                {
                    isCurrentlyOnFire = true;
                    if (!hasaddedFire)
                    {
                        //create an instance of the fire prefab (then stop more prefabs from being created with teh "has added fire" bool.
                        hasaddedFire = true;
                        GameObject fireInstance = Instantiate(Fire, transform.position, transform.rotation).gameObject;
                        fireInstance.transform.parent = transform;
                    }
                }
                else
                {
                    //once we drop below our combustion temp, then check all of the children of this object to
                    //see if there is a fire prefab there. if so, Get rid of it.
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).gameObject.CompareTag("Fire") == true)
                        {
                            Destroy(transform.GetChild(i).gameObject);
                        }
                    }
                }                
            }

            if (otherObject != null) //check if we are currently in contact with another object
            {
                //fire
                if (otherObject.isCurrentlyOnFire)
                {
                    conductivity = fireConductivity;
                }
                else if (isCurrentlyWet)
                {
                    conductivity = wetConductivity;
                }
                else
                {
                    conductivity = baseConductivity;
                }

                if (otherObject.isCurrentlyWet && isSolid && isWettable) //sets a drying point if made wet
                {
                    isCurrentlyWet = true;
                    // dryingPoint = baseTemp + 20;
                }
            }
            else
            {
                if (!isCurrentlyWet)
                {
                    conductivity = baseConductivity;
                }
                else
                {
                    conductivity = wetConductivity;
                }
                //we shouldnt become dry unless we arent touching anything
                if (isCurrentlyWet && dryingPoint <= currentTemp) //dries objects
                {
                    isCurrentlyWet = false;
                }
            }

            if (isBurnable && currentTemp > burnPoint) //destroys burnables
            {
                destroy();
            }

            if (currentTemp < freezingPoint) //freezes
            {
                Freeze();
            }
        }
        else { currentTemp = baseTemp; }
    }

    IEnumerator startTransferringTemp(ObjectBehaviour other)
    {
        //get the difference in their temps

        float tempDiff = Mathf.Abs(other.currentTemp - currentTemp);

        tempDifferenceMultiplier = tempDifferenceMulitplerCurve.Evaluate(tempDiff);

        if (conductsTemp && other.conductsTemp)
        {
            canConduct = false;

            if ((!other.isConstantHeat || !isConstantHeat) && tempDiff > 1000)
            {
                destroy();
            }

            if (tempDiff > 0.1f)
            {
                if (currentTemp < other.currentTemp) //if our temp is lower than theirs, then increase ours and reduce theirs
                {
                    currentTemp += conductivity * tempDifferenceMultiplier;
                    other.currentTemp -= other.conductivity * other.tempDifferenceMultiplier;
                }
                else
                {

                    currentTemp -= conductivity * tempDifferenceMultiplier;
                    other.currentTemp += other.conductivity * other.tempDifferenceMultiplier;
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

    void destroy()
    {
        // <instansiate ash object>
        mat.color = Color.black;
        mat.SetColor("_EmissionColor", Color.black);
        Destroy(this);
    }

    public void Freeze()
    {
        //become something new
    }
}
