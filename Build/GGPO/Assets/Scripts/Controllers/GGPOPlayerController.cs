using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    public void UpdatePlayerPosition(Player player)
    {
        transform.position = player.position;
    }
}
