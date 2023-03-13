using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class RPGPlayerBaseState : RPGState
{
    protected RPGPlayerStateMachine rpgStateMachine;


    public RPGPlayerBaseState(RPGPlayerStateMachine rpgStateMachine)
    {
        this.rpgStateMachine = rpgStateMachine;
    }
    [Command]
    protected void CmdMove(float deltaTime)
    {
        CmdMove(Vector3.zero, deltaTime);
    }
    [Command]
    protected void CmdMove(Vector3 motion, float deltaTime)
    {
        rpgStateMachine.CharacterController.Move((motion + rpgStateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        if (rpgStateMachine.Targeter.CurrentTarget == null) { return; }
        Vector3 lookPos = rpgStateMachine.Targeter.CurrentTarget.transform.position - rpgStateMachine.transform.position;
        lookPos.y = 0f;

        rpgStateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        if (rpgStateMachine.Targeter.CurrentTarget != null)
        {
            rpgStateMachine.SwitchState(new RPGPlayerTargetingState(rpgStateMachine));
        }
        else
        {
            rpgStateMachine.SwitchState(new RPGPlayerFreeLookState(rpgStateMachine));
        }
    }
}
