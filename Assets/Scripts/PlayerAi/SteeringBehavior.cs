using System.Collections;
using Pathfinding;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    private float chaseFactor = 0f;
    private float fleeFactor = 0f;
    private float patrolFactor = 0f;
    private float attackFactor = 0f;
    
    private AIPath aiPath;
    private float patrolRadius=2f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
   
    public float distanceToTarget;
    [SerializeField] public float stoppingDistanceThreshold;
    [SerializeField] public float chasingDistance;


    #region Enemy Factors

    public float ChaseFactor
    {
        get => chaseFactor;
        set => chaseFactor = value;
    }

    public float FleeFactor
    {
        get => fleeFactor;
        set => fleeFactor = value;
    }

    public float PatrolFactor
    {
        get => patrolFactor;
        set => patrolFactor = value;
    }

    public float AttackFactor
    {
        get => attackFactor;
        set => attackFactor = value;
    }

    #endregion

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.75f);
    }

    #region Enemy Behaviors

    private void OnPatrol()
    {
        
        Vector3 randomOffset = Random.insideUnitCircle * patrolRadius;
        aiPath.destination = transform.position + randomOffset;
        
    }
    private void OnChase()
    {
        aiPath.destination = target.position;
    }

    private void OnAttack()
    {
        aiPath.destination = target.position;
    }
    private void OnFlee()
    {
        var destination = (transform.position - target.position);
        //Flee when the player is far
        aiPath.destination = destination;
    }

    #endregion

    // Update is called once per frame
    void UpdatePath()
    {
        aiPath.maxSpeed = moveSpeed;
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        
        if (chaseFactor > 0)
        {
            Debug.Log("Chase");
            OnChase();
            if (attackFactor > 0)
            {
                Debug.Log("Attack");
                OnAttack();
            }
        }
        if (fleeFactor ! > 0)
        {
            Debug.Log("Flee");
            OnFlee();
        }
       
        if (patrolFactor > 0)
        {
            Debug.Log("Patrol");
            OnPatrol();
        }

    }

    
}