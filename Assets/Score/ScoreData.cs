using UnityEngine;

[CreateAssetMenu(menuName = "ScoreData", fileName = "ScoreData")]
public class ScoreData : ScriptableObject
{
    [field: SerializeField] public int BestScore { get; set; } 
}
