﻿using System;
using UnityEngine;
using Stateless;

public class CharacterStateBlockInitData
{
    public StateMachine<CharacterState, CharacterStateTrigger> StateMachine;
    public Player Player;

    public CharacterStateBlockInitData(StateMachine<CharacterState, CharacterStateTrigger> stateMachine, Player player)
    {
        StateMachine = stateMachine;
    }
}

public class CharacterStateBlock : AbstractStateBlock, IDisposable
{
    protected StateMachine<CharacterState, CharacterStateTrigger> _StateMachine;
    //protected FighterController characterController;
    //protected Animator animator;
    //protected PlayerIDs _PlayerID;

    //protected Move CurrentAttack
    //{
    //    get => characterController.HitboxCompoment.CurrentAttack;
    //    set => characterController.HitboxCompoment.CurrentAttack = value;
    //}

    public CharacterStateBlock(CharacterStateBlockInitData stateBlockData)
    {
        _StateMachine = stateBlockData.StateMachine;
        //animator = stateBlockData.Animator;
        //characterController = stateBlockData.CharacterController;
        //_PlayerID = _BaseCharacterController.CharacterManager.PlayerID;
    }

    public virtual void Dispose()
    {
        RemoveListeners();
        _StateMachine = null;
        //animator = null;
        //characterController = null;
    }

    protected override void AddListeners()
    {
        //UpdateHandle.OnUpdate += OnUpdate;
        //characterController.AnimationEvents.OnAnimationComplete += HandleAnimationComplete;
        //characterController.HurtboxComponent.OnHit += HandleGetHit;

    }

    protected override void RemoveListeners()
    {
        //UpdateHandle.OnUpdate -= OnUpdate;
        //characterController.AnimationEvents.OnAnimationComplete -= HandleAnimationComplete;
        //characterController.HurtboxComponent.OnHit -= HandleGetHit;
    }

    protected override void OnEnterState()
    {
        AddListeners();
    }

    protected override void OnExitState()
    {
        RemoveListeners();
    }

    //protected override void OnUpdate()
    //{

    //}

    //protected bool DoesAttackExist(string key)
    //{
    //    return characterController.Movelist.AttackList.ContainsKey(key);
    //}

    //protected virtual void ExecuteAttack(string key, Move move = null)
    //{
    //    CurrentAttack = characterController.Movelist.AttackList[key];

    //    // Attack will be included for special attacks.
    //    // Run the special attack command.
    //    if (move != null)
    //    {
    //        CurrentAttack.SpecialMoveCommand.OnComplete += HandleSpecialMoveComplete;
    //        CurrentAttack.Execute();
    //    }
    //    else
    //    {
    //        // Regular attacks can just play their animation.
    //        animator.Play(Animator.StringToHash(characterController.Movelist.AttackList[key].AttackData.AnimatorKey));
    //    }
    //}

    //protected virtual void HandleSpecialMoveComplete(object sender, EventArgs e)
    //{
    //    CurrentAttack.SpecialMoveCommand.OnComplete -= HandleSpecialMoveComplete;
    //}

    //protected virtual void HandleAnimationComplete()
    //{

    //}

    //protected virtual void HandleGetHit(object sender, HurtboxArgs e)
    //{
    //    characterController.CorrectLookDirection();
    //    characterController.ApplyImpact(e.Move.AttackData.HitPushBack);

    //    switch (e.Move.AttackData.Property)
    //    {
    //        case AttackProperty.Sweep:
    //            stateMachine.FireOnHit(CharacterStateTrigger.TriggerSweep, e.Move);
    //            break;
    //        default:
    //            stateMachine.FireOnHit(CharacterStateTrigger.TriggerHitStanding, e.Move);
    //            break;
    //    }
    //}
}