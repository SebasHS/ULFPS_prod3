using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    public float health = 100;

    private bool brokenShield = false;
    public float shield = 50;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        if (!brokenShield)
        {
            shield -= damage;
            if (shield <= 0)
            {
                health += shield;
                brokenShield = true;
                shield = 0f;
            }
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                Debug.Log("Te moriste");
                Destroy(gameObject);
            }
        }

    }
}