using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game System", menuName = "SystemDirection/GameSystem", order = 3)]
public class InpuGameSystem : ScriptableObject
{
    [Header("Gameplay - Default Time")]
    [SerializeField] public int MaxTime = 60;

    [Header("Gameplay - Rounds")]
    [SerializeField] public int MaxRounds = 2;

    [Header("Meter Resource")]
    [SerializeField] MeterResource MeterSource;

    [Header("Player Movement - General")]
    [SerializeField] public InputCommand Forward;
    [SerializeField] public InputCommand Back;

    [SerializeField] public InputCommand JumpUp;
    [SerializeField] public InputCommand JumpForward;
    [SerializeField] public InputCommand JumpBack;

    [SerializeField] public InputCommand Down;
    [SerializeField] public InputCommand DownForward;
    [SerializeField] public InputCommand DownBack;

    [Header("Player Movement - Adavnced")]
    [SerializeField] public bool SmallJump;
    [SerializeField] public bool QuickRecovery;
    [SerializeField] public bool DelayedRecovery;

    [Header("Sub Systems - Defensive")]
    [SerializeField] public bool Parry;
    [SerializeField] public bool GuardImpact;
    [SerializeField] public bool PushBlock;
    [SerializeField] public bool CounterAttack;
    [SerializeField] public bool RollForward;
    [SerializeField] public bool RollBackward;
    [SerializeField] public bool Dodge;

    [Header("Sub Systems - Other")]
    [SerializeField] public bool GuardBreak;
    [SerializeField] public bool Stun;
}