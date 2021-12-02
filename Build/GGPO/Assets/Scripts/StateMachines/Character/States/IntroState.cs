using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class IntroState : CharacterStateBlock
{
    public IntroState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }
}