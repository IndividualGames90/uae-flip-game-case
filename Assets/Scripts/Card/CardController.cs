using System;
using IndividualGames.CardMatch.Card;
using UnityEngine;
using UnityEngine.UI;

namespace IndividualGames.CardMatch.Game
{
    public class CardController : MonoBehaviour
    {
        [Header("Data (Assigned at runtime)")]
        [SerializeField] private CardData cardData;

        [Header("Visual")]
        [SerializeField] private Image cardImage;
        [SerializeField] private Button cardButton;

        private bool isRevealed = false;

        public event Action<CardController> OnCardClicked;

        public CardData Data => cardData;
        public int CardID => cardData.CardID;
        public bool IsRevealed => isRevealed;

        public void Initialize(CardData data)
        {
            cardButton.onClick.AddListener(OnClick);
            cardData = data;
            Hide();
        }

        private void OnClick()
        {
            OnCardClicked?.Invoke(this);
        }

        private void OnDestroy()
        {
            cardButton.onClick.RemoveListener(OnClick);
        }

        public void Reveal()
        {
            isRevealed = true;

            if (cardImage != null)
                cardImage.sprite = cardData.FrontFace;
        }

        public void Hide()
        {
            isRevealed = false;

            if (cardImage != null)
                cardImage.sprite = cardData.BackFace;
        }
    }
}
