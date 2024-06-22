using System;
using UnityEngine;

[CreateAssetMenu(menuName = "DifficultyConfig", fileName = "new Difficulty Config")]
public class ModifyByDifficultyData : ScriptableObject
{
    [field: SerializeField] public AnimationCurve DifficultyToDamage { get; private set; }
    [field: SerializeField] public AnimationCurve DifficultyToSpeed { get; private set; }
    [field: SerializeField] public MaterialChanger MaterialThreshHold { get; private set; }
    [field: SerializeField] public AnimationCurve DifficultyToSpawnRate { get; private set; }

    
}
[Serializable]
public abstract class ThresholdChanger
{
    public float Threshold;
}

[Serializable]
public class MaterialChanger : ThresholdChanger
{
    public Material Material;

    public Material TryGetMaterial(float difficulty)
    {
        if (difficulty >= Threshold) return Material;
        return null;
    }
}