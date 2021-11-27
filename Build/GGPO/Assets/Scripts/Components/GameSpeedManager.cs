using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManager : MonoBehaviour
{
    [SerializeField] private GGPOGameManager m_GameManager;
    [SerializeField] private InputField m_SpeedModifier;
    [SerializeField] private Button m_ResetSpeedModifierBtn;

    private void Start()
    {
        AddListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        m_SpeedModifier.onValueChanged.AddListener(OnGameSpeedChanged);
        m_ResetSpeedModifierBtn.onClick.AddListener(OnGameSpeedResetPressed);
    }

    private void RemoveListeners()
    {
        m_SpeedModifier.onValueChanged.RemoveListener(OnGameSpeedChanged);
        m_ResetSpeedModifierBtn.onClick.RemoveListener(OnGameSpeedResetPressed);
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
}