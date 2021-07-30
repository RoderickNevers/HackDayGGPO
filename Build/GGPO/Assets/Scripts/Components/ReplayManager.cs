using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ReplayManager : MonoBehaviour
{
    private GGPOComponent _GGPOComponent;

    private bool m_IsRecording;
    private int m_CurrentFrameNum;

    private ReplayData m_CurrentReplay;

    private bool m_PlaybackForward;
    private int m_CurrentPlaybackIndex;
    private List<GGPOGameState> m_GameStates;

    public ReplayManager()
    {
        m_IsRecording = false;
        m_PlaybackForward = true;

        Reset();
    }

    public void Reset()
    {
        m_CurrentFrameNum = 0;
        m_GameStates = new List<GGPOGameState>();

        m_CurrentReplay = null;
        m_CurrentPlaybackIndex = 0;
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
            m_CurrentReplay = new ReplayData();

            m_CurrentReplay.m_InitialState = ((GGPOGameState)_GGPOComponent.Runner.Game).Clone();
            m_CurrentFrameNum = m_CurrentReplay.m_InitialState.Framenumber;
        }
    }

    public void StopRecording()
    {
        m_IsRecording = false;
    }

    // Once we've got our inputs, we can regenerate all game states
    public void GenerateGameStates()
    {
        if (m_GameStates.Count > 0)
        {
            Debug.LogError("GameStates have already been loaded/generated");
            return;
        }

        if (m_CurrentReplay == null)
        {
            Debug.LogError("Player Inputs have not been loaded!");
            return;
        }

        // Sanitize initial inputs
        List<PlayerInputs> playerInputs = m_CurrentReplay.m_PlayerInputs;
        if (playerInputs[0].m_FrameNumber != 0)
        {
            playerInputs.Insert(0, new PlayerInputs()
            {
                m_FrameNumber = 0,
                m_P1Input = 0,
                m_P2Input = 0
            });
        }

        int finalFrameNumber = playerInputs[playerInputs.Count - 1].m_FrameNumber;
        int currentInputIndex = 0;

        m_GameStates.Add(m_CurrentReplay.m_InitialState);
        for (int i = 0; i < finalFrameNumber; ++i)
        {
            GGPOGameState currentGameState = m_GameStates[i].Clone();
            PlayerInputs nextInputs = playerInputs[currentInputIndex];

            long[] inputs = new long[2];

            // Get inputs for this frame (if there are any)
            if (currentInputIndex == 0)
            {
                inputs[0] = nextInputs.m_P1Input;
                inputs[1] = nextInputs.m_P2Input;
            }
            else if (i > 0)
            {
                PlayerInputs prevInputs = playerInputs[currentInputIndex - 1];
                if (playerInputs[currentInputIndex].m_FrameNumber == i)
                {
                    // Set inputs
                    inputs[0] = ResolveInputs(nextInputs.m_P1Input, prevInputs.m_P1Input);
                    inputs[1] = ResolveInputs(nextInputs.m_P2Input, prevInputs.m_P2Input);
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

    private ReplayData ConvertGameStatesToReplayData()
    {
        if (m_GameStates?.Count == 0)
        {
            return null;
        }

        ReplayData replay = new ReplayData();
        replay.m_InitialState = m_GameStates[0];

        PlayerInputs inputsToSave = new PlayerInputs();
        PlayerInputs mostRecentInputs = new PlayerInputs()
        {
            m_P1Input = 0,
            m_P2Input = 0
        };

        // Iterate through loaded game states and only record changes in input
        for (int i = 1; i < m_GameStates.Count; ++i)
        {
            GGPOGameState currGameState = m_GameStates[i];
            inputsToSave.Reset();

            if (mostRecentInputs.m_P1Input != currGameState.UnserializedInputsP1)
            {
                mostRecentInputs.m_P1Input = currGameState.UnserializedInputsP1;

                inputsToSave.m_FrameNumber = currGameState.Framenumber;
                inputsToSave.m_P1Input = mostRecentInputs.m_P1Input;
            }
            if (mostRecentInputs.m_P2Input != currGameState.UnserializedInputsP2)
            {
                mostRecentInputs.m_P2Input = currGameState.UnserializedInputsP2;

                inputsToSave.m_FrameNumber = currGameState.Framenumber;
                inputsToSave.m_P2Input = mostRecentInputs.m_P2Input;
            }

            // if there were any change in inputs, load it into list
            if (inputsToSave.m_FrameNumber != -1)
            {
                replay.m_PlayerInputs.Add(inputsToSave.Clone());
            }
        }

        return replay;
    }

    private long ResolveInputs(long currentInput, long previousInput)
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

    public void StartPlayback()
    {
        if (m_GameStates.Count == 0)
        {
            Debug.LogError("No GameStates loaded!");
            return;
        }

        // Notify GGPOComponent that 
        _GGPOComponent.StartPlayback(m_GameStates[0], this);
        m_CurrentPlaybackIndex = 0;
    }

    public void StopPlayback()
    {
        _GGPOComponent.StopPlayback();
    }

    public GGPOGameState GetNextGameState()
    {
        if (m_PlaybackForward)
        {
           m_CurrentPlaybackIndex = Mathf.Min(++m_CurrentPlaybackIndex, m_GameStates.Count - 1);
        }
        else
        {
            m_CurrentPlaybackIndex = Mathf.Max(0, --m_CurrentPlaybackIndex);
        }

        return m_GameStates[m_CurrentPlaybackIndex];
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
