using System;
using System.Collections.Generic;
using System.Linq;

using DG.Tweening;
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
    [SerializeField] private HUDComponent _HUDComponent;

    [Header("Main Menu")]
    [SerializeField] private Button _CreateBtn;
    [SerializeField] private Button _ListLobbiesBtn;
    [SerializeField] private Button _VersusModeBtn;
    [SerializeField] private Button _TrainingModeBtn;

    [Header("Debug UI")]
    [SerializeField] private Button _StartStopSessionBtn;
    [SerializeField] private GameObject _MainMenuPanel;
    [SerializeField] private GameObject _DebugPanel;

    public enum MatchState
    {
        PreBattle,
        Battle,
        PostBattle
    }

    public enum GameType
    {
        None,
        Versus,
        Training
    }

    [HideInInspector] public GameType CurrentGameType = GameType.None;
    [HideInInspector] public MatchState GameState;

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
        _VersusModeBtn.onClick.AddListener(StartVersusMode);
        _TrainingModeBtn.onClick.AddListener(StartTrainingMode);
        _StartStopSessionBtn.onClick.AddListener(ToggleSession);
    }

    private void RemoveListeners()
    {
        _GGPOComponent.OnRunningChanged -= HandleRunningChanged;
        _GGPOComponent.OnStateChanged -= HandleStateChanged;
        _GGPOComponent.OnCheckCollision -= HandleCheckCollision;

        _CreateBtn.onClick.RemoveListener(_LobbyComponent.CreateLobby);
        _ListLobbiesBtn.onClick.RemoveListener(_LobbyComponent.ListCloseLobbies);
        _VersusModeBtn.onClick.RemoveListener(StartVersusMode);
        _TrainingModeBtn.onClick.RemoveListener(StartTrainingMode);
        _StartStopSessionBtn.onClick.RemoveListener(ToggleSession);
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

    private void SetEnableLocalSessionFeatures(bool isEnabled)
    {
        _GameSpeedManager.enabled = isEnabled;
        _ReplayManager.enabled = isEnabled;
    }

    public void ShowHud()
    {
        _DebugPanel.SetActive(false);
        _HUDComponent.HideScreen().OnComplete(() =>
        {
            _HUDComponent.gameObject.SetActive(true);
            CurrentGameType = GameType.Versus;

        });
    }

    private void StartVersusMode()
    {
        if (_GGPOComponent.IsRunning)
        {
            return;
        }

        StartLocalGame(isDebugMode: false);
    }

    private void StartTrainingMode()
    {
        if (_GGPOComponent.IsRunning)
        {
            return;
        }

        CurrentGameType = GameType.Training;
        StartLocalGame(isDebugMode: true);

        _DebugPanel.SetActive(true);
        _HUDComponent.gameObject.SetActive(false);
    }

    private void ToggleSession()
    {
        var btnText = _StartStopSessionBtn.GetComponentInChildren<Text>();

        if (!_GGPOComponent.IsRunning)
        {
            CurrentGameType = GameType.Training;
            StartLocalGame(isDebugMode: true);
            btnText.text = "Stop Local Session";
        }
        else
        {
            StopLocalGame();
            btnText.text = "Start Local Session";
        }
    }

    private void StartLocalGame(bool isDebugMode)
    {
        SetEnableLocalSessionFeatures(true);
        _LobbyComponent.enabled = false;
        _GGPOComponent.StartLocalGame(isDebugMode);
        _MainMenuPanel.SetActive(false);
    }

    private void StopLocalGame()
    {
        SetEnableLocalSessionFeatures(false);
        _LobbyComponent.enabled = true;
        _GGPOComponent.Shutdown();
        _MainMenuPanel.SetActive(true);
        _DebugPanel.SetActive(false);
        _HUDComponent.gameObject.SetActive(false);
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
        switch (GameState)
        {
            case MatchState.PreBattle:
                Sequence preSequence = DOTween.Sequence();
                preSequence.Append(_HUDComponent.ShowScreen());
                preSequence.Append(_HUDComponent.Announce("Fight"));
                preSequence.Play().OnComplete(() => { GameState = MatchState.Battle; });
                break;

            case MatchState.Battle:
                // let the players fight
                // look for a winner
                // change the game to post battle
                if (!players.Any(x => x.State == PlayerState.KO))
                {
                    break;
                }

                var play = players.Where(x => x.State == PlayerState.KO).Single();
                play.Loses += 1;

                GameState = MatchState.PostBattle;

                break;

            case MatchState.PostBattle:
                // lock the players
                // allow the win and lose aniations to play
                // fade the screen out
                // change the game to pre battle

                Sequence postSequence = DOTween.Sequence();
                postSequence.Append(_HUDComponent.Announce("KO"));
                postSequence.Append(_HUDComponent.HideScreen());
                postSequence.Play().OnComplete(() => { GameState = MatchState.PreBattle; });
                break;
        }

        return GameState;
    }
}