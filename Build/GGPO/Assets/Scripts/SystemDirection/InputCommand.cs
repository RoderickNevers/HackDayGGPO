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
    INPUT_UP = 1 << 0,
    INPUT_DOWN = 1 << 1,
    INPUT_LEFT = 1 << 2,
    INPUT_RIGHT = 1 << 3,

    INPUT_BUTTON_0 = 1 << 4,
    INPUT_BUTTON_1 = 1 << 5,
    INPUT_BUTTON_2 = 1 << 6,
    INPUT_BUTTON_3 = 1 << 7,
    INPUT_BUTTON_4 = 1 << 8,
    INPUT_BUTTON_5 = 1 << 9,

    INPUT_BUTTON_START = 1 << 10,
    INPUT_BUTTON_SELECT = 1 << 11
}

public enum InputCommandType
{
    None,
    Single,
    Combination,
    Sequence
}