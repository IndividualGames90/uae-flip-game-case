using UnityEngine;
using TMPro;

namespace IndividualGames.CardMatch.Game
{
    public class GameController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_Text turnsText;
        [SerializeField] private TMP_Text matchesText;

        [Header("Game Over")]
        [SerializeField] private GameObject gameOverPanel;

        public int CurrentMatches => matches;
        private int turns;
        private int matches;

        private int totalCards;

        private void Start()
        {
            RefreshUI();
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);

            UpdateMatches();
        }

        public void CardSpawned()
        {
            totalCards++;
        }

        public void CardDestroyed(int amount)
        {
            totalCards -= amount;
            if (totalCards <= 0)
            {
                GameOver();
            }
        }

        public void UpdateTurns()
        {
            turns++;
            RefreshUI();
        }

        public void UpdateMatches(int amount = 1)
        {
            matches += amount;
            RefreshUI();
        }

        public void GameOver()
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        private void RefreshUI()
        {
            if (turnsText != null)
                turnsText.text = turns.ToString();

            if (matchesText != null)
                matchesText.text = matches.ToString();
        }
    }
}
