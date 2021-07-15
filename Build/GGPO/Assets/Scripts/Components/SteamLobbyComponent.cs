using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks.Data;
using System.Threading.Tasks;
using System;

public class SteamLobbyComponent : MonoBehaviour
{
    [SerializeField] private Button m_JoinBtn;
    [SerializeField] private Button m_CreateBtn;
    [SerializeField] private Button m_InviteBtn;

    [SerializeField] private Button m_LeaveBtn;

    [SerializeField] private Button m_ListLobbiesBtn;
    [SerializeField] private Button m_ListLobbyMembersBtn;
    [SerializeField] private TMP_Text m_LobbyData;
    [SerializeField] private TMP_Text m_Lobbies;

    private Lobby? m_CurrentLobby;
    private int m_MaxLobbyMembers = 4;

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            Debug.Log("Steam API init -- SUCCESS!");
            AddListeners();
        }
        else
        {
            Debug.Log("Steam API init -- failure ...");
        }
    }

    private void AddListeners()
    {
        SteamMatchmaking.OnLobbyCreated += OnLobbyCreated;
        SteamMatchmaking.OnLobbyInvite += OnLobbyInvite;
        SteamMatchmaking.OnLobbyMemberJoined += OnLobbyMemberJoined;
        SteamFriends.OnGameLobbyJoinRequested += OnGameLobbyJoinRequested;

        m_CreateBtn.onClick.AddListener(CreateLobby);
        m_LeaveBtn.onClick.AddListener(LeaveLobby);
        //m_ListLobbiesBtn.onClick.AddListener(ListLobbies);
        //m_ListLobbyMembersBtn.onClick.AddListener(ListLobbyMembers);
    }

    /// <summary>
    /// You created a lobby
    /// </summary>
    /// <param name="result"></param>
    /// <param name="lobby"></param>
    private void OnLobbyCreated(Result result, Lobby lobby)
    {
        if (result == Result.OK)
            Debug.Log("Lobby created -- SUCCESS!");
        else
            Debug.Log("Lobby created -- failure ...");

        m_CurrentLobby = lobby;

        Debug.Log($"Lobby Members: {lobby.MemberCount}\n");
        foreach (Friend member in lobby.Members)
        {
            Debug.Log($"Member Name: {member.Name}\n");
        }
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
    }

    /// <summary>
    /// Called when the user tries to join a lobby from their friends list game client should attempt to connect to specified lobby when this is received
    /// </summary>
    /// <param name="lobby"></param>
    /// <param name="steamID"></param>
    private void OnGameLobbyJoinRequested(Lobby lobby, SteamId steamID)
    {
        Debug.Log($"Barging, uninvited into {lobby.Owner.Name}'s Lobby.\n");
        JoinLobby(steamID);
    }

    private async void CreateLobby()
    {
        Debug.Log("Trying to create lobby ...");
        try
        {
            m_CurrentLobby = await CreateLobbyAsync();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error Creating Lobby {e.Message}");
        }
    }

    public async Task<Lobby?> CreateLobbyAsync()
    {
        return await SteamMatchmaking.CreateLobbyAsync(m_MaxLobbyMembers);
    }
    
    private async void JoinLobby(SteamId lobbyToJoin)
    {
        Debug.Log("Trying to join lobby ...");
        try
        {
            await JoinLobbyAsync(lobbyToJoin);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error Creating Lobby {e.Message}");
        }
    }

    public async Task<Lobby?> JoinLobbyAsync(SteamId lobbyToJoin)
    {
        Lobby? lobby = await SteamMatchmaking.JoinLobbyAsync(lobbyToJoin);
        return lobby;
    }

    private void LeaveLobby()
    {
        Debug.Log($"Leave lobby {m_CurrentLobby?.Id}...");
        m_CurrentLobby?.Leave();
    }

    private void InviteFriendToLobby(SteamId friend)
    {
        m_CurrentLobby?.InviteFriend(friend);
    }    

    //private void ListLobbies()
    //{
    //    Debug.Log("Trying to get list of available lobbies ...");
    //    SteamAPICall_t try_getList = SteamMatchmaking.RequestLobbyList();

    //    displaylobby();
    //}

    //private void ListLobbyMembers()
    //{
    //    int numPlayers = SteamMatchmaking.GetNumLobbyMembers((CSteamID)current_lobbyID);
    //    string header = $"\t Number of players currently in lobby: {numPlayers})";
    //    Debug.Log(header);
    //    m_LobbyData.text += header;

    //    for (int i = 0; i < numPlayers; i++)
    //    {
    //        string message = $"\t Player({i}) == {SteamFriends.GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)current_lobbyID, i))}\n";
    //        Debug.Log(message);
    //        m_LobbyData.text += message;
    //    }
    //}



    //private void OnLobbyCreated(LobbyCreated_t result)
    //{
    //    if (result.m_eResult == EResult.k_EResultOK)
    //        Debug.Log("Lobby created -- SUCCESS!");
    //    else
    //        Debug.Log("Lobby created -- failure ...");

    //    string personalName = SteamFriends.GetPersonaName();
    //    SteamMatchmaking.SetLobbyData((CSteamID)result.m_ulSteamIDLobby, "name", personalName + "'s game");
    //    m_CurrentLobbyID = (CSteamID)result.m_ulSteamIDLobby;
    //}

    //private void OnGetLobbiesList(LobbyMatchList_t result)
    //{
    //    Debug.Log("Found " + result.m_nLobbiesMatching + " lobbies!");
    //    for (int i = 0; i < result.m_nLobbiesMatching; i++)
    //    {
    //        CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
    //        lobbyIDS.Add(lobbyID);
    //        SteamMatchmaking.RequestLobbyData(lobbyID);
    //    }
    //}

    //private void OnGetLobbyInfo(LobbyDataUpdate_t result)
    //{
    //    for (int i = 0; i < lobbyIDS.Count; i++)
    //    {
    //        if (lobbyIDS[i].m_SteamID == result.m_ulSteamIDLobby)
    //        {
    //            Debug.Log("Lobby " + i + " :: " + SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name"));
    //            return;
    //        }
    //    }
    //}

    //private void OnLobbyEntered(LobbyEnter_t result)
    //{
    //    current_lobbyID = result.m_ulSteamIDLobby;

    //    if (result.m_EChatRoomEnterResponse == 1)
    //        Debug.Log("Lobby joined!");
    //    else
    //        Debug.Log("Failed to join lobby.");
    //}

    //void displaylobby()
    //{
    //    Lobby lobby = new Lobby();
    //    lobby.m_SteamID = m_CurrentLobbyID; // ID, which was passed to the method
    //    lobby.m_Owner = SteamMatchmaking.GetLobbyOwner(m_CurrentLobbyID);
    //    lobby.m_Members = new LobbyMembers[SteamMatchmaking.GetNumLobbyMembers(m_CurrentLobbyID)];
    //    lobby.m_MemberLimit = SteamMatchmaking.GetLobbyMemberLimit(m_CurrentLobbyID);

    //    int DataCount = SteamMatchmaking.GetLobbyDataCount(m_CurrentLobbyID);

    //    lobby.m_Data = new LobbyMetaData[DataCount];
    //    for (int i = 0; i < DataCount; i++) // Getting all the lobby metadata
    //    {
    //        bool lobbyDataRet = SteamMatchmaking.GetLobbyDataByIndex(
    //            m_CurrentLobbyID,
    //            iLobbyData: i,
    //            out lobby.m_Data[i].m_Key,
    //            Constants.k_nMaxLobbyKeyLength,
    //            out lobby.m_Data[i].m_Value,
    //            Constants.k_cubChatMetadataMax);

    //        if (!lobbyDataRet)
    //        {
    //            Debug.LogError("Error retrieving lobby metadata");
    //            continue;
    //        }
    //    }
    //    //
    //    // Displaying the lobby in the list...
    //    //
    //}

    //void lol()
    //{
    //    SteamGameServer.GetPublicIP();
    //    SteamNetworkingSockets.ConnectP2P();
    //    SteamUser.BIsBehindNAT();
    //}
}