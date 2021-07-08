using Mirror;
using UnityEngine;

public class GGPONetworkManager : MonoBehaviour
{
    [SerializeField] private GGPOComponent _GGPOComponent;
    [SerializeField] private ReplayManager _ReplayManager;
    public GameObject playerPrefab;
    public Transform _P1Spawn;
    public Transform _P2Spawn;

    public void Start()
    {
        _GGPOComponent.OnRunningChanged += OnRunningChanged;
    }

    public void OnDestroy()
    {
        _GGPOComponent.OnRunningChanged -= OnRunningChanged;
    }

    private void OnRunningChanged(bool running)
    {
        if (running)
        {
            OnConnectionStart();
        }
    }

    public void OnConnectionStart()
    {
        for (int i = 0; i < 2; i++)
        {
            // add player at correct spawn position
            Transform start = i == 0 ? _P1Spawn : _P2Spawn;
            GameObject player = Object.Instantiate(playerPrefab, start.position, start.rotation);

            GGPOGameState gameState = (GGPOGameState)_GGPOComponent.Runner.Game;
            gameState.InitPlayer(i, start.position);

            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.ReplayManager = _ReplayManager;
            playerController.GameState = gameState;
            playerController.Setup(i);
        }
    }
}
