using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class LandingState : CharacterStateBlock
{
    public LandingState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        return player;
    }
}