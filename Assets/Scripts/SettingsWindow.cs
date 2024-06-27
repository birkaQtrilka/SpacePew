using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] Button _quitBtn;

    void Start()
    {
        _quitBtn.onClick.AddListener(() => SceneHandler.Instance.ExitGame());
    }
}
