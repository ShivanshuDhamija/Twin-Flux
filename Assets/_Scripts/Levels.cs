using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "CreateLevel/Level")]
public class Levels : ScriptableObject
{
    public int rows;
    public int columns;
    public List<CardData> availableCardData;
}
