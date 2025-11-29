using System.Collections;
using UnityEngine;

namespace IndividualGames.CardMatch.Card
{
    public class CardFlipAnimation : MonoBehaviour
    {
        [Header("Flip Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private float flipHalfDuration = 0.12f;

        private Coroutine flipRoutine;
        private Vector3 originalScale;

        public System.Action OnFlipMidpoint;

        private void Awake()
        {
            if (target == null)
                target = transform;

            originalScale = target.localScale;
        }

        public void PlayFlip()
        {
            if (flipRoutine != null)
                StopCoroutine(flipRoutine);

            flipRoutine = StartCoroutine(FlipRoutine());
        }

        private IEnumerator FlipRoutine()
        {
            // Shrink to 0 (hidden)
            float t = 0f;
            while (t < flipHalfDuration)
            {
                t += Time.deltaTime;
                float p = t / flipHalfDuration;
                float x = Mathf.Lerp(originalScale.x, 0f, p);
                target.localScale = new Vector3(x, originalScale.y, originalScale.z);
                yield return null;
            }

            // Face swap happens here (CardController hooks this)
            OnFlipMidpoint?.Invoke();

            // Grow back to original scale
            t = 0f;
            while (t < flipHalfDuration)
            {
                t += Time.deltaTime;
                float p = t / flipHalfDuration;
                float x = Mathf.Lerp(0f, originalScale.x, p);
                target.localScale = new Vector3(x, originalScale.y, originalScale.z);
                yield return null;
            }

            target.localScale = originalScale;
            flipRoutine = null;
        }
    }
}
