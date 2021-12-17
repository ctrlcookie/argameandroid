using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnder : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField] GameObject instance;

    public bool spawnitnow;

    public Transform spawnPoint;

    public Vector3 offset;

    public void Start()
    {
        InvokeRepeating("spawn", 0, 10);

    }

    void spawn()
    {
        spawnitnow = GameObject.Find("SPAWN").GetComponent<fuu>().spawn;
        if (spawnitnow)
        {
            Vector3 offset2 = offset * 2;
            instance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, transform);

        }

    }
}
