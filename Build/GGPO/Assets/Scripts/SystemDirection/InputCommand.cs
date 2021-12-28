using System;
using System.Collections.Generic;
using UnityEngine;

// Master Template
[Serializable]
public class BaseInputCommand
{
    public InputCommandType Type;
    public InputAction InputAction;
    public List<InputButtons> Buttons;
}

// Commands unique to the character
[Serializable]
public class InputCommand: BaseInputCommand
{
    public FrameData FrameData;
}

// Commands unique to the game systems associated with the player
[Serializable]
public class GameSystemInputCommand : BaseInputCommand
{
    public bool IsEnabled;
}

public enum InputAction
{
    None,
    Press,
    Tap,
    DoubleTap,
    Hold
}

public enum InputButtons
{
    None,
    Up,
    Down,
    Right,
    Left,
    ButtonA,
    ButtonB,
    ButtonC,
    ButtonD,
    ButtonE,
    ButtonF
}

public enum InputCommandType
{
    None,
    Single,
    Combination,
    Sequence
}