using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnder : MonoBehaviour
{
    public GameObject chemical;
    public GameObject Seed;
    public GameObject Water;

    public Transform chemicalSpawnPoint;
    public Transform SeedSpawnPoint;
    public Transform WaterSpawnPoint;

    public Transform latestChemical;
    public Transform latestSeed;
    public Transform latestWater;


    public fuu fuu_;

    int timer;

    public float maximumDistance;

    bool ChemicalinRange = true;
    bool seedinRange = true;
    bool waterinRange = true;

    public void Start()
    {
        timer = 5;
        InvokeRepeating("reduceTimer", 0, 1);
    }

    public void reset()
    {
        timer = 5;
        spawn();
    }

    void reduceTimer()
    {
        if (timer == 0)
        {
            timer = 5;
            spawn();
        }
        else
        {
            timer--;
        }
        
    }

    void spawn()
    {
        if (ChemicalinRange)
        {
            latestChemical = Instantiate(chemical, chemicalSpawnPoint.position, chemicalSpawnPoint.rotation, chemicalSpawnPoint).transform;
            latestChemical.name = chemical.name;
        }

        if (waterinRange)
        {
            latestSeed = Instantiate(Seed, SeedSpawnPoint.position, SeedSpawnPoint.rotation, SeedSpawnPoint).transform;
            latestSeed.name = Seed.name;
        }

        if (seedinRange)
        {
            latestWater = Instantiate(Water, WaterSpawnPoint.position, WaterSpawnPoint.rotation, WaterSpawnPoint).transform;
            latestWater.name = Water.name;
        }

        //-------------
        if (latestChemical != null)
        {
            if (Vector3.Distance(latestChemical.position, chemicalSpawnPoint.position) < maximumDistance)
            {
                ChemicalinRange = false;
            }
            else
            {
                ChemicalinRange = true;
            }
        }

        if (latestSeed != null)
        {
            if (Vector3.Distance(latestSeed.position, SeedSpawnPoint.position) < maximumDistance)
            {
                seedinRange = false;
            }
            else
            {
                seedinRange = true;
            }
        }

        if (latestWater != null)
        {
            if (Vector3.Distance(latestWater.position, WaterSpawnPoint.position) < maximumDistance)
            {
                waterinRange = false;
            }
            else
            {
                waterinRange = true;
            }
        }
    }
}