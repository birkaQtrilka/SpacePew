using System.Collections;
using UnityEngine;

public class ActiveSlot : MonoBehaviour, IInvetoryInteractable
{
    [SerializeField] Player _player;

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
        StartCoroutine(Timer(item.EffectDuration, inventoryDrag)); 
    }

    IEnumerator Timer(float time, InventoryDrag item)
    {
        yield return new WaitForSeconds(time);
        item.CanDrag = true;
        item.PutBack();
    }
}
