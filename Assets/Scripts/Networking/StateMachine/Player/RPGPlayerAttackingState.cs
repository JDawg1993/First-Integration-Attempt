using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayerAttackingState : RPGPlayerBaseState
{
    private float previousFrameTime;
    private Attack attack;
    private bool alreadyAppliedForce;

    public RPGPlayerAttackingState(RPGPlayerStateMachine rpgStateMachine, int attackIndex) : base(rpgStateMachine)
    {
        attack = rpgStateMachine.Attacks[attackIndex];
    }
    public override void Enter()
    {
        rpgStateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        rpgStateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        CmdMove(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(rpgStateMachine.Animator, "Attack");
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime > attack.ForceTime)
            {
                TryApplyforce();
            }
            if (rpgStateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
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
        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }
        if (normalizedTime < attack.ComboAttackTime) { return; }
        rpgStateMachine.SwitchState(
            new RPGPlayerAttackingState(
                rpgStateMachine,
                attack.ComboStateIndex
            )
        );
    }
    private void TryApplyforce()
    {
        if (alreadyAppliedForce) { return; }
        rpgStateMachine.ForceReceiver.AddForce(rpgStateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }

}
