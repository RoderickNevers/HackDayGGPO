using System;
using UnityEngine;
using Stateless;
using SharedGame;

public class CharacterControllerStateMachine: IDisposable
{
    private readonly StateMachine<CharacterState, CharacterStateTrigger> _Machine;

    public readonly IntroState _IntroState;
    public readonly StandingState _StandingState;
    public readonly AdvancingState _AdvancingState;
    public readonly RetreatingState _RetreatingState;
    public readonly CrouchingState _CrouchingState;
    public readonly GroundedAttackState _AttackGroundState;
    public readonly InAirAttackState _AttackInAirState;
    public readonly JumpUpState _JumpUpState;
    public readonly JumpTowardsState _JumpTowardsState;
    public readonly JumpBackState _JumpAwayState;
    public readonly FallingState _FallingState;
    public readonly LandingState _LandingState;
    public readonly HitStandingState _HitStandingState;
    public readonly SweepState _SweepState;
    public readonly OnTheGroundState _OnTheGroundState;
    public readonly GettingUpState _GettingUpState;
    public readonly DizzyState _DizzyState;
    public readonly KOState _KOState;

    public CharacterState CurrentTrigger => _Machine.State;

    //-----------

    public CharacterControllerStateMachine()
    {
        _Machine = new StateMachine<CharacterState, CharacterStateTrigger>(CharacterState.Intro);

        CharacterStateBlockInitData stateBlockData = new CharacterStateBlockInitData(_Machine);

        _IntroState = new IntroState(stateBlockData);
        _StandingState = new StandingState(stateBlockData);
        _AdvancingState = new AdvancingState(stateBlockData);
        _RetreatingState = new RetreatingState(stateBlockData);
        _CrouchingState = new CrouchingState(stateBlockData);
        _AttackGroundState = new GroundedAttackState(stateBlockData);
        _AttackInAirState = new InAirAttackState(stateBlockData);
        _JumpUpState = new JumpUpState(stateBlockData);
        _JumpTowardsState = new JumpTowardsState(stateBlockData);
        _JumpAwayState = new JumpBackState(stateBlockData);
        _FallingState = new FallingState(stateBlockData);
        _LandingState = new LandingState(stateBlockData);
        _HitStandingState = new HitStandingState(stateBlockData);
        _SweepState = new SweepState(stateBlockData);
        _OnTheGroundState = new OnTheGroundState(stateBlockData);
        _GettingUpState = new GettingUpState(stateBlockData);
        _DizzyState = new DizzyState(stateBlockData);
        _KOState = new KOState(stateBlockData);
    }

    public void Dispose()
    {
        _IntroState?.Dispose();
        _StandingState?.Dispose();
        _AdvancingState?.Dispose();
        _RetreatingState?.Dispose();
        _CrouchingState?.Dispose();
        _AttackGroundState?.Dispose();
        _AttackInAirState?.Dispose();
        _JumpUpState?.Dispose();
        _JumpTowardsState?.Dispose();
        _JumpAwayState?.Dispose();
        _FallingState?.Dispose();
        _LandingState?.Dispose();
        _HitStandingState?.Dispose();
        _SweepState?.Dispose();
        _OnTheGroundState?.Dispose();
        _GettingUpState?.Dispose();
        _DizzyState?.Dispose();
        _KOState?.Dispose();
    }

    public void Reset()
    {
        _Machine.Fire(CharacterStateTrigger.TriggerStanding);
    }

    public Player Run(Player player, long input)
    {
        Debug.Log("B");

        player.LookDirection = MatchComponent.Instance.CheckLookDirection(player);
        // this is kinda of iffy but it currently works
        // Hacky landing state
        if (!player.IsGrounded && Mathf.Approximately(player.Position.y, 0.5f) || player.Position.y < 0)
        {
            player.IsAttacking = false;
            //player.State = PlayerState.Landing;
            //player = _LandingState.UpdatePlayer(player, input);
        }

        player.IsGrounded = GroundCheck(player);

        // Ensure the players feet are always on the ground
        if (player.IsGrounded)
        {
            player.Position.y = 0;
        }

        CheckDirectionInput(ref player, input);

        // Grounded states
        if (player.IsGrounded && !player.IsJumping)
        {
            switch (player.State)
            {
                case PlayerState.Standing:
                    player = _StandingState.UpdatePlayer(player, input);
                    break;
                case PlayerState.Forward:
                    player = _AdvancingState.UpdatePlayer(player, input);
                    break;
                case PlayerState.Back:
                    player = _RetreatingState.UpdatePlayer(player, input);
                    break;
                case PlayerState.Crouching:
                    player = _CrouchingState.UpdatePlayer(player, input);
                    break;
                case PlayerState.DownForward:
                    player = _CrouchingState.UpdatePlayer(player, input);
                    break;
                case PlayerState.DownBack:
                    player = _CrouchingState.UpdatePlayer(player, input);
                    break;
                case PlayerState.StandingAttack:
                    player = _AttackGroundState.UpdatePlayer(player, input);
                    break;
                case PlayerState.CrouchngAttack:
                    player = _AttackGroundState.UpdatePlayer(player, input);
                    break;
                case PlayerState.StandHit:
                    break;
                case PlayerState.CrouchingHit:
                    break;
                case PlayerState.StandBlock:
                    break;
                case PlayerState.CrouchBlock:
                    break;
                default:
                    player = _StandingState.UpdatePlayer(player, input);
                    break;
            }
        }
        // Air states
        else if (player.IsJumping)
        {
            switch(player.State)
            {
                case PlayerState.JumpUp:
                    player = _JumpUpState.UpdatePlayer(player, input);
                    break;
                case PlayerState.JumpForward:
                    player = _JumpTowardsState.UpdatePlayer(player, input);
                    break;
                case PlayerState.JumpBack:
                    player = _JumpAwayState.UpdatePlayer(player, input);
                    break;
                case PlayerState.JumpAttack:
                    break;
                case PlayerState.JumpHit:
                    break;
                //case PlayerState.Falling:
                //    player = _FallingState.UpdatePlayer(player, input);
                //    break;
                //case PlayerState.Landing:
                //    player = _LandingState.UpdatePlayer(player, input);
                //    break;
            }
        }

        // Trigger falling
        if (player.Position.y >= PlayerConstants.JUMP_HEIGHT)
        {
            player.IsJumping = false;
            //player.State = PlayerState.Falling;
            //player = _FallingState.UpdatePlayer(player, input);
        }

        // Apply gravity
        if (!player.IsGrounded && player.Position.y >= 0)
        {
            float gravityModifier = player.Velocity.y == 0 ? PlayerConstants.FALLING_GRAVITY : PlayerConstants.RAISING_GRAVITY;
            player.Velocity.y += gravityModifier * Time.fixedDeltaTime;

            switch (player.State)
            {
                case PlayerState.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case PlayerState.JumpForward:
                    player.Velocity.x = 0;
                    player.Velocity.x += PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
                case PlayerState.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
            }
        }

        // Move Player
        player.Position += player.Velocity;

        return player;
    }

    private bool GroundCheck(Player player)
    {
        return player.Position.y <= 0;
    }

    public void CheckDirectionInput(ref Player player, long input)
    {
        if (player.IsGrounded && !player.IsJumping && !player.IsAttacking)
        {
            if ((input & InputConstants.INPUT_UP) != 0 && (input & InputConstants.INPUT_LEFT) != 0)
            {
                player.IsJumping = true;
                player.State = PlayerState.JumpBack;
            }
            else if ((input & InputConstants.INPUT_UP) != 0 && (input & InputConstants.INPUT_RIGHT) != 0)
            {
                player.IsJumping = true;
                player.State = PlayerState.JumpForward;
            }
            else if ((input & InputConstants.INPUT_UP) != 0)
            {
                player.IsJumping = true;
                player.State = PlayerState.JumpUp;
            }
            else if ((input & InputConstants.INPUT_DOWN) != 0 && (input & InputConstants.INPUT_LEFT) != 0)
            {
                player.State = PlayerState.DownBack;
            }
            else if ((input & InputConstants.INPUT_DOWN) != 0 && (input & InputConstants.INPUT_RIGHT) != 0)
            {
                player.State = PlayerState.DownForward;
            }
            else if ((input & InputConstants.INPUT_DOWN) != 0)
            {
                player.State = PlayerState.Crouching;
            }
            else if ((input & InputConstants.INPUT_LEFT) != 0)
            {
                player.State = PlayerState.Back;
            }
            else if ((input & InputConstants.INPUT_RIGHT) != 0)
            {
                player.State = PlayerState.Forward;
            }
            else if ((input & InputConstants.INPUT_LEFT) == 0 && (input & InputConstants.INPUT_RIGHT) == 0)
            {
                player.State = PlayerState.Standing;
            }
        }
    }
}