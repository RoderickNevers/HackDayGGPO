using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CrouchingState : CharacterStateBlock
{

    public CrouchingState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        // Returning attack
        if (player.IsAttacking)
        {
            FrameData attackFrameData = commandList.AttackLookup.Where(x => x.Value.Attack == player.CurrentButtonPressed).FirstOrDefault().Value;
            if (attackFrameData != null)
            {
                PlayAttackAnimation(ref player, attackFrameData);
            }
        }
        //New attack or nothing
        else
        {
            AttackButtonState currentButtonPressed = CheckAttacking(input);
            FrameData attackFrameData = commandList.AttackLookup.Where(x => x.Value.Attack == currentButtonPressed).FirstOrDefault().Value;
            if (attackFrameData != null)
            {
                PlayAttackAnimation(ref player, attackFrameData);
            }
            else
            {
                PlayAnimationLoop(ref player, commandList.Idle.FrameData);

                if (player.IsCloseToOpponent && player.IsBeingPushed)
                {
                    // push the opponent backwards
                    var pushVelocity = player.LookDirection == LookDirection.Right ? -1 : 1;
                    player.Velocity.Set(pushVelocity, 0, 0);
                    player.Velocity = PlayerConstants.PUSH_SPEED * Time.fixedDeltaTime * player.Velocity;
                    return player;
                }
            }
        }

        player.Velocity.Set(0, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        return player;
    }
}