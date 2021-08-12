using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalSessionManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GGPOComponent m_GameManager;
    [SerializeField] private LobbyComponent m_LobbyComponent;
    [SerializeField] private GameSpeedManager m_GameSpeedManager;
    [SerializeField] private ReplayManager m_ReplayManager;

    [Header("UI")]
    [SerializeField] private Button m_StartOnlySessionBtn;
    [SerializeField] private Button m_StartStopSessionBtn;

    [SerializeField] private GameObject m_MainMenuPanel;
    [SerializeField] private GameObject m_DebugPanel;


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
        m_StartOnlySessionBtn.onClick.AddListener(OnStartStopSession);
        m_StartStopSessionBtn.onClick.AddListener(OnStartStopSession);
    }

    private void RemoveListeners()
    {
        m_StartOnlySessionBtn.onClick.RemoveListener(OnStartStopSession);
        m_StartStopSessionBtn.onClick.RemoveListener(OnStartStopSession);
    }

    private void SetButtonsActive(bool enabled)
    {
        m_StartStopSessionBtn.gameObject.SetActive(enabled);
    }

    public void ShowMainMenu()
    {
        m_MainMenuPanel.SetActive(true);
        m_DebugPanel.SetActive(false);
    }

    public void ShowGame()
    {
        m_MainMenuPanel.SetActive(false);
        m_DebugPanel.SetActive(true);
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
            m_LobbyComponent.enabled = false;

            SetEnableLocalSessionFeatures(true);

            ShowGame();

            btnText.text = "Stop Local Session";
        }
        else
        {
            m_GameManager.Shutdown();

            // Reenable Steam Lobby component
            m_LobbyComponent.enabled = true;

            SetEnableLocalSessionFeatures(false);

            ShowMainMenu();

            btnText.text = "Start Local Session";
        }
    }
}
