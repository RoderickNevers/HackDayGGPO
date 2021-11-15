using System;
using System.Collections.Generic;

public class AnimationData
{
    public static float FRAME_COUNTER = 0.2f;
    public readonly static Dictionary<Guid, FrameData> AttackLookup = new Dictionary<Guid, FrameData>()
    {
        {StandingAttacks.LIGHT_PUNCH.ID, StandingAttacks.LIGHT_PUNCH },
        {StandingAttacks.MEDIUM_PUNCH.ID, StandingAttacks.MEDIUM_PUNCH },
        {StandingAttacks.HEAVY_PUNCH.ID, StandingAttacks.HEAVY_PUNCH },
        {StandingAttacks.LIGHT_KICK.ID, StandingAttacks.LIGHT_KICK },
        {StandingAttacks.MEDIUM_KICK.ID, StandingAttacks.MEDIUM_KICK },
        {StandingAttacks.HEAVY_KICK.ID, StandingAttacks.HEAVY_KICK },

        {CrouchingAttacks.LIGHT_PUNCH.ID, CrouchingAttacks.LIGHT_PUNCH },
        {CrouchingAttacks.MEDIUM_PUNCH.ID, CrouchingAttacks.MEDIUM_PUNCH },
        {CrouchingAttacks.HEAVY_PUNCH.ID, CrouchingAttacks.HEAVY_PUNCH },
        {CrouchingAttacks.LIGHT_KICK.ID, CrouchingAttacks.LIGHT_KICK },
        {CrouchingAttacks.MEDIUM_KICK.ID, CrouchingAttacks.MEDIUM_KICK },
        {CrouchingAttacks.HEAVY_KICK.ID, CrouchingAttacks.HEAVY_KICK },

        {JumpUpAttacks.LIGHT_PUNCH.ID, JumpUpAttacks.LIGHT_PUNCH },
        {JumpUpAttacks.MEDIUM_PUNCH.ID, JumpUpAttacks.MEDIUM_PUNCH },
        {JumpUpAttacks.HEAVY_PUNCH.ID, JumpUpAttacks.HEAVY_PUNCH },
        {JumpUpAttacks.LIGHT_KICK.ID, JumpUpAttacks.LIGHT_KICK },
        {JumpUpAttacks.MEDIUM_KICK.ID, JumpUpAttacks.MEDIUM_KICK },
        {JumpUpAttacks.HEAVY_KICK.ID, JumpUpAttacks.HEAVY_KICK },

        {JumpTowardAttacks.LIGHT_PUNCH.ID, JumpTowardAttacks.LIGHT_PUNCH },
        {JumpTowardAttacks.MEDIUM_PUNCH.ID, JumpTowardAttacks.MEDIUM_PUNCH },
        {JumpTowardAttacks.HEAVY_PUNCH.ID, JumpTowardAttacks.HEAVY_PUNCH },
        {JumpTowardAttacks.LIGHT_KICK.ID, JumpTowardAttacks.LIGHT_KICK },
        {JumpTowardAttacks.MEDIUM_KICK.ID, JumpTowardAttacks.MEDIUM_KICK },
        {JumpTowardAttacks.HEAVY_KICK.ID, JumpTowardAttacks.HEAVY_KICK },

        {JumpBackAttacks.LIGHT_PUNCH.ID, JumpBackAttacks.LIGHT_PUNCH },
        {JumpBackAttacks.MEDIUM_PUNCH.ID, JumpBackAttacks.MEDIUM_PUNCH },
        {JumpBackAttacks.HEAVY_PUNCH.ID, JumpBackAttacks.HEAVY_PUNCH },
        {JumpBackAttacks.LIGHT_KICK.ID, JumpBackAttacks.LIGHT_KICK },
        {JumpBackAttacks.MEDIUM_KICK.ID, JumpBackAttacks.MEDIUM_KICK },
        {JumpBackAttacks.HEAVY_KICK.ID, JumpBackAttacks.HEAVY_KICK },
    };

    public class Movememt
    {
        public static FrameData IDLE = new FrameData()
        {
            AnimationKey = "Idle",
            TotalFrames = 9f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData WALK_FORWARD = new FrameData()
        {
            AnimationKey = "WalkForward",
            TotalFrames = 11f,
            FrameRate = 12f,
            State = PlayerState.Forward,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData WALK_BACKWARD = new FrameData()
        {
            AnimationKey = "WalkBackward",
            TotalFrames = 11f,
            FrameRate = 12f,
            State = PlayerState.Back,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData CROUCH = new FrameData()
        {
            AnimationKey = "Crouch",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData JUMP_UP = new FrameData()
        {
            AnimationKey = "JumpUp",
            TotalFrames = 11f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData JUMP_FORWARD = new FrameData()
        {
            AnimationKey = "JumpForward",
            TotalFrames = 13f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData JUMP_BACKWARD = new FrameData()
        {
            AnimationKey = "JumpBackward",
            TotalFrames = 13f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DESCENDING_CONTROLLED_UP = new FrameData()
        {
            AnimationKey = "Falling",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DESCENDING_CONTROLLED_FORWARD = new FrameData()
        {
            AnimationKey = "Falling",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DESCENDING_CONTROLLED_BACKWARD = new FrameData()
        {
            AnimationKey = "Falling",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData FALLING_CRASH_UP = new FrameData()
        {

        };

        public static FrameData FALLING_CRASH_FORWARD = new FrameData()
        {

        };

        public static FrameData FALLING_CRASH_BACKWARD = new FrameData()
        {

        };
    }

    public class StandingAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            AnimationKey = "StandingLightPunch",
            Name = "Standing Light Punch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.LightPunch,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 05,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnHit = 5,
            MeterGainOnWiff = 0,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            AnimationKey = "StandingMediumPunch",
            Name = "Standing Medium Punch",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.MediumPunch,
            Damage = 60,
            HitStun = 120,
            DizzyStun = 100,
            HitPushBack = 30,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            AnimationKey = "StandingHeavyPunch",
            TotalFrames = 10f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.HeavyPunch,
            Damage = 90,
            HitStun = 200,
            DizzyStun = 150,
            HitPushBack = 30,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            AnimationKey = "StandingLightKick",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.LightKick,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.Low,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            AnimationKey = "StandingMediumKick",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.MediumKick,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            AnimationKey = "StandingHeavyKick",
            TotalFrames = 13f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.HeavyKick,
            Damage = 90,
            HitStun = 200,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };
    }

    public class CrouchingAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            AnimationKey = "CrouchingLightPunch",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.LightPunch,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            AnimationKey = "CrouchingMediumPunch",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.MediumPunch,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            AnimationKey = "CrouchingHeavyPunch",
            TotalFrames = 10f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.HeavyPunch,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Launcher,
            Category = AttackCategory.Ground
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            AnimationKey = "CrouchingLightKick",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.LightKick,
            Damage = 20,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            AnimationKey = "CrouchingMediumKick",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.MediumKick,
            Damage = 50,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.Low,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Ground
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            AnimationKey = "CrouchingHeavyKick",
            TotalFrames = 10f,
            FrameRate = 12f,
            State = PlayerState.Crouching,
            Attack = AttackButtonState.HeavyKick,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.Low,
            Property = AttackProperty.Sweep,
            Category = AttackCategory.Ground
        };
    }

    public class JumpUpAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingUpLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.LightPunch,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingUpMediumPunch",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.MediumPunch,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingUpHeavyPunch",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.HeavyPunch,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            AnimationKey = "JumpingUpLightKick",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.LightKick,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            AnimationKey = "JumpingUpMediumKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.MediumKick,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            AnimationKey = "JumpingUpHeavyKick",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.HeavyKick,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };
    }

    public class JumpTowardAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingDirectionLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.LightPunch,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingDirectionMediumPunch",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.MediumPunch,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingDirectionHeavyPunch",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.HeavyPunch,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            AnimationKey = "JumpingDirectionLightKick",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.LightKick,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            AnimationKey = "JumpingDirectionMediumKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.MediumKick,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            AnimationKey = "JumpingDirectionHeavyKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpToward,
            Attack = AttackButtonState.HeavyKick,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };
    }

    public class JumpBackAttacks
    {
        public static FrameData LIGHT_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingDirectionLightPunch",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.LightPunch,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData MEDIUM_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingDirectionMediumPunch",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.MediumPunch,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData HEAVY_PUNCH = new FrameData()
        {
            AnimationKey = "JumpingDirectionHeavyPunch",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.HeavyPunch,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData LIGHT_KICK = new FrameData()
        {
            AnimationKey = "JumpingDirectionLightKick",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.LightKick,
            Damage = 30,
            HitStun = 100,
            DizzyStun = 70,
            HitPushBack = 25,
            BlockPushBack = 0,
            MeterGainOnWiff = 0,
            MeterGainOnHit = 5,
            Type = AttackType.Weak,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData MEDIUM_KICK = new FrameData()
        {
            AnimationKey = "JumpingDirectionMediumKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.MediumKick,
            Damage = 60,
            HitStun = 100,
            DizzyStun = 100,
            HitPushBack = 35,
            BlockPushBack = 0,
            MeterGainOnWiff = 3,
            MeterGainOnHit = 10,
            Type = AttackType.Medium,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };

        public static FrameData HEAVY_KICK = new FrameData()
        {
            AnimationKey = "JumpingDirectionHeavyKick",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.JumpBack,
            Attack = AttackButtonState.HeavyKick,
            Damage = 90,
            HitStun = 100,
            DizzyStun = 150,
            HitPushBack = 40,
            BlockPushBack = 0,
            MeterGainOnWiff = 5,
            MeterGainOnHit = 15,
            Type = AttackType.Heavy,
            Level = AttackLevel.High,
            Property = AttackProperty.Standard,
            Category = AttackCategory.Air
        };
    }

    public class Hit
    {
        public static FrameData HIGH_LIGHT = new FrameData()
        {
            AnimationKey = "Hit_HighAndWeak",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData UPPER = new FrameData()
        {
            AnimationKey = "Hit_Upper",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData HIGH_MEDIUM = new FrameData()
        {
            AnimationKey = "Hit_Medium",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData HIGH_HEAVY = new FrameData()
        {
            AnimationKey = "Hit_Heavy",
            TotalFrames = 9f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData GUT_LIGHT = new FrameData()
        {
            AnimationKey = "Hit_Gut",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData GUT_HEAVY = new FrameData()
        {
            AnimationKey = "Hit_GutHeavy",
            TotalFrames = 2f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData GUT_RECOVERY = new FrameData()
        {
            AnimationKey = "Hit_GutRecovery",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData CROUCHING_HEAVY = new FrameData()
        {
            AnimationKey = "Hit_CrouchingHeavy",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.CrouchingHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData CROUCHING_MEDIUM = new FrameData()
        {
            AnimationKey = "Hit_CrouchingMedium",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.CrouchingHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData CROUCHING_LIGHT = new FrameData()
        {
            AnimationKey = "Hit_CrouchingLight",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.CrouchingHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData InAir = new FrameData()
        {
            AnimationKey = "Hit_InAir",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.JumpUp,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };


        public static FrameData TURN = new FrameData()
        {
            AnimationKey = "Hit_Turn",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData SWEEP = new FrameData()
        {
            AnimationKey = "Hit_Sweep",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.Sweep,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData FALL_OVER = new FrameData()
        {
            AnimationKey = "Hit_FallOver",
            TotalFrames = 22f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData BLOW_BACK = new FrameData()
        {
            AnimationKey = "Hit_BlowBack",
            TotalFrames = 5f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };
    }
}