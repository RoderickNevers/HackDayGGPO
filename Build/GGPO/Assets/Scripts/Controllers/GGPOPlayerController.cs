using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private const int BASE_LAYER = -1;
    [SerializeField] private Animator m_Animator;

    private CharacterController m_CharacterController;
    private GGPOComponent m_GGPOComponent;

    void Start()
    {
        m_Animator.speed = 0.0f;
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
        m_Animator.Play(player.AnimationKey, BASE_LAYER, player.CurrentFrame);
    }

    // dont think we need this anymore

    //public void UpdatePlayerPosition(Player player)
    //{
    //    if (!m_GGPOComponent.manualFrameIncrement)
    //    {
    //        float SpeedModifier = SharedGame.GameManager.FRAME_LENGTH_SEC / m_GGPOComponent.currentFrameLength;
    //        m_CharacterController.Move(player.Velocity * SpeedModifier);
    //    }
    //}
}
