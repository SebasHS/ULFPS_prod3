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
    private float DistanceToFollow = 15;

    private float DistanceToAttack = 3;

    private EnemyState state = EnemyState.Idle;

    private void Update()
    {
        
    } 
    
}
