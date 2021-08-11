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
    public PlayerState State;
    public string AnimationClip;
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
        bw.Write((int)State);

        if (AnimationClip == null)
            AnimationClip = "Idle";

        bw.Write(AnimationClip);
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
        State = (PlayerState)br.ReadInt32();
        AnimationClip = br.ReadString();
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
        hashCode = hashCode * -1521134295 + State.GetHashCode();
        hashCode = hashCode * -1521134295 + AnimationClip.GetHashCode();
        hashCode = hashCode * -1521134295 + CurrentFrame.GetHashCode();
        hashCode = hashCode * -1521134295 + AnimationIndex.GetHashCode();
        return hashCode;
    }
};