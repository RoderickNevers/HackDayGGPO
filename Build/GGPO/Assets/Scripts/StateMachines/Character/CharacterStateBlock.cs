using System;
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
        if (player.IncomingAttackFrameData == null || player.IncomingAttackFrameData == FrameData.Empty)
        {
            return player;
        }

        int direction = player.LookDirection == LookDirection.Left ? 1 : -1;
        FrameData incomingAttack = player.IncomingAttackFrameData;

        if (IsAnimationComplete(player, incomingAttack))
        {
            return player;
        }

        PlayAnimationOneShot(ref player, commandList.DeathReactions[3]); //TODO
        ApplyHitStun(ref player, incomingAttack.HitStun);
        ApplyPush(ref player, direction, incomingAttack.HitPushBack);
        ApplyDamage(ref player, incomingAttack.Damage);

        return player;
    }

    protected Player UpdateBlockReaction(Player player, PlayerCommandList commandList)
    {
        if (player.IncomingAttackFrameData == null || player.IncomingAttackFrameData == FrameData.Empty)
        {
            return player;
        }

        int direction = player.LookDirection == LookDirection.Left ? 1 : -1;
        FrameData incomingAttack = player.IncomingAttackFrameData;

        if (IsAnimationComplete(player, incomingAttack))
        {
            return player;
        }

        PlayAnimationOneShot(ref player, commandList.Block.FrameData);
        ApplyBlockStun(ref player, incomingAttack.BlockStun);
        ApplyPush(ref player, direction, incomingAttack.BlockPushBack);
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
        if ((input & (int)InputButtons.INPUT_BUTTON_0) != 0)
        {
            return AttackButtonState.Button_1;
        }

        if ((input & (int)InputButtons.INPUT_BUTTON_1) != 0)
        {
            return AttackButtonState.Button_2;
        }

        if ((input & (int)InputButtons.INPUT_BUTTON_2) != 0)
        {
            return AttackButtonState.Button_3;
        }

        if ((input & (int)InputButtons.INPUT_BUTTON_3) != 0)
        {
            return AttackButtonState.Button_4;
        }

        if ((input & (int)InputButtons.INPUT_BUTTON_4) != 0)
        {
            return AttackButtonState.Button_5;
        }

        if ((input & (int)InputButtons.INPUT_BUTTON_5) != 0)
        {
            return AttackButtonState.Button_6;
        }

        return AttackButtonState.None;
    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        return player;
    }
}