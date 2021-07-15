using UnityEngine;
using UnityEngine.Events;

public static class ClickListener
{
    public static UnityAction<GameObject, Entity> OnClick;
    
    public static void ClickOn(GameObject go, Entity entity)
    {
        OnClick?.Invoke(go, entity);
    }
}
