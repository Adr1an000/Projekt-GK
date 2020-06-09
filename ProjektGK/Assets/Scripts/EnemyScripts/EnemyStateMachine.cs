using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// author: Paweł Salicki

public class EnemyStateMachine : MonoBehaviour
{
    public Dictionary<Type, EnemyBaseState> enemyStates; // dictionary of enemy states

    public EnemyBaseState CurrentState { get; set; } // current enemy state

    public event Action<EnemyBaseState> OnStateChanged; // event state enemy changed

    public LookAtPlayer LookAtPlayer;

    // set enemy state
    public void SetState(Dictionary<Type, EnemyBaseState> _states)
    {
        enemyStates = _states;
    }

    // Update is called once per frame
    private void Update()
    {

        if (CurrentState == null) // set state
        {
            CurrentState = enemyStates[typeof(SpawnState)];
        }

        if (CurrentState == enemyStates[typeof(SpawnState)])
        {
            LookAtPlayer.enabled = false;
        } 
        else
        {
            LookAtPlayer.enabled = true;
        }

        Type nextState = CurrentState?.StatePerform(); // get next state

        if (nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = enemyStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
