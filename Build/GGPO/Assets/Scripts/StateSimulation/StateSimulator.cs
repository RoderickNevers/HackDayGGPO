using UnityEngine;

public class StateSimulator
{
    private float x = 0;

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
        if (player.IsGrounded && !player.IsJumping)
        {
            if ((input & PlayerConstants.INPUT_UP) != 0 && (input & PlayerConstants.INPUT_LEFT) != 0)
            {
                //player.IsJumping = true;
                //player.MoveDirection = PlayerState.JumpBack;
            }
            else if ((input & PlayerConstants.INPUT_UP) != 0 && (input & PlayerConstants.INPUT_RIGHT) != 0)
            {
                //player.IsJumping = true;
                //player.MoveDirection = PlayerState.JumpForward;
            }
            else if ((input & PlayerConstants.INPUT_UP) != 0)
            {
                //player.IsJumping = true;
                //player.MoveDirection = PlayerState.JumpUp;
            }
            else if ((input & PlayerConstants.INPUT_DOWN) != 0 && (input & PlayerConstants.INPUT_LEFT) != 0)
            {
                //player.MoveDirection = PlayerState.DownBack;
            }
            else if ((input & PlayerConstants.INPUT_DOWN) != 0 && (input & PlayerConstants.INPUT_RIGHT) != 0)
            {
                //player.MoveDirection = PlayerState.DownForward;
            }
            else if ((input & PlayerConstants.INPUT_DOWN) != 0)
            {
                //player.MoveDirection = PlayerState.Crouching;
            }
            else if ((input & PlayerConstants.INPUT_LEFT) != 0)
            {
                //x = -1;
                //player.MoveDirection = PlayerState.Back;
            }
            else if ((input & PlayerConstants.INPUT_RIGHT) != 0)
            {
                //x = 1;
                //player.MoveDirection = PlayerState.Forward;
            }
            else if ((input & PlayerConstants.INPUT_LEFT) == 0 && (input & PlayerConstants.INPUT_RIGHT) == 0)
            {
                //x = 0;
                //player.MoveDirection = PlayerState.Standing;
            }
        }



        //jump stuff
        if (player.IsJumping)
        {
            player.Velocity.y += Mathf.Sqrt(PlayerConstants.JUMP_FORCE_VERT * Time.fixedDeltaTime);

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
}
