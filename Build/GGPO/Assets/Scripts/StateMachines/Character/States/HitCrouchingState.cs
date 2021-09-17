using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;

public class HitCrouchingState : CharacterStateBlock
{
    private readonly Dictionary<Guid, FrameData> _HitReactionLookup = new Dictionary<Guid, FrameData>()
    {
        {AnimationData.CrouchingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.CROUCHING_LIGHT},
        {AnimationData.CrouchingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        {AnimationData.CrouchingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.CROUCHING_HEAVY},
        {AnimationData.CrouchingAttacks.LIGHT_KICK.ID, AnimationData.Hit.CROUCHING_LIGHT},
        {AnimationData.CrouchingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.CROUCHING_MEDIUM},
        {AnimationData.CrouchingAttacks.HEAVY_KICK.ID, AnimationData.Hit.SWEEP}
    };

    public HitCrouchingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.HitCrouching)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.OnTheGround)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }
    protected override void OnEnterState()
    {
        base.OnEnterState();
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    public Player UpdatePlayer(Player player, long input)
    {
        if (player.CurrentlyHitByID == Guid.Empty)
        {
            return player;
        }

        Debug.Log("Crouching hit and this is cheap");

        PlayHitAnimation(ref player, _HitReactionLookup[player.CurrentlyHitByID]);

        return player;
    }
}
