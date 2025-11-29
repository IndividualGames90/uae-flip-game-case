using UnityEngine;

namespace IndividualGames.CardMatch.MainMenu
{
    public class QuitGameButton : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
