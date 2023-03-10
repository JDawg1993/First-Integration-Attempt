using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;
    private readonly int PullUpHash = Animator.StringToHash("PullUp");

    private readonly Vector3 OffSet = new Vector3(0f, 0.25f, 0.65f);


    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine){ }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator, "Climbing") < 1f) {return;}

        stateMachine.CharacterController.enabled = false;
        stateMachine.transform.Translate(OffSet, Space.Self);
        stateMachine.CharacterController.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }

    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }

}
