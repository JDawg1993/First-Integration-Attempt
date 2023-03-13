using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerImpactState : RPGPlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;
    private readonly int ImpactHash = Animator.StringToHash("Impact");

    private float duration = 1f;

    public RPGPlayerImpactState(RPGPlayerStateMachine rpgStateMachine) : base(rpgStateMachine)
    {
    }

    public override void Enter()
    {
        rpgStateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        CmdMove(deltaTime);
        duration -= deltaTime;
        if (duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit(){}
}
