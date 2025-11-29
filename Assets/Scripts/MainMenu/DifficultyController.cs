using UnityEngine;
using UnityEngine.UI;

namespace IndividualGames.CardMatch.MainMenu
{
    public class DifficultyController : MonoBehaviour
    {
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        public static Difficulty CurrentDifficulty = Difficulty.Easy;

        [SerializeField] private Button easyButton;
        [SerializeField] private Button mediumButton;
        [SerializeField] private Button hardButton;

        [SerializeField] private GameObject easyGlare;
        [SerializeField] private GameObject mediumGlare;
        [SerializeField] private GameObject hardGlare;

        private void Start()
        {
            easyButton.onClick.AddListener(SetEasy);
            mediumButton.onClick.AddListener(SetMedium);
            hardButton.onClick.AddListener(SetHard);

            UpdateGlare();
        }

        private void SetEasy()
        {
            CurrentDifficulty = Difficulty.Easy;
            UpdateGlare();
        }

        private void SetMedium()
        {
            CurrentDifficulty = Difficulty.Medium;
            UpdateGlare();
        }

        private void SetHard()
        {
            CurrentDifficulty = Difficulty.Hard;
            UpdateGlare();
        }

        private void UpdateGlare()
        {
            easyGlare.SetActive(CurrentDifficulty == Difficulty.Easy);
            mediumGlare.SetActive(CurrentDifficulty == Difficulty.Medium);
            hardGlare.SetActive(CurrentDifficulty == Difficulty.Hard);
        }

        private void OnDestroy()
        {
            easyButton.onClick.RemoveListener(SetEasy);
            mediumButton.onClick.RemoveListener(SetMedium);
            hardButton.onClick.RemoveListener(SetHard);
        }
    }
}
