using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IBounce
{
    [SerializeField] private Image imageCell;
    [SerializeField] private GameObject entityObject;
    
    private Entity entity;
    public Entity Entity
    {
        get => entity;
        set
        {
            entity = value;
            imageCell.sprite = value.Sprite;
        }
    }

    public void Click()
    {
        ClickListener.ClickOn(entityObject, Entity);
    }

    public void Bounce(float time)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector2.one, time).SetEase(Ease.InBounce);
    }
}
