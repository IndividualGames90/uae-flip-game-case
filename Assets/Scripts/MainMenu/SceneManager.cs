using UnityEngine;
using UnityEngine.SceneManagement;

namespace IndividualGames.CardMatch.MainMenu
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public static void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
