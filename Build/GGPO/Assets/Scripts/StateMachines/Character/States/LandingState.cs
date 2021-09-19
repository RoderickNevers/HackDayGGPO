using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class LandingState : CharacterStateBlock
{
    public LandingState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }
}