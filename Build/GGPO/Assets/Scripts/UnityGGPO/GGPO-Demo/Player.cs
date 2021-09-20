using SharedGame;
using System;
using System.IO;
using Unity.Collections;
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
    public int Health;
    public int Stun;
    public int Power;
    public PlayerState State;
    public PlayerState JumpType;
    public AttackButtonState CurrentButtonPressed;
    public Guid CurrentAttackID;
    public Guid CurrentlyHitByID;
    public LookDirection LookDirection;
    public string AnimationKey;
    public float CurrentFrame;
    public float AnimationIndex;

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
        bw.Write(Health);
        bw.Write(Stun);
        bw.Write(Power);
        bw.Write((int)State);
        bw.Write((int)JumpType);
        bw.Write((int)CurrentButtonPressed);
        bw.Write(CurrentAttackID.ToString());
        bw.Write(CurrentlyHitByID.ToString());
        bw.Write((int)LookDirection);

        if (AnimationKey == null)
            AnimationKey = AnimationData.Movememt.IDLE.AnimationKey;

        bw.Write(AnimationKey);
        bw.Write(CurrentFrame);
        bw.Write(AnimationIndex);
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
        Health = br.ReadInt32();
        Stun = br.ReadInt32();
        Power = br.ReadInt32();
        State = (PlayerState)br.ReadInt32();
        JumpType = (PlayerState)br.ReadInt32();
        CurrentButtonPressed = (AttackButtonState)br.ReadInt32();
        CurrentAttackID = Guid.Parse(br.ReadString());
        CurrentlyHitByID = Guid.Parse(br.ReadString());
        LookDirection = (LookDirection)br.ReadInt32();
        AnimationKey = br.ReadString();
        CurrentFrame = br.ReadSingle();
        AnimationIndex = br.ReadSingle();
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;

        hashCode = hashCode * -1521134295 + ID.GetHashCode();
        hashCode = hashCode * -1521134295 + Position.GetHashCode();
        hashCode = hashCode * -1521134295 + Velocity.GetHashCode();
        hashCode = hashCode * -1521134295 + IsGrounded.GetHashCode();
        hashCode = hashCode * -1521134295 + IsJumping.GetHashCode();
        hashCode = hashCode * -1521134295 + IsAttacking.GetHashCode();
        hashCode = hashCode * -1521134295 + IsHit.GetHashCode();
        hashCode = hashCode * -1521134295 + Health.GetHashCode();
        hashCode = hashCode * -1521134295 + Stun.GetHashCode();
        hashCode = hashCode * -1521134295 + Power.GetHashCode();
        hashCode = hashCode * -1521134295 + State.GetHashCode();
        hashCode = hashCode * -1521134295 + JumpType.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentButtonPressed.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentAttackID.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentlyHitByID.GetHashCode();
        hashCode = hashCode * -1521134295 + LookDirection.GetHashCode();
        hashCode = hashCode * -1521134295 + AnimationKey.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentFrame.GetHashCode();
        hashCode = hashCode * -1521134295 + AnimationIndex.GetHashCode();

        return hashCode;
    }
};