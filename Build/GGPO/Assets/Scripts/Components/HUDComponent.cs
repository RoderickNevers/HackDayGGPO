using DG.Tweening;
using TMPro;
using UnityEngine;


public class HUDComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Announcer;
    [SerializeField] private TextMeshProUGUI _P1LifeDisplay;
    [SerializeField] private TextMeshProUGUI _P2LifeDisplay;

    public void Setup(int p1Lives, int p2Lives)
    {
        _P1LifeDisplay.text = $"Lives: {p1Lives}";
        _P2LifeDisplay.text = $"Lives: {p2Lives}";
    }

    public Sequence Announce(string announcement)
    {
        float time = GameController.Instance.CurrentGameType == GameType.Versus ? 0.5f : 0.1f;
        float delay = GameController.Instance.CurrentGameType == GameType.Versus ? 1f : 0f;

        _Announcer.text = announcement;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_Announcer.transform.DOScale(Vector2.zero, 0));
        sequence.Append(_Announcer.transform.DOScale(Vector2.one, time)).SetDelay(delay);
        sequence.Append(_Announcer.transform.DOScale(Vector2.zero, time));

        return sequence;
    }
}