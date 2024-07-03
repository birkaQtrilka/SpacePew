using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public interface IInvetoryInteractable
{
    bool CanInteract(InventoryDrag inventoryDrag);
    void Interact(InventoryDrag inventoryDrag);
}

public class InventoryDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool CanDrag { get; set; } = true;
    Vector2 _origPosition;
    readonly List<RaycastResult> _results = new();

    public void PutBack()
    {
        transform.position = _origPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag) return;

        _origPosition = transform.position;
        Debug.Log("begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag) return;
        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanDrag) return;

        Debug.Log("end");

        _results.Clear();
        EventSystem.current.RaycastAll(eventData, _results);
        bool interacted = false;
        foreach (var r in _results)
        {
            if (r.gameObject.TryGetComponent<IInvetoryInteractable>(out var interactable) && interactable.CanInteract(this))
            {
                interactable.Interact(this);
                Debug.Log("interacted");

                interacted = true;
                break;
            }
        }
        if (!interacted)
            transform.position = _origPosition;
    }
}
