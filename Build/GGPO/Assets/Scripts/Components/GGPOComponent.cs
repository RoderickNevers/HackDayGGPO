using SharedGame;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityGGPO;

public class GGPOComponent : GameManager
{
    private IGameRunner runner;

    public override void StartLocalGame()
    {
        runner = new LocalRunner(new GGPOGameState(2));
        StartGame(runner);
    }

    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        var game = new GGPORunner("GGPO-Demo", new GGPOGameState(connections.Count), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);

        runner = game;
    }

    public string DisplayCurrentInputs()
    {
        string fp = "";

        if (runner != null)
        {
            GGPOGameState gameState = (GGPOGameState)runner.Game;
            fp = string.Format("Frame: {0} - P1 input: {1}, P2 input: {2}\n", gameState.Framenumber, gameState.UnserializedInputsP1, gameState.UnserializedInputsP2);

            Debug.Log(fp);
        }

        return fp;
    }
}
