using SharedGame;
using UnityEngine;

public static class Simulation
{
    private enum MoveDirection
    {
        Standing,
        Towards,
        Back,
        Crouching,
        JumpUp,
        JumpTowards,
        JumpBack
    }

    public const int INPUT_UP = 1 << 0;
    public const int INPUT_DOWN = 1 << 1;
    public const int INPUT_LEFT = 1 << 2;
    public const int INPUT_RIGHT = 1 << 3;

    private const int MOVE_SPEED = 2;
    private const int JUMP_FORCE_VERT = 8;
    private const int JUMP_FORCE_HORIZ = 1;
    private const int JUMP_HEIGHT = 10;
    private const float RAISING_GRAVITY = -2f;
    private const float FALLING_GRAVITY = -3f;

    //private const float time = 0.02f;

    private static float x = 0;
    private static MoveDirection movedirection;

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

        if (player.IsGrounded && !player.IsJumping)
        {
            if ((input & INPUT_UP) != 0 && (input & INPUT_LEFT) != 0)
            {
                player.IsJumping = true;
                movedirection = MoveDirection.JumpBack;
            }
            else if ((input & INPUT_UP) != 0 && (input & INPUT_RIGHT) != 0)
            {
                player.IsJumping = true;
                movedirection = MoveDirection.JumpTowards;
            }
            else if ((input & INPUT_UP) != 0)
            {
                player.IsJumping = true;
                movedirection = MoveDirection.JumpUp;
            }
            else if ((input & INPUT_LEFT) != 0)
            {
                x = -1;
                movedirection = MoveDirection.Back;
            }
            else if ((input & INPUT_RIGHT) != 0)
            {
                x = 1;
                movedirection = MoveDirection.Towards;
            }
            else if((input & INPUT_DOWN) != 0)
            {
                movedirection = MoveDirection.Crouching;
            }
            else if ( (input & INPUT_LEFT) == 0 && (input & INPUT_RIGHT) == 0)
            {
                x = 0;
                movedirection = MoveDirection.Standing;
            }
        }

        player.Velocity.Set(x, 0, 0);
        player.Velocity = MOVE_SPEED * /*time **/ player.Velocity;

        //jump stuff
        if (player.IsJumping)
        {
            player.Velocity.y += Mathf.Sqrt(JUMP_FORCE_VERT);// * time);;

            switch(movedirection)
            {
                case MoveDirection.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case MoveDirection.JumpTowards:
                    player.Velocity.x = 0;
                    player.Velocity.x += JUMP_FORCE_HORIZ;// * time;
                    break;
                case MoveDirection.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -JUMP_FORCE_HORIZ;// * time;
                    break;
            }
        }

        Debug.Log($"{player.Velocity}");

        // Trigger falling
        if (player.Position.y >= JUMP_HEIGHT)
        {
            player.IsJumping = false;
        }

        // Apply gravity
        if (!player.IsGrounded && player.Position.y >= 0)
        {
            //Debug.Log(Time.fixedDeltaTime);
            float gravityModifier = player.Velocity.y == 0 ? FALLING_GRAVITY : RAISING_GRAVITY;
            player.Velocity.y += gravityModifier;// * time;

            switch (movedirection)
            {
                case MoveDirection.JumpUp:
                    player.Velocity.x = 0;
                    break;
                case MoveDirection.JumpTowards:
                    player.Velocity.x = 0;
                    player.Velocity.x += JUMP_FORCE_HORIZ;// * time;
                    break;
                case MoveDirection.JumpBack:
                    player.Velocity.x = 0;
                    player.Velocity.x += -JUMP_FORCE_HORIZ;// * time;
                    break;
            }
        }

        // Move Player
        player.Position += player.Velocity;

        return player;
    }
}