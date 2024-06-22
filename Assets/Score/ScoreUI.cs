using TMPro;
using UnityEngine;
[RequireComponent (typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
    ScoreManager _scoreManager;
    TextMeshProUGUI _textMesh;
    void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _scoreManager = ScoreManager.Instance;
        _scoreManager.ScoreUpdated += OnUpdateScore;
        _textMesh.text = $"Score: {0}";

    }

    void OnUpdateScore(int newScore)
    {
        _textMesh.text = $"Score: {newScore}";

    }
}
