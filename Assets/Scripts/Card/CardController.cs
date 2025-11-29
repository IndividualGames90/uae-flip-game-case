using System;
using UnityEngine;
using UnityEngine.UI;

namespace IndividualGames.CardMatch.Card
{
    public class CardController : MonoBehaviour
    {
        [Header("Data (Assigned at runtime)")]
        [SerializeField] private CardData cardData;

        [Header("Visual")]
        [SerializeField] private Image cardImage;
        [SerializeField] private Button cardButton;
        [SerializeField] private CardFlipAnimation flipAnimation;
        [SerializeField] private CardDestroyAnimation destroyAnimation;

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

        public void FlipOpen()
        {
            if (isRevealed) return;

            flipAnimation.OnFlipMidpoint = Reveal;
            flipAnimation.PlayFlip();
        }

        public void FlipClose()
        {
            if (!isRevealed) return;

            flipAnimation.OnFlipMidpoint = Hide;
            flipAnimation.PlayFlip();
        }


        public void CardMatched()
        {
            destroyAnimation.AnimationCompleted += SelfDestroy;
            destroyAnimation.Play();
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            cardButton.onClick.RemoveListener(OnClick);
        }
    }
}
