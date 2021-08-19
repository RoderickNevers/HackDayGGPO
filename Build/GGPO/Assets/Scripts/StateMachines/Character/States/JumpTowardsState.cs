using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
public class JumpTowardsState : CharacterStateBlock
{
    public JumpTowardsState(CharacterStateBlockInitData stateBlockData) : base(stateBlockData)
    {
        _StateMachine.Configure(CharacterState.JumpToward)
            .Permit(CharacterStateTrigger.TriggerFalling, CharacterState.Falling)
            .Permit(CharacterStateTrigger.TriggerAttackInAir, CharacterState.InAirAttack)
            //.Permit(ChangeState.TriggerHitInAir, CharacterState.HitInAir)
            //.Permit(ChangeState.TriggerKO, CharacterState.KO)
            .OnEntry(OnEnterState)
            .OnExit(OnExitState);
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
        //Debug.Log("Enter Jump Towards");
        //characterController.MovementVector = Vector2.zero;
        //animator.Play(Animator.StringToHash(ProjectConstants.FighterAnimations.Basic.JUMP_FORWARDS));
        //prevents the jump animation from being played when you re-enter the jump state from an air attack state
        //if (_BaseCharacterController.IsGrounded)
        //    _Animator.SetTrigger(AnimatorConstants.PARAMENTER_JUMP);
    }

    protected override void OnExitState()
    {
        base.OnExitState();
        //Debug.Log("Exit Jump Towards");
    }

    //protected override void OnUpdate()
    //{
    //    base.OnUpdate();

    //    characterController.Jump(1);

    //    if (characterController.Controller.velocity.y < 0)
    //    {
    //        stateMachine.Fire(CharacterStateTrigger.TriggerFalling);
    //    }
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

    public Player UpdatePlayer(Player player, long input)
    {
        if (player.IsJumping)
        {
            if (player.IsHit)
            {
                Debug.Log("IM HIT CAPTAIN!!!!!!!!!!!!!!!");
            }
            //Returning attack
            else if (player.IsAttacking)
            {
                switch (player.Attack)
                {
                    case AttackState.LightPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.LIGHT_PUNCH);
                        break;
                    case AttackState.MediumPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.MEDIUM_PUNCH);
                        break;
                    case AttackState.HeavyPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.HEAVY_PUNCH);
                        break;
                    case AttackState.LightKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.LIGHT_KICK);
                        break;
                    case AttackState.MediumKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.MEDIUM_KICK);
                        break;
                    case AttackState.HeavyKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.HEAVY_KICK);
                        break;
                }
            }
            //New attack or nothing
            else
            {
                switch (CheckAttacking(input))
                {
                    case AttackState.LightPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.LIGHT_PUNCH);
                        break;
                    case AttackState.MediumPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.MEDIUM_PUNCH);
                        break;
                    case AttackState.HeavyPunch:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.HEAVY_PUNCH);
                        break;
                    case AttackState.LightKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.LIGHT_KICK);
                        break;
                    case AttackState.MediumKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.MEDIUM_KICK);
                        break;
                    case AttackState.HeavyKick:
                        PlayAttackAnimation(ref player, AnimationData.JumpForwardAttacks.HEAVY_KICK);
                        break;
                    case AttackState.None:
                        PlayAnimationOneShot(ref player, AnimationData.Movememt.JUMP_FORWARD);
                        break;
                }
            }

            player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;
            player.Velocity.y += Mathf.Sqrt(PlayerConstants.JUMP_FORCE_VERT * Time.fixedDeltaTime);
            player.Velocity.x += PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
        }
 
        return player;
    }

    //private void HandleInputCommand(object sender, InputCommandArgs e)
    //{
    //    //Debug.Log($"Standing Attack: {e.Attack}");
    //    string name = e.Move != null ? e.Move.AttackData.Name : e.AttackName;
    //    stateMachine.FireAttack(CharacterStateTrigger.TriggerAttackInAir, $"{ProjectConstants.MovelistStateKeys.JUMP_DIRECTION}-{name}", e.Move);
    //}
}