using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;
    private readonly int FallHash = Animator.StringToHash("Fall");
    private Vector3 momentum;


    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
        stateMachine.LedgeDetector.OnledgeDetect += HandleLedgeDetect;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);
        if(stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
        }
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.OnledgeDetect -= HandleLedgeDetect;
    }

    private void HandleLedgeDetect(Vector3 ledgeForward, Vector3 closestPoint)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, closestPoint));
    }
}
