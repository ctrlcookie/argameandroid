using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnder : MonoBehaviour
{
    public GameObject SeedPrefab;
    public GameObject waterPrefab;
    public GameObject chemicalPrefab;

    [SerializeField] GameObject seedInstance;
    [SerializeField] GameObject waterInstance;
    [SerializeField] GameObject chemicalInstance;

    public Vector3 offset;

    public void Start()
    {
        Vector3 offset2 = offset * 2;
        seedInstance = Instantiate(SeedPrefab, transform.position, transform.rotation, transform);
        waterInstance = Instantiate (waterPrefab, transform.position + offset, transform.rotation, transform);
        chemicalInstance = Instantiate (chemicalPrefab, transform.position + offset2, transform.rotation, transform);
    }
}
