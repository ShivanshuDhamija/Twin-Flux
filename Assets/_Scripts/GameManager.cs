// GameManager.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isResetLevel;
    [SerializeField] private CardStatus status;
    // private bool isFirstCardSelected;
    // private bool isSecondCardSelected;
    [SerializeField] private VoidEventChannel onCardMatched;
    [SerializeField] private VoidEventChannel onCardNotMatched;
    [SerializeField] private VoidEventChannel onCardFlipped;
    [SerializeField] private VoidEventChannel onLevelUp;
    [SerializeField] private LevelKey levelKey;
    private Card firstCardData;
    private Card secondCardData;
    private int currentLevel = 0;
    
    // public static string LevelKey = "CurrentLevel";
    
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        if (isResetLevel)
        {
            PlayerPrefs.DeleteKey(levelKey.Key);
        }
        if (!PlayerPrefs.HasKey(levelKey.Key))
        {
            PlayerPrefs.SetInt(levelKey.Key, currentLevel);
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        // LevelController.onLevelUpgrade += ResetCards;
        onLevelUp.OnEventInvoked += ResetCards;
    }

  

    private void ResetCards(NoParam obj)
    {
        // isFirstCardSelected = false;
        // isSecondCardSelected = false;
        status.isFirstCardSelected = false;
        status.isSecondCardSelected = false;
    }

    public void OnCardFlipped(Card flippedCard)
    {
        if (!status.isFirstCardSelected)
        {
            firstCardData = flippedCard;
            // isFirstCardSelected = true;
            status.isFirstCardSelected = true;
            onCardFlipped?.Invoke(new NoParam());
        }
        else if (!status.isSecondCardSelected)
        {
            secondCardData = flippedCard;
            // isSecondCardSelected = true;
            status.isSecondCardSelected = true;
            onCardFlipped?.Invoke(new NoParam());
        }
        if (status.isFirstCardSelected && status.isSecondCardSelected)
        {
            if (firstCardData.CardID == secondCardData.CardID)
            {
                StartCoroutine(DisappearCards());
                Debug.Log("Success");
            }
            else
            {
                StartCoroutine(ReverseCards());
                Debug.Log("Failure");
            }
        }
    }
    private IEnumerator ReverseCards()
    {
        yield return new WaitForSeconds(0.7f);
        onCardNotMatched?.Invoke(new NoParam());
        firstCardData.ReverseFlipAnimation();
        secondCardData.ReverseFlipAnimation();
        // isFirstCardSelected = false;
        // isSecondCardSelected = false;
        status.isFirstCardSelected = false;
        status.isSecondCardSelected = false;
    }
    private IEnumerator DisappearCards()
    {
        yield return new WaitForSeconds(0.7f);
        onCardMatched?.Invoke(new NoParam());
        firstCardData.SetMatched();
        secondCardData.SetMatched();
        // isFirstCardSelected = false;
        // isSecondCardSelected = false;
        status.isFirstCardSelected = false;
        status.isSecondCardSelected = false;
    }
    public bool IsFirstCardSelected => status.isFirstCardSelected;
    public bool IsSecondCardSelected => status.isSecondCardSelected;
}



