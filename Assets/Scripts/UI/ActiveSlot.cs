using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActiveSlot : MonoBehaviour, IInvetoryInteractable
{
    [SerializeField] Player _player;
    [SerializeField] UnityEvent _onInteract;
    [SerializeField] UnityEvent _onStopInteract;
    public bool CanInteract(InventoryDrag inventoryDrag)
    {
        return true;
    }

    public void Interact(InventoryDrag inventoryDrag)
    {
        inventoryDrag.transform.position = transform.position;
        var item = inventoryDrag.GetComponent<InventorySpeedIncrease>();
        item.Use(_player);
        inventoryDrag.CanDrag = false;
        _onInteract.Invoke();
        StartCoroutine(Timer(item.EffectDuration, inventoryDrag)); 
    }

    IEnumerator Timer(float time, InventoryDrag item)
    {
        yield return new WaitForSeconds(time);
        item.CanDrag = true;
        item.PutBack();
        _onStopInteract.Invoke();
    }
}
