using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class FadeOverlayComponent : MonoBehaviour
{
    private Image _FadeOverlay;

    private void Awake()
    {
        _FadeOverlay = GetComponent<Image>();
    }

    public Tweener HideScreen(float time = 0.2f)
    {
        return _FadeOverlay.DOFade(1, time);
    }

    public Tweener ShowScreen(float time = 0.2f)
    {
        return _FadeOverlay.DOFade(0, time);
    }
}