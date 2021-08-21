using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct GGPOGameState : IGame
{
    public Player[] Players;
    private CharacterControllerStateMachine _StateSimulator;

    public long UnserializedInputsP1 { get; private set; }
    public long UnserializedInputsP2 { get; private set; }
    public int Framenumber { get; private set; }
    public int Checksum => GetHashCode();

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
        Framenumber = 0;
        UnserializedInputsP1 = 0;
        UnserializedInputsP2 = 0;

        Players = new Player[num_players];
        _StateSimulator = new CharacterControllerStateMachine();

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player();
        }
    }

    public void InitPlayer(int index, Vector3 startPosition)
    {
        Players[index].Position = startPosition;
        Players[index].ID = index == 0 ? PlayerID.Player1 : PlayerID.Player2;
    }

    public Player GetPlayer(int index)
    {
        return Players[index];
    }

    public void LogInfo(string filename)
    {
        Debug.Log(filename);
    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        Framenumber++;

        // hacky way to store inputs on state
        UnserializedInputsP1 = inputs[0];
        UnserializedInputsP2 = inputs[1];

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = _StateSimulator.Run(Players[i], inputs[i]);
        }
    }

    public long ReadInputs(int id)
    {
        long input = 0;

        if (Input.GetKey(KeyCode.W))
        {
            input |= InputConstants.INPUT_UP;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input |= InputConstants.INPUT_DOWN;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input |= InputConstants.INPUT_LEFT;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input |= InputConstants.INPUT_RIGHT;
        }

        // Attacks

        if (Input.GetKeyDown(KeyCode.U))
        {
            input |= InputConstants.INPUT_LIGHT_PUNCH;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            input |= InputConstants.INPUT_MEDIUM_PUNCH;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            input |= InputConstants.INPUT_HEAVY_PUNCH;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            input |= InputConstants.INPUT_LIGHT_KICK;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            input |= InputConstants.INPUT_MEDIUM_KICK;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            input |= InputConstants.INPUT_HEAVY_KICK;
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