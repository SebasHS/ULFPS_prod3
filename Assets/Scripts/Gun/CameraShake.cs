using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake MyInstance;

    private void Awake() {
        MyInstance=this;
    }

    public IEnumerator Shake(AnimationCurve curve, float ShakeTime)
    {
        Vector3 StartPosition = transform.position;
        float TimeUsed = 0f;
        while (TimeUsed<ShakeTime){
            TimeUsed += Time.deltaTime;
            float strenght = curve.Evaluate(TimeUsed/ShakeTime);
            transform.position=StartPosition+Random.insideUnitSphere* strenght;
            yield return null;

        }

        transform.position=StartPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
