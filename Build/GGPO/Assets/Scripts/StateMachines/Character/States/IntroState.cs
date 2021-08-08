using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class IntroState : CharacterStateBlock, IStateSimulator
{
    public IntroState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine = stateBlockData.StateMachine;

        _StateMachine.Configure(CharacterState.Intro)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //Debug.Log("Show Intro Animation");
        _StateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }
}
