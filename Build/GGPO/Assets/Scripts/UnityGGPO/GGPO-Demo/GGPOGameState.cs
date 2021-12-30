using System;
using System.IO;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Rewired;
using SharedGame;
using System.Linq;

[Serializable]
public struct GGPOGameState : IGame
{
    public Player[] Players;
    private readonly CharacterControllerStateMachine _StateSimulator;
    private readonly List<Rewired.Player> _Controls;
    private readonly List<Vector3> _StartPositions;

    public long UnserializedInputsP1 { get; private set; }
    public long UnserializedInputsP2 { get; private set; }

    public List<PlayerCommandList> CommandLists;

    [SerializeField]
    private int frameNumber;

    public int Framenumber 
    {
        get => frameNumber;
        private set => frameNumber = value;
    }

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
        frameNumber = 0;
        UnserializedInputsP1 = 0;
        UnserializedInputsP2 = 0;

        Players = new Player[num_players];
        _StateSimulator = new CharacterControllerStateMachine();

        _Controls = new List<Rewired.Player>
        {
            ReInput.players.GetPlayer(0),
            ReInput.players.GetPlayer(1)
        };

        _StartPositions = new List<Vector3>();

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player();
        }

        // TODO :: Need to pipe this in when the players select their characters and start their match
        // If two players are the same character, only need to populate one command list for both of them
        CommandLists = new List<PlayerCommandList>()
        {
            Resources.Load<PlayerCommandList>("Systems/Samurai/CommandList/SamuraiCommandList"),
            Resources.Load<PlayerCommandList>("Systems/Samurai/CommandList/SamuraiCommandList"),
        };

        CommandLists[0].PopulateLookups();

        // todo: need to unsubscribe too
        GameController.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(object sender, MatchState state)
    {
        switch (state)
        {
            case MatchState.PreBattle:
                ResetPlayers();
                break;
            case MatchState.Battle:
                break;
            case MatchState.PostBattle:
                break;
        }
    }

    public GGPOGameState Clone()
    {
        // COPY OVER ALL 
        GGPOGameState newState = new GGPOGameState(Players.Length);
        newState.Framenumber = Framenumber;
        newState.UnserializedInputsP1 = UnserializedInputsP1;
        newState.UnserializedInputsP2 = UnserializedInputsP2;

        for (int i = 0; i < newState.Players.Length; ++i)
        {
            newState.Players[i].Position = Players[i].Position;
            newState.Players[i].Velocity = Players[i].Velocity;
        }

        return newState;
    }

    public void InitPlayer(int index, Vector3 startPosition)
    {
        if (_StartPositions.Count == 0 || _StartPositions.Count == 1)
        {
            _StartPositions.Add(startPosition);
        }

        Players[index].Position = startPosition;
        Players[index].ID = index == 0 ? PlayerID.Player1 : PlayerID.Player2;
        Players[index].Health = 1;
        Players[index].HitStunTime = 0;
        Players[index].Power = 0;
        Players[index].State = PlayerState.Standing;
        Players[index].IsHit = false;
        Players[index].IsJumping = false;
        Players[index].IsAttacking = false;
    }

    public Player GetPlayer(int index)
    {
        return Players[index];
    }

    public ref Player GetPlayerRef(int index)
    {
        return ref Players[index];
    }

    public void LogInfo(string filename)
    {
        Debug.Log(filename);
    }

    public void UpdateSimulation(long[] inputs, int disconnect_flags)
    {
        Framenumber++;

        // hacky way to store inputs on state
        UnserializedInputsP1 = inputs[0];
        UnserializedInputsP2 = inputs[1];

        GameController.Instance.GameState = GameController.Instance.UpdateGameProgress(Players);

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = _StateSimulator.Run(Players[i], CommandLists[i], inputs[i]);
        }
    }

    public long ReadInputs(int id)
    {
        long input = 0;
        Rewired.Player control = _Controls[id];

        if (GameController.Instance.GameState == MatchState.Battle)
        {
            // Movement Buttons
            if (control.GetButton(RewiredConsts.Action.UP))
            {
                input |= (int)InputButtons.INPUT_UP;
            }

            if (control.GetButton(RewiredConsts.Action.DOWN))
            {
                input |= (int)InputButtons.INPUT_DOWN;
            }

            if (control.GetButton(RewiredConsts.Action.LEFT))
            {
                input |= (int)InputButtons.INPUT_LEFT;
            }

            if (control.GetButton(RewiredConsts.Action.RIGHT))
            {
                input |= (int)InputButtons.INPUT_RIGHT;
            }

            // Action Buttons
            if (control.GetButtonDown(RewiredConsts.Action.BUTTON_0))
            {
                input |= (int)InputButtons.INPUT_BUTTON_0;
            }

            if (control.GetButtonDown(RewiredConsts.Action.BUTTON_1))
            {
                input |= (int)InputButtons.INPUT_BUTTON_1;
            }

            if (control.GetButtonDown(RewiredConsts.Action.BUTTON_2))
            {
                input |= (int)InputButtons.INPUT_BUTTON_2;
            }

            if (control.GetButtonDown(RewiredConsts.Action.BUTTON_3))
            {
                input |= (int)InputButtons.INPUT_BUTTON_3;
            }

            if (control.GetButtonDown(RewiredConsts.Action.BUTTON_4))
            {
                input |= (int)InputButtons.INPUT_BUTTON_4;
            }

            if (control.GetButtonDown(RewiredConsts.Action.BUTTON_5))
            {
                input |= (int)InputButtons.INPUT_BUTTON_5;
            }

            // Utility Buttons
            if (control.GetButtonDown(RewiredConsts.Action.START))
            {
                input |= (int)InputButtons.INPUT_BUTTON_START;
            }

            if (control.GetButtonDown(RewiredConsts.Action.SELECT))
            {
                input |= (int)InputButtons.INPUT_BUTTON_SELECT;
            }
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

    private void ResetPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            InitPlayer(i, _StartPositions[i]);
        }
    }
}
