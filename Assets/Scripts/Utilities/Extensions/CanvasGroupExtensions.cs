using System.Collections;
using UnityEngine;

namespace Match3Linked
{
    public static class CanvasGroupExtensions
    {
        public static IEnumerator FadeInCoroutine(this CanvasGroup canvasGroup, float duration)
        {
            float process = 0.0f;
            float time = 0.0f;

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            while (process < 1)
            {
                time += Time.unscaledDeltaTime;

                process = duration <= 0.0f ? 1.0f : Mathf.Clamp01(time / duration);

                canvasGroup.alpha = Mathf.Lerp(0, 1, process);

                yield return null;
            }

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        public static IEnumerator FadeOutCoroutine(this CanvasGroup canvasGroup, float duration)
        {
            float process = 0.0f;
            float time = 0.0f;

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            while (process < 1)
            {
                time += Time.unscaledDeltaTime;

                process = duration <= 0.0f ? 1.0f : Mathf.Clamp01(time / duration);

                canvasGroup.alpha = Mathf.Lerp(1, 0, process);

                yield return null;
            }
        }
    }
}