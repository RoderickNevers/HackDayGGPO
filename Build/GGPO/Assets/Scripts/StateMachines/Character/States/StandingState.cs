using System.Linq;
using UnityEngine;

public class StandingState : CharacterStateBlock
{
    public StandingState()
    {
    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
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
                    var velocity = player.LookDirection == LookDirection.Right ? -1 : 1;
                    player.Velocity.Set(velocity, 0, 0);
                    player.Velocity = PlayerConstants.PUSH_SPEED * Time.fixedDeltaTime * player.Velocity;
                    return player;
                }

                player.Velocity.Set(0, 0, 0);
            }
        }

        return player;
    }
}