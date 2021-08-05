﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using System;
//using UnityEngine.InputSystem;

public class RetreatingState : CharacterStateBlock
{
    private const float retreatSpeed = 3f;

    public RetreatingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Retreating)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerAdvancing, CharacterState.Advancing)
            .Permit(CharacterStateTrigger.TriggerCrouching, CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerAttackGround, CharacterState.GroundedAttack)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.BlockStanding)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.Throw)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.GetThrown)
            .Permit(CharacterStateTrigger.TriggerJumpBack, CharacterState.JumpBack)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.HitStanding)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();

        //characterController.InputController.MovementCallback = HandleMovement;
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.WALK_BACKWARDS));
        //Debug.Log("ENTER RETREATING");
    }

    protected override void OnExitState()
    {
        base.OnExitState();

        //characterController.InputController.MovementCallback = null;
        //Debug.Log("EXIT RETREATING");
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();

    //    // Update the X based on which side of the screen the opponent is on.
    //    int directionMultiplier = characterController.CurrentSide == RingSide.LeftSide ? -1 : 1;
    //    characterController.Movement(directionMultiplier, retreatSpeed);
    //}

    protected override void AddListeners()
    {
        base.AddListeners();
        //characterController.InputController.OnInputCommand += HandleInputCommand;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        //characterController.InputController.OnInputCommand -= HandleInputCommand;
    }

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Retreating Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackGround, $"{ProjectConstants.MovelistStateKeys.STANDING}-{name}", e.Move);
    //}

    //private void HandleMovement()
    //{
    //    switch (characterController.InputController.CurrentInputDirection)
    //    {
    //        case InputCommandElement.Neutral:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerStanding);
    //            break;
    //        case InputCommandElement.UpForward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpTowards);
    //            break;
    //        case InputCommandElement.DownForward:
    //        case InputCommandElement.DownBack:
    //        case InputCommandElement.Down:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerCrouching);
    //            break;
    //        case InputCommandElement.Forward:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerAdvancing);
    //            break;
    //        case InputCommandElement.UpBack:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpBack);
    //            break;
    //        case InputCommandElement.Up:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerJumpUp);
    //            break;
    //    }
    //}
}