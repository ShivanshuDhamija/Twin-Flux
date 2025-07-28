using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelKey", menuName = "ScriptableObjects/LevelKey")]
public class LevelKey : ScriptableObject
{
    public string Key = "CurrentLevel";
}
