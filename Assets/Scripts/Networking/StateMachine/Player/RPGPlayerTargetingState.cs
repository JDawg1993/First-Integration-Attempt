using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerTargetingState : RPGPlayerBaseState
{
    public RPGPlayerTargetingState(RPGPlayerStateMachine rpgStateMachine) : base(rpgStateMachine)
    {
    }

    public override void Enter()
    {
        rpgStateMachine.InputReader.TargetEvent += OnTarget;
        rpgStateMachine.InputReader.DodgeEvent += OnDodge;
        rpgStateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        
    }
    public override void Exit()
    {
        rpgStateMachine.InputReader.TargetEvent -= OnTarget;
        rpgStateMachine.InputReader.DodgeEvent -= OnDodge;
        rpgStateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        rpgStateMachine.SwitchState(new RPGPlayerJumpingState(rpgStateMachine));
    }

    private void OnDodge()
    {
        if (rpgStateMachine.InputReader.MovementValue == Vector2.zero) { return; }
        rpgStateMachine.SwitchState(new RPGPlayerDodgingState(rpgStateMachine, rpgStateMachine.InputReader.MovementValue));
    }

    private void OnTarget()
    {
        rpgStateMachine.Targeter.Cancel();
        rpgStateMachine.SwitchState(new RPGPlayerFreeLookState(rpgStateMachine));
    }
}
