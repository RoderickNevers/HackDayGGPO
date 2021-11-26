using System;
using System.Collections.Generic;

public class AnimationData
{
    public static float FRAME_COUNTER = 0.2f;
    public readonly static Dictionary<Guid, FrameData> AttackLookup = new Dictionary<Guid, FrameData>()
    {
        {StandingAttacks.SLASH.ID, StandingAttacks.SLASH },
        {StandingAttacks.HEAVY_SLASH.ID, StandingAttacks.HEAVY_SLASH },
        {StandingAttacks.GUARD_BREAK.ID, StandingAttacks.GUARD_BREAK }
    };

    public class Movememt
    {
        public static FrameData IDLE = new FrameData()
        {
            AnimationKey = "Idle",
            TotalFrames = 12f,
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
            TotalFrames = 8f,
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
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.Back,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };
    }

    public class StandingAttacks
    {
        public static FrameData SLASH = new FrameData()
        {
            AnimationKey = "Slash",
            Name = "Slash",
            TotalFrames = 14f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.Slash,
            Damage = 100,
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

        public static FrameData HEAVY_SLASH = new FrameData()
        {
            AnimationKey = "HeavySlash",
            Name = "Heavy Slash",
            TotalFrames = 9f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.HeavySlash,
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

        public static FrameData GUARD_BREAK = new FrameData()
        {
            AnimationKey = "GuardBreak",
            Name = "Guard Break",
            TotalFrames = 7f,
            FrameRate = 12f,
            State = PlayerState.Standing,
            Attack = AttackButtonState.GuardBreak,
            Damage = 0,
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
    }

    public class Hit
    {
        public static FrameData HIT_1 = new FrameData()
        {
            AnimationKey = "Hit_1",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData HIT_2 = new FrameData()
        {
            AnimationKey = "Hit_2",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData HIT_3 = new FrameData()
        {
            AnimationKey = "Hit_3",
            TotalFrames = 6f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DEAD_1 = new FrameData()
        {
            AnimationKey = "Dead_1",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DEAD_2 = new FrameData()
        {
            AnimationKey = "Dead_2",
            TotalFrames = 2f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DEAD_3 = new FrameData()
        {
            AnimationKey = "Dead_3",
            TotalFrames = 8f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData DEAD_4 = new FrameData()
        {
            AnimationKey = "Dead_4",
            TotalFrames = 3f,
            FrameRate = 12f,
            State = PlayerState.StandHit,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };

        public static FrameData BLOCK = new FrameData()
        {
            AnimationKey = "Block",
            TotalFrames = 4f,
            FrameRate = 12f,
            State = PlayerState.StandBlock,
            Attack = AttackButtonState.None,
            Damage = 0,
            HitStun = 0,
            MeterGainOnWiff = 0
        };
    }
}