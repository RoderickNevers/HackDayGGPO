using Stateless.Graph;
using UnityEngine;

public class KOState : CharacterStateBlock
{
    public KOState()
    {

    }

    public Player UpdatePlayer(Player player, long input)
    {
        return player;
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
