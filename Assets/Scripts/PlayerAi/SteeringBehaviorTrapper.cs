using System.Collections;
using New_Folder.Bomb;
using Pathfinding;
using UnityEngine;

public class SteeringBehaviorTrapper : MonoBehaviour
{
    private float findFriendFactor = 0f;
    private float fleeFactor = 0f;
    private float patrolFactor = 0f;
    private float attackFactor = 0f;
    
    private AIPath aiPath;
    private float patrolRadius=2f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private BombInitiate bombInitiate;
    [SerializeField] private BombLaunch bombLaunch;
    
    [SerializeField] Animator animatorBomb;
    
    public float distanceToTarget;
    [SerializeField] public float stoppingDistanceThreshold;
    [SerializeField] public float chasingDistance;


    #region Enemy Factors

    public float FindFriendFactor
    {
        get => findFriendFactor;
        set => findFriendFactor = value;
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
        //animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var enemyManager=GetComponentInParent<EnemyManager>();
        target = enemyManager.Player;
        InvokeRepeating("UpdatePath", 0f, 0.75f);
    }

    #region Enemy Behaviors

    private void OnIdle()
    {
        
        aiPath.destination = transform.position;
        bombLaunch.enabled = false;
        bombInitiate.enabled = false;
        animatorBomb.SetBool("IsMoving", false);
        animatorBomb.SetBool("IsAttacking", false);

    }
    private void OnFound()
    {
        //aiPath.destination = friend.position;
        var destination = (transform.position - target.position);
        
        //Flee when the player is far
        aiPath.destination = destination;
        bombLaunch.enabled = false;
        bombInitiate.enabled = false;
        animatorBomb.SetBool("IsMoving", true);
        animatorBomb.SetBool("IsAttacking", false);
       
    }

    private void OnAttack()
    {
        aiPath.destination = transform.position;
        bombLaunch.enabled = true;
        bombInitiate.enabled = false;
        animatorBomb.SetBool("IsMoving", false);
        animatorBomb.SetBool("IsAttacking", true);
        
    }
    private void OnFlee()
    {
        aiPath.maxSpeed = moveSpeed*2;
        var destination = (transform.position - target.position);
        //Flee when the player is far
        aiPath.destination = destination;
        bombLaunch.enabled = false;
        bombInitiate.enabled = true;
        animatorBomb.SetBool("IsMoving", true);
        animatorBomb.SetBool("IsAttacking", false);
    }

    #endregion

    // Update is called once per frame
    void UpdatePath()
    {
        aiPath.maxSpeed = moveSpeed;
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        
        if (findFriendFactor > 0)
        {
            Debug.Log("Find friend");
            OnFound();
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
            Debug.Log("Idle");
            OnIdle();
        }

    }

    
}