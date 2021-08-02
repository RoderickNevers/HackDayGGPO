﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class LandingState : CharacterStateBlock
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

    //protected override void OnUpdate()
    //{

    //}
}
