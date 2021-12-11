using System;

public class FrameData
{
    private const float DEFAULT_PLAYBACK_SPEED = 0.2f;

    public Guid ID = Guid.NewGuid();
    public string Name;
    public string AnimationKey;
    public float TotalFrames;
    public float PlaybackSpeed = DEFAULT_PLAYBACK_SPEED;
    public PlayerState PlayerState;
    public AttackButtonState Attack;
    public int Damage;
    public float HitStun;
    public float BlockStun;
    public float DizzyStun;
    public float HitPushBack;
    public float BlockPushBack;
    public float MeterGainOnWiff;
    public float MeterGainOnHit;
    public AttackType Type;
    public AttackLevel Level;
    public AttackCategory Category;
    public AttackProperty Property;
}