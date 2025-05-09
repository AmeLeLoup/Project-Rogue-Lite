using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }


    public Rigidbody2D rb { get; set; }
    public bool IsFacingRight { get; set; } = true;

    #region State Machine Variables

    //public EnemyStateMachine StateMachine { get; set; }
    // public EnemyIdleState IdleState { get; set; }
    // public EnemyChaseState ChaseState { get; set; }
    // public EnemyAttackState AttackState { get; set; }

    #endregion

    #region Idle Variables

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    #endregion

    // private void Awake()
    // {
    //     StateMachine = new EnemyStateMachine();
    //     IdleState = new EnemyIdleState(this, StateMachine);
    //     ChaseState = new EnemyChaseState(this, StateMachine);
    //     AttackState = new EnemyAttackState(this, StateMachine);
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = MaxHealth;

        rb = GetComponent<Rigidbody2D>();

        //StateMachine.Initialize(IdleState);
    }

    // private void Update()
    // {
    //     StateMachine.CurrentEnemyState.FrameUpdate();
    // }
    //
    // private void FixedUpdate()
    // {
    //     StateMachine.CurrentEnemyState.PhysicsUpdate();
    // }

    #region Damage/Die

    //Health and death of the enemy
    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Movement/Facing

    //Enemy Movement
    public void MoveEnemy(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x <= 0f)
        {
            Vector3 rotor = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotor);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotor = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotor);
            IsFacingRight = !IsFacingRight;
        }
    }

    #endregion

    #region Animation Triggers

    // private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    // {
    //     StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    // }
    //
    // public enum AnimationTriggerType
    // {
    //     EnemyDamaged,
    //     PlayFootstepSound,
    // }

    #endregion
}