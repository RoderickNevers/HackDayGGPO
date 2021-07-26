using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ReplayManager : MonoBehaviour
{
    public class ReplayInfo
    {
        public int m_FrameNumber = -1;
        public StateInputManager.PlayerInputs m_CurrentInputs;
    }

    private GGPOComponent _GGPOComponent;

    private bool m_IsRecording;
    private int m_CurrentFrameNum;

    private GGPOGameState m_InitialState;
    private List<ReplayInfo> m_PlayerInputs;
    private List<GGPOGameState> m_GameStates;

    public ReplayManager()
    {
        m_IsRecording = false;
        m_CurrentFrameNum = 0;

        Reset();
    }

    public void Reset()
    {
        m_InitialState = new GGPOGameState();
        m_GameStates = new List<GGPOGameState>();
        m_PlayerInputs = new List<ReplayInfo>();
    }

    private void Awake()
    {
        _GGPOComponent = FindObjectOfType<GGPOComponent>();
    }

    public void StartRecording()
    {
        m_IsRecording = true;

        if (_GGPOComponent.Runner.Game is GGPOGameState)
        {
            m_InitialState = (GGPOGameState)_GGPOComponent.Runner.Game;
            m_CurrentFrameNum = m_InitialState.Framenumber;
        }
    }

    public void StopRecording()
    {
        m_IsRecording = false;
    }

    public void SetInitialState(GGPOGameState initialState)
    {
        m_InitialState = initialState;
    }

    // Once we've got our inputs, we can regenerate all game states
    public void GenerateGameStates()
    {
        if (m_GameStates.Count > 0)
        {
            Debug.LogError("GameStates have already been loaded/generated");
            return;
        }

        if (m_PlayerInputs.Count == 0)
        {
            Debug.LogError("Player Inputs have not been loaded!");
            return;
        }

        // Sanitize initial inputs
        if (m_PlayerInputs[0].m_FrameNumber != 0)
        {
            m_PlayerInputs.Insert(0, new ReplayInfo()
            {
                m_FrameNumber = 0,
                m_CurrentInputs = new StateInputManager.PlayerInputs()
                {
                    m_P1Input = 0,
                    m_P2Input = 0
                }
            });
        }

        int finalFrameNumber = m_PlayerInputs[m_PlayerInputs.Count - 1].m_FrameNumber;
        int currentInputIndex = 0;

        m_GameStates.Add(m_InitialState);
        for (int i = 0; i < finalFrameNumber; ++i)
        {
            GGPOGameState currentGameState = m_GameStates[i].Clone();
            StateInputManager.PlayerInputs nextInputs = m_PlayerInputs[currentInputIndex].m_CurrentInputs;

            long[] inputs = new long[2];

            // Get inputs for this frame (if there are any)
            if (currentInputIndex == 0)
            {
                inputs[0] = nextInputs.m_P1Input;
                inputs[1] = nextInputs.m_P2Input;
            }
            else if (i > 0)
            {
                StateInputManager.PlayerInputs prevInputs = m_PlayerInputs[currentInputIndex - 1].m_CurrentInputs;
                if (m_PlayerInputs[currentInputIndex].m_FrameNumber == i)
                {
                    // Set inputs
                    inputs[0] = ResolveInputs(i, nextInputs.m_P1Input, prevInputs.m_P1Input);
                    inputs[1] = ResolveInputs(i, nextInputs.m_P2Input, prevInputs.m_P2Input);
                }
                else
                {
                    inputs[0] = prevInputs.m_P1Input;
                    inputs[1] = prevInputs.m_P2Input;
                }

            }

            // Advance current gamestate to next gamestate
            currentGameState.Update(inputs, 0);

            m_GameStates.Add(currentGameState);
        }
    }

    private long ResolveInputs(int frame, long currentInput, long previousInput)
    {
        if (currentInput < 0)
        {
            return previousInput;
        }
        else
        {
            return currentInput;
        }
    }

    private void Update()
    {
        if (m_IsRecording)
        {
            if (m_CurrentFrameNum != _GGPOComponent.Runner.Game.Framenumber)
            {
                m_CurrentFrameNum = _GGPOComponent.Runner.Game.Framenumber;

                // Record State
                m_GameStates.Add(((GGPOGameState)_GGPOComponent.Runner.Game).Clone());
            }
        }
    }
}
