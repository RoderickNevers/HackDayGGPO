using System;
using UnityEngine;

public class AdvancingState : CharacterStateBlock
{
    private const float advanceSpeed = 3.5f;

    public AdvancingState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.Advancing)
            .Permit(CharacterStateTrigger.TriggerStanding, CharacterState.Standing)
            .Permit(CharacterStateTrigger.TriggerRetreating, CharacterState.Retreating)
            .Permit(CharacterStateTrigger.TriggerCrouching, CharacterState.Crouching)
            .Permit(CharacterStateTrigger.TriggerAttackGround, CharacterState.GroundedAttack)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.Throw)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.GetThrown)
            .Permit(CharacterStateTrigger.TriggerJumpTowards, CharacterState.JumpToward)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.HitStanding)
            //.Permit(ChangeState.TriggerRetreating, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();

        //characterController.InputController.MovementCallback = HandleMovement;
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.WALK_FORWARDS));
        //Debug.Log("ENTER ADVANCING");
    }

    protected override void OnExitState()
    {
        base.OnExitState();

        //characterController.InputController.MovementCallback = null;
        //Debug.Log("EXIT ADVANCING");
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();

    //    // Update the X based on which side of the screen the opponent is on.
    //    int directionMultiplier = characterController.CurrentSide == RingSide.LeftSide ? 1 : -1;
    //    characterController.Movement(directionMultiplier, advanceSpeed);
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

    internal void Update()
    {
        Debug.Log("advancing lol");
    }

    public Player UpdatePlayer(Player player, long input)
    {
        float velocity = 0;

        if (player.IsHit)
        {
            Debug.Log("IM HIT CAPTAIN!!!!!!!!!!!!!!!");
        }
        //Returning attack
        else if (player.IsAttacking)
        {
            switch (player.CurrentAttack)
            {
                case AttackState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_PUNCH);
                    break;
                case AttackState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_PUNCH);
                    break;
                case AttackState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_KICK);
                    break;
                case AttackState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_KICK);
                    break;
                case AttackState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_KICK);
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (CheckAttacking(input))
            {
                case AttackState.LightPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_PUNCH);
                    break;
                case AttackState.MediumPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_PUNCH);
                    break;
                case AttackState.HeavyPunch:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_PUNCH);
                    break;
                case AttackState.LightKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.LIGHT_KICK);
                    break;
                case AttackState.MediumKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.MEDIUM_KICK);
                    break;
                case AttackState.HeavyKick:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_KICK);
                    break;
                case AttackState.None:
                    PlayAnimationLoop(ref player, AnimationData.Movememt.WALK_FORWARD);
                    velocity = 1;
                    break;
            }
        }

        player.Velocity.Set(velocity, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        return player;
    }

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Advancing Attack: {e.Attack}");
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
    //        case InputCommandElement.Back:
    //            stateMachine.Fire(CharacterStateTrigger.TriggerRetreating);
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