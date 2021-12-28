using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Command List", menuName = "SystemDirection/CommandList", order = 1)]
public class PlayerCommandList : ScriptableObject
{
    [Header("Standing")]
    [SerializeField] public List<InputCommand> StandingAttacks;

    [Header("Crouching")]
    [SerializeField] public List<InputCommand> CrouchingAttacks;

    [Header("Jumping")]
    [SerializeField] public List<InputCommand> JumpingAttacks;

}