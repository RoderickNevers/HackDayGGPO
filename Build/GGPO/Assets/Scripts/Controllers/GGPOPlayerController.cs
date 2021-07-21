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
        //Debug.Log(player.Position);
        transform.position = player.Position;
        //transform.position = Vector3.Slerp(transform.position, player.Position, 1f);

        //transform.position = player.Position;
        //if (m_GGPOComponent.manualFrameIncrement)
        //{
        //    transform.position = player.Position;
        //}
        //else
        //{
        //    float SpeedModifier = SharedGame.GameManager.FRAME_LENGTH_SEC / m_GGPOComponent.currentFrameLength;
        //    transform.position = player.Position;
        //    m_CharacterController.Move(player.Velocity * SpeedModifier);
        //}
    }
}
