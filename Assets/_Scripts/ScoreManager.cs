using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchCount;
    [SerializeField] private TextMeshProUGUI totalTurns;
    [SerializeField] private VoidEventChannel onCardMatched;
    [SerializeField] private VoidEventChannel onCardNotMatched;
    [SerializeField] private VoidEventChannel onLevelUp;
    private int _matchedCards;
    private int _totalTurns;


    private void Start()
    {
        onCardMatched.OnEventInvoked += OnCardMatched;
        onCardNotMatched.OnEventInvoked += OnCardNotMatched;
        onLevelUp.OnEventInvoked += ResetScore;
    }

    private void OnCardNotMatched(NoParam obj)
    {
        CardNotMatched();
    }

    private void OnCardMatched(NoParam obj)
    {
        CardMatched();
    }


    private void CardMatched()
    {
        _matchedCards++;
        _totalTurns++;
        matchCount.text =_matchedCards.ToString();
        totalTurns.text = _totalTurns.ToString();
    }

    private void CardNotMatched()
    {
        _totalTurns++;
        totalTurns.text = _totalTurns.ToString();
    }
    private void ResetScore(NoParam obj)
    {
        _matchedCards = 0;
        _totalTurns = 0;
        matchCount.text =_matchedCards.ToString();
        totalTurns.text = _totalTurns.ToString();
    }
}
