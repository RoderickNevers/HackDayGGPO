using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;
using Rewired;
using System.Collections.Generic;

[Serializable]
public struct GGPOGameState : IGame
{
    public Player[] Players;
    private CharacterControllerStateMachine _StateSimulator;
    //private Rewired.Player controllerInput;
    private List<Rewired.Player> controls;

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
        //controllerInput = ReInput.players.GetPlayer(0);

        controls = new List<Rewired.Player>
        {
            ReInput.players.GetPlayer(0),
            ReInput.players.GetPlayer(1)
        };

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

    public ref Player GetPlayerReference(int index)
    {
        return ref Players[index];
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
        Rewired.Player control = controls[id];

        if (control.GetButton(RewiredConsts.Action.UP))
        {
            input |= InputConstants.INPUT_UP;
        }

        if (control.GetButton(RewiredConsts.Action.DOWN))
        {
            input |= InputConstants.INPUT_DOWN;
        }

        if (control.GetButton(RewiredConsts.Action.LEFT))
        {
            input |= InputConstants.INPUT_LEFT;
        }

        if (control.GetButton(RewiredConsts.Action.RIGHT))
        {
            input |= InputConstants.INPUT_RIGHT;
        }

        // Attacks

        if (control.GetButton(RewiredConsts.Action.LIGHTPUNCH))
        {
            input |= InputConstants.INPUT_LIGHT_PUNCH;
        }

        if (control.GetButton(RewiredConsts.Action.MEDIUMPUNCH))
        {
            input |= InputConstants.INPUT_MEDIUM_PUNCH;
        }

        if (control.GetButton(RewiredConsts.Action.HEAVYPUNCH))
        {
            input |= InputConstants.INPUT_HEAVY_PUNCH;
        }

        if (control.GetButton(RewiredConsts.Action.LIGHTKICK))
        {
            input |= InputConstants.INPUT_LIGHT_KICK;
        }

        if (control.GetButton(RewiredConsts.Action.MEDIUMKICK))
        {
            input |= InputConstants.INPUT_MEDIUM_KICK;
        }

        if (control.GetButton(RewiredConsts.Action.HEAVYKICK))
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