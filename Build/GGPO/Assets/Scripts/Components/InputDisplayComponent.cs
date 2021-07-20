using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDisplayComponent : MonoBehaviour
{

    [SerializeField] private GGPOComponent GameManager;
    [SerializeField] private Text CurrentP1Input;
    [SerializeField] private Text P1Text;
    [SerializeField] private Text P2Text;

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

            Player player1 = GameState.GetPlayer(0);
            string p1string = string.Format("Position: {0}, {1}, {2}", player1.Position.x, player1.Position.y, player1.Position.z);
            P1Text.text = p1string;

            Player player2 = GameState.GetPlayer(1);
            string p2string = string.Format("Position: {0}, {1}, {2}", player2.Position.x, player2.Position.y, player2.Position.z);
            P2Text.text = p2string;
        }
    }
}
