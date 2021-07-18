using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLobbyComponent : MonoBehaviour
{
    public EventHandler OnReady;

    [SerializeField] private TMP_Text _PlayerName;
    [SerializeField] private Button _Ready;
    [SerializeField] private GameObject _HostIcon;

    public bool IsReady { get; private set; }

    public void Init(string name, bool isHost)
    {
        _PlayerName.text = name;
        _HostIcon.SetActive(isHost);
    }

    private void OnEnable()
    {
        _Ready.onClick.AddListener(Ready);
    }

    private void OnDisable()
    {
        _Ready.onClick.RemoveAllListeners();
    }


    private void Ready()
    {
        OnReady?.Invoke(this, new EventArgs());
    }
}