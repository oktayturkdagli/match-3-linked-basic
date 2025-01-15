using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Match3Linked
{
    /// <summary>
    /// Provides extension methods for Transform to perform smooth scaling and movement animations.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Smoothly scales a Transform from a starting scale to a target scale over a specified duration.
        /// </summary>
        /// <param name="transform">The Transform to scale.</param>
        /// <param name="startScale">The initial scale.</param>
        /// <param name="endScale">The target scale.</param>
        /// <param name="duration">The duration of the scaling animation in seconds.</param>
        /// <param name="onComplete">Optional callback invoked when the scaling animation is complete.</param>
        /// <returns>An IEnumerator for use with coroutines.</returns>
        public static IEnumerator ScaleCoroutine(this Transform transform, Vector3 startScale, Vector3 endScale, float duration, UnityAction onComplete = null)
        {
            if (duration <= 0f)
            {
                transform.localScale = endScale;
                onComplete?.Invoke();
                yield break;
            }

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / duration);
                transform.localScale = Vector3.Lerp(startScale, endScale, progress);
                yield return null;
            }

            transform.localScale = endScale;
            onComplete?.Invoke();
        }

        /// <summary>
        /// Smoothly moves a Transform from its current position to a target position over a specified duration.
        /// </summary>
        /// <param name="transform">The Transform to move.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="duration">The duration of the movement animation in seconds.</param>
        /// <param name="onComplete">Optional callback invoked when the movement animation is complete.</param>
        /// <returns>An IEnumerator for use with coroutines.</returns>
        public static IEnumerator MoveCoroutine(this Transform transform, Vector3 targetPosition, float duration, UnityAction onComplete = null)
        {
            Vector3 startPosition = transform.position;

            if (duration <= 0f)
            {
                transform.position = targetPosition;
                onComplete?.Invoke();
                yield break;
            }

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / duration);
                transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
                yield return null;
            }

            transform.position = targetPosition;
            onComplete?.Invoke();
        }
    }
}
