using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class BlockStandingState : CharacterStateBlock
{
    public BlockStandingState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return base.UpdatePlayer(player, input);
    }
}
