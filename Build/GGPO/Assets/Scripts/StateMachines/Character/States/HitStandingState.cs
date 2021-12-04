using System;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
//using System.Windows.Markup;
//using System.Collections.Generic;
//using EZCameraShake;

public class HitStandingState : CharacterStateBlock
{
    private const float SCREEN_FREEZE_TIME = 0.11f;

    private readonly Dictionary<Guid, FrameData> _HitReactionLookup = new Dictionary<Guid, FrameData>()
    {
        // Standing attacks
        {AnimationData.StandingAttacks.SLASH.ID, AnimationData.Hit.DEAD_4},
        {AnimationData.StandingAttacks.HEAVY_SLASH.ID, AnimationData.Hit.DEAD_4},
        {AnimationData.StandingAttacks.GUARD_BREAK.ID, AnimationData.Hit.HIT_3},
    };

    public HitStandingState()
    {

    }

    //ScreenFreeze();
    //ScreenShake(stateMachine.Move.AttackData.Type);
    //characterController.HealthComponent.ApplyDamage(stateMachine.Move.AttackData);
    //characterController.HealthComponent.OnResetDizzyLock?.Invoke(this, new EventArgs());
        
    //if (characterController.HealthComponent.IsAlive)
    //{ 
    //    PlayHitAnimation(stateMachine.Move.AttackData.Type);
    //}
    //else
    //{
    //    KO();
    //}

    public override Player UpdatePlayer(Player player, long input)
    {
        return UpdateHitReaction(player, _HitReactionLookup);
    }

    //protected virtual void HandleDizzy(object sender, EventArgs e)
    //{
    //    stateMachine.Fire(CharacterStateTrigger.TriggerDizzy);
    //}

    //private void KO()
    //{
    //    stateMachine.Fire(CharacterStateTrigger.TriggerKO);
    //}

    //private void ScreenFreeze()
    //{
    //    Sequence seq = DOTween.Sequence().SetUpdate(true);
    //    seq.AppendCallback(() => { Time.timeScale = 0f; });
    //    seq.AppendInterval(SCREEN_FREEZE_TIME);
    //    seq.AppendCallback(() => { Time.timeScale = 1f; });
    //    seq.Play();
    //}

    //private void ScreenShake(AttackType type)
    //{
    //    switch(type)
    //    {
    //        case AttackType.Medium:
    //        case AttackType.Heavy:
    //            CameraShaker.Instance.ShakeOnce(1f, 4f, 0.05f, 0.05f);
    //            break;
    //    }
    //}
}