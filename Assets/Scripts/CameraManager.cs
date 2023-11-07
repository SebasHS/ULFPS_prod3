using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Camera.main.transform.localPosition = new Vector3(-0.004f, 1.563f, 0.418f);
        
    }
}
