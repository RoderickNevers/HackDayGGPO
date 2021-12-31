using System;
using System.Linq;
using UnityEngine;

public class AdvancingState : CharacterStateBlock
{
    public AdvancingState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        float velocity = 0;

        if (player.IsAttacking)
        {
            FrameData attackFrameData = commandList.AttackLookup.Where(x => x.Value.Input == player.CurrentButtonPressed).FirstOrDefault().Value;
            if (attackFrameData != null)
            {
                PlayAttackAnimation(ref player, attackFrameData);
            }
        }
        //New attack or nothing
        else
        {
            InputButtons currentButtonPressed = CheckAttacking(input);
            FrameData attackFrameData = commandList.AttackLookup.Where(x => x.Value.Input == currentButtonPressed).FirstOrDefault().Value;
            if (attackFrameData != null)
            {
                PlayAttackAnimation(ref player, attackFrameData);
            }
            else
            {
                PlayAnimationLoop(ref player, commandList.Right.FrameData);
                velocity = player.LookDirection == LookDirection.Right ? 1 : -1;
            }
        }

        if (player.IsCloseToOpponent)
        {
            player.Velocity = Vector3.zero;
            return player;
        }

        player.Velocity.Set(velocity, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        return player;
    }
}