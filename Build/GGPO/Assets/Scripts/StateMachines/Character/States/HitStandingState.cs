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
        {AnimationData.StandingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.HIGH_LIGHT},
        {AnimationData.StandingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.StandingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.HIGH_HEAVY},
        {AnimationData.StandingAttacks.LIGHT_KICK.ID, AnimationData.Hit.GUT_LIGHT},
        {AnimationData.StandingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.StandingAttacks.HEAVY_KICK.ID, AnimationData.Hit.HIGH_HEAVY},

        // Crouching attacks
        {AnimationData.CrouchingAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.GUT_LIGHT},
        {AnimationData.CrouchingAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.GUT_HEAVY},
        {AnimationData.CrouchingAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.UPPER},
        {AnimationData.CrouchingAttacks.LIGHT_KICK.ID, AnimationData.Hit.GUT_LIGHT},
        {AnimationData.CrouchingAttacks.MEDIUM_KICK.ID, AnimationData.Hit.GUT_HEAVY},
        {AnimationData.CrouchingAttacks.HEAVY_KICK.ID, AnimationData.Hit.SWEEP},

        //Jumping up attacks
        {AnimationData.JumpUpAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpUpAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpUpAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.HIGH_HEAVY},
        {AnimationData.JumpUpAttacks.LIGHT_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpUpAttacks.MEDIUM_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpUpAttacks.HEAVY_KICK.ID, AnimationData.Hit.HIGH_HEAVY},

        //Jumping forward attacks
        {AnimationData.JumpTowardAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpTowardAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpTowardAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.HIGH_HEAVY},
        {AnimationData.JumpTowardAttacks.LIGHT_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpTowardAttacks.MEDIUM_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpTowardAttacks.HEAVY_KICK.ID, AnimationData.Hit.HIGH_HEAVY},

        //Jumping backward attacks
        {AnimationData.JumpBackAttacks.LIGHT_PUNCH.ID, AnimationData.Hit.HIGH_LIGHT},
        {AnimationData.JumpBackAttacks.MEDIUM_PUNCH.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpBackAttacks.HEAVY_PUNCH.ID, AnimationData.Hit.HIGH_HEAVY},
        {AnimationData.JumpBackAttacks.LIGHT_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpBackAttacks.MEDIUM_KICK.ID, AnimationData.Hit.HIGH_MEDIUM},
        {AnimationData.JumpBackAttacks.HEAVY_KICK.ID, AnimationData.Hit.HIGH_HEAVY}
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
        if (player.CurrentlyHitByID == Guid.Empty)
        {
            return player;
        }

        FrameData attack = AnimationData.AttackLookup[player.CurrentlyHitByID];
        //Debug.Log($"Standing hit and it hurts!!! I got hit by {attack.AnimationKey}");
        PlayHitAnimation(ref player, _HitReactionLookup[player.CurrentlyHitByID]);
        int direction = player.LookDirection == LookDirection.Left ? 1 : -1;

        ApplyPush(ref player, direction, attack.HitPushBack);
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