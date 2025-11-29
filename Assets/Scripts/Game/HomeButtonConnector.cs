using IndividualGames.CardMatch.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace IndividualGames.CardMatch.Game
{
    public class HomeButtonConnector : MonoBehaviour
    {
        [SerializeField] private Button homeButton;

        private void Start()
        {
            homeButton.onClick.AddListener(SceneController.LoadMainMenu);
        }

        private void OnDestroy()
        {
            homeButton.onClick.RemoveListener(SceneController.LoadMainMenu);
        }
    }
}
