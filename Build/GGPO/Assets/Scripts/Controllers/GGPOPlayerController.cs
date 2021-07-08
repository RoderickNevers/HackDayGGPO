using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private bool _IsReplaying;
    public ReplayManager ReplayManager { get; set; }

    public void Setup()
    {
        ReplayManager.OnStartedReplaying += () => { _IsReplaying = true; };
        ReplayManager.OnStoppedReplaying += () => { _IsReplaying = false; };
    }

    public void UpdatePlayerPosition(Player player)
    {
        transform.position = player.position;
    }
}
