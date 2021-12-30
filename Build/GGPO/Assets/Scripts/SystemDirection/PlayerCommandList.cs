using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Command List", menuName = "SystemDirection/CommandList", order = 1)]
public class PlayerCommandList : ScriptableObject
{
    [Header("Player Movement")]
    [SerializeField] public InputCommand Idle;

    [SerializeField] public InputCommand Forward;
    [SerializeField] public InputCommand Back;

    [SerializeField] public InputCommand JumpUp;
    [SerializeField] public InputCommand JumpForward;
    [SerializeField] public InputCommand JumpBack;

    [SerializeField] public InputCommand Down;
    [SerializeField] public InputCommand DownForward;
    [SerializeField] public InputCommand DownBack;

    [Header("Basic Actions")]
    [SerializeField] public InputCommand Block;

    [Header("Attacks - Standing")]
    [SerializeField] private List<InputCommand> StandingAttacks;

    [Header("Attacks - Crouching")]
    [SerializeField] private List<InputCommand> CrouchingAttacks;

    [Header("Attacks - Jumping")]
    [SerializeField] private List<InputCommand> JumpingAttacks;

    [Header("Hit Animations")]
    [SerializeField] public List<FrameData> HitReactions;

    [Header("Death Animations")]
    [SerializeField] public List<FrameData> DeathReactions;

    public readonly Dictionary<Guid, FrameData> AttackLookup = new Dictionary<Guid, FrameData>();

    public void PopulateLookups()
    {
        // Attacks
        StandingAttacks.ForEach(x => AttackLookup.Add(x.FrameData.ID, x.FrameData));
        CrouchingAttacks.ForEach(x => AttackLookup.Add(x.FrameData.ID, x.FrameData));
        JumpingAttacks.ForEach(x => AttackLookup.Add(x.FrameData.ID, x.FrameData));
    }
}