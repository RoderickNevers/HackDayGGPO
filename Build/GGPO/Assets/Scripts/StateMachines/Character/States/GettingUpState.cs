using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class GettingUpState : CharacterStateBlock
{
    public GettingUpState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.GettingUp)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.GET_UP));
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    //protected override void HandleAnimationComplete()
    //{
    //    base.HandleAnimationComplete();
    //    stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //}
}
