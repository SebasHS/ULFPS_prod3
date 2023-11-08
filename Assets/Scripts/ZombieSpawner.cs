using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private float SpawnCooldown = 60f;
    private float canSpawn = -1f;
    [SerializeField] private int cantidad = 10;
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject brute;

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
        for (int i = 0; i < cantidad-2; i++)
        {
            Instantiate(zombie, transform.position, transform.rotation);
        }
        for (int i=0; i<2; i++)
        {
            Instantiate(brute, transform.position, transform.rotation);
        }
    }

}
