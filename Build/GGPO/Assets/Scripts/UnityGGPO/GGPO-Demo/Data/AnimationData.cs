public class AnimationData
{
    public static float FRAME_COUNTER = 0.2f;

    public class Movememt
    {
        public static FrameData IDLE = new FrameData()
        {
            ID = 0,
            AnimationKey = "Idle",
            TotalFrames = 9f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.None
        };

        public static FrameData WALK_FORWARD = new FrameData()
        {
            ID = 0,
            AnimationKey = "WalkForward",
            TotalFrames = 11f,
            FrameRate = 12f,
            State = PlayerState.Forward,
            Attack = AttackState.None
        };

        public static FrameData WALK_BACKWARD = new FrameData()
        {
            ID = 0,
            AnimationKey = "WalkBackward",
            TotalFrames = 11f,
            FrameRate = 12f,
            State = PlayerState.Back,
            Attack = AttackState.None
        };

        public static FrameData CROUCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "Crouch",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.None
        };

        public static FrameData JUMP_UP = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpUp",
            TotalFrames = 11f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.None
        };

        public static FrameData JUMP_FORWARD = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpForward",
            TotalFrames = 13f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.None
        };

        public static FrameData JUMP_BACKWARD = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpBackward",
            TotalFrames = 13f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.None
        };
    }

    public class StandingAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "StandingLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.LightPunch
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "StandingMediumPunch",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.MediumPunch
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "StandingHeavyPunch",
            TotalFrames = 10f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.HeavyPunch
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "StandingLightKick",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.LightKick
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "StandingMediumKick",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.MediumKick
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "StandingHeavyKick",
            TotalFrames = 13f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackState.HeavyKick
        };
    }

    public class CrouchingAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "CrouchingLightPunch",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.LightPunch
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "CrouchingMediumPunch",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.MediumPunch
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "CrouchingHeavyPunch",
            TotalFrames = 10f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.HeavyPunch
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "CrouchingLightKick",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.LightKick
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "CrouchingMediumKick",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.MediumKick
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "CrouchingHeavyKick",
            TotalFrames = 10f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackState.HeavyKick
        };
    }

    public class JumpUpAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingUpLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.LightPunch
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingUpMediumPunch",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.MediumPunch
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingUpHeavyPunch",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.HeavyPunch
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingUpLightKick",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.LightKick
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingUpMediumKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.MediumKick
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingUpHeavyKick",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackState.HeavyKick
        };
    }

    public class JumpForwardAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.LightPunch
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionMediumPunch",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.MediumPunch
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionHeavyPunch",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.HeavyPunch
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionLightKick",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.LightKick
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionMediumKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.MediumKick
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionHeavyKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpForward,
            Attack = AttackState.HeavyKick
        };
    }

    public class JumpBackAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.LightPunch
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionMediumPunch",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.MediumPunch
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionHeavyPunch",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.HeavyPunch
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionLightKick",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.LightKick
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionMediumKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.MediumKick
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            ID = 0,
            AnimationKey = "JumpingDirectionHeavyKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackState.HeavyKick
        };
    }
}