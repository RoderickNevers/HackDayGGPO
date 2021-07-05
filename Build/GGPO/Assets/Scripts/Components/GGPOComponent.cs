using SharedGame;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityGGPO;

public class GGPOComponent : GameManager
{
    public override void StartLocalGame()
    {
        StartGame(new LocalRunner(new GGPOGameState(2)));
    }

    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        var game = new GGPORunner("GGPO-Demo", new GGPOGameState(connections.Count), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);
    }
}
