using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class BlockStandingState : CharacterStateBlock
{
    public BlockStandingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.BlockStanding)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.HitStanding)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.HitCrouching)
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

    //protected override void OnUpdate()
    //{

    //}
}
