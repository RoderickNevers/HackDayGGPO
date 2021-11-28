using SharedGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    const int FRAME_RATE_LOCK = 60;

    [Header("GGPO Manager")]
    [SerializeField] private GGPOGameManager _GGPOComponent;

    [Header("Gameplay Objects")]
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private Transform _P1Spawn;
    [SerializeField] private Transform _P2Spawn;

    [Header("Components")]
    [SerializeField] private LobbyComponent _LobbyComponent;
    [SerializeField] private GameSpeedManager _GameSpeedManager;
    [SerializeField] private ReplayManager _ReplayManager;

    [Header("Main Menu")]
    [SerializeField] private Button _CreateBtn;
    [SerializeField] private Button _ListLobbiesBtn;
    [SerializeField] private Button _LocalGameBtn;

    [Header("Debug UI")]
    [SerializeField] private Button _StartStopSessionBtn;
    [SerializeField] private GameObject _MainMenuPanel;
    [SerializeField] private GameObject _DebugPanel;

    private readonly List<GGPOPlayerController> _Players = new List<GGPOPlayerController>();
    private GGPOPlayerController[] _PlayerControllers = Array.Empty<GGPOPlayerController>();

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        _DebugPanel.SetActive(false);

        AddListeners();

        SetLocalSessionActiveState(true);
        SetEnableLocalSessionFeatures(false);
    }

    private void Start()
    {
        LockFramerate();
    }

    public void OnDestroy()
    {
        RemoveListeners();
    }

    private void OnEnable()
    {
        SetLocalSessionActiveState(enabled);
    }

    private void OnDisable()
    {
        if (!enabled)
        {
            SetLocalSessionActiveState(enabled);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _DebugPanel.SetActive(!_DebugPanel.activeSelf);
        }
    }

    private void AddListeners()
    {
        _GGPOComponent.OnRunningChanged += HandleRunningChanged;
        _GGPOComponent.OnStateChanged += HandleStateChanged;
        _GGPOComponent.OnCheckCollision += HandleCheckCollision;

        _CreateBtn.onClick.AddListener(_LobbyComponent.CreateLobby);
        _ListLobbiesBtn.onClick.AddListener(_LobbyComponent.ListCloseLobbies);
        _LocalGameBtn.onClick.AddListener(StartStopSession);
        _StartStopSessionBtn.onClick.AddListener(StartStopSession);
    }

    private void RemoveListeners()
    {
        _GGPOComponent.OnRunningChanged -= HandleRunningChanged;
        _GGPOComponent.OnStateChanged -= HandleStateChanged;
        _GGPOComponent.OnCheckCollision -= HandleCheckCollision;

        _CreateBtn.onClick.RemoveListener(_LobbyComponent.CreateLobby);
        _ListLobbiesBtn.onClick.RemoveListener(_LobbyComponent.ListCloseLobbies);
        _StartStopSessionBtn.onClick.RemoveListener(StartStopSession);
        _StartStopSessionBtn.onClick.RemoveListener(StartStopSession);
    }

    private void HandleRunningChanged(bool running)
    {
        if (running)
        {
            HandleConnectionStart();
        }
        else
        {
            HandleConnectionEnd();
        }
    }

    public void HandleConnectionStart()
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

    public void HandleConnectionEnd()
    {
        for (int i = 0; i < _PlayerControllers.Length; ++i)
        {
            Destroy(_PlayerControllers[i].gameObject);
        }

        _PlayerControllers = Array.Empty<GGPOPlayerController>();
    }

    //Checks the players controller for attack collisions
    private void HandleCheckCollision()
    {
        var gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < _PlayerControllers.Length; ++i)
        {
            HitData result = _PlayerControllers[i].OnCheckCollision();
            gameState.GetPlayerRef(i).IsHit = result.IsHit;
            gameState.GetPlayerRef(i).CurrentlyHitByID = result.AttackData?.ID ?? Guid.Empty;
        }
    }

    // Triggers the Player Controller to update with new data
    private void HandleStateChanged()
    {
        var gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < _PlayerControllers.Length; ++i)
        {
            Player player = gameState.GetPlayer(i);
            _PlayerControllers[i].OnStateChanged(ref player);
        }
    }

    private void InstantiateNewPlayer(int playerIndex)
    {
        // add player at correct spawn position
        Transform start = playerIndex == 0 ? _P1Spawn : _P2Spawn;

        GameObject player = Instantiate(_PlayerPrefab, start.position, start.rotation);
        GGPOPlayerController playerController = player.GetComponent<GGPOPlayerController>();
        _PlayerControllers[playerIndex] = playerController;
        playerController.ID = playerIndex == 0 ? PlayerID.Player1 : PlayerID.Player2;
        _Players.Add(playerController);
    }

    private void ResetView(GGPOGameState gs)
    {
        Player[] players = gs.Players;
        _PlayerControllers = new GGPOPlayerController[players.Length];

        for (int i = 0; i < players.Length; ++i)
        {
            InstantiateNewPlayer(i);
        }
    }

    private void LockFramerate()
    {
        Time.captureFramerate = FRAME_RATE_LOCK;
        Application.targetFrameRate = FRAME_RATE_LOCK;
    }

    private void SetLocalSessionActiveState(bool isEnabled)
    {
        _StartStopSessionBtn.gameObject.SetActive(isEnabled);
    }

    public void ShowMainMenu()
    {
        _MainMenuPanel.SetActive(true);
        _DebugPanel.SetActive(false);
    }

    public void ShowGame()
    {
        _MainMenuPanel.SetActive(false);
        //_DebugPanel.SetActive(true);
    }

    private void SetEnableLocalSessionFeatures(bool isEnabled)
    {
        _GameSpeedManager.enabled = isEnabled;
        _ReplayManager.enabled = isEnabled;
    }

    private void StartStopSession()
    {
        var btnText = _StartStopSessionBtn.GetComponentInChildren<Text>();

        if (!_GGPOComponent.IsRunning)
        {
            _GGPOComponent.StartLocalGame();

            // Disable Steam Lobby component
            _LobbyComponent.enabled = false;

            SetEnableLocalSessionFeatures(true);

            ShowGame();

            btnText.text = "Stop Local Session";
        }
        else
        {
            _GGPOComponent.Shutdown();

            // Reenable Steam Lobby component
            _LobbyComponent.enabled = true;

            SetEnableLocalSessionFeatures(false);

            ShowMainMenu();

            btnText.text = "Start Local Session";
        }
    }

    public LookDirection CheckLookDirection(Player player)
    {
        if (player.ID == PlayerID.Player1)
        {
            return _Players[(int)PlayerID.Player1].transform.position.x < _Players[(int)PlayerID.Player2].transform.position.x ? LookDirection.Right : LookDirection.Left;
        }
        else
        {
            return _Players[(int)PlayerID.Player2].transform.position.x < _Players[(int)PlayerID.Player1].transform.position.x ? LookDirection.Right : LookDirection.Left;
        }
    }
}