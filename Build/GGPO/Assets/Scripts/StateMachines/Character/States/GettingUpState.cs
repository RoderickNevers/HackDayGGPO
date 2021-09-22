using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class GettingUpState : CharacterStateBlock
{
    public GettingUpState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    private void Regular()
    {

    }

    private void QuickRecovery()
    {

    }
}