using System;
using System.Collections.Generic;
using Stateless;

public class CharacterStateBlockInitData
{
    public StateMachine<CharacterState, CharacterStateTrigger> StateMachine;

    public CharacterStateBlockInitData(StateMachine<CharacterState, CharacterStateTrigger> stateMachine)
    {
        StateMachine = stateMachine;
    }
}

public class CharacterStateBlock : AbstractStateBlock, IDisposable
{
    protected StateMachine<CharacterState, CharacterStateTrigger> _StateMachine;
    protected Player _Player;

    //private Dictionary<AttackState, int> _AttackFrames = new Dictionary<AttackState, int>()
    //{
    //    {AttackState.LightPunch, AnimationData.StandingAttacks.LIGHT_PUNCH_FRAMES},
    //    {AttackState.MediumPunch, AnimationData.AnimatorKeys.StandingAttacks.MEDIUM_PUNCH_FRAMES},
    //    {AttackState.HeavyPunch, AnimationData.AnimatorKeys.StandingAttacks.HEAVY_PUNCH_FRAMES},
    //    {AttackState.LightKick, AnimationData.AnimatorKeys.StandingAttacks.LIGHT_KICK_FRAMES},
    //    {AttackState.MediumKick, AnimationData.AnimatorKeys.StandingAttacks.MEDIUM_KICK_FRAMES},
    //    {AttackState.HeavyKick, AnimationData.AnimatorKeys.StandingAttacks.HEAVY_KICK_FRAMES}
    //};

    //private Dictionary<string, float> _TotalFrameLookup = new Dictionary<string, float>()
    //{
    //    {AnimationData.AnimatorKeys.IDLE, AnimationData.AnimatorKeys.IDLE_TOTAL_FRAMES},
    //    {AnimationData.AnimatorKeys.WALK_FORWARD, AnimationData.AnimatorKeys.WALK_FORWARD_TOTAL_FRAMES},
    //    {AnimationData.AnimatorKeys.WALK_BACKWARD, AnimationData.AnimatorKeys.WALK_BACKWARD_TOTAL_FRAMES},
    //    {AnimationData.AnimatorKeys.CROUCH, AnimationData.AnimatorKeys.CROUCH_TOTAL_FRAMES},
    //    {AnimationData.AnimatorKeys.JUMP_UP, AnimationData.AnimatorKeys.JUMP_UP_TOTAL_FRAMES},
    //    {AnimationData.AnimatorKeys.JUMP_FORWARD, AnimationData.AnimatorKeys.JUMP_FORWARD_TOTAL_FRAMES},
    //    {AnimationData.AnimatorKeys.JUMP_BACKWARD, AnimationData.AnimatorKeys.JUMP_BACKWARD_TOTAL_FRAMES}
    //};

    //protected FighterController characterController;
    //protected Animator animator;
    //protected PlayerIDs _PlayerID;

    //protected Move CurrentAttack
    //{
    //    get => characterController.HitboxCompoment.CurrentAttack;
    //    set => characterController.HitboxCompoment.CurrentAttack = value;
    //}

    public CharacterStateBlock(CharacterStateBlockInitData stateBlockData)
    {
        _StateMachine = stateBlockData.StateMachine;
        //animator = stateBlockData.Animator;
        //characterController = stateBlockData.CharacterController;
        //_PlayerID = _BaseCharacterController.CharacterManager.PlayerID;
    }

    public virtual void Dispose()
    {
        RemoveListeners();
        _StateMachine = null;
        //animator = null;
        //characterController = null;
    }

    protected override void AddListeners()
    {
        //UpdateHandle.OnUpdate += OnUpdate;
        //characterController.AnimationEvents.OnAnimationComplete += HandleAnimationComplete;
        //characterController.HurtboxComponent.OnHit += HandleGetHit;

    }

    protected override void RemoveListeners()
    {
        //UpdateHandle.OnUpdate -= OnUpdate;
        //characterController.AnimationEvents.OnAnimationComplete -= HandleAnimationComplete;
        //characterController.HurtboxComponent.OnHit -= HandleGetHit;
    }

    protected override void OnEnterState()
    {
        AddListeners();
    }

    protected override void OnExitState()
    {
        RemoveListeners();
    }

    protected void PlayAnimationLoop(ref Player player, FrameData frameData)
    {
        if (player.AnimationKey != frameData.AnimationKey)
        {
            player.AnimationIndex = 1;
        }

        player.AnimationKey = frameData.AnimationKey;
        player.AnimationIndex += AnimationData.FRAME_COUNTER;
        player.CurrentFrame = player.AnimationIndex / frameData.TotalFrames;

        if (player.AnimationIndex >= frameData.TotalFrames)
        {
            player.AnimationIndex = 1;
        }
    }

    protected void PlayAnimationOneShot(ref Player player, FrameData frameData)
    {
        if (player.AnimationKey != frameData.AnimationKey)
        {
            player.AnimationIndex = 1;
        }

        player.AnimationKey = frameData.AnimationKey;

        if (player.AnimationIndex < frameData.TotalFrames)
        {
            player.AnimationIndex += AnimationData.FRAME_COUNTER;
        }

        player.CurrentFrame = player.AnimationIndex / frameData.TotalFrames;
    }

    protected void PlayAttackAnimation(ref Player player, FrameData frameData)
    {
        PlayAnimationOneShot(ref player, frameData);

        player.IsAttacking = player.AnimationIndex < frameData.TotalFrames;

        if (player.IsAttacking)
        {
            player.State = frameData.State;
            player.CurrentButtonPressed = frameData.Attack;
            player.CurrentAttackID = frameData.ID;
        }
        else
        {
            player.CurrentButtonPressed = AttackButtonState.None;
            player.CurrentAttackID = Guid.Empty;
        }
    }

    protected void PlayHitAnimation(ref Player player, FrameData frameData)
    {
        PlayAnimationOneShot(ref player, frameData);
    }

    protected AttackButtonState CheckAttacking(long input)
    {
        if ((input & InputConstants.INPUT_LIGHT_PUNCH) != 0)
        {
            return AttackButtonState.LightPunch;
        }

        if ((input & InputConstants.INPUT_MEDIUM_PUNCH) != 0)
        {
            return AttackButtonState.MediumPunch;
        }

        if ((input & InputConstants.INPUT_HEAVY_PUNCH) != 0)
        {
            return AttackButtonState.HeavyPunch;
        }

        if ((input & InputConstants.INPUT_LIGHT_KICK) != 0)
        {
            return AttackButtonState.LightKick;
        }

        if ((input & InputConstants.INPUT_MEDIUM_KICK) != 0)
        {
            return AttackButtonState.MediumKick;
        }

        if ((input & InputConstants.INPUT_HEAVY_KICK) != 0)
        {
            return AttackButtonState.HeavyKick;
        }

        return AttackButtonState.None;
    }


    //protected override void OnUpdate()
    //{

    //}

    //protected bool DoesAttackExist(string key)
    //{
    //    return characterController.Movelist.AttackList.ContainsKey(key);
    //}

    //protected virtual void ExecuteAttack(string key, Move move = null)
    //{
    //    CurrentAttack = characterController.Movelist.AttackList[key];

    //    // Attack will be included for special attacks.
    //    // Run the special attack command.
    //    if (move != null)
    //    {
    //        CurrentAttack.SpecialMoveCommand.OnComplete += HandleSpecialMoveComplete;
    //        CurrentAttack.Execute();
    //    }
    //    else
    //    {
    //        // Regular attacks can just play their animation.
    //        animator.Play(Animator.StringToHash(characterController.Movelist.AttackList[key].AttackData.AnimatorKey));
    //    }
    //}

    //protected virtual void HandleSpecialMoveComplete(object sender, EventArgs e)
    //{
    //    CurrentAttack.SpecialMoveCommand.OnComplete -= HandleSpecialMoveComplete;
    //}

    //protected virtual void HandleAnimationComplete()
    //{

    //}

    //protected virtual void HandleGetHit(object sender, HurtboxArgs e)
    //{
    //    characterController.CorrectLookDirection();
    //    characterController.ApplyImpact(e.Move.AttackData.HitPushBack);

    //    switch (e.Move.AttackData.Property)
    //    {
    //        case AttackProperty.Sweep:
    //            stateMachine.FireOnHit(CharacterStateTrigger.TriggerSweep, e.Move);
    //            break;
    //        default:
    //            stateMachine.FireOnHit(CharacterStateTrigger.TriggerHitStanding, e.Move);
    //            break;
    //    }
    //}
}