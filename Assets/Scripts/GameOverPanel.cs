using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Button _restartBtn;
    [SerializeField] Button _quitBtn;
    [SerializeField] TextMeshProUGUI _scoreTextMesh;
    [SerializeField] TextMeshProUGUI _highScoreTextMesh;

    void Start()
    {
        _restartBtn.onClick.AddListener(Restart);
        _quitBtn.onClick.AddListener(Quit);

        _scoreTextMesh.text = "Score: " + ScoreManager.Instance.Score.ToString();
        _highScoreTextMesh.text = "High Score: " + ScoreManager.Instance.HighScore;
    }

    public void Restart()
    {
        SceneHandler.Instance.RestartScene();
    }

    public void Quit()
    {
        SceneHandler.Instance.ExitGame();

    }
}
