using System;
using System.Collections.Generic;

public class KOState : CharacterStateBlock
{
    //private readonly Dictionary<Guid, FrameData> _HitReactionLookup = new Dictionary<Guid, FrameData>()
    //{
    //    // Standing attacks
    //    {AnimationData.StandingAttacks.SLASH.ID, AnimationData.Hit.DEAD_4},
    //    {AnimationData.StandingAttacks.HEAVY_SLASH.ID, AnimationData.Hit.DEAD_4},
    //    {AnimationData.StandingAttacks.GUARD_BREAK.ID, AnimationData.Hit.HIT_3},
    //};

    public KOState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        return UpdateHitReaction(player, commandList);
    }
}