using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class OnTheGroundState : CharacterStateBlock, IStateSimulator
{
    float onTheGroundTime = 0;

    public OnTheGroundState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.OnTheGround)
            .Permit(CharacterStateTrigger.TriggerGetUp, CharacterState.GettingUp)
            .Permit(CharacterStateTrigger.TriggerKO, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.DEAD));
    }

    protected override void OnExitState()
    {
        base.OnExitState();
    }

    public Player UpdatePlayer(Player player, long input)
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