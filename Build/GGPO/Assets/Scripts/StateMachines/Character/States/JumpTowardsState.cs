using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.InputSystem;
public class JumpTowardsState : CharacterStateBlock
{
    public JumpTowardsState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        player.JumpType = PlayerState.JumpToward;

        if (player.IsJumping)
        {
            // Returning attack
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
            }

            player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;
            player.Velocity.y += Mathf.Sqrt(PlayerConstants.JUMP_FORCE_VERT * Time.fixedDeltaTime);
            player.Velocity.x += PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
        }
 
        return player;
    }
}