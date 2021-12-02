﻿using UnityEngine;

public class DizzyState : CharacterStateBlock
{
    private const float dizzyTime = 2;
    private float dizzyTimer = 0;

    public DizzyState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    //protected override void HandleGetHit(object sender, HurtboxArgs e)
    //{
    //    base.HandleGetHit(sender, e);
    //    characterController.HealthComponent.RecoverFromDizzy();
    //    stateMachine.Fire(CharacterStateTrigger.TriggerHitStanding);
    //}

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();

    //    dizzyTimer += 1f * Time.deltaTime;

    //    if (dizzyTimer >= dizzyTime)
    //    {
    //        characterController.HealthComponent.RecoverFromDizzy();
    //        //TODO may not want to directly to standing
    //        stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //    }
    //}
}