using UnityEngine;
using TMPro;

namespace IndividualGames.CardMatch.Game
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private int score;
        public int Score => score;

        private void Awake()
        {
            ResetScore();
        }

        public void AddScore(int amount)
        {
            score += amount;
            UpdateText();
        }

        public void ResetScore()
        {
            score = 0;
            UpdateText();
        }

        private void UpdateText()
        {
            if (scoreText != null)
                scoreText.text = score.ToString();
        }
    }
}
