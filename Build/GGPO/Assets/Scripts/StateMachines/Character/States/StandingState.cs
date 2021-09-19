using System;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class StandingState : CharacterStateBlock
{
    public StandingState()
    {
    }

    //characterController.UpdateLookDirection.Invoke();

    //// Immediately move to the next state.
    //if (TransitionStates())
    //    return;

    //characterController.InputController.MovementCallback = HandleMovement;
    //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.STANDING));


    public override Player UpdatePlayer(Player player, long input)
    {
        if (player.IsAttacking)
        {
            switch (player.CurrentButtonPressed)
            {
                case AttackButtonState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_PUNCH);
                    break;
                case AttackButtonState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackButtonState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_PUNCH);
                    break;
                case AttackButtonState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_KICK);
                    break;
                case AttackButtonState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_KICK);
                    break;
                case AttackButtonState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_KICK);
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (CheckAttacking(input))
            {
                case AttackButtonState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_PUNCH);
                    break;
                case AttackButtonState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackButtonState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_PUNCH);
                    break;
                case AttackButtonState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_KICK);
                    break;
                case AttackButtonState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_KICK);
                    break;
                case AttackButtonState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_KICK);
                    break;
                case AttackButtonState.None:
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