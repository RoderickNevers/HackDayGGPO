using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game System", menuName = "SystemDirection/GameSystem", order = 3)]
public class GameSystem : ScriptableObject
{
    [Header("Meter Resource")]
    [SerializeField] MeterResource MeterSource;

    [Header("Sub Systems")]
    [SerializeField] public GameSystemInputCommand SmallJump = new GameSystemInputCommand() { InputAction = InputAction.Tap };
    [SerializeField] public GameSystemInputCommand QuickRecovery = new GameSystemInputCommand() { InputAction = InputAction.Tap };
    [SerializeField] public GameSystemInputCommand DelayedRecovery = new GameSystemInputCommand() { InputAction = InputAction.Hold };
    [SerializeField] public GameSystemInputCommand Parry = new GameSystemInputCommand() { InputAction = InputAction.Tap };
    [SerializeField] public GameSystemInputCommand GuardImpact = new GameSystemInputCommand();
    [SerializeField] public GameSystemInputCommand PushBlock = new GameSystemInputCommand();
    [SerializeField] public GameSystemInputCommand CounterAttack = new GameSystemInputCommand();
    [SerializeField] public GameSystemInputCommand RollForward = new GameSystemInputCommand() { InputAction = InputAction.Press };
    [SerializeField] public GameSystemInputCommand RollBackward = new GameSystemInputCommand() { InputAction = InputAction.Press };
    [SerializeField] public GameSystemInputCommand Dodge = new GameSystemInputCommand() { InputAction = InputAction.Press };

    [Header("Sub Systems - Other")]
    [SerializeField] public bool GuardBreak;
    [SerializeField] public bool Dizzy;
}