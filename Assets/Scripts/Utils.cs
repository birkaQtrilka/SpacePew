using UnityEngine;

public static class Utils 
{
    public static GameObject GetRootParent(this Transform obj)
    {
        if (obj.parent != null)
            return obj.parent.GetRootParent();
        return obj.gameObject;
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
        return (mask & (1 << layer)) != 0;
    }
}
