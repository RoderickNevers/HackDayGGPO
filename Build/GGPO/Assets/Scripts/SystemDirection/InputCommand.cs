using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputCommand", menuName = "SystemDirection/InputCommand", order = 0)]
public class InputCommand : ScriptableObject
{
    [SerializeField] public bool Enabled = true;

    [SerializeField] public InputCommandType Type;
    //[SerializeField] private List<>
}

public enum InputCommandType
{
    Single,
    Combination,
    Sequence
}