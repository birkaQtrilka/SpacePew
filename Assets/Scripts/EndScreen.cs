using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _highestScoreTxtMesh;
    [SerializeField] Button _restartBtn;
    void Awake()
    {
        _highestScoreTxtMesh.text = ScoreManager.Instance.LocalHighScore.ToString();
    }

    void Start()
    {
        _restartBtn.onClick.AddListener(() => SceneHandler.Instance.LoadLevel(0));
    }
}
