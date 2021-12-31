using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;
using System.Linq;

public class RetreatingState : CharacterStateBlock
{
    private const float retreatSpeed = 3f;

    public RetreatingState()
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
                PlayAnimationLoop(ref player, commandList.Left.FrameData);
                velocity = player.LookDirection == LookDirection.Right ? -1 : 1;
            }
        }


        // screen bounds left
        if (player.Position.x < -14)
        {
            player.Position.x = -14;
            return player;
        }

        // screen bounds right
        if (player.Position.x > 14)
        {
            player.Position.x = 14;
            return player;
        }

        player.Velocity.Set(velocity, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        return player;
    }
}