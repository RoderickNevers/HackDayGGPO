using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;

public class HitCrouchingState : CharacterStateBlock
{
    private readonly Dictionary<Guid, FrameData> _HitReactionLookup = new Dictionary<Guid, FrameData>()
    {
        // Standing attacks
        //{AnimationData.StandingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.CROUCHING_LIGHT},
        //{AnimationData.StandingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.StandingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.CROUCHING_HEAVY},
        //{AnimationData.StandingAttacks.LIGHT_KICK.ID, AnimationData.Hit.CROUCHING_LIGHT},
        //{AnimationData.StandingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.StandingAttacks.HEAVY_KICK.ID, AnimationData.Hit.CROUCHING_HEAVY},

        //{AnimationData.CrouchingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.CROUCHING_LIGHT},
        //{AnimationData.CrouchingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.CrouchingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.CROUCHING_HEAVY},
        //{AnimationData.CrouchingAttacks.LIGHT_KICK.ID, AnimationData.Hit.CROUCHING_LIGHT},
        //{AnimationData.CrouchingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.CrouchingAttacks.HEAVY_KICK.ID, AnimationData.Hit.SWEEP},

        ////Jumping up attacks
        //{AnimationData.JumpUpAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpUpAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpUpAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.CROUCHING_HEAVY},
        //{AnimationData.JumpUpAttacks.LIGHT_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpUpAttacks.MEDIUM_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpUpAttacks.HEAVY_KICK.ID, AnimationData.Hit.CROUCHING_HEAVY},

        ////Jumping forward attacks
        //{AnimationData.JumpTowardAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpTowardAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpTowardAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.CROUCHING_HEAVY},
        //{AnimationData.JumpTowardAttacks.LIGHT_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpTowardAttacks.MEDIUM_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpTowardAttacks.HEAVY_KICK.ID, AnimationData.Hit.CROUCHING_HEAVY},

        ////Jumping backward attacks
        //{AnimationData.JumpBackAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpBackAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpBackAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.CROUCHING_HEAVY},
        //{AnimationData.JumpBackAttacks.LIGHT_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpBackAttacks.MEDIUM_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        //{AnimationData.JumpBackAttacks.HEAVY_KICK.ID, AnimationData.Hit.CROUCHING_HEAVY}
    };

    public HitCrouchingState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return UpdateHitReaction(player, _HitReactionLookup);
    }
}