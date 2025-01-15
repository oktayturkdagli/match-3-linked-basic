using System.Collections;
using UnityEngine;

namespace Match3Linked
{
    /// <summary>
    /// Provides extension methods for fading CanvasGroup components in and out.
    /// </summary>
    public static class CanvasGroupExtensions
    {
        /// <summary>
        /// Smoothly fades in a CanvasGroup by increasing its alpha value over a specified duration.
        /// </summary>
        /// <param name="canvasGroup">The CanvasGroup to fade in.</param>
        /// <param name="duration">The duration of the fade-in effect, in seconds.</param>
        /// <returns>An IEnumerator for use in a coroutine.</returns>
        public static IEnumerator FadeInCoroutine(this CanvasGroup canvasGroup, float duration)
        {
            // Initialize variables
            float elapsedTime = 0.0f;

            // Ensure the CanvasGroup starts in a non-interactable state
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            // Perform the fade-in effect
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
                yield return null;
            }

            // Finalize the state of the CanvasGroup
            canvasGroup.alpha = 1.0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// Smoothly fades out a CanvasGroup by decreasing its alpha value over a specified duration.
        /// </summary>
        /// <param name="canvasGroup">The CanvasGroup to fade out.</param>
        /// <param name="duration">The duration of the fade-out effect, in seconds.</param>
        /// <returns>An IEnumerator for use in a coroutine.</returns>
        public static IEnumerator FadeOutCoroutine(this CanvasGroup canvasGroup, float duration)
        {
            // Initialize variables
            float elapsedTime = 0.0f;

            // Ensure the CanvasGroup starts in a non-interactable state
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            // Perform the fade-out effect
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Clamp01(1.0f - (elapsedTime / duration));
                yield return null;
            }

            // Finalize the state of the CanvasGroup
            canvasGroup.alpha = 0.0f;
        }
    }
}