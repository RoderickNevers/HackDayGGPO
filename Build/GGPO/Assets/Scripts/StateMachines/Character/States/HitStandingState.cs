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

    private readonly Dictionary<Guid, FrameData> _HitReactionLookup = new Dictionary<Guid, FrameData>()
    {
        // Standing attacks
        {AnimationData.StandingAttacks.SLASH.ID, AnimationData.Hit.DEAD_4},
        {AnimationData.StandingAttacks.HEAVY_SLASH.ID, AnimationData.Hit.DEAD_4},
        {AnimationData.StandingAttacks.GUARD_BREAK.ID, AnimationData.Hit.HIT_3},
    };

    public HitStandingState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        // check if they are blocking
        if (!player.IsTakingDamage && IsBlocking(player, input))
        {
            return UpdateBlockReaction(player);
        }

        return UpdateHitReaction(player, _HitReactionLookup);
    }

    private bool IsBlocking(Player player, long input)
    {
        return player.LookDirection == LookDirection.Left && input == InputConstants.INPUT_RIGHT || player.LookDirection == LookDirection.Right && input == InputConstants.INPUT_LEFT;
    }
}