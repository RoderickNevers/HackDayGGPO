using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class FallingState : CharacterStateBlock
{
    public FallingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Falling)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerAttackInAir, CharacterState.InAirAttack)
            //.Permit(ChangeState.TriggerHitInAir, CharacterState.HitInAir)
            //.Permit(ChangeState.TriggerKO, CharacterState.KO)
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

    protected override void AddListeners()
    {
        base.AddListeners();
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }

    public Player UpdatePlayer(Player player, long input)
    {
        Debug.Log("Shit, i am falling!");
        return player;
    }
}
