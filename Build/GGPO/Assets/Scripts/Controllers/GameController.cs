using System;
using System.Collections.Generic;
using System.Linq;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchStateEventArgs
{
    public MatchState state;
    public MatchStateEventArgs(MatchState matchState)
    {
        state = matchState;
    }
}

public class GameController : MonoBehaviour
{
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
    [SerializeField] private HUDComponent _HUDComponent;
    [SerializeField] private FadeOverlayComponent _FadeOverlayComponent;

    [Header("Main Menu")]
    [SerializeField] private Button _CreateBtn;
    [SerializeField] private Button _ListLobbiesBtn;
    [SerializeField] private Button _VersusModeBtn;
    [SerializeField] private Button _TrainingModeBtn;

    [Header("Post FX")]
    [SerializeField] private GameObject _PostFX;

    [Header("Debug UI")]
    [SerializeField] private Button _StartStopSessionBtn;
    [SerializeField] private GameObject _MainMenuPanel;
    [SerializeField] private GameObject _DebugPanel;
    [SerializeField] private TextMeshProUGUI _RefreshRateWindow;

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

    public EventHandler<MatchState> OnGameStateChanged;

    private MatchState _GameState = MatchState.PreBattle;

    private readonly List<GGPOPlayerController> _Players = new List<GGPOPlayerController>();
    private GGPOPlayerController[] _PlayerControllers = Array.Empty<GGPOPlayerController>();

    private bool isPlaying = false;

    public static int MonitorRefreshRate => Screen.currentResolution.refreshRate;

    public GameType CurrentGameType { get; set; } = GameType.None;

    public MatchState GameState
    {
        get => _GameState;
        set
        {
            _GameState = value;
            OnGameStateChanged?.Invoke(this, _GameState);
        }
    }

    public void Awake()
    {
        LockFramerate();
        EnablePostFX();

        _DebugPanel.SetActive(false);
        _HUDComponent.gameObject.SetActive(false);

        AddListeners();
        SetLocalSessionActiveState(true);
        SetEnableLocalSessionFeatures(false);
    }

    private void Start()
    {
        _FadeOverlayComponent.ShowScreen(0.2f);
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
        _GGPOComponent.OnBodyCollisionCheck += HandleBodyCollision;

        _CreateBtn.onClick.AddListener(_LobbyComponent.CreateLobby);
        _ListLobbiesBtn.onClick.AddListener(_LobbyComponent.ListCloseLobbies);

        GGPOGameManager.Instance.OnLauncheGame += HandleLaunchGame;

        _VersusModeBtn.onClick.AddListener(StartVersusMode);
        _TrainingModeBtn.onClick.AddListener(StartTrainingMode);
        //_StartStopSessionBtn.onClick.AddListener(ToggleSession);
    }

    private void RemoveListeners()
    {
        _GGPOComponent.OnRunningChanged -= HandleRunningChanged;
        _GGPOComponent.OnStateChanged -= HandleStateChanged;
        _GGPOComponent.OnCheckCollision -= HandleCheckCollision;
        _GGPOComponent.OnBodyCollisionCheck -= HandleBodyCollision;

        _CreateBtn.onClick.RemoveListener(_LobbyComponent.CreateLobby);
        _ListLobbiesBtn.onClick.RemoveListener(_LobbyComponent.ListCloseLobbies);

        if (GGPOGameManager.Instance != null)
        {
            GGPOGameManager.Instance.OnLauncheGame -= HandleLaunchGame;
        }

        _VersusModeBtn.onClick.RemoveListener(StartVersusMode);
        _TrainingModeBtn.onClick.RemoveListener(StartTrainingMode);
        //_StartStopSessionBtn.onClick.RemoveListener(ToggleSession);
    }

    private void HandleLaunchGame(object sender, GameType gameType)
    {
        switch(gameType)
        {
            case GameType.Versus:
                CurrentGameType = GameType.Versus;

                _DebugPanel.SetActive(false);
                _HUDComponent.gameObject.SetActive(true);
                break;

            case GameType.Training:
                CurrentGameType = GameType.Training;

                _DebugPanel.SetActive(true);
                _HUDComponent.gameObject.SetActive(false);
                break;
        }

        _LobbyComponent.enabled = false;
        _MainMenuPanel.SetActive(false);
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
        GGPOGameState gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < _PlayerControllers.Length; ++i)
        {
            HitData result = _PlayerControllers[i].OnCheckCollision();
            gameState.GetPlayerRef(i).IsHit = result.IsHit;
            gameState.GetPlayerRef(i).CurrentlyHitByID = result.AttackData?.ID ?? Guid.Empty;
        }
    }

    //Checks the players controller for push body collisions
    private void HandleBodyCollision()
    {
        GGPOGameState gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < _PlayerControllers.Length; ++i)
        {
            PushBodyData push = _PlayerControllers[i].OnBodyCollision();
            gameState.GetPlayerRef(i).IsCloseToOpponent = push.IsCloseToOpponent;
            gameState.GetPlayerRef(i).IsBeingPushed = push.IsBeingPushing;
        }
    }

    // Triggers the Player Controller to update with new data
    private void HandleStateChanged()
    {
        GGPOGameState gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

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

    private void EnablePostFX()
    {
        _PostFX.SetActive(true);
    }

    private void LockFramerate()
    {
        Time.captureFramerate = MonitorRefreshRate;
        Application.targetFrameRate = MonitorRefreshRate;

        _RefreshRateWindow.text = $"Time.captureFramerate: {Time.captureFramerate}\nApplication.targetFrameRate: {Application.targetFrameRate}\nMonitor Refresh Rate: {MonitorRefreshRate}";
    }

    private void SetLocalSessionActiveState(bool isEnabled)
    {
        _StartStopSessionBtn.gameObject.SetActive(isEnabled);
    }

    private void SetEnableLocalSessionFeatures(bool isEnabled)
    {
        _GameSpeedManager.enabled = isEnabled;
        _ReplayManager.enabled = isEnabled;
    }

    private void StartVersusMode()
    {
        if (_GGPOComponent.IsRunning)
        {
            return;
        }

        _FadeOverlayComponent.HideScreen(1f).OnComplete(() =>
        {
            StartLocalGame(isDebugMode: false);
        });
    }

    private void StartTrainingMode()
    {
        if (_GGPOComponent.IsRunning)
        {
            return;
        }

        _FadeOverlayComponent.HideScreen(1f).OnComplete(() =>
        {
            StartLocalGame(isDebugMode: true);
        });
    }

    //private void ToggleSession()
    //{
    //    var btnText = _StartStopSessionBtn.GetComponentInChildren<Text>();

    //    if (!_GGPOComponent.IsRunning)
    //    {
    //        CurrentGameType = GameType.Training;
    //        StartLocalGame(isDebugMode: true);
    //        btnText.text = "Stop Local Session";
    //    }
    //    else
    //    {
    //        StopLocalGame();
    //        btnText.text = "Start Local Session";
    //    }
    //}

    private void StartLocalGame(bool isDebugMode)
    {
        SetEnableLocalSessionFeatures(true);
        _GGPOComponent.StartLocalGame(isDebugMode);
    }

    private void StopLocalGame()
    {
        SetEnableLocalSessionFeatures(false);
        _LobbyComponent.enabled = true;
        _MainMenuPanel.SetActive(true);
        _DebugPanel.SetActive(false);
        _HUDComponent.gameObject.SetActive(false);

        _GGPOComponent.Shutdown();
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

    public MatchState UpdateGameProgress(Player[] players)
    {
        switch (_GameState)
        {
            case MatchState.PreBattle:
                if (isPlaying)
                {
                    return _GameState;
                }

                isPlaying = true;

                Sequence preSequence = DOTween.Sequence();
                preSequence.Append(_FadeOverlayComponent.ShowScreen());
                preSequence.Append(_HUDComponent.Announce("Fight"));
                preSequence.OnComplete(() => 
                {
                    preSequence.Kill();
                    isPlaying = false;
                    _GameState = MatchState.Battle;
                });

                preSequence.Play();
                break;

            case MatchState.Battle:
                if (!players.Any(x => x.State == PlayerState.KO))
                {
                    break;
                }

                var play = players.Where(x => x.State == PlayerState.KO).Single();
                play.Loses += 1;

                _GameState = MatchState.PostBattle;

                break;

            case MatchState.PostBattle:
                if (isPlaying)
                {
                    return _GameState;
                }

                isPlaying = true;

                Sequence postSequence = DOTween.Sequence();
                postSequence.Append(_HUDComponent.Announce("KO"));
                postSequence.Append(_FadeOverlayComponent.HideScreen());
                postSequence.OnComplete(() =>
                {
                    postSequence.Kill();
                    isPlaying = false;
                    _GameState = MatchState.PreBattle;
                });

                postSequence.Play();
                break;
        }

        return _GameState;
    }
}