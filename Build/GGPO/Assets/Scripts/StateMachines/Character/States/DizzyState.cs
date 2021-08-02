using UnityEngine;

public class DizzyState : CharacterStateBlock
{
    private const float dizzyTime = 2;
    private float dizzyTimer = 0;

    public DizzyState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Dizzy)
            .PermitReentry(CharacterStateTrigger.TriggerDizzy)
            .Permit(CharacterStateTrigger.TriggerHitStanding, CharacterState.HitStanding)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerSweep, CharacterState.Sweep)
            .Permit(CharacterStateTrigger.TriggerKO, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //Debug.Log("Dizzy");
        //dizzyTimer = 0;
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.DIZZY));
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