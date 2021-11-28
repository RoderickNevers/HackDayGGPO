using SharedGame;
using System.Collections.Generic;
using UnityGGPO;

public class GGPOGameManager : GameManager
{
    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        var game = new GGPORunner("GGPO-Demo", new GGPOGameState(connections.Count), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);

        GGPORunner.OnFrameDelay += OnFrameDelay;
    }

    public override void StartLocalGame(bool isDebugMode)
    {
        var game = new LocalRunner(new GGPOGameState(2));
        StartGame(game, isDebugMode);
    }

    public override void StartGame(IGameRunner runner, bool isDebugMode = false)
    {
        base.StartGame(runner);
        if (!isDebugMode)
        {
            GameController.Instance.ShowHud();
        }
    }

    public void StopGGPOGame()
    {
        Shutdown();
    }

    public override void Shutdown()
    {
        GGPORunner.OnFrameDelay -= OnFrameDelay;

        base.Shutdown();
    }

    public string DisplayFrameInputs()
    {
        string fp = "";

        if (Runner != null)
        {
            GGPOGameState gameState = (GGPOGameState)Runner.Game;
            fp = string.Format("Frame: {0} - P1 input: {1}, P2 input: {2}\n", gameState.Framenumber, gameState.UnserializedInputsP1, gameState.UnserializedInputsP2);
        }

        return fp;
    }
}
