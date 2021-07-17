using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLobbyComponent : MonoBehaviour
{
    public EventHandler OnStartSession;
    public EventHandler OnReady;

    [SerializeField] private TMP_Text _PlayerName;
    [SerializeField] private Button _StartSession;
    [SerializeField] private Button _Ready;
    [SerializeField] private GameObject _HostIcon;

    public bool IsReady { get; private set; }

    public void Init(string name, bool isHost)
    {
        _PlayerName.text = name;
        _HostIcon.SetActive(isHost);
        _StartSession.gameObject.SetActive(isHost);
    }

    private void OnEnable()
    {
        _StartSession.onClick.AddListener(StartSession);
        _Ready.onClick.AddListener(Ready);
    }

    private void OnDisable()
    {
        _StartSession.onClick.RemoveAllListeners();
        _Ready.onClick.RemoveAllListeners();
    }

    private void StartSession()
    {
        OnStartSession?.Invoke(this, new EventArgs());
    }

    private void Ready()
    {
        OnReady?.Invoke(this, new EventArgs());
    }
}