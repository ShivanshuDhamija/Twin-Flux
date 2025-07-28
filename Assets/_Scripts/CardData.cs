using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "NewCard/CardData")]
public class CardData : ScriptableObject
{
    public string cardID;
    public Sprite frontSprite;    
}

