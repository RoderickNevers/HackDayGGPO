using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct Player
{
    public Vector3 Position;
    public Vector3 Velocity;
    public bool IsGrounded;
    public bool IsJumping;
    public bool IsAttacking;
    public bool IsHit;
    public PlayerState State;
    public PlayerState PreviousState;
    public AttackState CurrentAttack;
    public string AnimationKey;
    public float CurrentFrame;
    public float AnimationIndex;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Position.x);
        bw.Write(Position.y);
        bw.Write(Velocity.x);
        bw.Write(Velocity.y);
        bw.Write(IsGrounded);
        bw.Write(IsJumping);
        bw.Write(IsAttacking);
        bw.Write(IsHit);
        bw.Write((int)State);
        bw.Write((int)PreviousState);
        bw.Write((int)CurrentAttack);

        if (AnimationKey == null)
            AnimationKey = AnimationData.Movememt.IDLE.AnimationKey;

        bw.Write(AnimationKey);
        bw.Write(CurrentFrame);
        bw.Write(AnimationIndex);
    }

    public void Deserialize(BinaryReader br)
    {
        Position.x = br.ReadSingle();
        Position.y = br.ReadSingle();
        Velocity.x = br.ReadSingle();
        Velocity.y = br.ReadSingle();
        IsGrounded = br.ReadBoolean();
        IsJumping = br.ReadBoolean();
        IsAttacking = br.ReadBoolean();
        IsHit = br.ReadBoolean();
        State = (PlayerState)br.ReadInt32();
        PreviousState = (PlayerState)br.ReadInt32();
        CurrentAttack = (AttackState)br.ReadInt32();
        AnimationKey = br.ReadString();
        CurrentFrame = br.ReadSingle();
        AnimationIndex = br.ReadSingle();
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + Position.GetHashCode();
        hashCode = hashCode * -1521134295 + Velocity.GetHashCode();
        hashCode = hashCode * -1521134295 + IsGrounded.GetHashCode();
        hashCode = hashCode * -1521134295 + IsJumping.GetHashCode();
        hashCode = hashCode * -1521134295 + IsAttacking.GetHashCode();
        hashCode = hashCode * -1521134295 + IsHit.GetHashCode();
        hashCode = hashCode * -1521134295 + State.GetHashCode();
        hashCode = hashCode * -1521134295 + PreviousState.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentAttack.GetHashCode();
        hashCode = hashCode * -1521134295 + AnimationKey.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentFrame.GetHashCode();
        hashCode = hashCode * -1521134295 + AnimationIndex.GetHashCode();
        return hashCode;
    }
};