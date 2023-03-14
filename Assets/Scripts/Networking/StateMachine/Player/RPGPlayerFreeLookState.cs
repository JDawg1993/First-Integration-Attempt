using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerFreeLookState : RPGPlayerBaseState
{
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendHash = Animator.StringToHash("FreeLookBlendTree");

    private bool shouldFade;

    public RPGPlayerFreeLookState(RPGPlayerStateMachine rpgStateMachine, bool shouldFade = true) : base(rpgStateMachine)
    {
        this.shouldFade = shouldFade;
    }


    public override void Enter()
    {
        rpgStateMachine.InputReader.TargetEvent += OnTarget;
        rpgStateMachine.InputReader.JumpEvent += OnJump;
        rpgStateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);
        if (shouldFade)
        {
            rpgStateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendHash, CrossFadeDuration);
        }
        else
        {
            rpgStateMachine.Animator.Play(FreeLookBlendHash);
        }

    }

    public override void Tick(float deltaTime)
    {

        if (rpgStateMachine.InputReader.IsAttacking)
        {
            rpgStateMachine.SwitchState(new RPGPlayerAttackingState(rpgStateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();

        CmdMove(movement * rpgStateMachine.FreeLookMovementSpeed, deltaTime);

        if (rpgStateMachine.InputReader.MovementValue == Vector2.zero)
        {
            rpgStateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);

            return;
        }
        rpgStateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        rpgStateMachine.InputReader.TargetEvent -= OnTarget;
        rpgStateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        rpgStateMachine.SwitchState(new RPGPlayerJumpingState(rpgStateMachine));
    }

    private void OnTarget()
    {
        if (!rpgStateMachine.Targeter.SelectTarget()) { return; }
        rpgStateMachine.SwitchState(new RPGPlayerTargetingState(rpgStateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = rpgStateMachine.MainCameraTransform.forward;
        Vector3 right = rpgStateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * rpgStateMachine.InputReader.MovementValue.y +
            right * rpgStateMachine.InputReader.MovementValue.x;

    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        rpgStateMachine.transform.rotation = Quaternion.Lerp(
            rpgStateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * rpgStateMachine.RotationDamping);
    }
}

