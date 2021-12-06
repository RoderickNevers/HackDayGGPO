using System;

public class FrameData
{
    public Guid ID = Guid.NewGuid();
    public string Name;
    public string AnimationKey;
    public float TotalFrames;
    public float FrameRate;
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