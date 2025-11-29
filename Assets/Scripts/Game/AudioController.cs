using UnityEngine;

namespace IndividualGames.CardMatch.Game
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [Header("Clips")]
        [SerializeField] private AudioClip flipClip;
        [SerializeField] private AudioClip matchSuccessClip;
        [SerializeField] private AudioClip matchFailClip;
        [SerializeField] private AudioClip gameOverClip;

        public void PlayCardFlip()
        {
            audioSource.PlayOneShot(flipClip);
        }

        public void PlayMatchSuccess()
        {
            audioSource.PlayOneShot(matchSuccessClip);
        }

        public void PlayMatchFail()
        {
            audioSource.PlayOneShot(matchFailClip);
        }

        public void PlayGameOver()
        {
            audioSource.PlayOneShot(gameOverClip);
        }
    }
}
