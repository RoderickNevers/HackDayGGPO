using System.Threading.Tasks;
using System;

using Steamworks;
using Steamworks.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyComponent : MonoBehaviour
{
    private readonly int MaxLobbyMembers = 4;

    // MAIN MENU STUFF TO BE RELOCATED TO THE GAME CONTROLLER
    [Header("Main Menu Content")]
    [SerializeField] private GameObject m_MainMenuPanel;

    [Header("Lobby Content")]
    [SerializeField] private GameObject m_LobbyPanel;
    [SerializeField] private Transform m_PlayerLobbyObjectContainer;
    [SerializeField] private PlayerLobbyComponent m_PlayerLobbyComponent;
    [SerializeField] private Button m_LeaveBtn;
    [SerializeField] private Button m_StartSessionBtn;
    [SerializeField] private Button m_EndSessionBtn;

    [Header("Gameplay Debug Panel")]
    [SerializeField] private GameObject m_DebugPanel;
    [SerializeField] private TextMeshProUGUI m_SteamDebug;

    private SteamManager m_SteamManager;
    private Lobby? m_CurrentLobby;
    private LobbyQuery m_LobbyList;

    private void Start()
    {
        try
        {
            if (SteamManager.Initialized)
            {
                m_SteamDebug.text = "Steam API init -- SUCCESS!";
                Debug.Log("Steam API init -- SUCCESS!");
                m_SteamManager = GetComponent<SteamManager>();
                AddListeners();
            }
            else
            {
                m_SteamDebug.text = "Steam isn't running. Going to debug mode";
                Debug.Log($"Steam isn't running. Going to debug mode");
            }
        }
        catch (Exception e)
        {
            m_SteamDebug.text = $"It's ALLLLLLLLLLLLL burning {e.Message}";
            Debug.LogError($"It's ALLLLLLLLLLLLL burning {e.Message}");
        }

        ShowMainMenu();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        SteamMatchmaking.OnLobbyCreated += OnLobbyCreated;
        SteamMatchmaking.OnLobbyInvite += OnLobbyInvite;
        SteamMatchmaking.OnLobbyEntered += OnLobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined += OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave += OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyMemberDisconnected += OnLobbyMemberDisconnected;
        SteamMatchmaking.OnLobbyMemberDataChanged += OnLobbyMemberDataChanged;
        SteamMatchmaking.OnChatMessage += OnChatMessage;

        SteamFriends.OnGameLobbyJoinRequested += OnGameLobbyJoinRequested;

        // Match starting
        SteamMatchmaking.OnLobbyGameCreated += OnLobbyGameCreated;

        m_LeaveBtn.onClick.AddListener(LeaveLobby);
        m_StartSessionBtn.onClick.AddListener(StartSession);
        m_EndSessionBtn.onClick.AddListener(EndSession);
    }

    private void RemoveListeners()
    {
        SteamMatchmaking.OnLobbyCreated -= OnLobbyCreated;
        SteamMatchmaking.OnLobbyInvite -= OnLobbyInvite;
        SteamMatchmaking.OnLobbyEntered -= OnLobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined -= OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave -= OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyMemberDisconnected -= OnLobbyMemberDisconnected;
        SteamMatchmaking.OnLobbyMemberDataChanged -= OnLobbyMemberDataChanged;
        SteamMatchmaking.OnChatMessage -= OnChatMessage;

        SteamMatchmaking.OnLobbyGameCreated -= OnLobbyGameCreated;

        m_LeaveBtn.onClick.RemoveAllListeners();
        m_StartSessionBtn.onClick.RemoveAllListeners();
        m_EndSessionBtn.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// You created a lobby
    /// </summary>
    /// <param name="result"></param>
    /// <param name="lobby"></param>
    private void OnLobbyCreated(Result result, Lobby lobby)
    {
        if (result == Result.OK)
        {
             Debug.Log($"SUCCESS! Lobby created id:{lobby.Id}");
            ShowLobby();
        }
        else
            Debug.Log("Lobby created -- failure ...");

        m_CurrentLobby = lobby;
    }

    /// <summary>
    /// Someone invited you to a lobby
    /// </summary>
    /// <param name="friend"></param>
    /// <param name="lobby"></param>
    private void OnLobbyInvite(Friend friend, Lobby lobby)
    {
        Debug.Log($"{friend.Name} invited me to lobby: {lobby.Id}\n");
    }

    /// <summary>
    /// The lobby member joined
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="friend"></param>
    private void OnLobbyMemberJoined(Lobby lobby, Friend friend)
    {
        Debug.Log($"Friend {friend.Name} joined the lobby\n");
        DisplayLobbyMembers(lobby);
    }

    /// <summary>
    /// Called when you join a lobby
    /// </summary>
    /// <param name="lobby"></param>
    private void OnLobbyEntered(Lobby lobby)
    {
        ShowLobby();
        DisplayLobbyMembers(lobby);
    }

    /// <summary>
    /// Called when the user tries to join a lobby from their friends list game client should attempt to connect to specified lobby when this is received
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="steamID"></param>
    private void OnGameLobbyJoinRequested(Lobby lobby, SteamId steamID)
    {
        Debug.Log($"Barging, uninvited into {lobby.Owner.Name}'s Lobby.\n");
        m_CurrentLobby = lobby;
        lobby.Join();
    }

    /// <summary>
    /// Is called when the session starts
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="steamId"></param>
    private void OnLobbyGameCreated(Lobby lobby, uint ip, ushort port, SteamId steamId)
    {
        Debug.Log($"OnLobbyGameCreated for {lobby.Owner.Name}'s Lobby. {steamId.AccountId} is the host!\n");

        if (lobby.Owner.Id == SteamClient.SteamId)
        {
            // Start game?
            Debug.Log($"OnLobbyGameCreated - I am the host!!\n");
        }
        else
        {
            m_SteamManager.StartSteamworksConnection(false, steamId);
        }

        GameController.Instance.CurrentGameType = GameType.Versus;
        ShowGame();
    }

    /// <summary>
    /// The lobby member left the room
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="friend"></param>
    private void OnLobbyMemberDisconnected(Lobby lobby, Friend friend)
    {
        Debug.Log($"{friend.Name} disconnected from lobby {lobby.Id}");
    }

    /// <summary>
    /// The lobby member metadata has changed
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="friend"></param>
    private void OnLobbyMemberDataChanged(Lobby lobby, Friend friend)
    {
        Debug.Log($"{friend.Name} data changed in {lobby.Id}");
    }

    /// <summary>
    /// The lobby member left the room
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="friend"></param>
    private void OnLobbyMemberLeave(Lobby lobby, Friend friend)
    {
        Debug.Log($"{friend.Name} left lobby {lobby.Id}");
        DisplayLobbyMembers(lobby);
    }

    /// <summary>
    /// The lobby member left the room
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="friend"></param>
    /// <param name="message"></param>
    private void OnChatMessage(Lobby lobby, Friend friend, string message)
    {
        Debug.Log($"{friend.Name} sent message {message}  to\nlobby:{lobby.Id}");
    }

    public async void CreateLobby()
    {
        Debug.Log("Trying to create lobby ...");
        try
        {
            m_CurrentLobby = await CreateLobbyAsync();
            m_CurrentLobby?.SetJoinable(true);
            m_CurrentLobby?.SetPublic();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error Creating Lobby {e.Message}");
        }
    }

    public async Task<Lobby?> CreateLobbyAsync()
    {
        return await SteamMatchmaking.CreateLobbyAsync(MaxLobbyMembers);
    }

    public void ShowLobby()
    {
        m_MainMenuPanel.SetActive(false);
        m_LobbyPanel.SetActive(true);
        m_DebugPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        m_MainMenuPanel.SetActive(true);
        m_LobbyPanel.SetActive(false);
        m_DebugPanel.SetActive(false);
    }

    public void ShowGame()
    {
        m_MainMenuPanel.SetActive(false);
        m_LobbyPanel.SetActive(false);
        m_DebugPanel.SetActive(false);
    }

    private void LeaveLobby()
    {
        Debug.Log($"Leave lobby {m_CurrentLobby?.Id}...");
        m_CurrentLobby?.Leave();
        ShowMainMenu();
    }

    private void InviteFriendToLobby(SteamId friend)
    {
        m_CurrentLobby?.InviteFriend(friend);
    }

    private void StartSession()
    {
        if (m_CurrentLobby.HasValue && m_CurrentLobby.Value.IsOwnedBy(SteamClient.SteamId))
        {
            // Take the role of host
            m_SteamManager.StartSteamworksConnection(true, SteamClient.SteamId);

            // Alert all in lobby
            m_CurrentLobby?.SetGameServer(SteamClient.SteamId);
        }
    }

    private void EndSession()
    {
        m_SteamManager.CloseSteamworksConnection();
    }

    private void DisplayLobbyMembers(Lobby lobby)
    {
        Debug.Log($"Lobby Members: {lobby.MemberCount}\n");

        // Clear children
        for (int i = 0; i < m_PlayerLobbyObjectContainer.transform.childCount; i++)
        {
            GameObject child = m_PlayerLobbyObjectContainer.transform.GetChild(i).gameObject;
            GameObject.Destroy(child);
        }

        // Add new objects to ui
        foreach (Friend member in lobby.Members)
        {
            Debug.Log($"Member Name: {member.Name}\n");
            string name = member.Name;
            bool isHost = lobby.IsOwnedBy(member.Id);

            if (SteamClient.SteamId == member.Id)
            {
                m_StartSessionBtn.gameObject.SetActive(isHost);
                m_EndSessionBtn.gameObject.SetActive(isHost);
            }

            PlayerLobbyComponent playerObject = Instantiate<PlayerLobbyComponent>(m_PlayerLobbyComponent, m_PlayerLobbyObjectContainer);
            playerObject.Init(name, isHost);
        }
    }

    public async void ListCloseLobbies()
    {
        Debug.Log("Trying to get list of available lobbies ...");
        Lobby[] lobbies = await m_LobbyList.FilterDistanceClose().RequestAsync();
        Debug.Log($"Total Lobbies found: {lobbies.Length}");

        foreach (Lobby lobby in lobbies)
        {
            Debug.Log(lobby.Id);
        }
    }

    private async Task<Lobby?> FindFriendsLobby(SteamId friendID)
    {
        Debug.Log("Trying to find friends lobby...");
        Lobby[] lobbies = await m_LobbyList.RequestAsync();

        foreach (Lobby lobby in lobbies)
        {
            if (lobby.IsOwnedBy(friendID))
            {
                return lobby;
            }
        }

        Debug.Log("Lobby was not found");
        return null;
    }
}