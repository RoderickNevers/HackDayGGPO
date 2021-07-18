using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalSessionManager : MonoBehaviour
{
    [SerializeField] private GGPOComponent m_GameManager;
    [SerializeField] private SteamLobbyComponent m_SteamLobbyComponent;

    [SerializeField] private Button m_StartStopSessionBtn;
    [SerializeField] private GameSpeedManager m_GameSpeedManager;
    [SerializeField] private ReplayManager m_ReplayManager;

    private void Awake()
    {
        AddListeners();

        SetButtonsActive(true);

        SetEnableLocalSessionFeatures(false);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void OnDisable()
    {
        if (!enabled)
        {
            SetButtonsActive(enabled);
        }
    }
    private void OnEnable()
    {
        SetButtonsActive(enabled);
    }

    private void AddListeners()
    {
        m_StartStopSessionBtn.onClick.AddListener(OnStartStopSession);
    }

    private void RemoveListeners()
    {
        m_StartStopSessionBtn.onClick.RemoveListener(OnStartStopSession);
    }

    private void SetButtonsActive(bool enabled)
    {
        m_StartStopSessionBtn.gameObject.SetActive(enabled);
    }

    private void SetEnableLocalSessionFeatures(bool enabled)
    {
        m_GameSpeedManager.enabled = enabled;
        m_ReplayManager.enabled = enabled;
    }

    private void OnStartStopSession()
    {
        var btnText = m_StartStopSessionBtn.GetComponentInChildren<Text>();

        if (!m_GameManager.IsRunning)
        {
            m_GameManager.StartLocalGame();

            // Disable Steam Lobby component
            m_SteamLobbyComponent.enabled = false;

            SetEnableLocalSessionFeatures(true);

            btnText.text = "Stop Local Session";
        }
        else
        {
            m_GameManager.Shutdown();

            // Reenable Steam Lobby component
            m_SteamLobbyComponent.enabled = true;

            SetEnableLocalSessionFeatures(false);

            btnText.text = "Start Local Session";
        }
    }
}
