using System;
using UnityEngine;

//[Serializable] public class ItemDestroyEvent : UnityEvent<GameObject> { }

public class Barrier : MonoBehaviour
{
    public event Action<GameObject> ItemDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        var obj = other.transform.GetRootParent();
        ItemDestroy?.Invoke(obj);
        Destroy(obj);   
    }
    private void OnDestroy()
    {
        ItemDestroy = null;
    }
}
