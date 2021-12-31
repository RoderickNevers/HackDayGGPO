// Commands unique to the game systems associated with the player
using System;

[Serializable]
public class GameSystemInputCommand : BaseInputCommand
{
    public bool IsEnabled;
}