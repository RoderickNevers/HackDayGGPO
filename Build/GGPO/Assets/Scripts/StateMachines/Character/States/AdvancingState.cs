using System;
using UnityEngine;

public class AdvancingState : CharacterStateBlock
{
    public AdvancingState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
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
                    PlayAnimationLoop(ref player, AnimationData.Movememt.WALK_FORWARD);
                    velocity = player.LookDirection == LookDirection.Right? 1 : -1;
                    break;
            }
        }

        if (player.IsCloseToOpponent)
        {
            player.Velocity = Vector3.zero;
            return player;
        }

        player.Velocity.Set(velocity, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        return player;
    }
}