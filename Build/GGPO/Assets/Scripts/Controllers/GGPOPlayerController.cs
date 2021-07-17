using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private CharacterController _CharacterController;

    private void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();
    }

    public void UpdatePlayerPosition(Player player)
    {
        _CharacterController.Move(player.velocity * Time.fixedDeltaTime);
    }
}
