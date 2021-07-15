using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadedText : MonoBehaviour, IFade
{
    [SerializeField] private Text text;
    
    public void FadeIn(float time)
    {
        text.DOFade(0, time);
    }

    public void FadeOut(float endValue, float time)
    {
        text.DOFade(endValue, time);
    }
}
