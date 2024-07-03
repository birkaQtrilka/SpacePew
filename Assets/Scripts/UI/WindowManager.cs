using UnityEngine;

public class WindowManager : MonoBehaviour
{
    GameObject _openedWindow;
    [SerializeField] GameObject _settingsWindow;
    [SerializeField] bool _canOpenSettings = true;
    public void OpenSettingsWindow()
    {
        CloseOpenedWindow();
        _settingsWindow.SetActive(true);
        _openedWindow = _settingsWindow;
    }

    public void CloseOpenedWindow()
    {
        if (_openedWindow == null) return;

        _openedWindow.SetActive(false);
        _openedWindow = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _canOpenSettings)
        {
            if(!_settingsWindow.activeInHierarchy)
                OpenSettingsWindow();
            else
                CloseOpenedWindow();
        }
    }
}
