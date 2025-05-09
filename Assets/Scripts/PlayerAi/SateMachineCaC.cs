using System;
using UnityEngine;

public class SateMachineCaC : MonoBehaviour
{
    private StateM _currentState = StateM.Empty;
    [SerializeField] private SteeringBehaviorCaC _motionCaC;


    

    [Header("Chase/Attack")] [SerializeField]
    private bool PlayerIsLook = false;

    [SerializeField] private bool PlayerIsNotCaC = false;
    [SerializeField] private bool PlayerIsCaC = false;
    [Header("Patrol")] [SerializeField] private bool PlayerIsNotLook = false;
    [SerializeField] private bool PlayerIsNotChasing = false;
    [Header("Flee")] [SerializeField] private bool LowLife = false;
    [SerializeField] private bool NoEnemyInRoom = false;
    [SerializeField] private bool LowLifeTwo = false;


    private enum StateM
    {
        Empty,
        Patrol,
        Attack,
        Chase,
        Flee
    }

    #region Enter And Exit State

    private void OnStateEnter(StateM state)
    {
        switch (state)
        {
            case StateM.Patrol:
                _motionCaC.PatrolFactor = 1;
                break;
            case StateM.Attack:
                _motionCaC.AttackFactor = 1;
                _motionCaC.ChaseFactor = 1;
                break;
            case StateM.Chase:
                _motionCaC.ChaseFactor = 1;
                break;
            case StateM.Flee:
                _motionCaC.FleeFactor = 1;
                break;
            case StateM.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnExitState(StateM state)
    {
        switch (state)
        {
            case StateM.Patrol:
                _motionCaC.PatrolFactor = 0;
                break;
            case StateM.Attack:
                _motionCaC.AttackFactor = 0;
                _motionCaC.ChaseFactor = 0;
                break;
            case StateM.Chase:
                _motionCaC.ChaseFactor = 0;
                break;
            case StateM.Flee:
                _motionCaC.FleeFactor = 0;
                break;
            case StateM.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
                ;
        }
    }

    #endregion

    private void OnStateUpdate(StateM state)
    {
    }

    private void CheckTransition(StateM state)
    {
        switch (state)
        {
            case StateM.Patrol:
                if (NoEnemyInRoom) //if no other enemy in the room
                {
                    SetState(StateM.Flee);
                    NoEnemyInRoom = false;
                }

                if (_motionCaC.distanceToTarget < _motionCaC.chasingDistance) //if player is look 
                {
                    SetState(StateM.Chase);
                    PlayerIsLook = false;
                }

                break;
            case StateM.Chase:
                if (_motionCaC.distanceToTarget < _motionCaC.stoppingDistanceThreshold) //if player is near enough to been hit
                {
                    SetState(StateM.Attack);
                    //PlayerIsCaC = false;
                }

                if (_motionCaC.distanceToTarget > _motionCaC.chasingDistance) //if player is out of look
                {
                    SetState(StateM.Flee);
                    //PlayerIsNotLook = false;
                }

                if (LowLife) //if monster has low life
                {
                    SetState(StateM.Patrol);
                    LowLife = false;
                }

                break;
            case StateM.Attack:
                if (_motionCaC.distanceToTarget > _motionCaC.stoppingDistanceThreshold) //if player can't be hit
                {
                    SetState(StateM.Chase);
                    //PlayerIsNotCaC = false;
                }

                if (LowLifeTwo) //if monster is chasing or attacking but has low life
                {
                    SetState(StateM.Flee);
                    LowLifeTwo = false;
                }

                break;
            case StateM.Flee:
                if (_motionCaC.distanceToTarget > _motionCaC.chasingDistance) //if player is not chasing monster
                {
                    SetState(StateM.Patrol);
                    //PlayerIsNotChasing = false;
                }

                break;

            case StateM.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void SetState(StateM newState)
    {
        //Test for glitch
        if (newState == StateM.Empty) return;

        if (_currentState != StateM.Empty)
        {
            OnExitState(_currentState);
        }

        OnStateEnter(newState);
        _currentState = newState;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _motionCaC = GetComponent<SteeringBehaviorCaC>();
        SetState(StateM.Patrol);
    }


    // Update is called once per frame
    void Update()
    {
        CheckTransition(_currentState);
        OnStateUpdate(_currentState);
    }
}