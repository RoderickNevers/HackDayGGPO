using System;
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

    protected void ApplyDamage(ref Player player, int damage)
    {
        player.Health -= damage;
    }

    protected void GainHealth(ref Player player, int health)
    {
        player.Health += health;
    }

    protected void ApplyStun(ref Player player, int stun)
    {
        player.Stun += stun;
    }

    protected void ReduceStun(ref Player player, int stunReduction)
    {
        player.Stun -= stunReduction;
    }

    protected void ResetStun(ref Player player)
    {
        player.Stun = 0;
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
    /// <param name="input"></param>
    /// <returns></returns>
    protected AttackButtonState CheckAttacking(long input)
    {
        if ((input & InputConstants.INPUT_SLASH) != 0)
        {
            return AttackButtonState.Slash;
        }

        if ((input & InputConstants.INPUT_HEAVY_SLASH) != 0)
        {
            return AttackButtonState.HeavySlash;
        }

        if ((input & InputConstants.INPUT_GUARD_BREAK) != 0)
        {
            return AttackButtonState.GuardBreak;
        }

        return AttackButtonState.None;
    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    protected Player UpdateHitReaction(Player player, long input, Dictionary<Guid, FrameData> hitReactionLookup)
    {
        if (player.CurrentlyHitByID == Guid.Empty)
        {
            return player;
        }

        FrameData attack = AnimationData.AttackLookup[player.CurrentlyHitByID];
        if (attack.ID == AnimationData.StandingAttacks.GUARD_BREAK.ID)
        {
            player.State = PlayerState.KO;
        }

        int direction = player.LookDirection == LookDirection.Left ? 1 : -1;

        PlayHitAnimation(ref player, hitReactionLookup[player.CurrentlyHitByID]);
        ApplyPush(ref player, direction, attack.HitPushBack);
        ApplyDamage(ref player, attack.Damage);

        return player;
    }
}