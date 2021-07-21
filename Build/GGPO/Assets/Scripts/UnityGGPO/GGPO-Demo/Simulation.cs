using SharedGame;
using UnityEngine;

public static class Simulation
{
    public const int INPUT_UP = 1 << 0;
    public const int INPUT_DOWN = 1 << 1;
    public const int INPUT_LEFT = 1 << 2;
    public const int INPUT_RIGHT = 1 << 3;

    private const int MOVE_SPEED = 10;
    private const int JUMP_SPEED = 3;
    private const int JUMP_HEIGHT = 5;
    private const float GRAVITY = -9.8f;

    private static float velocityY;

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

        if (player.IsGrounded && player.Velocity.y < 0)
        {
            player.Velocity.y = 0f;
        }

        //player.Velocity.Set(0, 0, 0);
        float x = 0;

        if ((input & INPUT_LEFT) != 0)
        {
            x = -1; 
        }

        if ((input & INPUT_RIGHT) != 0)
        {
            x = 1;
        }

        player.Velocity.Set(x, 0, 0);
        player.Velocity = MOVE_SPEED * Time.fixedDeltaTime * player.Velocity;

        //jump stuff
        Debug.Log($"A: {input} B: {INPUT_UP}");

        if ((input & INPUT_UP) != 0 && player.IsGrounded)
        {
            var jumpVal = Mathf.Sqrt(JUMP_HEIGHT * -3.0f * GRAVITY);
            Debug.Log($"Jump value: {jumpVal}");
            player.Velocity.y += jumpVal;
        }

        if (!player.IsGrounded && player.Position.y >= 0)
        {
            player.Velocity.y += GRAVITY * Time.fixedDeltaTime;
        }

        //end jump stuff

        //if ((input & INPUT_DOWN) != 0)
        //{
        //    player.Velocity.Set(0, 0, 0);
        //}

        // Move Player
        player.Position += player.Velocity;

        return player;
    }

    //--------------------

    //private static Vector3 UpdatePlayerA(Player player, long input)
    //{
    //    groundedPlayer = controller.isGrounded;
    //    if (groundedPlayer && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }

    //    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    controller.Move(move * Time.deltaTime * playerSpeed);

    //    if (move != Vector3.zero)
    //    {
    //        gameObject.transform.forward = move;
    //    }
    //}

    //private static Vector3 UpdatePlayerB(Player player, long input)
    //{
    //    // Changes the height position of the player..
    //    if (Input.GetButtonDown("Jump") && groundedPlayer)
    //    {
    //        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //    }

    //    playerVelocity.y += gravityValue * Time.deltaTime;
    //    controller.Move(playerVelocity * Time.deltaTime);
    //}
}