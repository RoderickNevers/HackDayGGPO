using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;
//using UnityEngine.InputSystem;

public class RetreatingState : CharacterStateBlock
{
    private const float retreatSpeed = 3f;

    public RetreatingState()
    {
    }

    public Player UpdatePlayer(Player player, long input)
    {
        float velocity = 0;

        if (player.IsHit)
        {
            Debug.Log("IM HIT CAPTAIN!!!!!!!!!!!!!!!");
        }
        //Returning attack
        else if (player.IsAttacking)
        {
            switch (player.CurrentButtonPressed)
            {
                case AttackButtonState.Slash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.SLASH);
                    break;
                case AttackButtonState.HeavySlash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_SLASH);
                    break;
                case AttackButtonState.GuardBreak:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.GUARD_BREAK);
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (CheckAttacking(input))
            {
                case AttackButtonState.Slash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.SLASH);
                    break;
                case AttackButtonState.HeavySlash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_SLASH);
                    break;
                case AttackButtonState.GuardBreak:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.GUARD_BREAK);
                    break;
                case AttackButtonState.None:
                    PlayAnimationLoop(ref player, AnimationData.Movememt.WALK_BACKWARD);
                    velocity = player.LookDirection == LookDirection.Right ? -1 : 1;
                    break;
            }
        }

        player.Velocity.Set(velocity, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        return player;
    }

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Retreating Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackGround, $"{ProjectConstants.MovelistStateKeys.STANDING}-{name}", e.Move);
    //}

    //private void HandleMovement()
    //{
    //    switch (characterController.InputController.CurrentInputDirection)
    //    {
    //        case InputCommandElement.Neutral:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //            break;
    //        case InputCommandElement.UpForward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpTowards);
    //            break;
    //        case InputCommandElement.DownForward:
    //        case InputCommandElement.DownBack:
    //        case InputCommandElement.Down:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerCrouching);
    //            break;
    //        case InputCommandElement.Forward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerAdvancing);
    //            break;
    //        case InputCommandElement.UpBack:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpBack);
    //            break;
    //        case InputCommandElement.Up:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpUp);
    //            break;
    //    }
    //}
}