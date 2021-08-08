using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class LandingState : CharacterStateBlock, IStateSimulator
{
    public LandingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Landing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.HitStanding)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //_Animator.SetTrigger(AnimatorConstants.PARAMENTER_LANDING);

        //trigger standing on animation complete
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    //protected override void OnUpdate()
    //{

    //}
}
