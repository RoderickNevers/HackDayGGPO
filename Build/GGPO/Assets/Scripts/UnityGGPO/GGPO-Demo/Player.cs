using System;
using System.IO;

using UnityEngine;

[Serializable]
public struct Player
{
    public PlayerID ID;
    public Vector3 Position;
    public Vector3 Velocity;
    public bool IsGrounded;
    public bool IsJumping;
    public bool IsAttacking;
    public bool IsHit;
    public bool IsStunned;
    public bool IsTakingDamage;
    public bool IsCloseToOpponent;
    public bool IsBeingPushed;
    public int Health;
    public float HitStunTime;
    public float BlockStunTime;
    public int Power;
    public float CurrentPushbackTime;
    public PlayerState State;
    public PlayerState JumpType;
    public AttackButtonState CurrentButtonPressed;
    public Guid CurrentAttackID;
    public Guid CurrentlyHitByID;
    public LookDirection LookDirection;
    public string AnimationKey;
    public float CurrentFrame;
    public float AnimationIndex;
    public int Loses;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((int)ID);
        bw.Write(Position.x);
        bw.Write(Position.y);
        bw.Write(Velocity.x);
        bw.Write(Velocity.y);
        bw.Write(IsGrounded);
        bw.Write(IsJumping);
        bw.Write(IsAttacking);
        bw.Write(IsHit);
        bw.Write(IsStunned);
        bw.Write(IsTakingDamage);
        bw.Write(IsCloseToOpponent);
        bw.Write(IsBeingPushed);
        bw.Write(Health);
        bw.Write(HitStunTime);
        bw.Write(BlockStunTime);
        bw.Write(Power);
        bw.Write(CurrentPushbackTime);
        bw.Write((int)State);
        bw.Write((int)JumpType);
        bw.Write((int)CurrentButtonPressed);
        bw.Write(CurrentAttackID.ToString());
        bw.Write(CurrentlyHitByID.ToString());
        bw.Write((int)LookDirection);

        // TODO: FIX THIS FLOATING STRING
        if (AnimationKey == null)
            AnimationKey = "Idle";

        bw.Write(AnimationKey);
        bw.Write(CurrentFrame);
        bw.Write(AnimationIndex);
        bw.Write(Loses);
    }

    public void Deserialize(BinaryReader br)
    {
        ID = (PlayerID)br.ReadInt32();
        Position.x = br.ReadSingle();
        Position.y = br.ReadSingle();
        Velocity.x = br.ReadSingle();
        Velocity.y = br.ReadSingle();
        IsGrounded = br.ReadBoolean();
        IsJumping = br.ReadBoolean();
        IsAttacking = br.ReadBoolean();
        IsHit = br.ReadBoolean();
        IsStunned = br.ReadBoolean();
        IsTakingDamage = br.ReadBoolean();
        IsCloseToOpponent = br.ReadBoolean();
        IsBeingPushed = br.ReadBoolean();
        Health = br.ReadInt32();
        HitStunTime = br.ReadSingle();
        BlockStunTime = br.ReadSingle();
        Power = br.ReadInt32();
        CurrentPushbackTime = br.ReadSingle();
        State = (PlayerState)br.ReadInt32();
        JumpType = (PlayerState)br.ReadInt32();
        CurrentButtonPressed = (AttackButtonState)br.ReadInt32();
        CurrentAttackID = Guid.Parse(br.ReadString());
        CurrentlyHitByID = Guid.Parse(br.ReadString());
        LookDirection = (LookDirection)br.ReadInt32();
        AnimationKey = br.ReadString();
        CurrentFrame = br.ReadSingle();
        AnimationIndex = br.ReadSingle();
        Loses = br.ReadInt32();
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        int number = -1521134295;

        hashCode = hashCode * number + ID.GetHashCode();
        hashCode = hashCode * number + Position.GetHashCode();
        hashCode = hashCode * number + Velocity.GetHashCode();
        hashCode = hashCode * number + IsGrounded.GetHashCode();
        hashCode = hashCode * number + IsJumping.GetHashCode();
        hashCode = hashCode * number + IsAttacking.GetHashCode();
        hashCode = hashCode * number + IsHit.GetHashCode();
        hashCode = hashCode * number + IsStunned.GetHashCode();
        hashCode = hashCode * number + IsTakingDamage.GetHashCode();
        hashCode = hashCode * number + IsCloseToOpponent.GetHashCode();
        hashCode = hashCode * number + IsBeingPushed.GetHashCode();
        hashCode = hashCode * number + Health.GetHashCode();
        hashCode = hashCode * number + HitStunTime.GetHashCode();
        hashCode = hashCode * number + BlockStunTime.GetHashCode();
        hashCode = hashCode * number + Power.GetHashCode();
        hashCode = hashCode * number + CurrentPushbackTime.GetHashCode(); 
        hashCode = hashCode * number + State.GetHashCode();
        hashCode = hashCode * number + JumpType.GetHashCode();
        hashCode = hashCode * number + CurrentButtonPressed.GetHashCode();
        hashCode = hashCode * number + CurrentAttackID.GetHashCode();
        hashCode = hashCode * number + CurrentlyHitByID.GetHashCode();
        hashCode = hashCode * number + LookDirection.GetHashCode();
        hashCode = hashCode * number + AnimationKey.GetHashCode();
        hashCode = hashCode * number + CurrentFrame.GetHashCode();
        hashCode = hashCode * number + AnimationIndex.GetHashCode();
        hashCode = hashCode * number + Loses.GetHashCode();

        return hashCode;
    }
};