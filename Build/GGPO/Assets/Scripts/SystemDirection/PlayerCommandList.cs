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

    [Header("Attacks - Standing")]
    [SerializeField] public List<InputCommand> StandingAttacks;

    [Header("Attacks - Crouching")]
    [SerializeField] public List<InputCommand> CrouchingAttacks;

    [Header("Attacks - Jumping")]
    [SerializeField] public List<InputCommand> JumpingAttacks;
}