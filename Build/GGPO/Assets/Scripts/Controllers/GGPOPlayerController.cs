using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private const int BASE_LAYER = -1;
    [SerializeField] private Animator m_Animator;

    public PlayerID ID {get;set;}

    void Start()
    {
        m_Animator.speed = 0.0f;
    }

    // Called from GameController.cs. Is called anytime the GameManager's Update calls the Runner's update (ie advances the frame)
    // Won't be called on rollbacks (I think)
    public void OnStateChanged(Player player)
    {
        // move player
        transform.position = player.Position;

        transform.localScale = player.LookDirection == LookDirection.Left ? new Vector3(-1, 1, 1) : transform.localScale = new Vector3(1, 1, 1);

        // set the animator to the correct frame
        m_Animator.Play(player.AnimationKey, BASE_LAYER, player.CurrentFrame);
    }
}