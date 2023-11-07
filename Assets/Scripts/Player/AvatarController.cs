using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AvatarController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        animator = GetComponent<Animator>();
    }

    public void IsWalking(bool value)
    {
        animator.SetBool("IsWalking", value);
    }
}
