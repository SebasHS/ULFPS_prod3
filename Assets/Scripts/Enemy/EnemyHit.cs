using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 20;
    [SerializeField] private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void recieveHit()
    {
        Debug.Log("En: " + enemyHealth);
        enemyHealth -= 5;
        if(enemyHealth <= 0)
        {
            Destroy(enemy);
        }
    }
}
