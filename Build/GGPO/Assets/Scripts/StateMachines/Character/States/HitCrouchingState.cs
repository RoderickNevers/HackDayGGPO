using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class HitCrouchingState : CharacterStateBlock
{
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
        Debug.Log("Crouching hit and this is cheap");
        return player;
    }
}
