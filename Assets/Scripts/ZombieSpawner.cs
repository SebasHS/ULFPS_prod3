using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private float SpawnCooldown = 15f;
    private float canSpawn = -1f;
    [SerializeField] private int cantidad = 2;
    [SerializeField] private GameObject zombie;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Tiempo();
    }

    void Tiempo()
    {
        //Debug.Log("Time: " + Time.time);
        if (canSpawn < Time.time)
        {
            canSpawn = Time.time + SpawnCooldown;
            if(SpawnCooldown != 10)
            {
                SpawnCooldown -= 5f;
            }

            Spawner();
        }
    }

    void Spawner()
    {
        for (int i = 0; i < cantidad; i++)
        {
            Instantiate(zombie, transform.position, transform.rotation);
        }
    }

}
