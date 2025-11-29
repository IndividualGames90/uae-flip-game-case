using UnityEngine;
using System;
using System.Collections;
using IndividualGames.CardMatch.Card;

namespace IndividualGames.CardMatch.Game
{
    public class CardEventController : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private AudioController audioController;

        public event Action<bool> CardsMatched;

        private CardController first;
        private CardController second;

        private WaitForSeconds waitCardBackFlip = new(0.5f);

        public void RegisterCard(CardController card)
        {
            card.OnCardClicked += HandleCardClicked;
        }

        public void UnregisterCard(CardController card)
        {
            card.OnCardClicked -= HandleCardClicked;
        }

        private void HandleCardClicked(CardController card)
        {
            audioController.PlayCardFlip();

            if (first == null)
            {
                first = card;
                first.FlipOpen();
                return;
            }

            if (second == null && card != first)
            {
                second = card;
                second.FlipOpen();
                EvaluateMatch();
            }
        }

        private void EvaluateMatch()
        {
            gameController.UpdateTurns();

            bool match = first.CardID == second.CardID;

            if (match)
            {
                audioController.PlayMatchSuccess();
                UnregisterCard(first);
                UnregisterCard(second);
                CardsMatched?.Invoke(true);

                first.CardMatched();
                second.CardMatched();

                gameController.CardDestroyed(2);
                scoreController.AddScore(2);

                first = null;
                second = null;
                return;
            }

            audioController.PlayMatchFail();
            StartCoroutine(FlipBackRoutine());
        }

        private IEnumerator FlipBackRoutine()
        {
            yield return waitCardBackFlip;

            audioController.PlayCardFlip();

            first.FlipClose();
            second.FlipClose();

            CardsMatched?.Invoke(false);

            first = null;
            second = null;
        }

    }
}
