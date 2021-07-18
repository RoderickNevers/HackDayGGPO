using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private CharacterController _CharacterController;
    private GGPOComponent _GGPOComponent;

    private void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();

        _GGPOComponent = FindObjectOfType<GGPOComponent>();
    }

    public void UpdatePlayerPosition(Player player)
    {
        if (_GGPOComponent.manualFrameIncrement)
        {
            transform.position = player.position;
        }
        else
        {
            float SpeedModifier = SharedGame.GameManager.FRAME_LENGTH_SEC / _GGPOComponent.currentFrameLength;
            _CharacterController.Move(player.velocity * SpeedModifier);
        }
    }
}
