using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerDodgingState : RPGPlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");

    private Vector3 dodgingDirectionInput;
    private float remainingDodgeTime;

    private const float CrossFadeDuration = 0.1f;


    public RPGPlayerDodgingState(RPGPlayerStateMachine rpgStateMachine, Vector3 dodgingDirectionInput) : base(rpgStateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = rpgStateMachine.DodgeDuration;

        rpgStateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirectionInput.y);
        rpgStateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirectionInput.x);
        rpgStateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

        rpgStateMachine.Health.SetInvulnerable(true);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += rpgStateMachine.transform.right * dodgingDirectionInput.x * rpgStateMachine.DodgeDistance / rpgStateMachine.DodgeDuration;
        movement += rpgStateMachine.transform.forward * dodgingDirectionInput.y * rpgStateMachine.DodgeDistance / rpgStateMachine.DodgeDuration;

        CmdMove(movement, deltaTime);
        FaceTarget();

        remainingDodgeTime -= deltaTime;
        if (remainingDodgeTime <= 0f)
        {
            rpgStateMachine.SwitchState(new RPGPlayerTargetingState(rpgStateMachine));
        }
    }

    public override void Exit()
    {
        
    }


}
