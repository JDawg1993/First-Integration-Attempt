using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class RPGStateMachine : NetworkBehaviour
{
    private RPGState currentState;


    public void SwitchState(RPGState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
