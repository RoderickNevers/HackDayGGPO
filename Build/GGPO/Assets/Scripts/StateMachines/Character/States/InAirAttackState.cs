using System;
using UnityEngine;

public class InAirAttackState : CharacterStateBlock
{
    public InAirAttackState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    //protected override void HandleAnimationComplete()
    //{
    //    CompleteAttack();
    //}

    //protected override void HandleSpecialMoveComplete(object sender, EventArgs e)
    //{
    //    base.HandleSpecialMoveComplete(sender, e);

    //    CompleteAttack();
    //}

    //private void CompleteAttack()
    //{
    //    stateMachine.ResetAttackID();

    //    if (characterController.IsGrounded)
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //    }
    //    else
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerFalling);
    //    }
    //}
}