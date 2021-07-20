using SharedGame;
using UnityEngine;

public static class Simulation
{
    public const int INPUT_UP = 1 << 0;
    public const int INPUT_DOWN = 1 << 1;
    public const int INPUT_LEFT = 1 << 2;
    public const int INPUT_RIGHT = 1 << 3;

    private const int MOVE_SPEED = 1;
    private const int JUMP_HEIGHT = 10;
    private const float GRAVITY = -0.3f;

    public static bool GroundCheck(Player player)
    {
        return player.Position.y <= 0;
    }

    public static Vector3 ParseInputs(Player player, long input)
    {
        GGPORunner.LogGame($"parsing player {player} inputs: {input}.");

        player.Velocity.Set(0, 0, 0);

        if ((input & INPUT_LEFT) != 0)
        {
            player.Velocity.Set(-1, 0, 0);
        }

        if ((input & INPUT_RIGHT) != 0)
        {
            player.Velocity.Set(1, 0, 0);
        }

        //Jump Stuff
        if (!player.IsGrounded && player.Position.y >= 0)
        {
            player.Velocity.y += GRAVITY;
        }

        if (player.IsGrounded && player.Velocity.y < 0)
        {
            player.Velocity.y = 0f;
            player.Position.y = 0f;
        }

        if ((input & INPUT_UP) != 0 && player.IsGrounded)
        {
            player.Velocity.y += Mathf.Sqrt(JUMP_HEIGHT * -3.0f * GRAVITY);

        }
        //end jump stuff

        if ((input & INPUT_DOWN) != 0)
        {
            player.Velocity.Set(0, 0, 0);
        }

        return player.Velocity * MOVE_SPEED;
    }

    public static Vector3 MovePlayer(Player player)
    {
        return player.Position + player.Velocity;
    }
}