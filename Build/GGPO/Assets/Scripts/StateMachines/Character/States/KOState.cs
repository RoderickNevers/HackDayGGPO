using Stateless.Graph;
using UnityEngine;

public class KOState : CharacterStateBlock
{
    public KOState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.KO)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();

        //PlayAnimation();
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    //private void PlayAnimation()
    //{
    //    switch (stateMachine.Move.AttackData.Type)
    //    {
    //        case AttackType.Weak:
    //            animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.HEART_ATTACK));
    //            break;
    //        case AttackType.Medium:
    //        case AttackType.Heavy:
    //            animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Hit.SPIN));
    //            characterController.ApplyImpact(80);
    //            break;
    //    }
    //}

    //protected override void HandleAnimationComplete()
    //{
    //    base.HandleAnimationComplete();
    //    animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.DEAD));
    //    stateMachine.ResetAttackerData();
    //}
}
