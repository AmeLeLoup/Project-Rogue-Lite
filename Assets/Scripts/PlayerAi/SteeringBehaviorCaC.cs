using System.Collections;
using Pathfinding;
using UnityEngine;

public class SteeringBehaviorCaC : MonoBehaviour
{
    private float chaseFactor = 0f;
    private float fleeFactor = 0f;
    private float patrolFactor = 1f;
    private float attackFactor = 0f;

    private AIPath aiPath;
    private float patrolRadius = 2f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    

    public float distanceToTarget;
    [SerializeField] public float stoppingDistanceThreshold;
    [SerializeField] public float chasingDistance;
    [SerializeField] public Animator animatorCaC;
    [SerializeField]private CaCWeaponAnimManager animManager;
    //[SerializeField] public Animator animatorweaponCaC;


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
        animatorCaC = GetComponent<Animator>();
        var enemyManager=GetComponentInParent<EnemyManager>();
        target = enemyManager.Player;
    }

    #region Enemy Behaviors

    private void OnPatrol()
    {
        animatorCaC.SetBool("IsMoving", true);
        animManager.canAttack = false;
        Vector3 randomOffset = Random.insideUnitCircle * patrolRadius;
        aiPath.destination = transform.position + randomOffset;
    }

    private void OnChase()
    {
        animatorCaC.SetBool("IsMoving", true);
        animManager.canAttack = false;
        if (distanceToTarget > stoppingDistanceThreshold)
        {
            aiPath.destination = target.position;
        }
        else
        {
            aiPath.destination = transform.position;
        }
    }

    private void OnAttack()
    {
        animatorCaC.SetBool("IsMoving", false);
        animManager.canAttack = true;
        if (distanceToTarget > stoppingDistanceThreshold)
        {
            aiPath.destination = target.position;
        }
        else
        {
            aiPath.destination = transform.position;
        }
    }

    private void OnFlee()
    {
        animatorCaC.SetBool("IsMoving", true);
        animManager.canAttack = false;
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