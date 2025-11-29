using UnityEngine;

namespace IndividualGames.CardMatch.MainMenu
{
    public class SettingsController : MonoBehaviour
    {
        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;

            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
    }
}
