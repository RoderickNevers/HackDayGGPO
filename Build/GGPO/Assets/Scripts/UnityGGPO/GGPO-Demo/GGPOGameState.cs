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
        bw.Write(position.z);
        bw.Write(velocity.x);
        bw.Write(velocity.z);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.z = br.ReadSingle();
        velocity.x = br.ReadSingle();
        velocity.z = br.ReadSingle();
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

[Serializable]
public struct GGPOGameState : IGame
{
    public const int INPUT_FORWARD = (1 << 0);
    public const int INPUT_BACKWARD = (1 << 1);
    public const int INPUT_LEFT = (1 << 2);
    public const int INPUT_RIGHT = (1 << 3);

    public long UnserializedInputsP1 { get; private set; }
    public long UnserializedInputsP2 { get; private set; }

    public int Framenumber { get; private set; }
    public int Checksum => GetHashCode();

    public Player[] Players;

    private int _Speed;

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(Framenumber);
        writer.Write(Players.Length);

        for (int i = 0; i < Players.Length; ++i)
        {
            Players[i].Serialize(writer);
        }
    }

    public void Deserialize(BinaryReader reader)
    {
        Framenumber = reader.ReadInt32();
        int length = reader.ReadInt32();

        if (length != Players.Length)
        {
            Players = new Player[length];
        }

        for (int i = 0; i < Players.Length; ++i)
        {
            Players[i].Deserialize(reader);
        }
    }

    public NativeArray<byte> ToBytes()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                Serialize(writer);
            }

            return new NativeArray<byte>(memoryStream.ToArray(), Allocator.Persistent);
        }
    }

    public void FromBytes(NativeArray<byte> bytes)
    {
        using (var memoryStream = new MemoryStream(bytes.ToArray()))
        {
            using (var reader = new BinaryReader(memoryStream))
            {
                Deserialize(reader);
            }
        }
    }

    /*
        * InitGameState --
        *
        * Initialize our game state.
        */

    public GGPOGameState(int num_players)
    {
        // consts
        _Speed = 2;

        Framenumber = 0;
        UnserializedInputsP1 = 0;
        UnserializedInputsP2 = 0;

        Players = new Player[num_players];

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player();
        }
    }

    public GGPOGameState Clone()
    {
        GGPOGameState newState = new GGPOGameState(Players.Length);
        newState.Framenumber = Framenumber;
        newState.UnserializedInputsP1 = UnserializedInputsP1;
        newState.UnserializedInputsP2 = UnserializedInputsP2;

        for (int i = 0; i < newState.Players.Length; ++i)
        {
            newState.Players[i].position = Players[i].position;
            newState.Players[i].velocity = Players[i].velocity;
        }

        return newState;
    }

    public void InitPlayer(int index, Vector3 startPosition)
    {
        Players[index].position = startPosition;
    }

    public Player GetPlayer(int index)
    {
        return Players[index];
    }

    public void ParsePlayerInputs(long inputs, int i)
    {
        GGPORunner.LogGame($"parsing ship {i} inputs: {inputs}.");
        
        Players[i].velocity.Set(0, 0, 0);

        if ((inputs & INPUT_LEFT) != 0)
        {
            Players[i].velocity.Set(-1, 0, 0);
        }

        if ((inputs & INPUT_RIGHT) != 0)
        {
            Players[i].velocity.Set(1, 0, 0);
        }

        if ((inputs & INPUT_FORWARD) != 0)
        {
            Players[i].velocity.Set(0, 0, 1);
        }

        if ((inputs & INPUT_BACKWARD) != 0)
        {
            Players[i].velocity.Set(0, 0, -1);
        }

        Players[i].velocity = Players[i].velocity * _Speed;
    }

    public void MovePlayer(int index)
    {
        var player = Players[index];

        Players[index].position = player.position + player.velocity;
    }

    public void LogInfo(string filename)
    {

    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        Framenumber++;

        // hacky way to store inputs on state
        UnserializedInputsP1 = inputs[0];
        UnserializedInputsP2 = inputs[1];

        for (int i = 0; i < Players.Length; i++)
        {
            ParsePlayerInputs(inputs[i], i);
            MovePlayer(i);
        }
    }

    public long ReadInputs(int id)
    {
        long input = 0;

        if (Input.GetKey(KeyCode.W))
        {
            input |= INPUT_FORWARD;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input |= INPUT_BACKWARD;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input |= INPUT_LEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input |= INPUT_RIGHT;
        }

        return input;
    }

    public void FreeBytes(NativeArray<byte> data)
    {
        if (data.IsCreated)
        {
            data.Dispose();
        }
    }

    public override int GetHashCode()
    {
        int hashCode = -1214587014;
        hashCode = hashCode * -1521134295 + Framenumber.GetHashCode();
        foreach (var player in Players)
        {
            hashCode = hashCode * -1521134295 + player.GetHashCode();
        }
        return hashCode;
    }
}