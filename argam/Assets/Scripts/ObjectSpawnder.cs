using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnder : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField] GameObject instance;
    [SerializeField] GameObject itemsparent;


    [SerializeField] public bool spawnitnow;
    [SerializeField] public bool intheway;

    public Transform spawnPoint;

    public Vector3 offset;

    public void Start()
    {
        InvokeRepeating("spawn", 0, 10);
    }

    void spawn()
    {
        //spawnitnow = GameObject.Find("SPAWN").GetComponent<fuu>().spawn;
        if (spawnitnow && !intheway)
        {
            Vector3 offset2 = offset * 2;
            instance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, itemsparent.transform);

        }

    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("intrigger");

        if (col.gameObject.tag == "Selectable")
        {
            intheway = true;
        }
        else
        {
            intheway = false;

        }
    }

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("intriggerstay" + col.gameObject.name);

        if (col.gameObject.tag == "Selectable")
        {
            intheway = true;
        }
        else
        {
            intheway = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("exitingtrigger");
        if (col.gameObject.tag == "Selectable")
        {
            intheway = false;
        }
    }
}