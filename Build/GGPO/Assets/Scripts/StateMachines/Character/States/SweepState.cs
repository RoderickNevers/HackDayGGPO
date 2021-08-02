using System;
using UnityEngine;

public class SweepState : CharacterStateBlock
{
    public SweepState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Sweep)
            .Permit(CharacterStateTrigger.TriggerOnTheGround, CharacterState.OnTheGround)
            .Permit(CharacterStateTrigger.TriggerDizzy, CharacterState.Dizzy)
            .Permit(CharacterStateTrigger.TriggerKO, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }
    protected override void OnEnterState()
    {
        base.OnEnterState();

        //characterController.HealthComponent.ApplyDamage(stateMachine.Move.AttackData);
        //characterController.HealthComponent.OnResetDizzyLock?.Invoke(this, new EventArgs());
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Hit.SWEEP));
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    //protected override void HandleAnimationComplete()
    //{
    //    base.HandleAnimationComplete();

    //    if (characterController.HealthComponent.IsAlive)
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerOnTheGround);
    //    }
    //    else
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerKO);
    //    }
    //}
}