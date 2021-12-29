using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputDisplayComponent : MonoBehaviour
{
    [SerializeField] private GGPOGameManager GameManager;
    [SerializeField] private Text CurrentP1Input;
    [SerializeField] private List<TextMeshProUGUI> _PlayerDebugInfo;

    private GGPOGameState GameState;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Runner != null)
        {
            GameState = (GGPOGameState)GameManager.Runner.Game;
            text.text = GameManager.DisplayFrameInputs();
            CurrentP1Input.text = $"{GameState.ReadInputs(0)}";

            for (int i = 0; i < GameState.Players.Length; i++)
            {
                Player player = GameState.GetPlayer(i);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"ID: {player.ID}");
                sb.AppendLine($"Position: {player.Position}");
                sb.AppendLine($"Velocity: {player.Velocity}");
                sb.AppendLine($"IsGrounded: {player.IsGrounded}");
                sb.AppendLine($"IsJumping: {player.IsJumping}");
                sb.AppendLine($"IsAttacking: {player.IsAttacking}");
                sb.AppendLine($"IsHit: {player.IsHit}");
                sb.AppendLine($"IsStunned: {player.IsStunned}");
                sb.AppendLine($"IsTakingDamage: {player.IsTakingDamage}");
                sb.AppendLine($"IsCloseToOpponent: {player.IsCloseToOpponent}");
                sb.AppendLine($"IsBeingPushed: {player.IsBeingPushed}");
                sb.AppendLine($"Health: {player.Health}");
                sb.AppendLine($"HitStunTime: {player.HitStunTime}");
                sb.AppendLine($"BlockStunTime: {player.BlockStunTime}");
                sb.AppendLine($"Power: {player.Power}");
                sb.AppendLine($"CurrentPushbackTime: {player.CurrentPushbackTime}");
                sb.AppendLine($"State: {player.State}");
                sb.AppendLine($"JumpType: {player.JumpType}");
                sb.AppendLine($"CurrentButtonPressed: {player.CurrentButtonPressed}");
                sb.AppendLine($"CurrentAttackID: {player.CurrentAttackID}");
                sb.AppendLine($"CurrentlyHitByID: {player.CurrentlyHitByID}");
                sb.AppendLine($"LookDirection: {player.LookDirection}");
                sb.AppendLine($"AnimationKey: {player.AnimationKey}");
                sb.AppendLine($"CurrentFrame: {player.CurrentFrame}");
                sb.AppendLine($"AnimationIndex: {player.AnimationIndex}");
                sb.AppendLine($"Loses: {player.Loses}");
                _PlayerDebugInfo[i].text = sb.ToString();
            }
        }
    }
}
