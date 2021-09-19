using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CrouchingState : CharacterStateBlock
{

    public CrouchingState()
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
                case AttackButtonState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.LIGHT_PUNCH);
                    break;
                case AttackButtonState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackButtonState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.HEAVY_PUNCH);
                    break;
                case AttackButtonState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.LIGHT_KICK);
                    break;
                case AttackButtonState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.MEDIUM_KICK);
                    break;
                case AttackButtonState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.HEAVY_KICK);
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (CheckAttacking(input))
            {
                case AttackButtonState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.LIGHT_PUNCH);
                    break;
                case AttackButtonState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackButtonState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.HEAVY_PUNCH);
                    break;
                case AttackButtonState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.LIGHT_KICK);
                    break;
                case AttackButtonState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.MEDIUM_KICK);
                    break;
                case AttackButtonState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.CrouchingAttacks.HEAVY_KICK);
                    break;
                case AttackButtonState.None:
                    PlayAnimationLoop(ref player, AnimationData.Movememt.CROUCH);
                    break;
            }
        }

        player.Velocity.Set(velocity, 0, 0);
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