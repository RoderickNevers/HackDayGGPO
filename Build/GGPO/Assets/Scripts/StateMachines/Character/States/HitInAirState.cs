using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class HitInAirState : CharacterStateBlock
{
    public HitInAirState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        Debug.Log("Jumping hit and it sucks");
        return player;
    }
}