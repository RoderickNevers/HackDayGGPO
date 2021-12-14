using UnityEngine;

public class StandingState : CharacterStateBlock
{
    public StandingState()
    {
    }

    public override Player UpdatePlayer(Player player, long input)
    {
        if (player.IsAttacking)
        {
            switch (player.CurrentButtonPressed)
            {
                case AttackButtonState.Slash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.SLASH);
                    break;
                case AttackButtonState.HeavySlash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_SLASH);
                    break;
                case AttackButtonState.GuardBreak:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.GUARD_BREAK);
                    break;
            }
        }
        //New attack or nothing
        else
        {
            switch (CheckAttacking(input))
            {
                case AttackButtonState.Slash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.SLASH);
                    break;
                case AttackButtonState.HeavySlash:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.HEAVY_SLASH);
                    break;
                case AttackButtonState.GuardBreak:
                    PlayAttackAnimation(ref player, AnimationData.StandingAttacks.GUARD_BREAK);
                    break;
                case AttackButtonState.None:
                    PlayAnimationLoop(ref player, AnimationData.Movememt.IDLE);

                    if (player.IsCloseToOpponent && player.IsBeingPushed)
                    {
                        // push the opponent backwards
                        var velocity = player.LookDirection == LookDirection.Right ? -1 : 1;
                        player.Velocity.Set(velocity, 0, 0);
                        player.Velocity = PlayerConstants.PUSH_SPEED * Time.fixedDeltaTime * player.Velocity;
                        return player;
                    }

                    player.Velocity.Set(0, 0, 0);

                    break;
            }
        }

        return player;
    }
}