using Mirror;
using UnityEngine;

public class GGPONetworkManager : NetworkManager
{
    [SerializeField] private ReplayManager _ReplayManager;
    public Transform _P1Spawn;
    public Transform _P2Spawn;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? _P1Spawn : _P2Spawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);

        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.ReplayManager = _ReplayManager;
        playerController.Setup();

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
