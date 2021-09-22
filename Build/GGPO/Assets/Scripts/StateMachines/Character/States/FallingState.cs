using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class FallingState : CharacterStateBlock
{
    public FallingState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        //Returning attack
        if (player.IsAttacking)
        {
            switch (player.JumpType)
            {
                case PlayerState.JumpUp:
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
                    break;
                case PlayerState.JumpToward:
                    switch (player.CurrentButtonPressed)
                    {
                        case AttackButtonState.LightPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.LIGHT_PUNCH);
                            break;
                        case AttackButtonState.MediumPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.MEDIUM_PUNCH);
                            break;
                        case AttackButtonState.HeavyPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.HEAVY_PUNCH);
                            break;
                        case AttackButtonState.LightKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.LIGHT_KICK);
                            break;
                        case AttackButtonState.MediumKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.MEDIUM_KICK);
                            break;
                        case AttackButtonState.HeavyKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.HEAVY_KICK);
                            break;
                    }
                    break;
                case PlayerState.JumpBack:
                    switch (player.CurrentButtonPressed)
                    {
                        case AttackButtonState.LightPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_PUNCH);
                            break;
                        case AttackButtonState.MediumPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_PUNCH);
                            break;
                        case AttackButtonState.HeavyPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_PUNCH);
                            break;
                        case AttackButtonState.LightKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_KICK);
                            break;
                        case AttackButtonState.MediumKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_KICK);
                            break;
                        case AttackButtonState.HeavyKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_KICK);
                            break;
                    }
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (player.JumpType)
            {
                case PlayerState.JumpUp:
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
                            FrameData data = player.LookDirection == LookDirection.Right ? AnimationData.Movememt.JUMP_FORWARD : AnimationData.Movememt.JUMP_BACKWARD;
                            PlayAnimationOneShot(ref player, data);
                            break;
                    }
                    break;
                case PlayerState.JumpToward:
                    switch (CheckAttacking(input))
                    {
                        case AttackButtonState.LightPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.LIGHT_PUNCH);
                            break;
                        case AttackButtonState.MediumPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.MEDIUM_PUNCH);
                            break;
                        case AttackButtonState.HeavyPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.HEAVY_PUNCH);
                            break;
                        case AttackButtonState.LightKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.LIGHT_KICK);
                            break;
                        case AttackButtonState.MediumKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.MEDIUM_KICK);
                            break;
                        case AttackButtonState.HeavyKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpTowardAttacks.HEAVY_KICK);
                            break;
                        case AttackButtonState.None:
                            FrameData data = player.LookDirection == LookDirection.Right ? AnimationData.Movememt.JUMP_FORWARD : AnimationData.Movememt.JUMP_BACKWARD;
                            PlayAnimationOneShot(ref player, data);
                            break;
                    }
                    break;
                case PlayerState.JumpBack:
                    switch (CheckAttacking(input))
                    {
                        case AttackButtonState.LightPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_PUNCH);
                            break;
                        case AttackButtonState.MediumPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_PUNCH);
                            break;
                        case AttackButtonState.HeavyPunch:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_PUNCH);
                            break;
                        case AttackButtonState.LightKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.LIGHT_KICK);
                            break;
                        case AttackButtonState.MediumKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.MEDIUM_KICK);
                            break;
                        case AttackButtonState.HeavyKick:
                            PlayAttackAnimation(ref player, AnimationData.JumpBackAttacks.HEAVY_KICK);
                            break;
                        case AttackButtonState.None:
                            FrameData data = player.LookDirection == LookDirection.Right ? AnimationData.Movememt.JUMP_FORWARD : AnimationData.Movememt.JUMP_BACKWARD;
                            PlayAnimationOneShot(ref player, data);
                            break;
                    }
                    break;
            }
        }

        return player;
    }
}
