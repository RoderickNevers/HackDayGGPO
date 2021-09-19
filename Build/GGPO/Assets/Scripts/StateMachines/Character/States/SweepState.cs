using System;
using UnityEngine;

public class SweepState : CharacterStateBlock
{
    public SweepState()
    {

    }

        //characterController.HealthComponent.ApplyDamage(stateMachine.Move.AttackData);
        //characterController.HealthComponent.OnResetDizzyLock?.Invoke(this, new EventArgs());
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Hit.SWEEP));

    public override Player UpdatePlayer(Player player, long input)
    {
        return player;
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