using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    #region States
    public IdleState IdleState;
    public FollowState FollowState;
    private State currentState;
    #endregion

    #region Parameters
    public Transform Player;
    public float DistanceToFollow = 4f;
    public float DistanceToAttack = 3f;
    public float Speed = 1f;
    public Transform FirePoint;
    public float CoolDownTime = 1.0f;

    private float attackCooldown = 2f;
    private float canAttack = -1f; 
    private GameObject child1;
    #endregion

    #region Readonly Properties
    public Rigidbody rb { private set; get; }
    public Animator animator { private set; get; }
    public NavMeshAgent agent { private set; get; }
    #endregion

    public int maxHealth = 1000;
    private int currentHealth;

    public AnimationCurve curve;
    public float ShakeTime;


    private void Awake()
    {
        IdleState = new IdleState(this);
        FollowState = new FollowState(this);

        rb = GetComponent<Rigidbody>();
        child1 = this.transform.GetChild(0).gameObject;
        animator = child1.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Seteamos el estado inicial
        currentState = IdleState;
    }

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        currentState.OnStart();
        currentHealth = maxHealth;
        Debug.Log("currentHealth inicio"+currentHealth);
        
    }

    private void Update()
    {
        foreach (var transition in currentState.Transitions)
        {
            if (transition.IsValid())
            {
                // Ejecutar Transicion
                currentState.OnFinish();
                currentState = transition.GetNextState();
                currentState.OnStart();
                break;
            }
        }
        currentState.OnUpdate();
    }

    public void AttackEnemy()
    {
        RaycastHit hit;
        
        //Debug.Log("Atacando!");
        if (Physics.Raycast(
            FirePoint.transform.position,
            FirePoint.transform.forward,
            out hit,
            5f
        ))
        {
            //Debug.Log("Enemy hit: " + hit.collider.transform.name);
            if (hit.collider.transform.name == "Player" && Time.time > canAttack)
            {
                canAttack = Time.time + attackCooldown;
                Debug.Log("Arrañar: " + this.transform.name);
                if(this.transform.name == "En1(Clone)")
                {
                    PlayerHealth.Instance.TakeDamage(5f);
                    CameraShake.MyInstance.StartCoroutine(CameraShake.MyInstance.Shake(curve, ShakeTime));
                }
                else if(this.transform.name == "En2(Clone)")
                {
                    PlayerHealth.Instance.TakeDamage(10f);
                }

            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Restar el daño a la salud actual
        Debug.Log("currentHealth"+currentHealth);
        Debug.Log("damgeada"+damage);
        currentHealth -= damage;

        // Comprobar si la salud ha llegado a cero o menos
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
