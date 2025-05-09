using System;
using UnityEngine;

public class StateMachineSummon : MonoBehaviour
{
    private StateM _currentState = StateM.Empty;
    private SteeringBehaviorSummon _motionSummon;

    //[Header("Chase/Attack")] [SerializeField]
    // private bool PlayerIsLook = false;

    //[SerializeField] private bool PlayerIsNotCaC = false;
    //[SerializeField] private bool PlayerIsCaC = false;
    //[Header("Patrol")] [SerializeField] private bool PlayerIsNotLook = false;
    //[SerializeField] private bool PlayerIsNotChasing = false;
    [SerializeField] private float timeFlee = 2f;
    private float _timerFlee;


    private enum StateM
    {
        Empty,
        Idle,
        Attack,
        Chase,
        Flee
    }

    private void CheckTransition(StateM state)
    {
        switch (state)
        {
            case StateM.Idle:
                if (_motionSummon.LowLife && _motionSummon.distanceToTarget < _motionSummon.AttackFactor)
                {
                    SetState(StateM.Flee);
                }

                else if (_motionSummon.distanceToTarget < _motionSummon.attackDistance) //if player is look
                {
                    SetState(StateM.Attack);
                    //PlayerIsLook = false;
                }

                break;
            case StateM.Chase:
                if (_motionSummon.LowLife && _motionSummon.distanceToTarget < _motionSummon.AttackFactor)
                {
                    SetState(StateM.Flee);
                }
                else if (_motionSummon.distanceToTarget <
                         _motionSummon.stoppingDistanceThreshold) //if player is near enough to been hit
                {
                    SetState(StateM.Attack);
                    // PlayerIsCaC = false;
                }

                break;
            case StateM.Attack:
                if (_motionSummon.LowLife && _motionSummon.distanceToTarget < _motionSummon.AttackFactor)
                {
                    SetState(StateM.Flee);
                }
                else if (_motionSummon.distanceToTarget > _motionSummon.attackDistance) //if player can't be hit
                {
                    SetState(StateM.Chase);
                    // PlayerIsNotCaC = false;
                }

                break;
            case StateM.Flee:
                if (_timerFlee >= timeFlee) //if player is not chasing monster
                {
                    _timerFlee = 0;
                    SetState(StateM.Idle);
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

    #region Enter And Exit State

    private void OnStateEnter(StateM state)
    {
        switch (state)
        {
            case StateM.Idle:
                _motionSummon.IdleFactor = 1;
                break;
            case StateM.Attack:
                _motionSummon.AttackFactor = 1;
                // _motionSummon.ChaseFactor = 1;
                break;
            case StateM.Chase:
                _motionSummon.ChaseFactor = 1;
                break;
            case StateM.Flee:
                _motionSummon.FleeFactor = 1;
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
            case StateM.Idle:
                _motionSummon.IdleFactor = 0;
                break;
            case StateM.Attack:
                _motionSummon.AttackFactor = 0;
                _motionSummon.ChaseFactor = 0;
                break;
            case StateM.Chase:
                _motionSummon.ChaseFactor = 0;
                break;
            case StateM.Flee:
                _motionSummon.FleeFactor = 0;
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
        switch (_currentState)
        {
            case StateM.Empty:
                break;
            case StateM.Idle:
                break;
            case StateM.Attack:
                break;
            case StateM.Chase:
                break;
            case StateM.Flee:
                _timerFlee += Time.deltaTime;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _motionSummon = GetComponent<SteeringBehaviorSummon>();
        SetState(StateM.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTransition(_currentState);
        OnStateUpdate(_currentState);
    }
}