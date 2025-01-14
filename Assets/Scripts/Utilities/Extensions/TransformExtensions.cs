using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Match3Linked
{
    public static class TransformExtensions
    {
        public static IEnumerator ScaleCoroutine(this Transform transform, Vector2 from, Vector2 to, float time, UnityAction onComplete = null)
        {
            float currentTime = Time.timeSinceLevelLoad;
            float elapsedTime = 0.0f;
            float lastTime = currentTime;

            while (time > 0 && elapsedTime < time)
            {
                // Update Time
                currentTime = Time.timeSinceLevelLoad;
                elapsedTime += currentTime - lastTime;
                lastTime = currentTime;

                transform.localScale = Vector3.Lerp(from, to, elapsedTime / time);

                yield return null;
            }

            transform.localScale = to;

            onComplete?.Invoke();
        }
        
        public static IEnumerator MoveCoroutine(this Transform transform, Vector2 targetPos, float time, UnityAction onComplete = null)
        {
            Vector2 startPos = transform.position;

            float currentTime = Time.timeSinceLevelLoad;
            float elapsedTime = 0.0f;
            float lastTime = currentTime;

            while (time > 0 && elapsedTime < time)
            {
                // Update Time
                currentTime = Time.timeSinceLevelLoad;
                elapsedTime += currentTime - lastTime;
                lastTime = currentTime;

                transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / time);

                yield return null;
            }

            transform.position = targetPos;

            onComplete?.Invoke();
        }
    }
}