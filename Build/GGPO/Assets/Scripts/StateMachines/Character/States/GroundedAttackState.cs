using System;
using UnityEngine;

public class GroundedAttackState : CharacterStateBlock
{
    public GroundedAttackState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
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

    //    switch (characterController.InputController.CurrentInputDirection)
    //    {
    //        case InputCommandElement.Down:
    //        case InputCommandElement.DownBack:
    //        case InputCommandElement.DownForward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerCrouching);
    //            break;
    //        default:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //            break;
    //    }
    //}
}