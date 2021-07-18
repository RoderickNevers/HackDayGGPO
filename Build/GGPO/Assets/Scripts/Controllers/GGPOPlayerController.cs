using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private GGPOComponent m_GGPOComponent;


    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    public void Init(GGPOComponent ggpoComponent)
    {
        m_GGPOComponent = ggpoComponent;
    }

    public void UpdatePlayerPosition(Player player)
    {
        if (m_GGPOComponent.manualFrameIncrement)
        {
            transform.position = player.position;
        }
        else
        {
            float SpeedModifier = SharedGame.GameManager.FRAME_LENGTH_SEC / m_GGPOComponent.currentFrameLength;
            transform.position = player.position;
            m_CharacterController.Move(player.velocity * SpeedModifier);
        }
    }
}
