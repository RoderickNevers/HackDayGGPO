using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalSessionManager : MonoBehaviour
{
    [SerializeField] private GGPOComponent m_GameManager;
    [SerializeField] private SteamLobbyComponent m_SteamLobbyComponent;
    [SerializeField] private Button m_StartSession;
    [SerializeField] private Button m_StopSession;

    private void Awake()
    {
        AddListeners();

        SetButtonsActive(true);
    }

    private void OnDestroy()
    {
        RemoveListeners();

        SetButtonsActive(false);
    }

    private void OnDisable()
    {
        SetButtonsActive(false);
    }
    private void OnEnable()
    {
        SetButtonsActive(true);
    }

    private void AddListeners()
    {
        m_StartSession.onClick.AddListener(OnStartSession);
        m_StopSession.onClick.AddListener(OnStopSession);
    }

    private void RemoveListeners()
    {
        m_StartSession.onClick.RemoveListener(OnStartSession);
        m_StopSession.onClick.RemoveListener(OnStopSession);
    }

    private void SetButtonsActive(bool enabled)
    {
        m_StartSession.gameObject.SetActive(enabled);
        m_StopSession.gameObject.SetActive(enabled);
    }

    private void OnStartSession()
    {
        m_GameManager.StartLocalGame();

        // Disable Steam Lobby component
        m_SteamLobbyComponent.enabled = false;
    }

    private void OnStopSession()
    {
        m_GameManager.Shutdown();

        // Reenable Steam Lobby component
        m_SteamLobbyComponent.enabled = true;
    }
}
