using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class BlockCrouchingState : CharacterStateBlock
{
    public BlockCrouchingState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        return base.UpdatePlayer(player, commandList, input);
    }
}