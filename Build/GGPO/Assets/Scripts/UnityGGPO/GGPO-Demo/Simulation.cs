using SharedGame;
using UnityEngine;

public static class Simulation
{
    public const int INPUT_UP = 1 << 0;
    public const int INPUT_DOWN = 1 << 1;
    public const int INPUT_LEFT = 1 << 2;
    public const int INPUT_RIGHT = 1 << 3;

    private const int MOVE_SPEED = 10;
    private const int JUMP_FORCE_VERT = 15;
    private const int JUMP_FORCE_HORIZ = 15;
    private const int JUMP_HEIGHT = 10;
    private const float RAISING_GRAVITY = -9.8f;
    private const float FALLING_GRAVITY = -20.0f;

    private static float x = 0;

    /// <summary>
    /// The update loop that runs the simulation on the players
    /// </summary>
    /// <param name="player">Player to be simulated</param>
    /// <param name="input">Inputs for that player</param>
    /// <returns>The player after the simulation has been run</returns>
    public static Player Run(Player player, long input)
    {
        player.IsGrounded = GroundCheck(player);
        return UpdatePlayer(player, input);
    }

    private static bool GroundCheck(Player player)
    {
        return player.Position.y <= 0;
    }

    private static Player UpdatePlayer(Player player, long input)
    {
        GGPORunner.LogGame($"parsing player {player} inputs: {input}.");
        Debug.Log($" move direction {player.MoveDirection}");

        if (player.IsGrounded && !player.IsJumping)
        {
            if ((input & INPUT_UP) != 0 && (input & INPUT_LEFT) != 0)
            {
                player.IsJumping = true;
                player.MoveDirection = MoveDirection.JumpBack;
            }
            else if ((input & INPUT_UP) != 0 && (input & INPUT_RIGHT) != 0)
            {
                player.IsJumping = true;
                player.MoveDirection = MoveDirection.JumpForward;
            }
            else if ((input & INPUT_UP) != 0)
            {
                player.IsJumping = true;
                player.MoveDirection = MoveDirection.JumpUp;
            }
            else if ((input & INPUT_DOWN) != 0 && (input & INPUT_LEFT) != 0)
            {
                player.MoveDirection = MoveDirection.DownBack;
            }
            else if ((input & INPUT_DOWN) != 0 && (input & INPUT_RIGHT) != 0)
            {
                player.MoveDirection = MoveDirection.DownForward;
            }
            else if ((input & INPUT_DOWN) != 0)
            {
                player.MoveDirection = MoveDirection.Crouching;
            }
            else if ((input & INPUT_LEFT) != 0)
            {
                x = -1;
                player.MoveDirection = MoveDirection.Back;
            }
            else if ((input & INPUT_RIGHT) != 0)
            {
                x = 1;
                player.MoveDirection = MoveDirection.Forward;
            }
            else if ( (input & INPUT_LEFT) == 0 && (input & INPUT_RIGHT) == 0)
            {
                x = 0;
                player.MoveDirection = MoveDirection.Standing;
            }
        }

        player.Velocity.Set(x, 0, 0);
        player.Velocity = MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        //jump stuff
        if (player.IsJumping)
        {
            player.Velocity.y += Mathf.Sqrt(JUMP_FORCE_VERT * Time.fixedDeltaTime);

            switch(player.MoveDirection)
            {
                case MoveDirection.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case MoveDirection.JumpForward:
                    player.Velocity.x = 0;
                    player.Velocity.x += JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
                case MoveDirection.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
            }
        }

        //Debug.Log($"{player.Velocity}");

        // Trigger falling
        if (player.Position.y >= JUMP_HEIGHT)
        {
            player.IsJumping = false;
        }

        // Apply gravity
        if (!player.IsGrounded && player.Position.y >= 0)
        {
            float gravityModifier = player.Velocity.y == 0 ? FALLING_GRAVITY : RAISING_GRAVITY;
            player.Velocity.y += gravityModifier * Time.fixedDeltaTime;

            switch (player.MoveDirection)
            {
                case MoveDirection.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case MoveDirection.JumpForward:
                    player.Velocity.x = 0;
                    player.Velocity.x += JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
                case MoveDirection.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -JUMP_FORCE_HORIZ * Time.fixedDeltaTime;
                    break;
            }
        }

        // Move Player
        player.Position += player.Velocity;

        return player;
    }
}