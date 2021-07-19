using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct Player
{
    public Vector3 position;
    public Vector3 velocity;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        //bw.Write(position.z);
        bw.Write(velocity.x);
        //bw.Write(velocity.z);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        //position.z = br.ReadSingle();
        velocity.x = br.ReadSingle();
        //velocity.z = br.ReadSingle();
    }

    // @LOOK Not hashing bullets.
    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + position.GetHashCode();
        hashCode = hashCode * -1521134295 + velocity.GetHashCode();
        return hashCode;
    }
};