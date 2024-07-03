using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Button _startBtn;
    [SerializeField] Button _quitBtn;

    void Start()
    {
        _startBtn.onClick.AddListener(SceneHandler.Instance.NextLevel);
        _quitBtn.onClick.AddListener(SceneHandler.Instance.ExitGame);
    }
}
