using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    GameObject _lastSelectedObject;

    void Update()
    {

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_lastSelectedObject);

        }
        _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
    }
}
