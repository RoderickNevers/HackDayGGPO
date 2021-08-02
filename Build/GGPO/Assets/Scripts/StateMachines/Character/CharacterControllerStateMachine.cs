using System;
using UnityEngine;
using Stateless;

[Serializable]
public class CharacterControllerStateMachine: IDisposable
{
    private readonly StateMachine<CharacterState, CharacterStateTrigger> _Machine;

    private readonly IntroState _IntroState;
    private readonly StandingState _StandingState;
    private readonly AdvancingState _AdvancingState;
    private readonly RetreatingState _RetreatingState;
    private readonly CrouchingState _CrouchingState;
    private readonly GroundedAttackState _AttackGroundState;
    private readonly InAirAttackState _AttackInAirState;
    private readonly JumpUpState _JumpUpState;
    private readonly JumpTowardsState _JumpTowardsState;
    private readonly JumpBackState _JumpAwayState;
    private readonly FallingState _FallingState;
    private readonly LandingState _LandingState;
    private readonly HitStandingState _HitStandingState;
    private readonly SweepState _SweepState;
    private readonly OnTheGroundState _OnTheGroundState;
    private readonly GettingUpState _GettingUpState;
    private readonly DizzyState _DizzyState;
    private readonly KOState _KOState;

    public CharacterState CurrentTrigger => _Machine.State;

    public CharacterControllerStateMachine(Player player)
    {
        _Machine = new StateMachine<CharacterState, CharacterStateTrigger>(CharacterState.Intro);

        CharacterStateBlockInitData stateBlockData = new CharacterStateBlockInitData(_Machine, player);

        _IntroState = new IntroState(stateBlockData);
        _StandingState = new StandingState(stateBlockData);
        _AdvancingState = new AdvancingState(stateBlockData);
        _RetreatingState = new RetreatingState(stateBlockData);
        _CrouchingState = new CrouchingState(stateBlockData);
        _AttackGroundState = new GroundedAttackState(stateBlockData);
        _AttackInAirState = new InAirAttackState(stateBlockData);
        _JumpUpState = new JumpUpState(stateBlockData);
        _JumpTowardsState = new JumpTowardsState(stateBlockData);
        _JumpAwayState = new JumpBackState(stateBlockData);
        _FallingState = new FallingState(stateBlockData);
        _LandingState = new LandingState(stateBlockData);
        _HitStandingState = new HitStandingState(stateBlockData);
        _SweepState = new SweepState(stateBlockData);
        _OnTheGroundState = new OnTheGroundState(stateBlockData);
        _GettingUpState = new GettingUpState(stateBlockData);
        _DizzyState = new DizzyState(stateBlockData);
        _KOState = new KOState(stateBlockData);

        _Machine.Fire(CharacterStateTrigger.TriggerStanding);
    }

    public void Dispose()
    {
        _IntroState?.Dispose();
        _StandingState?.Dispose();
        _AdvancingState?.Dispose();
        _RetreatingState?.Dispose();
        _CrouchingState?.Dispose();
        _AttackGroundState?.Dispose();
        _AttackInAirState?.Dispose();
        _JumpUpState?.Dispose();
        _JumpTowardsState?.Dispose();
        _JumpAwayState?.Dispose();
        _FallingState?.Dispose();
        _LandingState?.Dispose();
        _HitStandingState?.Dispose();
        _SweepState?.Dispose();
        _OnTheGroundState?.Dispose();
        _GettingUpState?.Dispose();
        _DizzyState?.Dispose();
        _KOState?.Dispose();
    }

    public void Reset()
    {
        _Machine.Fire(CharacterStateTrigger.TriggerStanding);
    }
}