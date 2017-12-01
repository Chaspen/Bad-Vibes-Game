using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public int patrolRange;
    public int attackRange;
    public int shootRange;
    public Transform vision;
    public float alertTime;

    public GameObject missile;
    public AudioClip missileShoot;
    public float missileDamage;
    public float missileSpeed;

    public bool onlyMelee = false;
    public float meleeDamage;
    public float attackDelay;

    public LayerMask raycastMask;

    public GameObject wp;
    public List<Transform> waypoints;

    Animator anim;
    GameManager gameManager;

    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public IEnemyAI currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Vector3 lastKnownPosition;
    [HideInInspector] public AudioSource audio;

    void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

    }

    void Start()
    {
        GameObject wp1 = (GameObject)Instantiate(wp, this.transform.position + new Vector3(-3, 0, 3), Quaternion.identity, GameObject.Find("Map").transform);
        GameObject wp2 = (GameObject)Instantiate(wp, this.transform.position + new Vector3(3, 0, -3), Quaternion.identity, GameObject.Find("Map").transform);

        waypoints.Add(wp1.GetComponent<Transform>());
        waypoints.Add(wp2.GetComponent<Transform>());

        navMeshAgent.Warp(wp2.transform.position);

        currentState = patrolState;
    }

    void Update()
    {
        if (!gameManager.isDead)
        {
            currentState.UpdateActions();
        }

        if (currentState == attackState)
        {
            anim.SetBool("isAttacking", true);
        }
        else
            anim.SetBool("isAttacking", false);
    }

    void OnTriggerEnter(Collider otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }

    void HiddenShot(Vector3 shotPosition)
    {
        lastKnownPosition = shotPosition;
        currentState = alertState;
    }
}
