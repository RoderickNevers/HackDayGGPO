using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEditor;

public class ReplayManager : MonoBehaviour
{
    private GGPOComponent _GGPOComponent;

    private bool m_IsRecording;
    private int m_CurrentFrameNum;

    private ReplayData m_CurrentReplay;

    private bool m_PlaybackForward;
    private int m_CurrentPlaybackIndex;
    private List<GGPOGameState> m_GameStates;

    [SerializeField] private Button m_RecordBtn;
    [SerializeField] private Button m_LoadBtn;

    [SerializeField] private Button m_PlayPauseBtn;
    [SerializeField] private Button m_RewindPauseBtn;
    [SerializeField] private Button m_StepForwardBtn;
    [SerializeField] private Button m_StepBackwardsBtn;

    [SerializeField] private Slider m_Slider;
    [SerializeField] private Text m_MinSliderText;
    [SerializeField] private Text m_MaxSliderText;

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
        m_CurrentPlaybackIndex = -1;
    }

    private void Awake()
    {
        _GGPOComponent = FindObjectOfType<GGPOComponent>();

        SetActivePlaybackButtons(false);
    }

    private void Start()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    // ----------------------- BUTTON HANDLERS ------------------------------
    private void AddListeners()
    {
        m_RecordBtn.onClick.AddListener(OnRecordPressed);
        m_LoadBtn.onClick.AddListener(OnLoadPressed);

        m_PlayPauseBtn.onClick.AddListener(OnPlayPausePressed);
        m_RewindPauseBtn.onClick.AddListener(OnRewindPausePressed);
        m_StepForwardBtn.onClick.AddListener(StepForwardPressed);
        m_StepBackwardsBtn.onClick.AddListener(StepBackwardsPressed);
        m_Slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void RemoveListeners()
    {
        m_RecordBtn.onClick.RemoveListener(OnRecordPressed);
        m_LoadBtn.onClick.RemoveListener(OnLoadPressed);

        m_PlayPauseBtn.onClick.RemoveListener(OnPlayPausePressed);
        m_RewindPauseBtn.onClick.RemoveListener(OnRewindPausePressed);
        m_StepForwardBtn.onClick.RemoveListener(StepForwardPressed);
        m_StepBackwardsBtn.onClick.RemoveListener(StepBackwardsPressed);
        m_Slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void SetActivePlaybackButtons(bool enable)
    {
        m_RewindPauseBtn.gameObject.SetActive(enable);
        m_StepBackwardsBtn.gameObject.SetActive(enable);
        m_Slider.gameObject.SetActive(enable);
        m_MinSliderText.gameObject.SetActive(enable);
        m_MaxSliderText.gameObject.SetActive(enable);
    }

    private void OnRecordPressed()
    {
        var recText = m_RecordBtn.GetComponentInChildren<Text>();
        var loadText = m_LoadBtn.GetComponentInChildren<Text>();

        if (m_GameStates.Count == 0)
        {
            // start recording
            StartRecording();

            // Disable save/load button during recording
            m_LoadBtn.gameObject.SetActive(false);

            // update text
            recText.text = "Stop Recording";
        }
        else if (m_IsRecording)
        {
            // stop recording
            StopRecording();

            // Enable playback buttons
            SetActivePlaybackButtons(true);

            // Enable save/load button during recording
            m_LoadBtn.gameObject.SetActive(true);

            // update text
            recText.text = "Clear Recording";
            loadText.text = "Save Recording";
        }
        else if (!m_IsRecording && m_GameStates.Count != 0)
        {
            // clear
            Reset();

            // Disable playback buttons
            SetActivePlaybackButtons(false);

            StopPlayback();

            // update text
            recText.text = "Start Recording";
            loadText.text = "Load Save Data";
        }
    }

    private void OnLoadPressed()
    {
        var recText = m_RecordBtn.GetComponentInChildren<Text>();
        var loadText = m_LoadBtn.GetComponentInChildren<Text>();

        if (m_GameStates.Count > 0)
        {
            // Saving
            string path = EditorUtility.SaveFilePanel("Save replay", "", "GGPODemo.json", "json");
            if (path.Length != 0)
            {
                ReplayData replayData = ConvertGameStatesToReplayData();
                string jsonData = JsonUtility.ToJson(replayData, true);
                File.WriteAllText(path, jsonData);
            }
        }
        else
        {
            // Loading
            string path = EditorUtility.OpenFilePanel("Load a save file", "", "json");
            if (path.Length != 0)
            {
                string jsonData = File.ReadAllText(path);

                m_CurrentReplay = JsonUtility.FromJson<ReplayData>(jsonData);

                GenerateGameStates();

                m_CurrentPlaybackIndex = 0;

                // Update game mode
                StartPlayback();

                // Enable playback buttons
                SetActivePlaybackButtons(true);

                recText.text = "Clear Recording";
                loadText.text = "Save Recording";
            }
        }
    }

    private void OnPlayPausePressed()
    {
        m_PlaybackForward = true;
        _GGPOComponent.manualFrameIncrement = !_GGPOComponent.manualFrameIncrement;

        UpdatePlayRewindButtonText();
    }

    private void OnRewindPausePressed()
    {
        m_PlaybackForward = false;
        _GGPOComponent.manualFrameIncrement = !_GGPOComponent.manualFrameIncrement;

        UpdatePlayRewindButtonText();
    }

    private void UpdatePlayRewindButtonText()
    {
        var playText = m_PlayPauseBtn.GetComponentInChildren<Text>();
        var rewindText = m_RewindPauseBtn.GetComponentInChildren<Text>();
        if (_GGPOComponent.manualFrameIncrement)
        {
            rewindText.text = "Rewind";
            playText.text = "Play";
        }
        else
        {
            rewindText.text = "Pause";
            playText.text = "Pause";
        }
    }

    private void StepForwardPressed()
    {
        m_PlaybackForward = true;

        _GGPOComponent.IncrementFrame();
    }

    private void StepBackwardsPressed()
    {
        m_PlaybackForward = false;

        _GGPOComponent.IncrementFrame();
    }

    private void OnSliderValueChanged(float value)
    {
        if (m_GameStates.Count > 0)
        {
            // Check to see if we changed this manually or through code
            if (m_CurrentPlaybackIndex != (int)value - m_GameStates[0].Framenumber)
            {
                m_CurrentPlaybackIndex = (int)value - m_GameStates[0].Framenumber;

                _GGPOComponent.Runner.SetGame(m_GameStates[m_CurrentPlaybackIndex]);
            }
        }
    }

    public void StartRecording()
    {
        Reset();

        m_IsRecording = true;

        _GGPOComponent.manualFrameIncrement = false;
        UpdatePlayRewindButtonText();

        if (_GGPOComponent.Runner.Game is GGPOGameState)
        {
            GGPOGameState startingGameState = (GGPOGameState)_GGPOComponent.Runner.Game;

            m_CurrentFrameNum = startingGameState.Framenumber;
            m_GameStates.Add(startingGameState.Clone());
        }
    }

    public void StopRecording()
    {
        m_IsRecording = false;

        StartPlayback();
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

        int initialFrameNumber = m_CurrentReplay.m_InitialState.Framenumber;

        // Sanitize initial inputs
        List<PlayerInputs> playerInputs = m_CurrentReplay.m_PlayerInputs;
        if (playerInputs[0].m_FrameNumber != initialFrameNumber)
        {
            playerInputs.Insert(0, new PlayerInputs()
            {
                m_FrameNumber = initialFrameNumber,
                m_P1Input = 0,
                m_P2Input = 0
            });
        }

        int finalFrameNumber = playerInputs[playerInputs.Count - 1].m_FrameNumber;
        int currentInputIndex = 0;

        m_GameStates.Add(m_CurrentReplay.m_InitialState);
        for (int i = initialFrameNumber; i < finalFrameNumber; ++i)
        {
            int gameStateIndex = i - initialFrameNumber;
            GGPOGameState currentGameState = m_GameStates[gameStateIndex].Clone();
            PlayerInputs nextInputs = playerInputs[currentInputIndex];

            long[] inputs = new long[2];

            // Get inputs for this frame (if there are any)
            if (currentInputIndex == 0)
            {
                inputs[0] = nextInputs.m_P1Input;
                inputs[1] = nextInputs.m_P2Input;
                ++currentInputIndex;
            }
            else
            {
                PlayerInputs prevInputs = playerInputs[currentInputIndex - 1];

                // we do framenumber-1 here because frame n was actually n-1 before we called update.
                if (playerInputs[currentInputIndex].m_FrameNumber - 1 == i)
                {
                    // Set inputs
                    inputs[0] = ResolveInputs(nextInputs.m_P1Input, prevInputs.m_P1Input);
                    inputs[1] = ResolveInputs(nextInputs.m_P2Input, prevInputs.m_P2Input);
                    ++currentInputIndex;
                }
                else
                {
                    inputs[0] = prevInputs.m_P1Input;
                    inputs[1] = prevInputs.m_P2Input;
                }
            }

            // Advance current gamestate to next gamestate
            currentGameState.UpdateSimulation(inputs, 0);

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

        _GGPOComponent.manualFrameIncrement = true;

        // Start playback mode
        if (m_CurrentPlaybackIndex == -1)
        {
            m_CurrentPlaybackIndex = m_GameStates.Count - 1;
        }

        _GGPOComponent.StartPlayback(m_GameStates[m_CurrentPlaybackIndex], this);

        UpdatePlayRewindButtonText();

        m_Slider.minValue = m_GameStates[0].Framenumber;
        m_Slider.maxValue = m_GameStates[m_GameStates.Count - 1].Framenumber;
        m_Slider.value = m_GameStates[m_CurrentPlaybackIndex].Framenumber;
        m_MinSliderText.text = m_Slider.minValue.ToString();
        m_MaxSliderText.text = m_Slider.maxValue.ToString();
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

        m_Slider.value = m_GameStates[m_CurrentPlaybackIndex].Framenumber;

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
        if (m_CurrentPlaybackIndex != -1)
        {
            if (m_CurrentPlaybackIndex == 0 || m_CurrentPlaybackIndex == m_GameStates.Count - 1)
            {
                _GGPOComponent.manualFrameIncrement = true;

                UpdatePlayRewindButtonText();
            }
        }
    }
}
