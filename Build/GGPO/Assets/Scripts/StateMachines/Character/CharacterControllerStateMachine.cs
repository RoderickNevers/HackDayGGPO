using System;
using UnityEngine;
using Stateless;
using SharedGame;

public class CharacterControllerStateMachine: IDisposable
{
    private readonly StateMachine<CharacterState, CharacterStateTrigger> _Machine;

    private readonly IntroState _IntroState;
    private readonly StandingState _StandingState;
    private readonly AdvancingState _AdvancingState;
    private readonly RetreatingState _RetreatingState;
    private readonly CrouchingState _CrouchingState;
    private readonly GroundedAttackState _AttackGroundState;
    private readonly InAirAttackState _AttackInAirState;
    private readonly JumpUpState _JumpUpState;
    private readonly JumpTowardsState _JumpTowardsState;
    private readonly JumpBackState _JumpAwayState;
    private readonly FallingState _FallingState;
    private readonly LandingState _LandingState;
    private readonly HitStandingState _HitStandingState;
    private readonly SweepState _SweepState;
    private readonly OnTheGroundState _OnTheGroundState;
    private readonly GettingUpState _GettingUpState;
    private readonly DizzyState _DizzyState;
    private readonly KOState _KOState;

    public CharacterState CurrentTrigger => _Machine.State;

    //-----------

    private float x = 0;

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

        //_Machine.Fire(CharacterStateTrigger.TriggerStanding);
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
        player.IsGrounded = GroundCheck(player);
        return UpdatePlayer(player, input);
    }

    private bool GroundCheck(Player player)
    {
        return player.Position.y <= 0;
    }

    private Player UpdatePlayer(Player player, long input)
    {
        GGPORunner.LogGame($"parsing player {player} inputs: {input}.");
        Debug.Log($" move direction {player.MoveDirection}");

        if (player.IsGrounded && !player.IsJumping)
        {
            if ((input & PlayerConstants.INPUT_UP) != 0 && (input & PlayerConstants.INPUT_LEFT) != 0)
            {
                player.IsJumping = true;
                player.MoveDirection = MoveDirection.JumpBack;
            }
            else if ((input & PlayerConstants.INPUT_UP) != 0 && (input & PlayerConstants.INPUT_RIGHT) != 0)
            {
                player.IsJumping = true;
                player.MoveDirection = MoveDirection.JumpForward;
            }
            else if ((input & PlayerConstants.INPUT_UP) != 0)
            {
                player.IsJumping = true;
                player.MoveDirection = MoveDirection.JumpUp;
            }
            else if ((input & PlayerConstants.INPUT_DOWN) != 0 && (input & PlayerConstants.INPUT_LEFT) != 0)
            {
                player.MoveDirection = MoveDirection.DownBack;
            }
            else if ((input & PlayerConstants.INPUT_DOWN) != 0 && (input & PlayerConstants.INPUT_RIGHT) != 0)
            {
                player.MoveDirection = MoveDirection.DownForward;
            }
            else if ((input & PlayerConstants.INPUT_DOWN) != 0)
            {
                player.MoveDirection = MoveDirection.Crouching;
            }
            else if ((input & PlayerConstants.INPUT_LEFT) != 0)
            {
                x = -1;
                player.MoveDirection = MoveDirection.Back;
            }
            else if ((input & PlayerConstants.INPUT_RIGHT) != 0)
            {
                x = 1;
                player.MoveDirection = MoveDirection.Forward;
            }
            else if ((input & PlayerConstants.INPUT_LEFT) == 0 && (input & PlayerConstants.INPUT_RIGHT) == 0)
            {
                x = 0;
                player.MoveDirection = MoveDirection.Standing;
            }
        }

        player.Velocity.Set(x, 0, 0);
        player.Velocity = PlayerConstants.MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        //jump stuff
        if (player.IsJumping)
        {
            player.Velocity.y += Mathf.Sqrt(PlayerConstants.JUMP_FORCE_VERT * Time.fixedDeltaTime);

            switch (player.MoveDirection)
            {
                case MoveDirection.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case MoveDirection.JumpForward:
                    player.Velocity.x = 0;
                    player.Velocity.x += PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
                case MoveDirection.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
            }
        }

        //Debug.Log($"{player.Velocity}");

        // Trigger falling
        if (player.Position.y >= PlayerConstants.JUMP_HEIGHT)
        {
            player.IsJumping = false;
        }

        // Apply gravity
        if (!player.IsGrounded && player.Position.y >= 0)
        {
            float gravityModifier = player.Velocity.y == 0 ? PlayerConstants.FALLING_GRAVITY : PlayerConstants.RAISING_GRAVITY;
            player.Velocity.y += gravityModifier * Time.fixedDeltaTime;

            switch (player.MoveDirection)
            {
                case MoveDirection.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case MoveDirection.JumpForward:
                    player.Velocity.x = 0;
                    player.Velocity.x += PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
                case MoveDirection.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
            }
        }

        // Move Player
        player.Position += player.Velocity;

        return player;
    }
}