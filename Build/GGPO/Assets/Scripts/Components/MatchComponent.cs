using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchComponent : MonoBehaviour
{
    private static MatchComponent _instance;

    public static MatchComponent Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MatchComponent>();
            }
            return _instance;
        }
    }

    public List<GGPOPlayerController> Players = new List<GGPOPlayerController>();

    public LookDirection CheckLookDirection(Player player)
    {
        if (player.ID == PlayerID.Player1)
        {
            return Players[(int)PlayerID.Player1].transform.position.x < Players[(int)PlayerID.Player2].transform.position.x ? LookDirection.Right : LookDirection.Left;
        }
        else
        {
            return Players[(int)PlayerID.Player2].transform.position.x < Players[(int)PlayerID.Player1].transform.position.x ? LookDirection.Right : LookDirection.Left;
        }
    }
}