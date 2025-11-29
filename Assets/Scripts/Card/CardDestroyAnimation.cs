using System;
using System.Collections;
using UnityEngine;

namespace IndividualGames.CardMatch.Card
{
    public class CardDestroyAnimation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private float rotationSpeed = 540f;
        [SerializeField] private float hangDuration = .5f;

        private Coroutine animRoutine;

        public event Action AnimationCompleted;

        private Vector3 startScale;
        private WaitForSeconds hangDurationWait;

        private void Awake()
        {
            hangDurationWait = new WaitForSeconds(hangDuration);

            if (target == null)
                target = transform;

            startScale = target.localScale;
        }

        public void Play()
        {
            if (animRoutine != null)
                StopCoroutine(animRoutine);

            animRoutine = StartCoroutine(PlayRoutine());
        }

        private IEnumerator PlayRoutine()
        {
            yield return hangDurationWait;

            float t = 0f;

            while (t < duration)
            {
                t += Time.deltaTime;
                float p = t / duration;

                // Scale down
                float scale = Mathf.Lerp(1f, 0f, p);
                target.localScale = startScale * scale;

                // Rotate
                target.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

                yield return null;
            }

            target.localScale = Vector3.zero;

            AnimationCompleted?.Invoke();
            animRoutine = null;
        }
    }
}
