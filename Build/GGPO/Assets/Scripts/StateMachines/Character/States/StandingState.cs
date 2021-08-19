using System;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class StandingState : CharacterStateBlock
{
    public StandingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Standing)
            .PermitReentry(CharacterStateTrigger.TriggerStanding)
            .Permit(CharacterStateTrigger.TriggerAdvancing, CharacterState.Advancing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Retreating)
            .Permit(CharacterStateTrigger.TriggerCrouching, CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerJumpUp, CharacterState.JumpUp)
            .Permit(CharacterStateTrigger.TriggerJumpTowards, CharacterState.JumpToward)
            .Permit(CharacterStateTrigger.TriggerJumpBack, CharacterState.JumpBack)
            .Permit(CharacterStateTrigger.TriggerAttackGround, CharacterState.GroundedAttack)
            .Permit(CharacterStateTrigger.TriggerHitStanding, CharacterState.HitStanding)
            .Permit(CharacterStateTrigger.TriggerSweep, CharacterState.Sweep)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();

        Debug.Log("I have entered");
        //characterController.UpdateLookDirection.Invoke();

        //// Immediately move to the next state.
        //if (TransitionStates())
        //    return;

        //characterController.InputController.MovementCallback = HandleMovement;
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.STANDING));
    }

    protected override void OnExitState()
    {
        base.OnExitState();

        //characterController.InputController.MovementCallback = null;
        //Debug.Log("EXIT STANDING");
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();
    //    characterController.MovementVector.x = 0;
    //}

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
        if (player.IsHit)
        {
            Debug.Log("IM HIT CAPTAIN!!!!!!!!!!!!!!!");
        }
        //Returning attack
        else if (player.IsAttacking)
        {
            switch (player.Attack)
            {
                case AttackState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_PUNCH);
                    break;
                case AttackState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_PUNCH);
                    break;
                case AttackState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_KICK);
                    break;
                case AttackState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_KICK);
                    break;
                case AttackState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_KICK);
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (CheckAttacking(input))
            {
                case AttackState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_PUNCH);
                    break;
                case AttackState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_PUNCH);
                    break;
                case AttackState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_KICK);
                    break;
                case AttackState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_KICK);
                    break;
                case AttackState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_KICK);
                    break;
                case AttackState.None:
                    PlayAnimationLoop(ref player, AnimationData.Movememt.IDLE);
                    player.Velocity.Set(0, 0, 0);
                    player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;
                    break;
            }
        }

        return player;
    }

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Standing Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackGround, $"{ProjectConstants.MovelistStateKeys.STANDING}-{name}", e.Move);
    //}

    //public void HandleMovement()
    //{
    //    TransitionStates();
    //}

    //private bool TransitionStates()
    //{
    //    switch (characterController.InputController.CurrentInputDirection)
    //    {
    //        case InputCommandElement.Forward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerAdvancing);
    //            return true;
    //        case InputCommandElement.Back:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerRetreating);
    //            return true;
    //        case InputCommandElement.Up:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpUp);
    //            return true;
    //        case InputCommandElement.UpBack:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpBack);
    //            return true;
    //        case InputCommandElement.UpForward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpTowards);
    //            return true;
    //        case InputCommandElement.Down:
    //        case InputCommandElement.DownBack:
    //        case InputCommandElement.DownForward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerCrouching);
    //            return true;
    //        default:
    //            return false;
    //    }
    //}
}