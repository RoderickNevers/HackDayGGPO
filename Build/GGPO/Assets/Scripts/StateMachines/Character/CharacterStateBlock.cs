﻿using System;
using System.Collections.Generic;
using Stateless;
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
        player.AnimationIndex += AnimationData.FRAME_COUNTER;
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
            player.AnimationIndex += AnimationData.FRAME_COUNTER;
        }

        player.CurrentFrame = player.AnimationIndex / frameData.TotalFrames;
    }

    protected void PlayAttackAnimation(ref Player player, FrameData frameData)
    {
        PlayAnimationOneShot(ref player, frameData);

        player.IsAttacking = player.AnimationIndex < frameData.TotalFrames;

        if (player.IsAttacking)
        {
            player.State = frameData.State;
            player.CurrentButtonPressed = frameData.Attack;
            player.CurrentAttackID = frameData.ID;
        }
        else
        {
            player.CurrentButtonPressed = AttackButtonState.None;
            player.CurrentAttackID = Guid.Empty;
        }
    }

    protected void PlayHitAnimation(ref Player player, FrameData frameData)
    {
        PlayAnimationOneShot(ref player, frameData);
    }

    /// <summary>
    /// Pushes the character in a direction.
    /// </summary>
    /// <param name="player"></param>
    protected void ApplyPush(ref Player player, int direction, float force)
    {
        player.Velocity.Set(direction, 0, 0);
        player.Velocity = force * Time.fixedDeltaTime * player.Velocity;
    }

    /// <summary>
    /// Checks if an attack button is being used.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    protected AttackButtonState CheckAttacking(long input)
    {
        if ((input & InputConstants.INPUT_LIGHT_PUNCH) != 0)
        {
            return AttackButtonState.LightPunch;
        }

        if ((input & InputConstants.INPUT_MEDIUM_PUNCH) != 0)
        {
            return AttackButtonState.MediumPunch;
        }

        if ((input & InputConstants.INPUT_HEAVY_PUNCH) != 0)
        {
            return AttackButtonState.HeavyPunch;
        }

        if ((input & InputConstants.INPUT_LIGHT_KICK) != 0)
        {
            return AttackButtonState.LightKick;
        }

        if ((input & InputConstants.INPUT_MEDIUM_KICK) != 0)
        {
            return AttackButtonState.MediumKick;
        }

        if ((input & InputConstants.INPUT_HEAVY_KICK) != 0)
        {
            return AttackButtonState.HeavyKick;
        }

        return AttackButtonState.None;
    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return player;
    }
}