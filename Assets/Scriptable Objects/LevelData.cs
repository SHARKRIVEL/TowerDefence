using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData" ,menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public List<WaveData> waveData = new List<WaveData>();
}
