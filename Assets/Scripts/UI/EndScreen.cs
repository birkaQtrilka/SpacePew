using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _highestScoreTxtMesh;
    [SerializeField] Button _restartBtn;
    [SerializeField] Button _quitBtn;

    void Start()
    {
        _highestScoreTxtMesh.text = ScoreManager.Instance.LocalHighScore.ToString();
        _restartBtn.onClick.AddListener(() => SceneHandler.Instance.LoadLevel(0));
        _quitBtn.onClick.AddListener(() => SceneHandler.Instance.ExitGame());
    }

}
