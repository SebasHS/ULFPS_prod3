using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    Idle, Follow, Attacking
}

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    private float DistanceToFollow = 5;

    private float DistanceToAttack = 1;

    private EnemyState state = EnemyState.Idle;

    private void Update()
    {
        
    } 
    
}
