using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private GGPOComponent m_GGPOComponent;
    private Animator m_Animator;
    private float m_Index = 0;
    private float m_CurrentFrame;
    private float m_TotalFrames;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.speed = 0.0f;
        AnimatorClipInfo[] clipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        m_TotalFrames = (int)(clipInfo[0].clip.length * clipInfo[0].clip.frameRate);
    }

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    public void Init(GGPOComponent ggpoComponent)
    {
        m_GGPOComponent = ggpoComponent;
    }

    // Called from GameController.cs. Is called anytime the GameManager's Update calls the Runner's update (ie advances the frame)
    // Won't be called on rollbacks (I think)
    public void OnStateChanged(Player player)
    {
        // move player
        transform.position = player.Position;

        // set the animator to the correct frame
        m_CurrentFrame = m_Index / m_TotalFrames;

        m_Animator.Play("Idle", -1, m_CurrentFrame);

        if (m_Index >= m_TotalFrames)
            m_Index = 1;
    }

    public void UpdatePlayerPosition(Player player)
    {
        //Debug.Log(player.Position);
        //transform.position = player.Position;
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

        if (!m_GGPOComponent.manualFrameIncrement)
        {
            float SpeedModifier = SharedGame.GameManager.FRAME_LENGTH_SEC / m_GGPOComponent.currentFrameLength;
            m_CharacterController.Move(player.Velocity * SpeedModifier);
        }
    }
}
