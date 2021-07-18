using SharedGame;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityGGPO;

public class GGPOComponent : GameManager
{
    public override void Shutdown()
    {
        GGPORunner.OnFrameDelay -= OnFrameDelay;

        base.Shutdown();
    }

    public override void StartLocalGame()
    {
        var game = new LocalRunner(new GGPOGameState(2));
        StartGame(game);
    }

    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        var game = new GGPORunner("GGPO-Demo", new GGPOGameState(connections.Count), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);

        GGPORunner.OnFrameDelay += OnFrameDelay;
    }

    public void StopGGPOGame()
    {
        Shutdown();
    }

    public string DisplayFrameInputs()
    {
        string fp = "";

        if (Runner != null)
        {
            GGPOGameState gameState = (GGPOGameState)Runner.Game;

            fp = string.Format("Frame: {0} - P1 input: {1}, P2 input: {2}\n", gameState.Framenumber, gameState.UnserializedInputsP1, gameState.UnserializedInputsP2);

            // Debug.Log(fp);
        }

        return fp;
    }
}
