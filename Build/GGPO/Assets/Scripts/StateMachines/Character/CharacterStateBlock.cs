﻿using System;
using System.Collections.Generic;

using UnityEngine;

public class CharacterStateBlock : AbstractStateBlock, IDisposable
{
    protected Player _Player;

    public CharacterStateBlock()
    {

    }

    public virtual void Dispose()
    {

    }

    protected void PlayAnimationLoop(ref Player player, FrameData frameData)
    {
        if (player.AnimationKey != frameData.AnimationKey)
        {
            player.AnimationIndex = 1;
        }

        player.AnimationKey = frameData.AnimationKey;
        player.AnimationIndex += frameData.PlaybackSpeed;
        player.CurrentFrame = player.AnimationIndex / frameData.TotalFrames;

        if (player.AnimationIndex >= frameData.TotalFrames)
        {
            player.AnimationIndex = 1;
        }
    }

    protected void PlayAnimationOneShot(ref Player player, FrameData frameData)
    {
        if (player.AnimationKey != frameData.AnimationKey)
        {
            player.AnimationIndex = 1;
        }

        player.AnimationKey = frameData.AnimationKey;

        if (player.AnimationIndex < frameData.TotalFrames)
        {
            player.AnimationIndex += frameData.PlaybackSpeed;
        }

        player.CurrentFrame = player.AnimationIndex / frameData.TotalFrames;
    }

    protected void PlayAttackAnimation(ref Player player, FrameData frameData)
    {
        PlayAnimationOneShot(ref player, frameData);

        player.IsAttacking = player.AnimationIndex < frameData.TotalFrames;

        if (player.IsAttacking)
        {
            player.State = frameData.PlayerState;
            player.CurrentButtonPressed = frameData.Attack;
            player.CurrentAttackID = frameData.ID;
        }
        else
        {
            player.CurrentButtonPressed = AttackButtonState.None;
            player.CurrentAttackID = Guid.Empty;
        }
    }

    protected Player UpdateHitReaction(Player player, PlayerCommandList commandList)
    {
        if (player.CurrentlyHitByID == Guid.Empty)
        {
            return player;
        }

        FrameData attack = commandList.AttackLookup[player.CurrentlyHitByID];
        int direction = player.LookDirection == LookDirection.Left ? 1 : -1;
        
        if (IsAnimationComplete(player, attack))
        {
            return player;
        }

        PlayAnimationOneShot(ref player, commandList.HitReactionLookup[player.CurrentlyHitByID]);
        ApplyHitStun(ref player, attack.HitStun);
        ApplyPush(ref player, direction, attack.HitPushBack);
        ApplyDamage(ref player, attack.Damage);

        return player;
    }

    protected Player UpdateBlockReaction(Player player, PlayerCommandList commandList)
    {
        if (player.CurrentlyHitByID == Guid.Empty)
        {
            return player;
        }

        FrameData attack = commandList.AttackLookup[player.CurrentlyHitByID];
        int direction = player.LookDirection == LookDirection.Left ? 1 : -1;

        if (IsAnimationComplete(player, attack))
        {
            return player;
        }

        PlayAnimationOneShot(ref player, commandList.Block.FrameData);
        ApplyBlockStun(ref player, attack.BlockStun);
        ApplyPush(ref player, direction, attack.BlockPushBack);
        return player;
    }

    protected bool IsAnimationComplete(Player player, FrameData frameData)
    {
        return player.AnimationIndex >= frameData.TotalFrames; 
    }

    protected void ApplyPush(ref Player player, int direction, float force)
    {
        //var velocity = player.LookDirection == LookDirection.Right ? -1 : 1;
        player.Velocity.Set(direction, 0, 0);
        player.Velocity *= force * Time.fixedDeltaTime;
    }

    protected void ApplyDamage(ref Player player, int damage)
    {
        player.Health -= damage;
        player.IsTakingDamage = true;
    }

    protected void GainHealth(ref Player player, int health)
    {
        player.Health += health;
    }

    protected void ApplyHitStun(ref Player player, float hitStunMax)
    {
        if (!player.IsStunned)
        {
            player.IsStunned = true;
            player.HitStunTime = hitStunMax;
        }
        else
        {
            player.HitStunTime -= 0.1f;

            if (player.HitStunTime <= 0)
            {
                player.IsStunned = false;
                player.HitStunTime = 0;
            }
        }
    }

    protected void ApplyBlockStun(ref Player player, float blockStunMax)
    {
        Debug.Log($"ID: {player.ID} Stun: {player.IsStunned}");
        if (!player.IsStunned)
        {
            player.IsStunned = true;
            player.BlockStunTime = blockStunMax;
        }
        else
        {
            player.BlockStunTime -= 0.1f;

            if (player.BlockStunTime <= 0)
            {
                player.IsStunned = false;
                player.BlockStunTime = 0;
            }
        }
    }

    protected void GainPower(ref Player player, int power)
    {
        player.Power += power;
    }

    protected void UsePower(ref Player player, int power)
    {
        player.Power -= power;
    }

    /// <summary>
    /// Checks if an attack button is being used.
    /// </summary>
    /// <param name="input">The input signature</param>
    /// <returns>The attack button state that is being used. Return none if nothing is used.</returns>
    protected AttackButtonState CheckAttacking(long input)
    {
        if ((input & InputConstants.INPUT_SLASH) != 0)
        {
            return AttackButtonState.Button_1;
        }

        if ((input & InputConstants.INPUT_HEAVY_SLASH) != 0)
        {
            return AttackButtonState.Button_2;
        }

        if ((input & InputConstants.INPUT_GUARD_BREAK) != 0)
        {
            return AttackButtonState.Button_3;
        }

        return AttackButtonState.None;
    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        return player;
    }
}