using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class EnemyStateMachine : MonoBehaviour
{
    private Dictionary<Type, EnemyBaseState> enemyStates; // dictionary of enemy states

    public EnemyBaseState CurrentState { get; private set; } // current enemy state

    public event Action<EnemyBaseState> OnStateChanged; // event state enemy changed

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
            CurrentState = enemyStates.Values.First();
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
