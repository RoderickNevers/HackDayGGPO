using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;

public class HitInAirState : CharacterStateBlock
{
    private readonly Dictionary<Guid, FrameData> _HitReactionLookup = new Dictionary<Guid, FrameData>()
    {
        // Standing attacks
        {AnimationData.StandingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.StandingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.StandingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.StandingAttacks.LIGHT_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.StandingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.StandingAttacks.HEAVY_KICK.ID, AnimationData.Hit.InAir},

        {AnimationData.CrouchingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.CrouchingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.CrouchingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.CrouchingAttacks.LIGHT_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.CrouchingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.CrouchingAttacks.HEAVY_KICK.ID, AnimationData.Hit.InAir},

        //Jumping up attacks
        {AnimationData.JumpUpAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpUpAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpUpAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpUpAttacks.LIGHT_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpUpAttacks.MEDIUM_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpUpAttacks.HEAVY_KICK.ID, AnimationData.Hit.InAir},

        //Jumping forward attacks
        {AnimationData.JumpTowardAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpTowardAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpTowardAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpTowardAttacks.LIGHT_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpTowardAttacks.MEDIUM_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpTowardAttacks.HEAVY_KICK.ID, AnimationData.Hit.InAir},

        //Jumping backward attacks
        {AnimationData.JumpBackAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpBackAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpBackAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpBackAttacks.LIGHT_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpBackAttacks.MEDIUM_KICK.ID, AnimationData.Hit.InAir},
        {AnimationData.JumpBackAttacks.HEAVY_KICK.ID, AnimationData.Hit.InAir}
    };

    public HitInAirState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return UpdateHitReaction(player, input, _HitReactionLookup);
    }
}