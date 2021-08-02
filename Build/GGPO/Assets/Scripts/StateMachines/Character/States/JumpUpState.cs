using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
public class JumpUpState : CharacterStateBlock
{
    public JumpUpState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.JumpUp)
            .Permit(CharacterStateTrigger.TriggerFalling, CharacterState.Falling)
            .Permit(CharacterStateTrigger.TriggerAttackInAir, CharacterState.InAirAttack)
            //.Permit(ChangeState.TriggerHitInAir, CharacterState.HitInAir)
            //.Permit(ChangeState.TriggerKO, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.JUMP_UP));
        //characterController.MovementVector = Vector2.zero;
        //Debug.Log("Enter Jump Up");
    }

    protected override void OnExitState()
    {
        base.OnExitState();
        //Debug.Log("Exit Jump Up");
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        //characterController.InputController.OnInputCommand += HandleInputCommand;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        //characterController.InputController.OnInputCommand -= HandleInputCommand;
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();

    //    characterController.Jump(0);

    //    if (characterController.Controller.velocity.y < 0)
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerFalling);
    //    }
    //}

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Standing Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackInAir, $"{ProjectConstants.MovelistStateKeys.JUMP_UP}-{name}", e.Move);
    //}
}