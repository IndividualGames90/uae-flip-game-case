using UnityEngine;

namespace IndividualGames.CardMatch.Game
{
    public class SaveController : MonoBehaviour
    {
        private const string MATCHES_KEY = "SAVED_MATCHES";

        [SerializeField] private GameController gameController;

        private void Start()
        {
            int saved = PlayerPrefs.GetInt(MATCHES_KEY, 0);
            gameController.UpdateMatches(saved);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(MATCHES_KEY, gameController.CurrentMatches);
        }
    }
}
