using DG.Tweening;
using UnityEngine;


public class EntityObject : MonoBehaviour, IBounce
{
    public void Bounce(float time)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(Vector2.zero, time).SetEase(Ease.InBounce));
        mySequence.Append(transform.DOScale(Vector2.one, 0).SetEase(Ease.InBounce));
        
    }

    public void EaseInBounce(float time)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(Vector2.zero, time/2).SetEase(Ease.InBounce));
        mySequence.Append(transform.DOScale(Vector2.one, time/2).SetEase(Ease.InBounce));
    }
}