using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadedImage : MonoBehaviour, IFade
{
    [SerializeField] private Image image;

    public void FadeIn(float time)
    {
        image.DOFade(0, time);
    }

    public void FadeOut(float endValue, float time)
    {
        image.DOFade(endValue, time);
    }
}
