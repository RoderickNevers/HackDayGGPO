using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
public class JumpUpState : CharacterStateBlock
{
    public JumpUpState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        player.JumpType = PlayerState.JumpUp;

        if (player.IsJumping)
        {
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
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.LIGHT_PUNCH);
                        break;
                    case AttackButtonState.MediumPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.MEDIUM_PUNCH);
                        break;
                    case AttackButtonState.HeavyPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.HEAVY_PUNCH);
                        break;
                    case AttackButtonState.LightKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.LIGHT_KICK);
                        break;
                    case AttackButtonState.MediumKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.MEDIUM_KICK);
                        break;
                    case AttackButtonState.HeavyKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.HEAVY_KICK);
                        break;
                }
            }
            //New attack or nothing
            else
            {
                switch (CheckAttacking(input))
                {
                    case AttackButtonState.LightPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.LIGHT_PUNCH);
                        break;
                    case AttackButtonState.MediumPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.MEDIUM_PUNCH);
                        break;
                    case AttackButtonState.HeavyPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.HEAVY_PUNCH);
                        break;
                    case AttackButtonState.LightKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.LIGHT_KICK);
                        break;
                    case AttackButtonState.MediumKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.MEDIUM_KICK);
                        break;
                    case AttackButtonState.HeavyKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpUpAttacks.HEAVY_KICK);
                        break;
                    case AttackButtonState.None:
                        PlayAnimationOneShot(ref player, AnimationData.Movememt.JUMP_UP);
                        break;
                }
            }

            player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;
            player.Velocity.y += Mathf.Sqrt(PlayerConstants.JUMP_FORCE_VERT * Time.fixedDeltaTime);
            player.Velocity.x = 0;
        }

        return player;
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