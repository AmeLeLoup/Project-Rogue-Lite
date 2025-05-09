using System.Collections;
using Pathfinding;
using UnityEngine;

public class SteeringBehaviorSummon : MonoBehaviour
{
    private float chaseFactor = 0f;
    private float fleeFactor = 0f;
    private float idleFactor = 0f;
    private float attackFactor = 0f;

    private AIPath aiPath;
    private float patrolRadius = 2f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;

    public float distanceToTarget;
    [SerializeField] public float stoppingDistanceThreshold;
    [SerializeField] public float attackDistance;
    [Header("Flee")] public bool LowLife = false;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject firePoint;
    private GameObject _currentFireball;
    private EnemyManager _enemyManager;

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

    public float IdleFactor
    {
        get => idleFactor;
        set => idleFactor = value;
    }

    public float AttackFactor
    {
        get => attackFactor;
        set => attackFactor = value;
    }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        _enemyManager=GetComponentInParent<EnemyManager>();
        target = _enemyManager.Player;
        InvokeRepeating("UpdatePath", 0f, 0.75f);
    }

    #region Enemy Behaviors

    private void OnIdle()
    {
       //animation
    }

    private void OnChase()
    {
        aiPath.destination = target.position;
    }

    private void OnAttack()
    {
        aiPath.destination = transform.position;

        if (_currentFireball == null)
        {
            Vector2 direction = (target.position - firePoint.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentFireball = Instantiate(fireBall, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
            _currentFireball.GetComponent<FireBallAttack>().SetEnemyManager(_enemyManager);
        }

        ;
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
        }

        if (attackFactor > 0)
        {
            Debug.Log("Attack");
            OnAttack();
        }

        if (fleeFactor ! > 0)
        {
            Debug.Log("Flee");
            OnFlee();
        }

        if (idleFactor > 0)
        {
            Debug.Log("Idle");
            OnIdle();
        }
    }
}