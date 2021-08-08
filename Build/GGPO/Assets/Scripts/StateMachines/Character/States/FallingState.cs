using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class FallingState : CharacterStateBlock, IStateSimulator
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
        //Debug.Log("Enter Falling");
        //_Animator.SetTrigger(AnimatorConstants.PARAMENTER_FALLING);
    }

    protected override void OnExitState()
    {
        base.OnExitState();
        //Debug.Log("Exit Falling");
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
        return player;
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();

    //    if (characterController.IsGrounded)
    //        stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //}
}
