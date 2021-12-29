using System;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
//using System.Windows.Markup;
//using System.Collections.Generic;
//using EZCameraShake;

public class HitStandingState : CharacterStateBlock
{
    private const float SCREEN_FREEZE_TIME = 0.11f;

    public HitStandingState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        // check if they are blocking
        if (!player.IsTakingDamage && IsBlocking(player, input))
        {
            return UpdateBlockReaction(player, commandList);
        }

        return UpdateHitReaction(player, commandList);
    }

    private bool IsBlocking(Player player, long input)
    {
        return player.LookDirection == LookDirection.Left && input == InputConstants.INPUT_RIGHT || player.LookDirection == LookDirection.Right && input == InputConstants.INPUT_LEFT;
    }
}