using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class BlockCrouchingState : CharacterStateBlock
{
    public BlockCrouchingState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return base.UpdatePlayer(player, input);
    }
}