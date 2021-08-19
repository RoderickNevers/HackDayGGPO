using System;
using UnityEngine;
//using DG.Tweening;
//using System.Windows.Markup;
//using System.Collections.Generic;
//using EZCameraShake;

public class HitStandingState : CharacterStateBlock
{
    private const float SCREEN_FREEZE_TIME = 0.11f;
    public HitStandingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.HitStanding)
            .PermitReentry(CharacterStateTrigger.TriggerHitStanding)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerDizzy, CharacterState.Dizzy)
            .Permit(CharacterStateTrigger.TriggerKO, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();

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
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        //characterController.HealthComponent.OnDizzy += HandleDizzy;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        //characterController.HealthComponent.OnDizzy -= HandleDizzy;
    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
    }

    //protected override void HandleAnimationComplete()
    //{
    //    base.HandleAnimationComplete();

    //    stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //    stateMachine.ResetAttackerData();
    //}

    //protected virtual void HandleDizzy(object sender, EventArgs e)
    //{
    //    stateMachine.Fire(CharacterStateTrigger.TriggerDizzy);
    //}

    //private void PlayHitAnimation(AttackType incommingAttack)
    //{
    //    switch (incommingAttack)
    //    {
    //        case AttackType.Weak:
    //            animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Hit.STANDING_LIGHT));
    //            break;
    //        case AttackType.Medium:
    //            animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Hit.STANDING_MEDIUM));
    //            break;
    //        case AttackType.Heavy:
    //            animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Hit.STANDING_HEAVY));
    //            break;
    //    }
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