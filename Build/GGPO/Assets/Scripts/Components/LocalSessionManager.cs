using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalSessionManager : MonoBehaviour
{
    [SerializeField] private GGPOComponent m_GameManager;
    [SerializeField] private LobbyComponent m_LobbyComponent;

    [SerializeField] private Button m_StartSessionBtn;
    [SerializeField] private Button m_StopSessionBtn;
    [SerializeField] private GameSpeedManager m_GameSpeedManager;

    private void Awake()
    {
        AddListeners();
        SetEnableLocalSessionFeatures(false);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        m_StartSessionBtn.onClick.AddListener(OnStartSession);
        m_StopSessionBtn.onClick.AddListener(OnStopSession);
    }

    private void RemoveListeners()
    {
        m_StartSessionBtn.onClick.RemoveListener(OnStartSession);
        m_StopSessionBtn.onClick.RemoveListener(OnStopSession);
    }

    private void SetEnableLocalSessionFeatures(bool enabled)
    {
        m_GameSpeedManager.enabled = enabled;
    }

    private void OnStartSession()
    {
        if (!m_GameManager.IsRunning)
        {
            m_GameManager.StartLocalGame();
            m_LobbyComponent.ShowGame();

            // Disable Lobby component
            m_LobbyComponent.enabled = false;

            SetEnableLocalSessionFeatures(true);
        }
    }

    private void OnStopSession()
    {
        if (m_GameManager.IsRunning)
        {
            m_GameManager.Shutdown();

            // Reenable Steam Lobby component
            m_LobbyComponent.enabled = true;

            SetEnableLocalSessionFeatures(false);
            m_LobbyComponent.ShowMainMenu();
        }
    }
}
