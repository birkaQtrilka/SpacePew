using UnityEngine;

public class WindowManager : MonoBehaviour
{
    GameObject _openedWindow;
    [SerializeField] GameObject _settingsWindow;
    [SerializeField] GameObject _gameOverWindow;

    public void OpenSettingsWindow()
    {
        CloseOpenedWindow();
        _settingsWindow.SetActive(true);
        _openedWindow = _settingsWindow;
    }
    public void OpenGameOverWindow()
    {
        CloseOpenedWindow();
        _gameOverWindow.SetActive(true);
        _openedWindow = _gameOverWindow;
    }
    public void CloseOpenedWindow()
    {
        if (_openedWindow == null) return;

        _openedWindow.SetActive(false);
        _openedWindow = null;
    }

    private void Update()
    {
        bool canOpenSettings = _openedWindow != _gameOverWindow;
        if (Input.GetKeyDown(KeyCode.Escape) && canOpenSettings)
        {
            if(!_settingsWindow.activeInHierarchy)
                OpenSettingsWindow();
            else
                CloseOpenedWindow();
        }
    }
}
