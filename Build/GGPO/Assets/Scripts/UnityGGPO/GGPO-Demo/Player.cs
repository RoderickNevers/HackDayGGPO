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
    public MoveDirection MoveDirection;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Position.x);
        bw.Write(Position.y);
        bw.Write(Velocity.x);
        bw.Write(Velocity.y);
        bw.Write(IsGrounded);
        bw.Write(IsJumping);
        bw.Write((int)MoveDirection);
    }

    public void Deserialize(BinaryReader br)
    {
        Position.x = br.ReadSingle();
        Position.y = br.ReadSingle();
        Velocity.x = br.ReadSingle();
        Velocity.y = br.ReadSingle();
        IsGrounded = br.ReadBoolean();
        IsJumping = br.ReadBoolean();
        MoveDirection = (MoveDirection)br.ReadInt32();
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + Position.GetHashCode();
        hashCode = hashCode * -1521134295 + Velocity.GetHashCode();
        hashCode = hashCode * -1521134295 + IsGrounded.GetHashCode();
        hashCode = hashCode * -1521134295 + IsJumping.GetHashCode();
        hashCode = hashCode * -1521134295 + MoveDirection.GetHashCode();
        return hashCode;
    }
};