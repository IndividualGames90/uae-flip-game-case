using UnityEngine;
using System;
using System.Collections;
using IndividualGames.CardMatch.Card;

namespace IndividualGames.CardMatch.Game
{
    public class CardEventController : MonoBehaviour
    {
        public event Action<bool> CardsMatched;

        private CardController first;
        private CardController second;

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
            bool match = first.CardID == second.CardID;

            if (match)
            {
                UnregisterCard(first);
                UnregisterCard(second);
                CardsMatched?.Invoke(true);

                first.CardMatched();
                second.CardMatched();

                first = null;
                second = null;
                return;
            }

            StartCoroutine(FlipBackRoutine());
        }

        private IEnumerator FlipBackRoutine()
        {
            yield return new WaitForSeconds(0.5f);

            first.FlipClose();
            second.FlipClose();

            CardsMatched?.Invoke(false);

            first = null;
            second = null;
        }

    }
}
