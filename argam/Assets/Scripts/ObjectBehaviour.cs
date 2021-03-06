using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public bool isSolid; //for boiling and freezing purposes
    public bool isLiquid;
    public bool isGas;
    public bool isConstantHeat; //for heating platforms and ambient temperatures
    [Space]
    public bool isFlammable; //basic vars, pretty self explanatory
    public bool isBurnable;
    public bool isWettable;
    public bool conductsTemp;
    [Space]
    public bool isCurrentlyDestroyed; 
    public bool isCurrentlyOnFire;
    public bool isCurrentlyWet;
    public bool isCurrentlyFrozen;
    [Space]
    public float currentTemp; //things to monitor
    public float baseTemp;
    public float freezingPoint;
    public float boilingPoint;
    public float dryingPoint;
    public float combustionPoint;
    public float burnPoint;
    [Space]
    public float baseConductivity; //this item's base conductivity
    public float fireConductivityMuliplier = 5; // how much to affect our normal conductvity on fire
    public float wetConductivityMultiplier = 0.5f; // how much to affect our normal conductvity when wet
    float fireConductivity; 
    float wetConductivity;

    [Space] //we on fire?
    public GameObject Fire;
    public GameObject fireInstance;

    [Space]
    public Color[] BaseColour;
    public Gradient Materials;
    public AnimationCurve emmisionAmount;
    public AnimationCurve tempDifferenceMulitplerCurve;

    public MeshRenderer[] rend;
    public List<Material> mat;

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
        if (!isConstantHeat)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                //mat[i] = rend[i].material;
                mat.Add(rend[i].material);
                mat[i].EnableKeyword("_EMISSION");
                BaseColour[i] = mat[i].color;
            }           
        }

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
            

            fireConductivity = baseConductivity * fireConductivityMuliplier; //handle cond diff
            wetConductivity = baseConductivity * wetConductivityMultiplier; 

            if (isTouching && canConduct) //transferring own temp
            {
                StartCoroutine(startTransferringTemp(otherObject));
            }

            float tempNormalized = currentTemp.map(-200, 1300, 0, 1);

            for (int i = 0; i < mat.Count; i++)
            {
                if (currentTemp > 100 || currentTemp < 0)
                {

                    mat[i].color = Materials.Evaluate(tempNormalized);
                }
                else
                {

                    mat[i].color = BaseColour[i];

                }

                mat[i].SetColor("_EmissionColor", Materials.Evaluate(tempNormalized) * emmisionAmount.Evaluate(currentTemp));
            }

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

                    isCurrentlyOnFire = false;
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

                if (otherObject.isCurrentlyWet && isSolid && isWettable) //sets a drying point if made wet, handle this differently
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
                isCurrentlyFrozen = true;
                Freeze();
            }
            else
            {
                isCurrentlyFrozen = false;
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
    
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.GetComponent<ObjectBehaviour>() != null)
        {
            isTouching = true;
            otherObject = col.gameObject.GetComponent<ObjectBehaviour>();
        }
    }

    void OnTriggerExit(Collider col)
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
        for (int i = 0; i < mat.Count; i++)
        {
            mat[i].color = Color.black;
            mat[i].SetColor("_EmissionColor", Color.black);
        }

        Debug.Log("destroying " + this.gameObject.name);
        //Destroy(gameObject);
    }

    public void Freeze()
    {
        //become something new
    }
}
