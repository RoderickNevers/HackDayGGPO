using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class OnTheGroundState : CharacterStateBlock
{
    float onTheGroundTime = 0;

    public OnTheGroundState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        // How did the player fall -> Hard or Soft Knockdown
        // Hard -> Nornal & Delayed
        // Soft -> Quick, Normal & Delayed
        onTheGroundTime += Time.deltaTime + 1f;
        if (onTheGroundTime > 3f)
        {
            //Get Up
        }

        return player;
    }

    private void Quick()
    {

    }

    private void Normal()
    {

    }

    private void Delayed()
    {

    }

    private void ResetTimer()
    {
        onTheGroundTime = 0;
    }
}