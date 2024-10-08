using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIObjectMover : MonoBehaviour
    {
        public static UIObjectMover Instance;

        private readonly float _popMultiplier = 1.15f;
        private readonly float _popDuration = 0.17f;
        private readonly float _moveDuration = 0.35f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public IEnumerator MoveToTarget(RectTransform rectTransform, RectTransform target)
        {
            Vector2 startPosition = rectTransform.anchoredPosition;
            Vector2 targetPosition = target.anchoredPosition;
            float elapsedTime = 0;

            while (elapsedTime < _moveDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / _moveDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = targetPosition;
        }

        public IEnumerator PopEffect(RectTransform rectTransform)
        {
            float elapsedTime = 0;

            Vector2 startScale = rectTransform.localScale;

            while (elapsedTime < _popDuration)
            {
                rectTransform.localScale =
                    Vector2.Lerp(startScale, startScale * _popMultiplier, (elapsedTime / _popDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = startScale * _popMultiplier;

            elapsedTime = 0;

            while (elapsedTime < _popDuration)
            {
                rectTransform.localScale =
                    Vector2.Lerp(startScale * _popMultiplier, startScale, elapsedTime / _popDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = startScale;
        }
    }
}