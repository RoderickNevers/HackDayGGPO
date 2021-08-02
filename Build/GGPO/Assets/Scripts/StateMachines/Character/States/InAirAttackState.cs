using System;
using UnityEngine;

public class InAirAttackState : CharacterStateBlock
{
    public InAirAttackState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.InAirAttack)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerJumpUp, CharacterState.JumpUp)
            .Permit(CharacterStateTrigger.TriggerFalling, CharacterState.Falling)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.HitStanding)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.HitCrouching)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.Throw)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.GetThrown)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.JumpToward)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.JumpAway)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //if (DoesAttackExist(stateMachine.AttackID))
        //{
        //    ExecuteAttack(stateMachine.AttackID, stateMachine.Move);
        //}
        //else
        //{
        //    HandleAnimationComplete();
        //}
        //Debug.Log("ENTER ATTACKING IN AIR");
    }

    protected override void OnExitState()
    {
        base.OnExitState();
        //stateMachine.ResetAttackID();
        //Debug.Log("EXIT ATTACKING IN AIR");
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();
    //}

    protected override void AddListeners()
    {
        base.AddListeners();
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }

    //protected override void HandleAnimationComplete()
    //{
    //    CompleteAttack();
    //}

    //protected override void HandleSpecialMoveComplete(object sender, EventArgs e)
    //{
    //    base.HandleSpecialMoveComplete(sender, e);

    //    CompleteAttack();
    //}

    //private void CompleteAttack()
    //{
    //    stateMachine.ResetAttackID();

    //    if (characterController.IsGrounded)
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //    }
    //    else
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerFalling);
    //    }
    //}
}