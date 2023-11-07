using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public float destroyTime = 1.0f;

    void Start()
    {
        // Destruye este objeto después de 'destroyTime' segundos
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
