using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class ThrowingState : CharacterStateBlock
{
    public ThrowingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Throw)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Standing)
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
