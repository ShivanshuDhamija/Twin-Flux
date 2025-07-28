using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private List<Levels> levels;
    [SerializeField] private ParticleSystem confetti;
    [SerializeField] private VoidEventChannel onCardMatched;
    [SerializeField] private VoidEventChannel onLevelUpgrade;
    [SerializeField] private LevelKey levelKey;
    private List<CardData> availableCardData;
    private int _matchedCards;
    private int totalCards;
    private int rowNumber;
    private int colNumber;

    // public static event Action onLevelUpgrade;
    private void Start()
    {
        onCardMatched.OnEventInvoked += OnCardMatched;
        int currentLevelIndex = PlayerPrefs.GetInt(levelKey.Key);
        SetupLevel(currentLevelIndex);
    }

    private void OnCardMatched(NoParam obj)
    {
        CardMatched();
    }

    private void SetupLevel(int levelIndex)
    {
        Levels currentLevel = levels[levelIndex];

        rowNumber = currentLevel.rows;
        colNumber = currentLevel.columns;
        totalCards = rowNumber * colNumber;

        var grid = cardHolder.GetComponent<GridLayoutGroup>();
        grid.constraintCount = colNumber;

        availableCardData = new List<CardData>();
        Shuffle(currentLevel.availableCardData);

        for (int i = 0; i < totalCards / 2; i++)
        {
            availableCardData.Add(currentLevel.availableCardData[i]);
        }
        ResizeCards();
        SpawnCards();
    }

    private void ResizeCards()
    {
        RectTransform rt = cardHolder.GetComponent<RectTransform>();
        GridLayoutGroup grid = cardHolder.GetComponent<GridLayoutGroup>();

        float maxCardWidth = 100f;
        float maxCardHeight = 150f;
        float aspectRatio = maxCardWidth / maxCardHeight;

        float totalWidth = rt.rect.width;
        float totalHeight = rt.rect.height;

        float spacingX = grid.spacing.x;
        float spacingY = grid.spacing.y;
        float paddingHorizontal = grid.padding.left + grid.padding.right;
        float paddingVertical = grid.padding.top + grid.padding.bottom;

        float availableWidth = totalWidth - paddingHorizontal - ((colNumber - 1) * spacingX);
        float availableHeight = totalHeight - paddingVertical - ((rowNumber - 1) * spacingY);

        float cardWidth = availableWidth / colNumber;
        float cardHeight = cardWidth / aspectRatio;

        if (cardHeight > (availableHeight / rowNumber))
        {
            cardHeight = availableHeight / rowNumber;
            cardWidth = cardHeight * aspectRatio;
        }

        cardWidth = Mathf.Min(cardWidth, maxCardWidth);
        cardHeight = Mathf.Min(cardHeight, maxCardHeight);

        grid.cellSize = new Vector2(cardWidth, cardHeight);
    }

    private void SpawnCards()
    {
        MakeCardHolderEmpty();

        var dataToUse = new List<CardData>(availableCardData);
        dataToUse.AddRange(availableCardData);
        Shuffle(dataToUse);

        foreach (CardData data in dataToUse)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardHolder);
            cardGO.GetComponent<Card>().Initialize(data);
        }
    }

    private void MakeCardHolderEmpty()
    {
        foreach (Transform child in cardHolder)
        {
            Destroy(child.gameObject);
        }
    }

    public void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    private void CardMatched()
    {
        _matchedCards++;
        if (_matchedCards == totalCards / 2)
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    

    private IEnumerator LoadNextLevel()
    {
        confetti.Play();
        yield return new WaitForSeconds(3f);
        MakeCardHolderEmpty();
        _matchedCards = 0;
        onLevelUpgrade?.Invoke(new NoParam());
        int currentLevelIndex = (PlayerPrefs.GetInt(levelKey.Key) + 1)%levels.Count;
        PlayerPrefs.SetInt(levelKey.Key, currentLevelIndex);
        SetupLevel(currentLevelIndex);
        confetti.Stop();
    }
    public void LoadLevel(int level)
    {
        MakeCardHolderEmpty();
        _matchedCards = 0;
        onLevelUpgrade?.Invoke(new NoParam());
        PlayerPrefs.SetInt(levelKey.Key, level);
        int currentLevelIndex = PlayerPrefs.GetInt(levelKey.Key);
        SetupLevel(currentLevelIndex);
    }
}