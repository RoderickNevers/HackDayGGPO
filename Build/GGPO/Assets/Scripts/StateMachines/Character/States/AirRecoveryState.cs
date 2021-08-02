using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class AirRecoveryState : CharacterStateBlock
{
    public AirRecoveryState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Landing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.HitInAir)
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

    //protected override void OnUpdate()
    //{

    //}
}
