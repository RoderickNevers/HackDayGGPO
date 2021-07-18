using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManager : MonoBehaviour
{
    [SerializeField] private GGPOComponent m_GameManager;
    [SerializeField] private InputField m_SpeedModifier;
    [SerializeField] private Button m_ResetSpeedModifierBtn;
    [SerializeField] private Button m_PlayPauseBtn;
    [SerializeField] private Button m_StepBtn;
    [SerializeField] private GameObject m_UIRoot;

    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void OnEnable()
    {
        SetActiveUI(true);
    }

    private void OnDisable()
    {
        if (!enabled)
        {
            SetActiveUI(false);
        }
    }

    private void SetActiveUI(bool active)
    {
        m_UIRoot.SetActive(active);
    }

    private void AddListeners()
    {
        m_SpeedModifier.onValueChanged.AddListener(OnGameSpeedChanged);
        m_ResetSpeedModifierBtn.onClick.AddListener(OnGameSpeedResetPressed);
        m_PlayPauseBtn.onClick.AddListener(OnPlayPausePressed);
        m_StepBtn.onClick.AddListener(OnStepPressed);
    }

    private void RemoveListeners()
    {
        m_SpeedModifier.onValueChanged.RemoveListener(OnGameSpeedChanged);
        m_ResetSpeedModifierBtn.onClick.RemoveListener(OnGameSpeedResetPressed);
        m_PlayPauseBtn.onClick.RemoveListener(OnPlayPausePressed);
        m_StepBtn.onClick.RemoveListener(OnStepPressed);
    }

    private void OnGameSpeedChanged(string newValue)
    {
        float newSpeedModifier = 1.0f;
        if (float.TryParse(newValue, out newSpeedModifier) && newSpeedModifier != 0)
        {
            m_GameManager.SetCurrentFrameLength(SharedGame.GameManager.FRAME_LENGTH_SEC / newSpeedModifier);
        }
    }

    private void OnGameSpeedResetPressed()
    {
        m_SpeedModifier.text = "1.0";
        m_GameManager.ResetFrameLengthToDefault();
    }

    private void OnPlayPausePressed()
    {
        m_GameManager.manualFrameIncrement = !m_GameManager.manualFrameIncrement;

        var text = m_PlayPauseBtn.GetComponentInChildren<Text>();
        if (m_GameManager.manualFrameIncrement)
        {
            text.text = "Play";
        }
        else
        {
            text.text = "Pause";
        }
    }

    private void OnStepPressed()
    {
        m_GameManager.IncrementFrame();
    }
}