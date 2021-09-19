using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class BeingThrownState : CharacterStateBlock
{
    public BeingThrownState()
    {

    }

    public override Player UpdatePlayer(Player player, long input)
    {
        return base.UpdatePlayer(player, input);
    }
}
