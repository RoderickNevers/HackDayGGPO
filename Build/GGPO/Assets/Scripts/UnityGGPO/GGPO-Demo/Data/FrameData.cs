using System;

public class FrameData
{
    public Guid ID = Guid.NewGuid();
    public string AnimationKey;
    public float TotalFrames;
    public float FrameRate;
    public PlayerState State;
    public AttackButtonState Attack;
    public int Damage;
    public int Stun;
    public int MeterGain;
    public int Knockback;
}