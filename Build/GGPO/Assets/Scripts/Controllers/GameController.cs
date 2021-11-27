using SharedGame;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    const int RATE_LOCK = 60;

    [SerializeField] private GGPOComponent _GGPOComponent;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform _P1Spawn;
    [SerializeField] private Transform _P2Spawn;

    [Header("Components")]
    //[SerializeField] private GGPOComponent m_GameManager;
    [SerializeField] private LobbyComponent m_LobbyComponent;
    [SerializeField] private GameSpeedManager m_GameSpeedManager;
    [SerializeField] private ReplayManager m_ReplayManager;

    [Header("UI")]
    //[SerializeField] private Button m_StartOnlySessionBtn;
    [SerializeField] private Button m_StartStopSessionBtn;

    [SerializeField] private GameObject m_MainMenuPanel;
    [SerializeField] private GameObject m_DebugPanel;

    private GGPOPlayerController[] PlayerControllers = Array.Empty<GGPOPlayerController>();

    public void Awake()
    {
        _GGPOComponent.OnRunningChanged += HandleRunningChanged;
        _GGPOComponent.OnStateChanged += HandleStateChanged;
        _GGPOComponent.OnCheckCollision += HandleCheckCollision;

        //------

        AddListeners();
        SetButtonsActive(true);
        SetEnableLocalSessionFeatures(false);
    }

    private void Start()
    {
        LockFramerate();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnDestroy()
    {
        _GGPOComponent.OnRunningChanged -= HandleRunningChanged;
        _GGPOComponent.OnStateChanged -= HandleStateChanged;
        _GGPOComponent.OnCheckCollision -= HandleCheckCollision;

        //-----

        RemoveListeners();
    }

    private void HandleRunningChanged(bool running)
    {
        if (running)
        {
            OnConnectionStart();
        }
        else
        {
            OnConnectionEnd();
        }
    }

    public void OnConnectionStart()
    {
        GGPOGameState gameState = (GGPOGameState)_GGPOComponent.Runner.Game;
        ResetView(gameState);

        for (int i = 0; i < 2; i++)
        {
            // add player at correct spawn position
            Transform start = i == 0 ? _P1Spawn : _P2Spawn;
            gameState.InitPlayer(i, start.position);
        }
    }

    public void OnConnectionEnd()
    {
        for (int i = 0; i < PlayerControllers.Length; ++i)
        {
            Destroy(PlayerControllers[i].gameObject);
        }

        PlayerControllers = Array.Empty<GGPOPlayerController>();
    }

    private void InstantiateNewPlayer(int playerIndex)
    {
        // add player at correct spawn position
        Transform start = playerIndex == 0 ? _P1Spawn : _P2Spawn;

        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        GGPOPlayerController playerController = player.GetComponent<GGPOPlayerController>();
        PlayerControllers[playerIndex] = playerController;
        playerController.ID = playerIndex == 0 ? PlayerID.Player1 : PlayerID.Player2;
        MatchComponent.Instance.Players.Add(playerController);
    }

    private void ResetView(GGPOGameState gs)
    {
        Player[] players = gs.Players;
        PlayerControllers = new GGPOPlayerController[players.Length];

        for (int i = 0; i < players.Length; ++i)
        {
            InstantiateNewPlayer(i);
        }
    }

    //Checks the players controller for attack collisions
    private void HandleCheckCollision()
    {
        var gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < PlayerControllers.Length; ++i)
        {
            HitData result = PlayerControllers[i].OnCheckCollision();
            gameState.GetPlayerRef(i).IsHit = result.IsHit;
            gameState.GetPlayerRef(i).CurrentlyHitByID = result.AttackData?.ID ?? Guid.Empty;
        }
    }

    // Triggers the Player Controller to update with new data
    private void HandleStateChanged()
    {
        var gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < PlayerControllers.Length; ++i)
        {
            Player player = gameState.GetPlayer(i);
            PlayerControllers[i].OnStateChanged(ref player);
        }
    }

    private void LockFramerate()
    {
        Time.captureFramerate = RATE_LOCK;
        Application.targetFrameRate = RATE_LOCK;
    }

    //---

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
        //m_StartOnlySessionBtn.onClick.AddListener(OnStartStopSession);
        m_StartStopSessionBtn.onClick.AddListener(OnStartStopSession);
    }

    private void RemoveListeners()
    {
        //m_StartOnlySessionBtn.onClick.RemoveListener(OnStartStopSession);
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

        if (!_GGPOComponent.IsRunning)
        {
            _GGPOComponent.StartLocalGame();

            // Disable Steam Lobby component
            m_LobbyComponent.enabled = false;

            SetEnableLocalSessionFeatures(true);

            ShowGame();

            btnText.text = "Stop Local Session";
        }
        else
        {
            _GGPOComponent.Shutdown();

            // Reenable Steam Lobby component
            m_LobbyComponent.enabled = true;

            SetEnableLocalSessionFeatures(false);

            ShowMainMenu();

            btnText.text = "Start Local Session";
        }
    }
}