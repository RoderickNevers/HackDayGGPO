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

    public override Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();
    //    onTheGroundTime += Time.deltaTime + 1f;

    //    if (onTheGroundTime > 3f)
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerGetUp);
    //    }
    //}
}