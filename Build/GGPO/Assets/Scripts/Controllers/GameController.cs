using SharedGame;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GGPOComponent _GGPOComponent;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform _P1Spawn;
    [SerializeField] private Transform _P2Spawn;

    private GGPOPlayerController[] PlayerControllers = Array.Empty<GGPOPlayerController>();

    public void Awake()
    {
        _GGPOComponent.OnRunningChanged += OnRunningChanged;
        _GGPOComponent.OnStateChanged += OnStateChanged;
    }

    public void OnDestroy()
    {
        _GGPOComponent.OnRunningChanged -= OnRunningChanged;
        _GGPOComponent.OnStateChanged -= OnStateChanged;
    }

    private void OnRunningChanged(bool running)
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

    private void OnStateChanged()
    {
        Debug.Log("A");

        var gameState = (GGPOGameState)_GGPOComponent.Runner.Game;

        for (int i = 0; i < PlayerControllers.Length; ++i)
        {
            Player player = gameState.GetPlayer(i);
            PlayerControllers[i].OnStateChanged(ref player);
        }
    }
}