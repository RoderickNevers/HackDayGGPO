using System.Collections.Generic;
using UnityEngine;

public class JumpBackState : CharacterStateBlock
{
    public JumpBackState()
    {

    }

    //Debug.Log("Enter Jump Back");
    //characterController.MovementVector = Vector2.zero;
    //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.JUMP_BACKWARDS));
    //prevents the jump animation from being played when you re-enter the jump state from an air attack state
    //if (_BaseCharacterController.IsGrounded)
    //    _Animator.SetTrigger(AnimatorConstants.PARAMENTER_JUMP);


    public override Player UpdatePlayer(Player player, long input)
    {
        player.JumpType = PlayerState.JumpBack;

        if (player.IsJumping)
        {
            if (player.IsHit)
            {
                Debug.Log("IM HIT CAPTAIN!!!!!!!!!!!!!!!");
            }
            //Returning attack
            //else if (player.IsAttacking)
            //{
            //    switch (player.CurrentButtonPressed)
            //    {
            //        case AttackButtonState.LightPunch:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_PUNCH);
            //            break;
            //        case AttackButtonState.MediumPunch:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_PUNCH);
            //            break;
            //        case AttackButtonState.HeavyPunch:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_PUNCH);
            //            break;
            //        case AttackButtonState.LightKick:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_KICK);
            //            break;
            //        case AttackButtonState.MediumKick:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_KICK);
            //            break;
            //        case AttackButtonState.HeavyKick:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_KICK);
            //            break;
            //    }
            //}
            ////New attack or nothing
            //else
            //{
            //    switch (CheckAttacking(input))
            //    {
            //        case AttackButtonState.LightPunch:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_PUNCH);
            //            break;
            //        case AttackButtonState.MediumPunch:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_PUNCH);
            //            break;
            //        case AttackButtonState.HeavyPunch:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_PUNCH);
            //            break;
            //        case AttackButtonState.LightKick:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_KICK);
            //            break;
            //        case AttackButtonState.MediumKick:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_KICK);
            //            break;
            //        case AttackButtonState.HeavyKick:
            //            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_KICK);
            //            break;
            //        case AttackButtonState.None:
            //            FrameData data = player.LookDirection == LookDirection.Right ? AnimationData.Movememt.JUMP_BACKWARD : AnimationData.Movememt.JUMP_FORWARD;
            //            PlayAnimationOneShot(ref player, data);
            //            break;
            //    }
            //}

            player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;
            player.Velocity.y += Mathf.Sqrt(PlayerConstants.JUMP_FORCE_VERT * Time.fixedDeltaTime);
            player.Velocity.x += -PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
        }

        return player;
    }

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Jump Back Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackInAir, $"{ProjectConstants.MovelistStateKeys.JUMP_DIRECTION}-{name}", e.Move);
    //}
}