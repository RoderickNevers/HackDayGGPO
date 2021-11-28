using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDComponent : MonoBehaviour
{
    [SerializeField] private Image _FadeOverlay;
    [SerializeField] private TextMeshProUGUI _Announcer;
    [SerializeField] private TextMeshProUGUI _P1LifeDisplay;
    [SerializeField] private TextMeshProUGUI _P2LifeDisplay;

    public void Setup(int p1Lives, int p2Lives)
    {
        _P1LifeDisplay.text = $"Lives: {p1Lives}";
        _P2LifeDisplay.text = $"Lives: {p2Lives}";
    }

    public Tweener HideScreen()
    {
        return _FadeOverlay.DOFade(1, 0.2f);
    }

    public Tweener ShowScreen()
    {
        return _FadeOverlay.DOFade(0, 0.2f);
    }

    public Sequence Announce(string announcement)
    {
        _Announcer.text = announcement;
        _Announcer.transform.localScale = Vector2.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_Announcer.transform.DOScale(Vector2.one, 0.2f));
        sequence.Append(_Announcer.transform.DOPunchScale(new Vector2(2,2), 0.2f));
        sequence.Append(_Announcer.transform.DOScale(Vector2.zero, 0.2f));
        return sequence;
    }
}