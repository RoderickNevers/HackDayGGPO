using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CrouchingState : CharacterStateBlock
{

    public CrouchingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerAdvancing, CharacterState.Advancing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Retreating)
            //.Permit(ChangeState.TriggerJumpUp, CharacterState.JumpUp)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.BlockCrouching)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.HitCrouching)
            .Permit(CharacterStateTrigger.TriggerAttackGround, CharacterState.GroundedAttack)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.Throw)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.GetThrown)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();

        //characterController.InputController.MovementCallback = HandleMovement;
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.CROUCHING));
        //Debug.Log("Enter Crouching");
    }

    protected override void OnExitState()
    {
        base.OnExitState();

        //characterController.InputController.MovementCallback = null;
        //Debug.Log("Exit Crouching");
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

    public Player UpdatePlayer(Player player, long input)
    {
        PlayAnimationLoop(ref player, "Crouch");

        player.Velocity.Set(0, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;
        return player;
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();
    //    //characterController.MovementVector.x = 0;
    //}

    //private void HandleMovement()
    //{
    //    switch (characterController.InputController.CurrentInputDirection)
    //    {
    //        case InputCommandElement.Neutral:
    //        case InputCommandElement.Forward:
    //        case InputCommandElement.Back:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //            break;
    //    }
    //}

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Crouching Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackGround, $"{ProjectConstants.MovelistStateKeys.CROUCHING}-{name}", e.Move);
    //}
}