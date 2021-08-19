using System;
using UnityEngine;

public class GroundedAttackState : CharacterStateBlock
{
    public GroundedAttackState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.GroundedAttack)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerAdvancing, CharacterState.Advancing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Retreating)
            .Permit(CharacterStateTrigger.TriggerCrouching, CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerJumpUp, CharacterState.JumpUp)
            .Permit(CharacterStateTrigger.TriggerJumpTowards, CharacterState.JumpToward)
            .Permit(CharacterStateTrigger.TriggerJumpBack, CharacterState.JumpBack)

            .Permit(CharacterStateTrigger.TriggerHitStanding, CharacterState.HitStanding)
            .Permit(CharacterStateTrigger.TriggerSweep, CharacterState.Sweep)
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
        //    CompleteAttack();
        //}
        //Debug.Log("ENTER ATTACKING GROUNDED");
    }

    protected override void OnExitState()
    {
        base.OnExitState();

        //stateMachine.ResetAttackID();
        //Debug.Log("EXIT ATTACKING GROUNDED");
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();
    //    characterController.MovementVector.x = 0;
    //}

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

    //    switch (characterController.InputController.CurrentInputDirection)
    //    {
    //        case InputCommandElement.Down:
    //        case InputCommandElement.DownBack:
    //        case InputCommandElement.DownForward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerCrouching);
    //            break;
    //        default:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //            break;
    //    }
    //}
}