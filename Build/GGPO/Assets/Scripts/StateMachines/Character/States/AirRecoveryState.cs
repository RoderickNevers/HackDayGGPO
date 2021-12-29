using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class AirRecoveryState : CharacterStateBlock
{
    public AirRecoveryState()
    {

    }

    public override Player UpdatePlayer(Player player, PlayerCommandList commandList, long input)
    {
        return base.UpdatePlayer(player, commandList, input);
    }

    //protected override void OnUpdate()
    //{

    //}
}
