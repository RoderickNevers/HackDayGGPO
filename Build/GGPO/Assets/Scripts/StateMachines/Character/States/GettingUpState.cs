using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class GettingUpState : CharacterStateBlock
{
    public GettingUpState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }
}