using System;
using System.Linq;
using UnityEngine;

public class GGPOPlayerController : MonoBehaviour
{
    private const int BASE_LAYER = -1;
    [Header("Animator")]
    [SerializeField] private Animator m_Animator;

    [Header("Collision Detection")]
    [SerializeField] private HitBoxComponent m_HitBox;
    [SerializeField] private Transform m_HurtBox;
    [SerializeField] private Transform m_Body;

    public PlayerID ID { get; set; }

    void Start()
    {
        m_Animator.speed = 0.0f;
    }

    /// <summary>
    /// Is the players' body touching the other player
    /// </summary>
    /// <returns>True if the players' body is touching the other player.</returns>
    public bool OnBodyCollision()
    {
        Collider[] bodyColliders = Physics.OverlapBox(m_Body.position, m_Body.localScale / 2, Quaternion.identity, 1 << LayerMask.NameToLayer("PlayerBody")); 
        return bodyColliders.Any(x => x.transform.root != this.transform);
    }

    /// <summary>
    /// Is the player being hit by an attack?
    /// </summary>
    /// <returns>True if the player is hit by an attack.</returns>
    public HitData OnCheckCollision()
    {
        HitData result = new HitData();

        Collider[] hitColliders = Physics.OverlapBox(m_HurtBox.position, m_HurtBox.localScale / 2, Quaternion.identity, 1 << LayerMask.NameToLayer("Hitbox"));
        foreach (Collider hitbox in hitColliders)
        {
            if (hitbox.transform.root != this.transform)
            {
                result.AttackData = hitbox.GetComponent<HitBoxComponent>().AttackData;
                result.IsHit = true;
            }
        }

        return result;
    }

    // Called from GameController.cs. Is called anytime the GameManager's Update calls the Runner's update (ie advances the frame)
    // Won't be called on rollbacks (I think)
    public void OnStateChanged(ref Player player)
    {
        // move player
        transform.position = player.Position;
        transform.localScale = player.LookDirection == LookDirection.Left ? new Vector3(-1, 1, 1) : transform.localScale = new Vector3(1, 1, 1);
        
        // set the animator to the correct frame
        m_Animator.Play(Animator.StringToHash(player.AnimationKey), BASE_LAYER, player.CurrentFrame);

        if (player.CurrentAttackID == Guid.Empty)
            return;

        m_HitBox.AttackData = AnimationData.AttackLookup[player.CurrentAttackID];
    }
}