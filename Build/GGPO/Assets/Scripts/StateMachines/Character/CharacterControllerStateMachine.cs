using System;
using UnityEngine;

public class CharacterControllerStateMachine: IDisposable
{
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
    public readonly HitCrouchingState _HitCrouchingState;
    public readonly HitInAirState _HitInAirState;
    public readonly SweepState _SweepState;
    public readonly OnTheGroundState _OnTheGroundState;
    public readonly GettingUpState _GettingUpState;
    public readonly DizzyState _DizzyState;
    public readonly KOState _KOState;

    public CharacterControllerStateMachine()
    {
        _IntroState = new IntroState();
        _StandingState = new StandingState();
        _AdvancingState = new AdvancingState();
        _RetreatingState = new RetreatingState();
        _CrouchingState = new CrouchingState();
        _AttackGroundState = new GroundedAttackState();
        _AttackInAirState = new InAirAttackState();
        _JumpUpState = new JumpUpState();
        _JumpTowardsState = new JumpTowardsState();
        _JumpAwayState = new JumpBackState();
        _FallingState = new FallingState();
        _LandingState = new LandingState();
        _HitStandingState = new HitStandingState();
        _HitCrouchingState = new HitCrouchingState();
        _HitInAirState = new HitInAirState();
        _SweepState = new SweepState();
        _OnTheGroundState = new OnTheGroundState();
        _GettingUpState = new GettingUpState();
        _DizzyState = new DizzyState();
        _KOState = new KOState();
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
        _HitCrouchingState?.Dispose();
        _HitInAirState?.Dispose();
        _SweepState?.Dispose();
        _OnTheGroundState?.Dispose();
        _GettingUpState?.Dispose();
        _DizzyState?.Dispose();
        _KOState?.Dispose();
    }

    public Player Run(Player player, long input)
    {
        player.LookDirection = GameController.Instance.CheckLookDirection(player);
        // this is kinda of iffy but it currently works
        // Hacky landing state
        if (!player.IsGrounded && Mathf.Approximately(player.Position.y, 0.5f) || player.Position.y < 0)
        {
            player.IsAttacking = false;
            player.JumpType = PlayerState.None;
            //player.State = PlayerState.Landing;
            //player = _LandingState.UpdatePlayer(player, input);
        }

        player.IsGrounded = GroundCheck(player);

        // Ensure the players feet are always on the ground
        if (player.IsGrounded)
        {
            player.Position.y = 0;
        }

        if (player.State == PlayerState.KO)
        {
            player = _KOState.UpdatePlayer(player, input);
            // when the animation is completem, tell the game to end
            return player;
        }

        // Getting hit
        if (player.IsHit)
        {
            if (player.IsGrounded && !player.IsJumping)
            {
                switch (player.State)
                {
                    case PlayerState.Standing:
                    case PlayerState.Forward:
                    case PlayerState.Back:
                    case PlayerState.StandHit:
                        player.State = PlayerState.StandHit;
                        player = _HitStandingState.UpdatePlayer(player, input);

                        if (player.Health <= 0)
                        {
                            player.State = PlayerState.KO;
                            return player;
                        }

                        break;

                    //case PlayerState.Crouching:
                    //case PlayerState.DownForward:
                    //case PlayerState.DownBack:
                    //case PlayerState.CrouchingHit:
                    //    player.State = PlayerState.CrouchingHit;
                    //    player = _HitCrouchingState.UpdatePlayer(player, input);
                    //    break;
                }
            }
            // Air states
            //else if (player.IsJumping)
            //{
            //    switch (player.State)
            //    {
            //        case PlayerState.JumpUp:
            //        case PlayerState.JumpToward:
            //        case PlayerState.JumpBack:
            //        case PlayerState.JumpAttack:
            //            player.State = PlayerState.JumpHit;
            //            player = _HitInAirState.UpdatePlayer(player, input);
            //            break;
            //    }
            //}
        }

        CheckDirectionInput(ref player, input);

        // Grounded states
        if (player.IsGrounded && !player.IsJumping && !player.IsHit)
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
                //case PlayerState.Crouching:
                //    player = _CrouchingState.UpdatePlayer(player, input);
                //    break;
                //case PlayerState.DownForward:
                //    player = _CrouchingState.UpdatePlayer(player, input);
                //    break;
                //case PlayerState.DownBack:
                //    player = _CrouchingState.UpdatePlayer(player, input);
                //    break;
                case PlayerState.StandingAttack:
                    player = _AttackGroundState.UpdatePlayer(player, input);
                    break;
                //case PlayerState.CrouchngAttack:
                //    player = _AttackGroundState.UpdatePlayer(player, input);
                //    break;
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
        //else if (player.IsJumping)
        //{
        //    switch(player.State)
        //    {
        //        case PlayerState.JumpUp:
        //            player = _JumpUpState.UpdatePlayer(player, input);
        //            break;
        //        case PlayerState.JumpToward:
        //            player = _JumpTowardsState.UpdatePlayer(player, input);
        //            break;
        //        case PlayerState.JumpBack:
        //            player = _JumpAwayState.UpdatePlayer(player, input);
        //            break;
        //        //case PlayerState.Falling:
        //        //    player = _FallingState.UpdatePlayer(player, input);
        //        //    break;
        //        //case PlayerState.Landing:
        //        //    player = _LandingState.UpdatePlayer(player, input);
        //        //    break;
        //    }
        //}

        // Trigger falling
        //if (player.Position.y >= PlayerConstants.JUMP_HEIGHT || player.Velocity.y < 0)
        //{
        //    player.IsJumping = false;
        //    if (!player.IsHit)
        //    {
        //        player.State = PlayerState.Falling;
        //        player = _FallingState.UpdatePlayer(player, input);
        //    }
        //}

        // Apply gravity
        if (!player.IsGrounded && player.Position.y >= 0)
        {
            float gravityModifier = player.Velocity.y == 0 ? PlayerConstants.FALLING_GRAVITY : PlayerConstants.RAISING_GRAVITY;
            player.Velocity.y += gravityModifier * Time.fixedDeltaTime;

            //switch (player.JumpType)
            //{
            //    case PlayerState.JumpUp:
            //        player.Velocity.x = 0;
            //        break;
            //    case PlayerState.JumpToward:
            //        player.Velocity.x = 0;
            //        player.Velocity.x += PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
            //        break;
            //    case PlayerState.JumpBack:
            //        player.Velocity.x = 0;
            //        player.Velocity.x += -PlayerConstants.JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
            //        break;
            //}
        }

        // Move Player
        player.Position += player.Velocity;

        // pre battle pose
        //if (GameController.Instance.CurrentGameType == GameType.Versus && GameController.Instance.GameState == MatchState.PreBattle)
        //{
        //    player = _StandingState.UpdatePlayer(player, input);
        //}

        // post battle pose
        //if (GameController.Instance.GameState == MatchState.PostBattle)
        //{
        //    player = _StandingState.UpdatePlayer(player, input);
        //}

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
            //if ((input & InputConstants.INPUT_UP) != 0 && (input & InputConstants.INPUT_LEFT) != 0)
            //{
            //    player.IsJumping = true;
            //    player.State = PlayerState.JumpBack;
            //}
            //else if ((input & InputConstants.INPUT_UP) != 0 && (input & InputConstants.INPUT_RIGHT) != 0)
            //{
            //    player.IsJumping = true;
            //    player.State = PlayerState.JumpToward;
            //}
            //else if ((input & InputConstants.INPUT_UP) != 0)
            //{
            //    player.IsJumping = true;
            //    player.State = PlayerState.JumpUp;
            //}
            //else if ((input & InputConstants.INPUT_DOWN) != 0 && (input & InputConstants.INPUT_LEFT) != 0)
            //{
            //    player.State = PlayerState.DownBack;
            //}
            //else if ((input & InputConstants.INPUT_DOWN) != 0 && (input & InputConstants.INPUT_RIGHT) != 0)
            //{
            //    player.State = PlayerState.DownForward;
            //}
            //else if ((input & InputConstants.INPUT_DOWN) != 0)
            //{
            //    player.State = PlayerState.Crouching;
            //}
            //Looking left
            if ((input & InputConstants.INPUT_LEFT) != 0 && player.LookDirection == LookDirection.Left)
            {
                player.State = PlayerState.Forward;
            }
            else if ((input & InputConstants.INPUT_RIGHT) != 0 && player.LookDirection == LookDirection.Left)
            {
                player.State = PlayerState.Back;
            }
            //Looking right
            else if ((input & InputConstants.INPUT_LEFT) != 0 && player.LookDirection == LookDirection.Right)
            {
                player.State = PlayerState.Back;
            }
            else if ((input & InputConstants.INPUT_RIGHT) != 0 && player.LookDirection == LookDirection.Right)
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