using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;
    [SerializeField] private CardStatus status;
    private CardData cardData;

    private bool isFlipped = false;
    private bool isMatched = false;

    public void Initialize(CardData data)
    {
        cardData = data;
        frontImage.sprite = data.frontSprite;
        SetFaceDown();
    }

    public void OnClick()
    {
        if (isFlipped || isMatched) return;
        if (!status.isFirstCardSelected || !status.isSecondCardSelected)
        {
            StartCoroutine(FlipAnimation());
            GameManager.Instance.OnCardFlipped(this);
        }
    }

    private IEnumerator FlipAnimation()
    {
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration / 2f)
        {
            transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(0, 90, elapsed / (duration / 2f)), 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isFlipped = !isFlipped;
        frontImage.gameObject.SetActive(isFlipped);
        backImage.gameObject.SetActive(!isFlipped);

        elapsed = 0f;
        while (elapsed < duration / 2f)
        {
            transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(90, 0, elapsed / (duration / 2f)), 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetFaceDown()
    {
        isFlipped = false;
        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetMatched()
    {
        isMatched = true;
        frontImage.color = new Color(0, 0, 0, 0);
        backImage.color = new Color(0, 0, 0, 0);
    }
    public void ReverseFlipAnimation()
    {
        StartCoroutine(FlipAnimation());
    }
    public string CardID => cardData.cardID;
    public bool IsMatched => isMatched;
}
