using System;
using UnityEngine;

public class SateMachineTrapper : MonoBehaviour
{
    private StateM _currentState = StateM.Empty;
    private SteeringBehaviorTrapper _motionTrapper;

   //[Header("Chase/Attack")] [SerializeField]
   // private bool PlayerIsLook = false;

    //[SerializeField] private bool PlayerIsNotCaC = false;
    //[SerializeField] private bool PlayerIsCaC = false;
    //[Header("Patrol")] [SerializeField] private bool PlayerIsNotLook = false;
    //[SerializeField] private bool PlayerIsNotChasing = false;
    [Header("Flee")] [SerializeField] private bool LowLife = false;
    [SerializeField] private bool NoEnemyInRoom = false;
    [SerializeField] private bool LowLifeTwo = false;


    private enum StateM
    {
        Empty,
        Patrol,
        Attack,
        FindFriends,
        Flee
    }

    #region Enter And Exit State

    private void OnStateEnter(StateM state)
    {
        switch (state)
        {
            case StateM.Patrol:
                _motionTrapper.PatrolFactor = 1;
                break;
            case StateM.Attack:
                _motionTrapper.AttackFactor = 1;
                _motionTrapper.FindFriendFactor = 1;
                break;
            case StateM.FindFriends:
                _motionTrapper.FindFriendFactor = 1;
                break;
            case StateM.Flee:
                _motionTrapper.FleeFactor = 1;
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
                _motionTrapper.PatrolFactor = 0;
                break;
            case StateM.Attack:
                _motionTrapper.AttackFactor = 0;
                _motionTrapper.FindFriendFactor = 0;
                break;
            case StateM.FindFriends:
                _motionTrapper.FindFriendFactor = 0;
                break;
            case StateM.Flee:
                _motionTrapper.FleeFactor = 0;
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
                if (NoEnemyInRoom)
                {
                    SetState(StateM.Flee);
                    NoEnemyInRoom = false;
                }

                if (_motionTrapper.distanceToTarget < _motionTrapper.chasingDistance) //if player is look
                {
                    SetState(StateM.FindFriends);
                    //PlayerIsLook = false;
                }

                break;
            case StateM.FindFriends:
                if (_motionTrapper.distanceToTarget < _motionTrapper.stoppingDistanceThreshold) //if player is near enough to been hit
                {
                    SetState(StateM.Attack);
                   // PlayerIsCaC = false;
                }

                if (_motionTrapper.distanceToTarget > _motionTrapper.chasingDistance) //if player is out of look
                {
                    SetState(StateM.Patrol);
                   // PlayerIsNotLook = false;
                }

                if (LowLife) //if monster has low life

                {
                    SetState(StateM.Flee);
                    LowLife = false;
                }

                break;
            case StateM.Attack:
                if (_motionTrapper.distanceToTarget < _motionTrapper.chasingDistance) //if player can't be hit
                {
                    SetState(StateM.FindFriends);
                   // PlayerIsNotCaC = false;
                }

                if (LowLifeTwo) //if monster is chasing or attacking but has low life
                {
                    SetState(StateM.Flee);
                    LowLifeTwo = false;
                }

                break;
            case StateM.Flee:
                if (_motionTrapper.distanceToTarget > _motionTrapper.chasingDistance) //if player is not chasing monster
                {
                    SetState(StateM.Patrol);
                   // PlayerIsNotChasing = false;
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
        _motionTrapper = GetComponent<SteeringBehaviorTrapper>();
        SetState(StateM.Patrol);
    }


    // Update is called once per frame
    void Update()
    {
        CheckTransition(_currentState);
        OnStateUpdate(_currentState);
    }
}