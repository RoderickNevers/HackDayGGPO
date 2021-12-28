using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Command List", menuName = "SystemDirection/CommandList", order = 1)]
public class PlayerCommandList : ScriptableObject
{
    [SerializeField] public InputCommand Standing_Attack_A;
    [SerializeField] public InputCommand Standing_Attack_B;
    [SerializeField] public InputCommand Standing_Attack_C;
    [SerializeField] public InputCommand Standing_Attack_D;
    [SerializeField] public InputCommand Standing_Attack_E;
    [SerializeField] public InputCommand Standing_Attack_F;
}

public class AttackCommand
{
    [SerializeField] public string Name;
    [SerializeField] public InputCommand Command;
}
