using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private void Update() 
    {
        Debug.DrawRay(
            transform.position, 
            transform.forward * 10f, 
            Color.red
        );
    }

    public void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out hit,
            10f
        )){
            // Choco con algo
            Debug.Log(hit.collider.transform.name);
            Quaternion lookAt = Quaternion.LookRotation(hit.normal);
            GameObject temp = Resources.Load<GameObject>("BulletCollision");
            var obj = Instantiate(
                temp, 
                hit.point, 
                lookAt);
            
            Destroy(obj, 2f);
            
        }

    }
}
